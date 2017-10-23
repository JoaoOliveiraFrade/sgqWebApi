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
using System.Collections;
using System.Web.Http.Description;

namespace ProjectWebApi.Controllers
{
    public class IndicatorDevController : ApiController
    {
        [HttpGet]
        [Route("indicatorDev/DefectsDensityByProjectIterations/{subproject}/{delivery}")]
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
        [Route("indicatorDev/DefectsDensity/{subproject}/{delivery}")]
        [ResponseType(typeof(DefectDensity))]
        public HttpResponseMessage getDefectsDensityByProject(HttpRequestMessage request, string subproject, string delivery)
        {
            var projectDAO = new ProjectDAO();
            var densityDefects = projectDAO.getDefectsDensityByProject(subproject, delivery);
            projectDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, densityDefects);
        }


        [HttpGet] // DEVERAR SAIR, QUANDO IMPLANTADO EM PRODUÇÃO
        [Route("indicatorDev/defectsMiddleAges")]
        public List<AgingMedioDefects> getAgingMedio()
        {
            string sql = @"
                select 
					--'{' +
					--'severity:''' + substring(severidade,3,10) + ''', ' +
					--'date:''' + substring(dt_final,4,2) + '/' + substring(dt_final,7,2) + ''', ' +
					--'devManufacturing:''' + fabrica_desenvolvimento + ''', ' +
					--'system:''' + sistema_defeito + ''', ' +
					--'project:''' + convert(varchar, cast(substring(subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(entrega,8,8) as int)) + ''', ' +
					--'subproject:''' + subprojeto + ''', ' +
					--'delivery:''' + entrega + ''', ' +
					--'qtyDefects:' + convert(varchar,count(*)) + ',' + 
					--'qtyHours:' + convert(varchar,round(sum(Aging),2)) + ',' + 
					--'Media:' + convert(varchar,round(sum(Aging) / count(*),2)) + 
					--'}, ' as json,
	                substring(severidade,3,10) as severity,
	                substring(dt_final,4,2) + '/' + substring(dt_final,7,2) as date,
	                fabrica_desenvolvimento as devManufacturing,
	                sistema_defeito as system,
	                convert(varchar, cast(substring(Subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(Entrega,8,8) as int)) as project,
	                subprojeto as subproject,
	                entrega as delivery,
	                count(*) as qtyDefects,
	                round(sum(Aging),2) as qtyHours,
	                round(sum(Aging) / count(*),2) as Media
                from 
	                alm_defeitos s
                where 
	                ciclo in ('TI', 'UAT') and
	                status_atual = 'CLOSED' and
	                dt_final <> ''
                group by
	                substring(severidade,3,10),
	                severidade,
	                substring(dt_final,4,2),
	                substring(dt_final,7,2),
	                subprojeto,
	                entrega,
	                fabrica_desenvolvimento,
	                sistema_defeito
                order by 
	                severidade,
	                substring(dt_final,7,2),
	                substring(dt_final,4,2),
	                fabrica_desenvolvimento 
            ";

            var Connection = new Connection(Bancos.Sgq);
            List<AgingMedioDefects> List = Connection.Executar<AgingMedioDefects>(sql);
            Connection.Dispose();

            return List;
        }

        [HttpGet] // DEVERAR SAIR, QUANDO IMPLANTADO EM PRODUÇÃO
        [Route("indicatorDev/defectsMiddleAges/{dateBegin}/{dateEnd}")]
        public List<AgingMedioDefects> getAgingMedioByDate(string dateBegin, string dateEnd)
        {
            string sql = @"
                select 
	                substring(severidade,3,10) as severity,
	                substring(dt_final,1,2) + '/' + substring(dt_final,4,2) + '/' + substring(dt_final,7,2) as date,
	                fabrica_desenvolvimento as devManufacturing,
	                sistema_defeito as system,
	                convert(varchar, cast(substring(Subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(Entrega,8,8) as int)) as project,
	                subprojeto as subproject,
	                entrega as delivery,
	                count(*) as qtyDefects,
	                round(sum(Aging),2) as qtyHours,
	                round(sum(Aging) / count(*),2) as Media
                from 
	                alm_defeitos s
                where 
	                ciclo in ('TI', 'UAT') and
	                status_atual = 'CLOSED' and
	                dt_final <> '' and
					substring(dt_final,7,2) + substring(dt_final,4,2) + substring(dt_final,1,2) >= '@dateBegin' and
					substring(dt_final,7,2) + substring(dt_final,4,2) + substring(dt_final,1,2) <= '@dateEnd'
                group by
	                substring(severidade,3,10),
	                severidade,
	                substring(dt_final,4,2),
	                substring(dt_final,7,2),
					substring(dt_final,1,2),
	                subprojeto,
	                entrega,
	                fabrica_desenvolvimento,
	                sistema_defeito
                order by 
	                severidade,
					substring(dt_final,1,2),
	                substring(dt_final,4,2),
					substring(dt_final,7,2),
	                fabrica_desenvolvimento
            ";

            sql = sql.Replace("@dateBegin", dateBegin);
            sql = sql.Replace("@dateEnd", dateEnd);

            var Connection = new Connection(Bancos.Sgq);
            List<AgingMedioDefects> List = Connection.Executar<AgingMedioDefects>(sql);
            Connection.Dispose();

            return List;
        }


        [HttpGet]
        [Route("indicatorDev/defectsWrongClassif")]
        public List<WrongClassif> getWrongClassificationDefectRate()
        {
            string sql = @"
                select 
	                --'{' +
	                --'date:''' + substring(dt_final,4,2) + '/' + substring(dt_final,7,2) + ''',' +
	                --'devManufacturing:''' + 
	                --	case when Aud_FD_Ofensora is not null 
	                --			then Aud_FD_Ofensora 
	                --			else fabrica_desenvolvimento 
	                --	end + ''',' +
	                --'system:''' + sistema_defeito + ''',' +
	                --'project:''' + convert(varchar, cast(substring(subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(entrega,8,8) as int)) + ''',' +
	                --'subproject:''' + subprojeto + ''',' +
	                --'delivery:''' + entrega + ''',' +
	                --'qtyTotal:' + convert(varchar,count(*)) + ',' + 
	                --'qty:' + 
	                --	convert(varchar,
	                --		sum(
	                --			case when 
	                --					Aud_Regra_Infringida is not null and
	                --					Aud_FD_Ofensora is not null
	                --				then 1
	                --				else 0
	                --			end	
	                --		)
	                --	) + ',' +
	                --'percentReference:5,' +
	                --'qtyReference:' + convert(varchar,round(convert(float,count(*) * 0.05),2)) + 
	                --'},' as json,
	                substring(dt_final,4,2) + '/' + substring(dt_final,7,2) as date,

	                case when Aud_FD_Ofensora is not null 
			                then Aud_FD_Ofensora 
			                else fabrica_desenvolvimento 
	                end as devManufacturing,

	                sistema_defeito as system,
	                convert(varchar, cast(substring(subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(entrega,8,8) as int)) as project,
	                subprojeto as subproject,
	                entrega as delivery,
	                count(*) as qtyTotal,
	                sum(
		                case when 
				                Aud_Regra_Infringida is not null and
				                Aud_FD_Ofensora is not null
			                then 1
			                else 0
		                end	
	                ) as qty,
	                5 as percentReference,
	                round(convert(float,count(*) * 0.05),2) as qtyReference
                from 
	                alm_defeitos 
                where 
	                ciclo in ('TI', 'UAT') and
	                status_atual = 'CLOSED' and 
	                dt_final <> ''
                group by
	                substring(dt_final,4,2),
	                substring(dt_final,7,2),
	                subprojeto,
	                entrega,
	                case when Aud_FD_Ofensora is not null 
			                then Aud_FD_Ofensora 
			                else fabrica_desenvolvimento 
	                end,
	                sistema_defeito
                order by 
	                substring(dt_final,7,2),
	                substring(dt_final,4,2),
	                case when Aud_FD_Ofensora is not null 
			                then Aud_FD_Ofensora 
			                else fabrica_desenvolvimento 
	                end
            ";

            var Connection = new Connection(Bancos.Sgq);
            List<WrongClassif> List = Connection.Executar<WrongClassif>(sql);
            Connection.Dispose();

            return List;
        }

        [HttpGet]
        [Route("indicatorDev/defectsWrongClassif/{dateBegin}/{dateEnd}")]
        public List<WrongClassif> getWrongClassificationDefectRateByDate(string dateBegin, string dateEnd)
        {
            string sql = @"
                select 
	                 substring(dt_final,1,2) + '/' + substring(dt_final,4,2) + '/' + substring(dt_final,7,2) as date,

	                case when Aud_FD_Ofensora is not null 
			                then Aud_FD_Ofensora 
			                else fabrica_desenvolvimento 
	                end as devManufacturing,

	                sistema_defeito as system,
	                convert(varchar, cast(substring(subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(entrega,8,8) as int)) as project,
	                subprojeto as subproject,
	                entrega as delivery,
	                count(*) as qtyTotal,
	                sum(
		                case when 
				                Aud_Regra_Infringida is not null and
				                Aud_FD_Ofensora is not null
			                then 1
			                else 0
		                end	
	                ) as qty,
	                5 as percentReference,
	                round(convert(float,count(*) * 0.05),2) as qtyReference
                from 
	                alm_defeitos 
                where 
	                ciclo in ('TI', 'UAT') and
	                status_atual = 'CLOSED' and 
	                dt_final <> '' and
					substring(dt_final,7,2) + substring(dt_final,4,2) + substring(dt_final,1,2) >= '@dateBegin' and
					substring(dt_final,7,2) + substring(dt_final,4,2) + substring(dt_final,1,2) <= '@dateEnd'
                group by
	                substring(dt_final,4,2),
	                substring(dt_final,7,2),
					substring(dt_final,1,2),
	                subprojeto,
	                entrega,
	                case when Aud_FD_Ofensora is not null 
			                then Aud_FD_Ofensora 
			                else fabrica_desenvolvimento 
	                end,
	                sistema_defeito
                order by 
	                substring(dt_final,7,2),
	                substring(dt_final,4,2),
					substring(dt_final,1,2),
	                case when Aud_FD_Ofensora is not null 
			                then Aud_FD_Ofensora 
			                else fabrica_desenvolvimento 
	                end
            ";

            sql = sql.Replace("@dateBegin", dateBegin);
            sql = sql.Replace("@dateEnd", dateEnd);

            var Connection = new Connection(Bancos.Sgq);
            List<WrongClassif> List = Connection.Executar<WrongClassif>(sql);
            Connection.Dispose();

            return List;
        }


        [HttpGet]
        [Route("indicatorDev/defectsDetectableInDev")]
        public List<DetectableInDev2> getDetectableInDev()
        {
            string sql = @"
                select 
	                --'{' +
	                --'date:''' + substring(dt_final,4,2) + '/' + substring(dt_final,7,2) + ''', ' +
	                --'devManufacturing:''' + fabrica_desenvolvimento + ''', ' +
	                --'system:''' + sistema_defeito + ''', ' +
	                --'project:''' + convert(varchar, cast(substring(subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(entrega,8,8) as int)) + ''', ' +
	                --'subproject:''' + subprojeto + ''', ' +
	                --'delivery:''' + entrega + ''', ' +
	                --'qtyTotal:' + convert(varchar,count(*)) + ',' + 
	                --'qty:' + 
	                --	convert(varchar,
	                --		sum(
	                --			case when Erro_Detectavel_Em_Desenvolvimento = 'SIM'
	                --				then 1
	                --				else 0
	                --			end	
	                --		)
	                --	) + ',' +
	                --'percentReference:5,' +
	                --'qtyReference:' + convert(varchar,round(convert(float,count(*) * 0.05),2)) + 
	                --'}, ' as json,
	                substring(dt_final,4,2) + '/' + substring(dt_final,7,2) as date,
	                fabrica_desenvolvimento as devManufacturing,
	                sistema_defeito as system,
	                convert(varchar, cast(substring(Subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(Entrega,8,8) as int)) as project,
	                subprojeto as subproject,
	                entrega as delivery,
	                count(*) as qtyTotal,
	                sum(
		                case when Erro_Detectavel_Em_Desenvolvimento = 'SIM'
			                then 1
			                else 0
		                end	
	                ) as qty,
	                5 as percentReference,
	                round(convert(float,count(*) * 0.05),2) as qtyReference
                from 
	                alm_defeitos 
                where 
	                ciclo in ('TI', 'UAT') and
	                status_atual = 'CLOSED' and 
	                dt_final <> ''
                group by
	                substring(dt_final,4,2),
	                substring(dt_final,7,2),
	                subprojeto,
	                entrega,
	                fabrica_desenvolvimento,
	                sistema_defeito
                order by 
	                substring(dt_final,7,2),
	                substring(dt_final,4,2),
	                fabrica_desenvolvimento
            ";

            var Connection = new Connection(Bancos.Sgq);
            List<DetectableInDev2> List = Connection.Executar<DetectableInDev2>(sql);
            Connection.Dispose();

            return List;
        }

        [HttpGet]
        [Route("indicatorDev/defectsDetectableInDev/{dateBegin}/{dateEnd}")]
        public List<DetectableInDev2> getDetectableInDevByDate(string dateBegin, string dateEnd)
        {
            string sql = @"
                select 
	                substring(dt_final,1,2) + '/' + substring(dt_final,4,2) + '/' + substring(dt_final,7,2) as date,
	                fabrica_desenvolvimento as devManufacturing,
	                sistema_defeito as system,
	                convert(varchar, cast(substring(Subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(Entrega,8,8) as int)) as project,
	                subprojeto as subproject,
	                entrega as delivery,
	                count(*) as qtyTotal,
	                sum(
		                case when Erro_Detectavel_Em_Desenvolvimento = 'SIM'
			                then 1
			                else 0
		                end	
	                ) as qty,
	                5 as percentReference,
	                round(convert(float,count(*) * 0.05),2) as qtyReference
                from 
	                alm_defeitos 
                where 
	                ciclo in ('TI', 'UAT') and
	                status_atual = 'CLOSED' and 
	                dt_final <> '' and
					substring(dt_final,7,2) + substring(dt_final,4,2) + substring(dt_final,1,2) >= '@dateBegin' and
					substring(dt_final,7,2) + substring(dt_final,4,2) + substring(dt_final,1,2) <= '@dateEnd'
                group by
				    substring(dt_final,1,2),
	                substring(dt_final,4,2),
	                substring(dt_final,7,2),
	                subprojeto,
	                entrega,
	                fabrica_desenvolvimento,
	                sistema_defeito
                order by 
	                substring(dt_final,7,2),
	                substring(dt_final,4,2),
					substring(dt_final,1,2),
	                fabrica_desenvolvimento
            ";

            sql = sql.Replace("@dateBegin", dateBegin);
            sql = sql.Replace("@dateEnd", dateEnd);

            var Connection = new Connection(Bancos.Sgq);
            List<DetectableInDev2> List = Connection.Executar<DetectableInDev2>(sql);
            Connection.Dispose();

            return List;
        }


        [HttpPut]
        [Route("indicatorDev/DefectsDetectableInDevIterations/{subproject}/{delivery}")]
        [ResponseType(typeof(DetectableInDev2))]
        public HttpResponseMessage getDetectableInDevByProjectIterations(HttpRequestMessage request, string subproject, string delivery, List<string> iterations)
        {
            var projectDAO = new ProjectDAO();
            var item = projectDAO.getDetectableInDevByProjectIterations(subproject, delivery, iterations);
            projectDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, item);
        }

        [HttpGet]
        [Route("indicatorDev/defectsDetectableInDev/{subproject}/{delivery}")]
        [ResponseType(typeof(DetectableInDev2))]
        public HttpResponseMessage defectsDetectableInDev(HttpRequestMessage request, string subproject, string delivery)
        {
            var indicatorDevDAO = new IndicatorDevDAO();
            var detectableInDev = indicatorDevDAO.getDetectableInDevByProject(subproject, delivery);
            indicatorDevDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, detectableInDev);
        }

        [HttpPost]
        [Route("indicatorDev/defectsDetectableInDev/fbyListDevManufSystemProject")]
        [ResponseType(typeof(IList<DetectableInDev2>))]
        public HttpResponseMessage defectsDetectableInDevFbyListDevManufSystemProject(HttpRequestMessage request, Parameters2 parameters)
        {
            var indicatorDevDAO = new IndicatorDevDAO();
            var list = indicatorDevDAO.defectsDetectableInDevFbyListDevManufSystemProject(parameters);
            indicatorDevDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, list);
        }

        [HttpPut]
        [Route("indicatorDev/DefectsReopenedIterations/{subproject}/{delivery}")]
        [ResponseType(typeof(DefectReopened))]
        public HttpResponseMessage getDefectReopenedByProjectIterations(HttpRequestMessage request, string subproject, string delivery, List<string> iterations)
        {
            var projectDAO = new ProjectDAO();
            var item = projectDAO.getDefectReopenedByProjectIterations(subproject, delivery, iterations);
            projectDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, item);
        }

        [HttpGet]
        [Route("indicatorDev/DefectsReopened/{subproject}/{delivery}")]
        [ResponseType(typeof(DefectReopened))]
        public HttpResponseMessage getDefectReopenedByProject(HttpRequestMessage request, string subproject, string delivery)
        {
            var projectDAO = new ProjectDAO();
            var densityDefects = projectDAO.getDefectReopenedByProject(subproject, delivery);
            projectDAO.Dispose();
            return request.CreateResponse(HttpStatusCode.OK, densityDefects);
        }


        [HttpGet]
        [Route("indicatorDev/defectsReopened")]
        public List<reopenedDefects> GetReopened()
        {
            string sql = @"
                select
	                --'{' +   'date: ''' + substring(dt_final,4,2) + '/' + substring(dt_final,7,2) + ''', ' +
	                --'devManufacturing:''' + fabrica_desenvolvimento + ''', ' +
	                --'system:''' + sistema_defeito + ''', ' +
	                --'project:''' + convert(varchar, cast(substring(subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(entrega,8,8) as int)) + ''', ' +
	                --'subproject:''' + subprojeto + ''', ' +
	                --'delivery:''' + entrega + ''', ' +
	                --'qtyTotal:'  + '' + convert(varchar, count(*)) + ', ' +
	                --'qty:'  + '' + convert(varchar, sum(qtd_reopen)) + ', ' +
	                --'percent:'  + '' + convert(varchar, round(convert(float,sum(qtd_reopen)) / count(*) * 100,2)) + ', ' +
	                --'percentReference:5, ' +
	                --'qtyReference:'  + '' + convert(varchar, round(convert(float,count(*) * 0.05),2)) + 
	                --'}, ' as json,
	                substring(dt_final,4,2) + '/' + substring(dt_final,7,2) as date,
	                fabrica_desenvolvimento as devManufacturing,
	                sistema_defeito as system,
	                convert(varchar, cast(substring(Subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(Entrega,8,8) as int)) as project,
	                entrega as delivery,
	                subprojeto as subproject,
	                count(*) as qtyTotal,
	                sum(qtd_reopen) as qty,
	                round(convert(float,sum(qtd_reopen)) / count(*) * 100,2) as [percent],
	                5 as percentReference,
	                round(convert(float,count(*) * 0.05),2) as qtyReference
                from 
	                alm_defeitos 
                where 
	                ciclo in ('TI', 'UAT') and
	                status_atual = 'CLOSED' and
	                dt_final <> ''
                group by
	                substring(dt_final,4,2),
	                substring(dt_final,7,2),
	                subprojeto,
	                entrega,
	                fabrica_desenvolvimento,
	                sistema_defeito
                order by 
	                substring(dt_final,7,2),
	                substring(dt_final,4,2),
	                fabrica_desenvolvimento
            ";

            var Connection = new Connection(Bancos.Sgq);
            List<reopenedDefects> List = Connection.Executar<reopenedDefects>(sql);
            Connection.Dispose();

            return List;
        }

        [HttpGet]
        [Route("indicatorDev/defectsReopened/{dateBegin}/{dateEnd}")]
        public List<reopenedDefects> getReopenedByDate(string dateBegin, string dateEnd)
        {
            string sql = @"
                select
	                substring(dt_final,1,2) + '/' + substring(dt_final,4,2) + '/' + substring(dt_final,7,2) as date,
	                fabrica_desenvolvimento as devManufacturing,
	                sistema_defeito as system,
	                convert(varchar, cast(substring(Subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(Entrega,8,8) as int)) as project,
	                entrega as delivery,
	                subprojeto as subproject,
	                count(*) as qtyTotal,
	                sum(qtd_reopen) as qty,
	                round(convert(float,sum(qtd_reopen)) / count(*) * 100,2) as [percent],
	                5 as percentReference,
	                round(convert(float,count(*) * 0.05),2) as qtyReference
                from 
	                alm_defeitos 
                where 
	                ciclo in ('TI', 'UAT') and
	                status_atual = 'CLOSED' and
	                dt_final <> '' and
					substring(dt_final,7,2) + substring(dt_final,4,2) + substring(dt_final,1,2) >= '@dateBegin' and
					substring(dt_final,7,2) + substring(dt_final,4,2) + substring(dt_final,1,2) <= '@dateEnd'
                group by
				    substring(dt_final,1,2),
	                substring(dt_final,4,2),
	                substring(dt_final,7,2),
	                subprojeto,
	                entrega,
	                fabrica_desenvolvimento,
	                sistema_defeito
                order by 
	                substring(dt_final,7,2),
	                substring(dt_final,4,2),
					substring(dt_final,1,2),
	                fabrica_desenvolvimento
            ";

            sql = sql.Replace("@dateBegin", dateBegin);
            sql = sql.Replace("@dateEnd", dateEnd);

            var Connection = new Connection(Bancos.Sgq);
            List<reopenedDefects> List = Connection.Executar<reopenedDefects>(sql);
            Connection.Dispose();

            return List;
        }


        [HttpGet]
        [Route("indicatorDev/defectsNoPrediction")]
        public List<noPredictionDefects> getnoPredictionDefects()
        {
            string sql = @"
                select
	                --'{' +   'date: ''' + substring(dt_final,4,2) + '/' + substring(dt_final,7,2) + ''', ' +
	                --'devManufacturing:''' + fabrica_desenvolvimento + ''', ' +
	                --'system:''' + sistema_defeito + ''', ' +
	                --'project:''' + convert(varchar, cast(substring(subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(entrega,8,8) as int)) + ''', ' +
	                --'subproject:''' + subprojeto + ''', ' +
	                --'delivery:''' + entrega + ''', ' +
	                --'qtyTotal:'  + '' + convert(varchar, count (*)) + ''', ' +
	                --'qty:'  + '' + convert(varchar, sum(case when Dt_Prevista_Solucao_Defeito <> '' then 0 else 1 end)) + ''', ' +
	                --'percent:'  + '' + convert(varchar, round(convert(float,sum(case when Dt_Prevista_Solucao_Defeito <> '' then 0 else 1 end)) / count(*) * 100,2)) + ''', ' +
	                --'percentReference:5, ' +
	                --'qtyReference:'  + '' + convert(varchar, round(convert(float,count(*) * 0.05),2)) + 
	                --'}, ' as json,
	                substring(dt_final,4,2) + '/' + substring(dt_final,7,2) as date,
	                fabrica_desenvolvimento as devManufacturing,
	                sistema_defeito as system,
	                convert(varchar, cast(substring(Subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(Entrega,8,8) as int)) as project,
	                subprojeto as subproject,
	                entrega as delivery,
	                count (*) as qtyTotal,
	                sum(case when Dt_Prevista_Solucao_Defeito <> '' then 0 else 1 end) as qty,
	                round(convert(float,sum(case when Dt_Prevista_Solucao_Defeito = '' then 1 else 0 end)) / count(*) * 100,2) as [percent],
	                5 as percentReference,
	                round(convert(float,count(*) * 0.05),2) as qtyReference
                from 
	                alm_defeitos 
                where 
	                ciclo in ('TI', 'UAT') and
	                status_atual = 'CLOSED' and
	                dt_final <> ''
                group by
	                substring(dt_final,4,2),
	                substring(dt_final,7,2),
	                subprojeto,
	                entrega,
	                fabrica_desenvolvimento,
	                sistema_defeito
                order by 
	                substring(dt_final,7,2),
	                substring(dt_final,4,2),
	                fabrica_desenvolvimento
            ";

            var Connection = new Connection(Bancos.Sgq);
            List<noPredictionDefects> List = Connection.Executar<noPredictionDefects>(sql);
            Connection.Dispose();

            return List;
        }

        [HttpGet]
        [Route("indicatorDev/defectsNoPrediction/{dateBegin}/{dateEnd}")]
        public List<noPredictionDefects> getnoPredictionDefectsByDate(string dateBegin, string dateEnd)
        {
            string sql = @"
                select
	                substring(dt_final,1,2) + '/' + substring(dt_final,4,2) + '/' + substring(dt_final,7,2) as date,
	                fabrica_desenvolvimento as devManufacturing,
	                sistema_defeito as system,
	                convert(varchar, cast(substring(Subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(Entrega,8,8) as int)) as project,
	                subprojeto as subproject,
	                entrega as delivery,
	                count (*) as qtyTotal,
	                sum(case when Dt_Prevista_Solucao_Defeito <> '' then 0 else 1 end) as qty,
	                round(convert(float,sum(case when Dt_Prevista_Solucao_Defeito = '' then 1 else 0 end)) / count(*) * 100,2) as [percent],
	                5 as percentReference,
	                round(convert(float,count(*) * 0.05),2) as qtyReference
                from 
	                alm_defeitos 
                where 
	                ciclo in ('TI', 'UAT') and
	                status_atual = 'CLOSED' and
	                dt_final <> '' and
					substring(dt_final,7,2) + substring(dt_final,4,2) + substring(dt_final,1,2) >= '@dateBegin' and
					substring(dt_final,7,2) + substring(dt_final,4,2) + substring(dt_final,1,2) <= '@dateEnd'
                group by
				    substring(dt_final,1,2),
	                substring(dt_final,4,2),
	                substring(dt_final,7,2),
	                subprojeto,
	                entrega,
	                fabrica_desenvolvimento,
	                sistema_defeito
                order by 
	                substring(dt_final,7,2),
	                substring(dt_final,4,2),
					substring(dt_final,1,2),
	                fabrica_desenvolvimento
            ";

            sql = sql.Replace("@dateBegin", dateBegin);
            sql = sql.Replace("@dateEnd", dateEnd);

            var Connection = new Connection(Bancos.Sgq);
            List<noPredictionDefects> List = Connection.Executar<noPredictionDefects>(sql);
            Connection.Dispose();

            return List;
        }

// ===============================

        [HttpPost]
		[Route("indicatorDev/rateDefectsWithinSLA/fbyListDevManufSystemProject")]
        [ResponseType(typeof(IList<rateDefectsWithinSLA>))]
        public HttpResponseMessage rateDefectsWithinSLAFbyListTestManufSystemProject(HttpRequestMessage request, Parameters2 parameters)
		{
			var indicatorAccomplishmentDAO = new IndicatorAccomplishmentDAO();
			var list = indicatorAccomplishmentDAO.rateDefectsWithinSLAFbyListTestManufSystemProject(parameters);
            indicatorAccomplishmentDAO.Dispose();
			return request.CreateResponse(HttpStatusCode.OK, list);
		}
    }

}