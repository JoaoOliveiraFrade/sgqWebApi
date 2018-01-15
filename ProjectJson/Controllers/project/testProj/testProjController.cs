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
using ProjectWebApi.Daos;
using ProjectWebApi.Models.Project;
using System.Collections;
using System.Web.Http.Description;

namespace ProjectWebApi.Controllers
{
    // [EnableCors(origins: "*", headers: "*", methods: "*", SupportsCredentials = false)]
    public class testProjController : ApiController
    {
        [HttpGet]
        [Route("project/testProj/data")]
        [ResponseType(typeof(IList<simpProject>))]
        public HttpResponseMessage TestProj(HttpRequestMessage request)
        {
            var TestProjDao = new TestProjDao();
            var projects = TestProjDao.Data();
            TestProjDao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, projects);
        }

        [HttpPost]
        [Route("project/testProj/fbyDevManufsAndSystems")]
        [ResponseType(typeof(IList<simpProject>))]
        public HttpResponseMessage fbyDevManufsAndSystems(HttpRequestMessage request, devManufsAndSystems parameters) {
            var TestProjDao = new TestProjDao();

            var projects = TestProjDao.fbyDevManufsAndSystems(parameters);
            TestProjDao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, projects);
        }

        [HttpPost]
        [Route("project/testProj/fromTestManufsAndSystems")]
        [ResponseType(typeof(IList<Project>))]
        public HttpResponseMessage fromTestManufsAndSystems(HttpRequestMessage request, testManufsAndSystems parameters)
        {
            var TestProjDao = new TestProjDao();

            var projects = TestProjDao.fromTestManufsAndSystems(parameters);
            TestProjDao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, projects);
        }

        [HttpPost]
        [Route("project/testProj/fromAgentFbyDevManufsAndSystems")]
        [ResponseType(typeof(IList<Project>))]
        public HttpResponseMessage fromAgentFbyDevManufsAndSystems(HttpRequestMessage request, devManufsAndSystems parameters)
        {
            var TestProjDao = new TestProjDao();

            var result = TestProjDao.fromAgentFbyDevManufsAndSystems(parameters);
            TestProjDao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, result);
        }


        //[HttpPost]
        //[Route("project/testProj/fbyproject")]
        //[ResponseType(typeof(IList<Project>))]
        //public HttpResponseMessage fbyproject(HttpRequestMessage request, IList<string> parameters)
        //{
        //    var TestProjDao = new TestProjDao();

        //    var projects = TestProjDao.fbyproject(parameters);
        //    TestProjDao.Dispose();
        //    return request.CreateResponse(HttpStatusCode.OK, projects);
        //}


      //  [HttpGet] // deve sair, quando converter o indicador de desenvolvimento
      //  [Route("projects")]
      //  public List<projects> getProjects()
      //  {
      //      string sql = @"
      //          select distinct
	     //           '{' +
	     //           'id:''' + convert(varchar, cast(substring(re.Subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(re.Entrega,8,8) as int)) + ''', ' +
	     //           'subproject:''' + re.Subprojeto + ''', ' +
	     //           'delivery:''' + re.Entrega + ''', ' +
	     //           'devManuf:''' + Fabrica_Desenvolvimento + ''', ' +
	     //           'system:''' + Sistema + ''', ' +
	     //           'name:''' + left(sp.Nome,30) + ''', ' +
	     //           'classification:''' + sp.Classificacao_Nome + ''', ' +
	     //           'release:''' + (select Sigla from sgq_meses m where m.id = re.release_mes) + ' ' + convert(varchar, re.release_ano) + '''' +
	     //           '}, ' as json,
	     //           convert(varchar, cast(substring(re.Subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(re.Entrega,8,8) as int)) as id,
	     //           Fabrica_Desenvolvimento as devManuf,
	     //           Sistema as system,
	     //           re.Subprojeto as subproject,
	     //           re.Entrega as delivery,
	     //           sp.nome as name,
	     //           sp.Classificacao_Nome as classification,
	     //           (select Sigla from sgq_meses m where m.id = re.release_mes) + ' ' + convert(varchar, re.release_ano)  as release,
	     //           re.release_ano,
	     //           re.release_mes
      //          from 
	     //           SGQ_Releases_Entregas re WITH (NOLOCK)
	     //           inner join biti_Subprojetos sp WITH (NOLOCK)
		    //            on sp.id = re.Subprojeto
      //              inner join biti_usuarios us WITH (NOLOCK)
		    //            on us.id = sp.lider_tecnico_id 
      //              inner join 
						//(
						//select distinct
						//	Subprojeto, Entrega, fabrica_desenvolvimento, sistema
						//from
						//	(
						//	select distinct 
						//		cts.Subprojeto,
						//		cts.Entrega,
						//		cts.Fabrica_Desenvolvimento,
						//		cts.sistema
						//	from 
						//		alm_cts cts WITH (NOLOCK)
						//	union all
						//	select distinct 
						//		d.Subprojeto,
						//		d.Entrega,
						//		d.Fabrica_Desenvolvimento,
						//		d.sistema_defeito as sistema
						//	from 
						//		alm_defeitos d WITH (NOLOCK)
						//	) aux
						//) cts 
						//on cts.Subprojeto = re.Subprojeto and
						//	cts.Entrega = re.Entrega
      //          where
	     //           re.id = (select top 1 re2.id from  SGQ_Releases_Entregas re2 where re2.subprojeto = re.subprojeto and re2.entrega = re.entrega order by re2.release_ano desc, re2.release_mes desc) and
	     //           Fabrica_Desenvolvimento is not null
      //          order by
	     //           Fabrica_Desenvolvimento,
	     //           Sistema,
	     //           re.Subprojeto,
	     //           re.Entrega,
	     //           sp.Classificacao_Nome,
	     //           re.release_ano,
	     //           re.release_mes
      //          ";

      //      var Connection = new Connection(Bancos.Sgq);
      //      List<projects> ListProjects = Connection.Executar<projects>(sql);
      //      Connection.Dispose();

      //      return ListProjects;
      //  }




     //   [HttpGet]
     //   [Route("projects_")] // DEVERA ALTERAR O NOME QUANDO IMPLATADO EM PRODUCAO
     //   public List<project> getProjects_()
     //   {
     //       string sql = @"
     //           select 
     //               sgq_projects.id,
     //               sgq_projects.subproject as subproject,
     //               sgq_projects.delivery as delivery,
     //               convert(varchar, cast(substring(sgq_projects.subproject,4,8) as int)) + ' ' + convert(varchar,cast(substring(sgq_projects.delivery,8,8) as int)) as subDel,
     //               biti_subprojetos.nome as name,
     //               biti_subprojetos.objetivo as objective,
					//biti_subprojetos.classificacao_nome as classification,
					//replace(replace(replace(replace(replace(biti_subprojetos.estado,'CONSOLIDAÇÃO E APROVAÇÃO DO PLANEJAMENTO','CONS/APROV. PLAN'),'PLANEJAMENTO','PLANEJ.'),'DESENHO DA SOLUÇÃO','DES.SOL'),'VALIDAÇÃO','VALID.'),'AGUARDANDO','AGUAR.') as state,
					//(select Sigla from sgq_meses m where m.id = SGQ_Releases_Entregas.release_mes) + ' ' + convert(varchar, SGQ_Releases_Entregas.release_ano) as release,
					//biti_subprojetos.Gerente_Projeto as GP,
			  //      biti_subprojetos.Gestor_Do_Gestor_LT as N3,
     //               sgq_projects.trafficLight as trafficLight,
     //               sgq_projects.rootCause as rootCause,
     //               sgq_projects.actionPlan as actionPlan,
     //               sgq_projects.informative as informative,
     //               sgq_projects.attentionPoints as attentionPoints,
     //               sgq_projects.attentionPointsIndicators as attentionPointsOfIndicators
     //           from 
     //               sgq_projects
     //               inner join alm_projetos WITH (NOLOCK)
	    //              on alm_projetos.subprojeto = sgq_projects.subproject and
	    //                alm_projetos.entrega = sgq_projects.delivery and
	    //                alm_projetos.ativo = 'Y'
     //               left join biti_subprojetos WITH (NOLOCK)
	    //              on biti_subprojetos.id = sgq_projects.subproject
					//left join SGQ_Releases_Entregas WITH (NOLOCK)
	    //              on SGQ_Releases_Entregas.subprojeto = sgq_projects.subproject and
					//     SGQ_Releases_Entregas.entrega = sgq_projects.delivery and
					//	 SGQ_Releases_Entregas.id = (select top 1 re2.id from SGQ_Releases_Entregas re2 
					//	                             where re2.subprojeto = SGQ_Releases_Entregas.subprojeto and 
					//								       re2.entrega = SGQ_Releases_Entregas.entrega 
					//								 order by re2.release_ano desc, re2.release_mes desc)
     //           order by 
     //               sgq_projects.subproject, 
     //               sgq_projects.delivery
     //           ";

     //       var Connection = new Connection(Bancos.Sgq);
     //       List<project> ListProjects = Connection.Executar<project>(sql);
     //       Connection.Dispose();

     //       return ListProjects;
     //   }

        [HttpGet]
        [Route("project/testProj/byIds/{ids}")]
        [ResponseType(typeof(IList<Project>))]
        public HttpResponseMessage byIds(HttpRequestMessage request, string ids)
        {
            var TestProjDao = new TestProjDao();
            var projects = TestProjDao.byIds(ids);
            TestProjDao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, projects);
        }


        [HttpGet]
        [Route("project/testProj/byproject/{subproject}/{delivery}")]
        [ResponseType(typeof(Project))]
        public HttpResponseMessage getProject(HttpRequestMessage request, string subproject, string delivery)
        {
            var TestProjDao = new TestProjDao();
            var project = TestProjDao.getProject(subproject, delivery);
            TestProjDao.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, project);
        }

        [HttpPut]
        [Route("project/testProj/update/{id:int}")]
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
        [Route("project/testProj/CtsImpactedXDefects/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<CtsImpactedXDefects>))]
        public HttpResponseMessage getCtsImpactedXDefects(HttpRequestMessage request, string subproject, string delivery)
        {
            var TestProjDao = new TestProjDao();
            var list = TestProjDao.getCtsImpactedXDefects(subproject, delivery);
            TestProjDao.Dispose();

            return request.CreateResponse(HttpStatusCode.OK, list);
        }


        // ITERATIONS

        [HttpGet]
        [Route("project/testProj/iterations/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<iteration>))]
        public HttpResponseMessage iterations(HttpRequestMessage request, string subproject, string delivery)
        {
            var TestProjDao = new TestProjDao();
            var list = TestProjDao.iterations(subproject, delivery);
            TestProjDao.Dispose();

            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpGet]
        [Route("project/testProj/iterationsActive/{subproject}/{delivery}")]
        [ResponseType(typeof(List<string>))]
        public HttpResponseMessage iterationsActive(HttpRequestMessage request, string subproject, string delivery)
        {
            var TestProjDao = new TestProjDao();
            var list = TestProjDao.iterationsActive(subproject, delivery);
            TestProjDao.Dispose();

            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpGet]
        [Route("project/testProj/iterationsSelected/{subproject}/{delivery}")]
        [ResponseType(typeof(List<string>))]
        public HttpResponseMessage iterationsSelected(HttpRequestMessage request, string subproject, string delivery)
        {
            var TestProjDao = new TestProjDao();
            var list = TestProjDao.iterationsSelected(subproject, delivery);
            TestProjDao.Dispose();

            return request.CreateResponse(HttpStatusCode.OK, list);
        }


        [HttpPut]
        [Route("project/testProj/UpdateIterationsActive/{id:int}")]
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
        [Route("project/testProj/UpdateIterationsSelected/{id:int}")]
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
        [Route("project/testProj/ClearIterations/{id:int}")]
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


        [HttpPut]
        [Route("project/testProj/CtsImpactedXDefectsIterations/{subproject}/{delivery}")]
        [ResponseType(typeof(IList<CtsImpactedXDefects>))]
        public HttpResponseMessage getCtsImpactedXDefectsIterations(HttpRequestMessage request, string subproject, string delivery, List<string> iterations)
        {
            var TestProjDao = new TestProjDao();
            var list = TestProjDao.getCtsImpactedXDefectsIterations(subproject, delivery, iterations);
            TestProjDao.Dispose();

            return request.CreateResponse(HttpStatusCode.OK, list);
        }
    }
}