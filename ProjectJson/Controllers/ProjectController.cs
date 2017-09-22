using Classes;
using ProjectWebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ProjectWebApi.DAOs;
using ProjectWebApi.Models.Project;
using System.Collections;
using System.Web.Http.Description;

namespace ProjectWebApi.Controllers
{
    // [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = false)]
    public class ProjectController : ApiController
    {
        [HttpGet]
        [Route("project/all")]
        [ResponseType(typeof(IList<Project>))]
        public HttpResponseMessage all(HttpRequestMessage request)
        {
            var projectDAO = new ProjectDAO();
            var projects = projectDAO.all();
            projectDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, projects);
        }

        [HttpPost]
        [Route("project/ofTestManufsAndSystems")]
        [ResponseType(typeof(IList<Project>))]
        public HttpResponseMessage ofTestManufsAndSystems(HttpRequestMessage request, testManufsAndSystems parameters)
        {
            var projectDAO = new ProjectDAO();

            var projects = projectDAO.ofTestManufsAndSystems(parameters);
            projectDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, projects);
        }

        [HttpPost]
        [Route("project/subprojectDeliveryOfQueueFbyDevManufAndSystem")]
        [ResponseType(typeof(IList<string>))]
        public HttpResponseMessage subprojectDeliveryOfQueueFbyDevManufAndSystem(HttpRequestMessage request, testManufsAndSystems parameters)
        {
            var projectDAO = new ProjectDAO();

            var result = projectDAO.subprojectDeliveryOfQueueFbyDevManufAndSystem(parameters);
            projectDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPost]
        [Route("project/fbySubprojectDelivery")]
        [ResponseType(typeof(IList<Project>))]
        public HttpResponseMessage fbySubprojectDelivery(HttpRequestMessage request, IList<string> parameters)
        {
            var projectDAO = new ProjectDAO();

            var projects = projectDAO.fbySubprojectDelivery(parameters);
            projectDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, projects);
        }


        [HttpGet] // deve sair, quando converter o indicador de desenvolvimento
        [Route("projects")]
        public List<projects> getProjects()
        {
            string sql = @"
                select distinct
	                '{' +
	                'id:''' + convert(varchar, cast(substring(re.Subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(re.Entrega,8,8) as int)) + ''', ' +
	                'subproject:''' + re.Subprojeto + ''', ' +
	                'delivery:''' + re.Entrega + ''', ' +
	                'devManufacturing:''' + Fabrica_Desenvolvimento + ''', ' +
	                'system:''' + Sistema + ''', ' +
	                'name:''' + left(sp.Nome,30) + ''', ' +
	                'classification:''' + sp.Classificacao_Nome + ''', ' +
	                'release:''' + (select Sigla from sgq_meses m where m.id = re.release_mes) + ' ' + convert(varchar, re.release_ano) + '''' +
	                '}, ' as json,
	                convert(varchar, cast(substring(re.Subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(re.Entrega,8,8) as int)) as id,
	                Fabrica_Desenvolvimento as devManufacturing,
	                Sistema as system,
	                re.Subprojeto as subproject,
	                re.Entrega as delivery,
	                sp.nome as name,
	                sp.Classificacao_Nome as classification,
	                (select Sigla from sgq_meses m where m.id = re.release_mes) + ' ' + convert(varchar, re.release_ano)  as release,
	                re.release_ano,
	                re.release_mes
                from 
	                SGQ_Releases_Entregas re WITH (NOLOCK)
	                inner join biti_Subprojetos sp WITH (NOLOCK)
		                on sp.id = re.Subprojeto
                    inner join biti_usuarios us WITH (NOLOCK)
		                on us.id = sp.lider_tecnico_id 
                    inner join 
						(
						select distinct
							Subprojeto, Entrega, fabrica_desenvolvimento, sistema
						from
							(
							select distinct 
								cts.Subprojeto,
								cts.Entrega,
								cts.Fabrica_Desenvolvimento,
								cts.sistema
							from 
								alm_cts cts WITH (NOLOCK)
							union all
							select distinct 
								d.Subprojeto,
								d.Entrega,
								d.Fabrica_Desenvolvimento,
								d.sistema_defeito as sistema
							from 
								alm_defeitos d WITH (NOLOCK)
							) aux
						) cts 
						on cts.Subprojeto = re.Subprojeto and
							cts.Entrega = re.Entrega
                where
	                re.id = (select top 1 re2.id from  SGQ_Releases_Entregas re2 where re2.subprojeto = re.subprojeto and re2.entrega = re.entrega order by re2.release_ano desc, re2.release_mes desc) and
	                Fabrica_Desenvolvimento is not null
                order by
	                Fabrica_Desenvolvimento,
	                Sistema,
	                re.Subprojeto,
	                re.Entrega,
	                sp.Classificacao_Nome,
	                re.release_ano,
	                re.release_mes
                ";

            var Connection = new Connection(Bancos.Sgq);
            List<projects> ListProjects = Connection.Executar<projects>(sql);
            Connection.Dispose();

            return ListProjects;
        }

        [HttpGet]
        [Route("projects_")] // DEVERA ALTERAR O NOME QUANDO IMPLATADO EM PRODUCAO
        public List<project> getProjects_()
        {
            string sql = @"
                select 
                    sgq_projects.id,
                    sgq_projects.subproject as subproject,
                    sgq_projects.delivery as delivery,
                    convert(varchar, cast(substring(sgq_projects.subproject,4,8) as int)) + ' ' + convert(varchar,cast(substring(sgq_projects.delivery,8,8) as int)) as subprojectDelivery,
                    biti_subprojetos.nome as name,
                    biti_subprojetos.objetivo as objective,
					biti_subprojetos.classificacao_nome as classification,
					replace(replace(replace(replace(replace(biti_subprojetos.estado,'CONSOLIDAÇÃO E APROVAÇÃO DO PLANEJAMENTO','CONS/APROV. PLAN'),'PLANEJAMENTO','PLANEJ.'),'DESENHO DA SOLUÇÃO','DES.SOL'),'VALIDAÇÃO','VALID.'),'AGUARDANDO','AGUAR.') as state,
					(select Sigla from sgq_meses m where m.id = SGQ_Releases_Entregas.release_mes) + ' ' + convert(varchar, SGQ_Releases_Entregas.release_ano) as release,
					biti_subprojetos.Gerente_Projeto as GP,
			        biti_subprojetos.Gestor_Do_Gestor_LT as N3,
                    sgq_projects.trafficLight as trafficLight,
                    sgq_projects.rootCause as rootCause,
                    sgq_projects.actionPlan as actionPlan,
                    sgq_projects.informative as informative,
                    sgq_projects.attentionPoints as attentionPoints,
                    sgq_projects.attentionPointsIndicators as attentionPointsOfIndicators
                from 
                    sgq_projects
                    inner join alm_projetos WITH (NOLOCK)
	                  on alm_projetos.subprojeto = sgq_projects.subproject and
	                    alm_projetos.entrega = sgq_projects.delivery and
	                    alm_projetos.ativo = 'Y'
                    left join biti_subprojetos WITH (NOLOCK)
	                  on biti_subprojetos.id = sgq_projects.subproject
					left join SGQ_Releases_Entregas WITH (NOLOCK)
	                  on SGQ_Releases_Entregas.subprojeto = sgq_projects.subproject and
					     SGQ_Releases_Entregas.entrega = sgq_projects.delivery and
						 SGQ_Releases_Entregas.id = (select top 1 re2.id from SGQ_Releases_Entregas re2 
						                             where re2.subprojeto = SGQ_Releases_Entregas.subprojeto and 
													       re2.entrega = SGQ_Releases_Entregas.entrega 
													 order by re2.release_ano desc, re2.release_mes desc)
                order by 
                    sgq_projects.subproject, 
                    sgq_projects.delivery
                ";

            var Connection = new Connection(Bancos.Sgq);
            List<project> ListProjects = Connection.Executar<project>(sql);
            Connection.Dispose();

            return ListProjects;
        }

        [HttpGet]
        [Route("project/byIds/{ids}")]
        [ResponseType(typeof(IList<Project>))]
        public HttpResponseMessage getProjectsByIds(HttpRequestMessage request, string ids)
        {
            var projectDAO = new ProjectDAO();
            var projects = projectDAO.getProjectsByIds(ids);
            projectDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, projects);
        }

        [HttpGet]
        [Route("project/bySubprojectDelivery/{subproject}/{delivery}")]
        [ResponseType(typeof(Project))]
        public HttpResponseMessage getProject(HttpRequestMessage request, string subproject, string delivery)
        {
            var projectDAO = new ProjectDAO();
            var project = projectDAO.getProject(subproject, delivery);
            projectDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, project);
        }

        [HttpPut]
        [Route("project/update/{id:int}")]
        public HttpResponseMessage UpdateProject(int id, project item)
        {
            try
            {
                var connection = new Connection(Bancos.Sgq);

                if (item.trafficLight == null)
                    item.trafficLight = "";

                if (item.rootCause == null)
                    item.rootCause = "";

                if (item.actionPlan == null)
                    item.actionPlan = "";

                if (item.informative == null)
                    item.informative = "";

                if (item.attentionPoints == null)
                    item.attentionPoints = "";

                if (item.attentionPointsOfIndicators == null)
                    item.attentionPointsOfIndicators = "";

                bool resultado = false;
                if (item == null) throw new ArgumentNullException("item");
                if (id == 0) throw new ArgumentNullException("id");
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection.connection;
                    command.CommandText = @"
                        update sgq_projects
                        set
                            trafficLight = @trafficLight,
                            rootCause = @rootCause,
                            actionPlan = @actionPlan,
                            informative = @informative,
                            attentionPoints = @attentionPoints,
                            attentionPointsIndicators = @attentionPointsOfIndicators
                        where
                            id = @id
                        ";

                    command.Parameters.AddWithValue("id", item.id);
                    command.Parameters.AddWithValue("trafficLight", item.trafficLight);
                    command.Parameters.AddWithValue("rootCause", item.rootCause);
                    command.Parameters.AddWithValue("actionPlan", item.actionPlan);
                    command.Parameters.AddWithValue("informative", item.informative);
                    command.Parameters.AddWithValue("attentionPoints", item.attentionPoints);
                    command.Parameters.AddWithValue("attentionPointsOfIndicators", item.attentionPointsOfIndicators);

                    int i = command.ExecuteNonQuery();
                    resultado = i > 0;
                }
                connection.Dispose();
                return Request.CreateResponse(HttpStatusCode.OK, resultado);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }

        }

        [HttpGet]
        [Route("project/DefectsDensity/{subproject}/{delivery}")]
        [ResponseType(typeof(DefectDensity))]
        public HttpResponseMessage getDefectsDensityByProject(HttpRequestMessage request, string subproject, string delivery )
        {
            var projectDAO = new ProjectDAO();
            var densityDefects = projectDAO.getDefectsDensityByProject(subproject, delivery);
            projectDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, densityDefects);
        }

        [HttpGet]
        [Route("project/DefectsAverangeTime/{subproject}/{delivery}")]
        [ResponseType(typeof(DefectAverangeTime))]
        public HttpResponseMessage getDefectsAverageTimeByProject(HttpRequestMessage request, string subproject, string delivery)
        {
            var projectDAO = new ProjectDAO();
            var defectAverangeTime = projectDAO.getDefectsAverageTimeByProject(subproject, delivery);
            projectDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, defectAverangeTime);
        }

        [HttpGet]
        [Route("project/DefectsAverangeTime/{subproject}/{delivery}/{severity}")]
        [ResponseType(typeof(DefectAverangeTime))]
        public HttpResponseMessage getDefectsAverageTimeByProject(HttpRequestMessage request, string subproject, string delivery, string severity)
        {
            var projectDAO = new ProjectDAO();
            var defectAverangeTime = projectDAO.getDefectsAverageTimeByProject(subproject, delivery, severity);
            projectDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, defectAverangeTime);
        }

        [HttpGet]
        [Route("project/DefectsAverangeTimeGroupSeverity/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<DefectAverangeTimeGroupSeverity>))]
        public HttpResponseMessage getDefectsAverangeTimeGroupSeverityByProject(HttpRequestMessage request, string subproject, string delivery)
        {
            var projectDAO = new ProjectDAO();
            var defectAverangeTimeGroupSeverity = projectDAO.getDefectAverangeTimeGroupSeverityByProject(subproject, delivery);
            projectDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, defectAverangeTimeGroupSeverity);
        }

        [HttpGet]
        [Route("project/DefectsReopened/{subproject}/{delivery}")]
        [ResponseType(typeof(DefectReopened))]
        public HttpResponseMessage getDefectReopenedByProject(HttpRequestMessage request, string subproject, string delivery)
        {
            var projectDAO = new ProjectDAO();
            var densityDefects = projectDAO.getDefectReopenedByProject(subproject, delivery);
            projectDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, densityDefects);
        }

        [HttpGet]
        [Route("project/DefectsDetectableInDev/{subproject}/{delivery}")]
        [ResponseType(typeof(DetectableInDev))]
        public HttpResponseMessage getDetectableInDevByProject(HttpRequestMessage request, string subproject, string delivery)
        {
            var projectDAO = new ProjectDAO();
            var detectableInDev = projectDAO.getDetectableInDevByProject(subproject, delivery);
            projectDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, detectableInDev);
        }

        [HttpGet]
        [Route("project/StatusLastDays/{subproject}/{delivery}")]
        [ResponseType(typeof(StatusLastDays))]
        public HttpResponseMessage getStatusLastDaysByProject(HttpRequestMessage request, string subproject, string delivery)
        {
            var projectDAO = new ProjectDAO();
            var statusLastDays = projectDAO.getStatusLastDaysByProject(subproject, delivery);
            projectDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, statusLastDays);
        }

        [HttpGet]
        [Route("project/StatusGroupMonth/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<Status>))]
        public HttpResponseMessage getStatusGroupMonthByProject(HttpRequestMessage request, string subproject, string delivery)
        {
            var projectDAO = new ProjectDAO();
            var list = projectDAO.getStatusLastMonthByProject(subproject, delivery);
            projectDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpGet]
        [Route("project/DefectsStatus/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<DefectStatus>))]
        public HttpResponseMessage getDefectStatusByProject(HttpRequestMessage request, string subproject, string delivery)
        {
            var projectDAO = new ProjectDAO();
            var list = projectDAO.getDefectStatusByProject(subproject, delivery);
            projectDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpGet]
        [Route("project/DefectsGroupOrigin/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<DefectStatus>))]
        public HttpResponseMessage getDefectsGroupOrigin(HttpRequestMessage request, string subproject, string delivery)
        {
            var projectDAO = new ProjectDAO();
            var list = projectDAO.getDefectsGroupOrigin(subproject, delivery);
            projectDAO.Dispose();

            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpGet]
        [Route("project/CtsImpactedXDefects/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<CtsImpactedXDefects>))]
        public HttpResponseMessage getCtsImpactedXDefects(HttpRequestMessage request, string subproject, string delivery)
        {
            var projectDAO = new ProjectDAO();
            var list = projectDAO.getCtsImpactedXDefects(subproject, delivery);
            projectDAO.Dispose();

            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpGet]
        [Route("project/DefectsOpenInDevManuf/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<DefectsOpen>))]
        public HttpResponseMessage getDefectsOpenInDevManuf(HttpRequestMessage request, string subproject, string delivery)
        {
            var projectDAO = new ProjectDAO();
            var list = projectDAO.getDefectsOpenInDevManuf(subproject, delivery);
            projectDAO.Dispose();

            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpGet]
        [Route("project/DefectsOpenInTestManuf/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<DefectsOpen>))]
        public HttpResponseMessage getDefectsOpenInTestManuf(HttpRequestMessage request, string subproject, string delivery)
        {
            var projectDAO = new ProjectDAO();
            var list = projectDAO.getDefectsOpenInTestManuf(subproject, delivery);
            projectDAO.Dispose();

            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpGet]
        [Route("project/ProductivityXDefects/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<ProductivityXDefects>))]
        public HttpResponseMessage getProductivityXDefects(HttpRequestMessage request, string subproject, string delivery)
        {
            var projectDAO = new ProjectDAO();
            var list = projectDAO.getProductivityXDefects(subproject, delivery);
            projectDAO.Dispose();

            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpGet]
        [Route("project/ProductivityXDefectsGroupWeekly/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<ProductivityXDefectsGroupWeekly>))]
        public HttpResponseMessage getProductivityXDefectsGroupWeekly(HttpRequestMessage request, string subproject, string delivery)
        {
            var projectDAO = new ProjectDAO();
            var list = projectDAO.getProductivityXDefectsGroupWeekly(subproject, delivery);
            projectDAO.Dispose();

            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        // ITERATIONS

        [HttpGet]
        [Route("project/Iterations/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<iteration>))]
        public HttpResponseMessage getIterations(HttpRequestMessage request, string subproject, string delivery)
        {
            var projectDAO = new ProjectDAO();
            var list = projectDAO.getIterations(subproject, delivery);
            projectDAO.Dispose();

            return request.CreateResponse(HttpStatusCode.OK, list);
        }


        [HttpGet]
        [Route("project/IterationsActive/{subproject}/{delivery}")]
        [ResponseType(typeof(List<string>))]
        public HttpResponseMessage getIterationsActive(HttpRequestMessage request, string subproject, string delivery)
        {
            var projectDAO = new ProjectDAO();
            var list = projectDAO.getIterationsActive(subproject, delivery);
            projectDAO.Dispose();

            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpGet]
        [Route("project/IterationsSelected/{subproject}/{delivery}")]
        [ResponseType(typeof(List<string>))]
        public HttpResponseMessage getIterationsSelected(HttpRequestMessage request, string subproject, string delivery)
        {
            var projectDAO = new ProjectDAO();
            var list = projectDAO.getIterationsSelected(subproject, delivery);
            projectDAO.Dispose();

            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpPut]
        [Route("project/UpdateIterationsActive/{id:int}")]
        [ResponseType(typeof(Boolean))]
        public HttpResponseMessage UpdateIterationsActive(HttpRequestMessage request, int id, IList<string> iterations)
        {
            try
            {
                var connection = new Connection(Bancos.Sgq);

                bool resultado = false;
                if (iterations == null) throw new ArgumentNullException("iterations");
                if (id == 0) throw new ArgumentNullException("id");
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection.connection;
                    command.CommandText = @"
                        update sgq_projects
                        set
                            IterationsActive = @iterations
                        where
                            id = @id
                        ";
                    command.Parameters.AddWithValue("id", id);
                    command.Parameters.AddWithValue("iterations", string.Join("','", iterations));

                    int i = command.ExecuteNonQuery();
                    resultado = i > 0;
                }
                connection.Dispose();
                return Request.CreateResponse(HttpStatusCode.OK, resultado);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [HttpPut]
        [Route("project/UpdateIterationsSelected/{id:int}")]
        [ResponseType(typeof(Boolean))]
        public HttpResponseMessage UpdateIterationsSelected(HttpRequestMessage request, int id, IList<string> iterations)
        {
            try
            {
                var connection = new Connection(Bancos.Sgq);

                bool resultado = false;
                if (iterations == null) throw new ArgumentNullException("iterations");
                if (id == 0) throw new ArgumentNullException("id");
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection.connection;
                    command.CommandText = @"
                        update sgq_projects
                        set
                            IterationsSelected = @iterations
                        where
                            id = @id
                        ";
                    command.Parameters.AddWithValue("id", id);
                    command.Parameters.AddWithValue("iterations", string.Join("','", iterations));

                    int i = command.ExecuteNonQuery();
                    resultado = i > 0;
                }
                connection.Dispose();
                return Request.CreateResponse(HttpStatusCode.OK, resultado);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }


        [HttpGet]
        [Route("project/ClearIterations/{id:int}")]
        [ResponseType(typeof(string))]
        public HttpResponseMessage ClearIterations(HttpRequestMessage request, int id)
        {
            try
            {
                var connection = new Connection(Bancos.Sgq);

                bool resultado = false;
                if (id == 0) throw new ArgumentNullException("id");
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connection.connection;
                    command.CommandText = @"
                        update sgq_projects
                        set
                            iterations = ''
                        where
                            id = @id";
                    command.Parameters.AddWithValue("id", id);

                    int i = command.ExecuteNonQuery();
                    resultado = i > 0;
                }
                connection.Dispose();
                return Request.CreateResponse(HttpStatusCode.OK, resultado);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        // ----------

        [HttpGet]
        [Route("project/DefectsDensityByProjectIterations/{subproject}/{delivery}")]
        [ResponseType(typeof(DefectDensity))]
        public HttpResponseMessage getDefectsDensityByProjectIterations(HttpRequestMessage request, string subproject, string delivery)
        {
            var projectDAO = new ProjectDAO();
            List<string> iterations = projectDAO.getIterationsSelected(subproject, delivery);
            var result = projectDAO.getDefectsDensityByProjectIterations(subproject, delivery, iterations);
            projectDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet]
        [Route("project/DefectsAverangeTimeIterations/{subproject}/{delivery}/{severity}")]
        [ResponseType(typeof(DefectAverangeTime))]
        public HttpResponseMessage getDefectsAverageTimeByProjectIterations(HttpRequestMessage request, string subproject, string delivery, string severity)
        {
            var projectDAO = new ProjectDAO();
            List<string> iterations = projectDAO.getIterationsSelected(subproject, delivery);
            var result = projectDAO.getDefectsAverageTimeByProjectIterations(subproject, delivery, severity, iterations);
            projectDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpPut]
        [Route("project/DefectsReopenedIterations/{subproject}/{delivery}")]
        [ResponseType(typeof(DefectReopened))]
        public HttpResponseMessage getDefectReopenedByProjectIterations(HttpRequestMessage request, string subproject, string delivery, List<string> iterations)
        {
            var projectDAO = new ProjectDAO();
            var item = projectDAO.getDefectReopenedByProjectIterations(subproject, delivery, iterations);
            projectDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, item);
        }

        [HttpPut]
        [Route("project/DefectsDetectableInDevIterations/{subproject}/{delivery}")]
        [ResponseType(typeof(DetectableInDev))]
        public HttpResponseMessage getDetectableInDevByProjectIterations(HttpRequestMessage request, string subproject, string delivery, List<string> iterations)
        {
            var projectDAO = new ProjectDAO();
            var item = projectDAO.getDetectableInDevByProjectIterations(subproject, delivery, iterations);
            projectDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, item);
        }

        [HttpPut]
        [Route("project/StatusLastDaysIterations/{subproject}/{delivery}")]
        [ResponseType(typeof(StatusLastDays))]
        public HttpResponseMessage getStatusLastDaysByProjectIterations(HttpRequestMessage request, string subproject, string delivery, List<string> iterations)
        {
            var projectDAO = new ProjectDAO();
            var item = projectDAO.getStatusLastDaysByProjectIterations(subproject, delivery, iterations);
            projectDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, item);
        }

        [HttpPut]
        [Route("project/StatusGroupMonthIterations/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<Status>))]
        public HttpResponseMessage getStatusGroupMonthByProjectIterations(HttpRequestMessage request, string subproject, string delivery, List<string> iterations)
        {
            var projectDAO = new ProjectDAO();
            var list = projectDAO.getStatusGroupMonthByProjectIterations(subproject, delivery, iterations);
            projectDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpPut]
        [Route("project/DefectsStatusIterations/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<DefectStatus>))]
        public HttpResponseMessage getDefectStatusByProjectIterations(HttpRequestMessage request, string subproject, string delivery, List<string> iterations)
        {
            var projectDAO = new ProjectDAO();
            var list = projectDAO.getDefectStatusByProjectIterations(subproject, delivery, iterations);
            projectDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpPut]
        [Route("project/DefectsGroupOriginIterations/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<DefectStatus>))]
        public HttpResponseMessage getDefectsGroupOriginIterations(HttpRequestMessage request, string subproject, string delivery, List<string> iterations)
        {
            var projectDAO = new ProjectDAO();
            var list = projectDAO.getDefectsGroupOriginIterations(subproject, delivery, iterations);
            projectDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpPut]
        [Route("project/CtsImpactedXDefectsIterations/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<CtsImpactedXDefects>))]
        public HttpResponseMessage getCtsImpactedXDefectsIterations(HttpRequestMessage request, string subproject, string delivery, List<string> iterations)
        {
            var projectDAO = new ProjectDAO();
            var list = projectDAO.getCtsImpactedXDefectsIterations(subproject, delivery, iterations);
            projectDAO.Dispose();

            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpPut]
        [Route("project/DefectsOpenInDevManufIterations/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<DefectsOpen>))]
        public HttpResponseMessage getDefectsOpenInDevManufIterations(HttpRequestMessage request, string subproject, string delivery, List<string> iterations)
        {
            var projectDAO = new ProjectDAO();
            var list = projectDAO.getDefectsOpenInDevManufIterations(subproject, delivery, iterations);
            projectDAO.Dispose();

            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpPut]
        [Route("project/DefectsOpenInTestManufIterations/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<DefectsOpen>))]
        public HttpResponseMessage getDefectsOpenInTestManufIterations(HttpRequestMessage request, string subproject, string delivery, List<string> iterations)
        {
            var projectDAO = new ProjectDAO();
            var list = projectDAO.getDefectsOpenInTestManufIterations(subproject, delivery, iterations);
            projectDAO.Dispose();

            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpPut]
        [Route("project/ProductivityXDefectsIterations/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<ProductivityXDefects>))]
        public HttpResponseMessage getProductivityXDefectsIterations(HttpRequestMessage request, string subproject, string delivery, List<string> iterations)
        {
            var projectDAO = new ProjectDAO();
            var list = projectDAO.getProductivityXDefectsIterations(subproject, delivery, iterations);
            projectDAO.Dispose();

            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpPut]
        [Route("project/ProductivityXDefectsGroupWeeklyIterations/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<ProductivityXDefectsGroupWeekly>))]
        public HttpResponseMessage getProductivityXDefectsGroupWeeklyIterations(HttpRequestMessage request, string subproject, string delivery, List<string> iterations)
        {
            var projectDAO = new ProjectDAO();
            var list = projectDAO.getProductivityXDefectsGroupWeeklyIterations(subproject, delivery, iterations);
            projectDAO.Dispose();

            return request.CreateResponse(HttpStatusCode.OK, list);
        }

    }
}