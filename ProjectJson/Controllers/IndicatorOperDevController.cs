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
using System.Collections;
using System.Web.Http.Description;

namespace ProjectWebApi.Controllers
{
    public class indicatorOperDevController : ApiController
    {
        #region Density

            [HttpGet]
            [Route("indicatorOperDev/defectDensity/fbyProject/{subproject}/{delivery}")]
            [ResponseType(typeof(IList<DefectDensity>))]
            public HttpResponseMessage defectDensityFbyProject(HttpRequestMessage request, string subproject, string delivery) {
                var indicatorOperDevDao = new IndicatorOperDevDao();
                var list = indicatorOperDevDao.defectDensityFbyProject(subproject, delivery);
                indicatorOperDevDao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, list);
            }

            [HttpPost]
            [Route("indicatorOperDev/defectDensity/fbydevManufsystemProject")]
            [ResponseType(typeof(IList<DefectDensity>))]
            public HttpResponseMessage defectDensityFbydevManufsystemProject(HttpRequestMessage request, devManufsystemProject parameters) {
                var indicatorOperDevDao = new IndicatorOperDevDao();
                var list = indicatorOperDevDao.defectDensityFbydevManufsystemProject(parameters);
                indicatorOperDevDao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, list);
            }
        
        #endregion


        #region AverangeTime

            [HttpGet]
            [Route("indicatorOperDev/defectAverangeTime/fbyProject/{subproject}/{delivery}")]
            [ResponseType(typeof(DefectAverangeTime))]
            public HttpResponseMessage defectAverangeTimeFbyProject(HttpRequestMessage request, string subproject, string delivery) {
                var indicatorOperDevDao = new IndicatorOperDevDao();
                var defectAverangeTime = indicatorOperDevDao.defectAverangeTimeFbyProject(subproject, delivery);
                indicatorOperDevDao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, defectAverangeTime);
            }

            [HttpPost]
            [Route("indicatorOperDev/defectAverangeTime/fbyProjectAndListIteration")]
            [ResponseType(typeof(DefectAverangeTime))]
            public HttpResponseMessage defectAverangeTimeFbyProjectAndListIteration(HttpRequestMessage request, ProjectAndListIteration parameters) {
                var indicatorOperDevDao = new IndicatorOperDevDao();
                var defectAverangeTime = indicatorOperDevDao.defectAverangeTimeFbyProjectAndListIteration(parameters);
                indicatorOperDevDao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, defectAverangeTime);
            }

            [HttpPost]
            [Route("indicatorOperDev/defectAverangeTime/fbydevManufsystemProject")]
            [ResponseType(typeof(IList<DefectAverangeTime>))]
            public HttpResponseMessage defectAverangeTimeFbydevManufsystemProject(HttpRequestMessage request, devManufsystemProject parameters) {
                var indicatorOperDevDao = new IndicatorOperDevDao();
                var list = indicatorOperDevDao.defectAverangeTimeFbydevManufsystemProject(parameters);
                indicatorOperDevDao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, list);
            }

            [HttpPost]
            [Route("indicatorOperDev/defectAverangeTime/fbydevManufsystemProjectIteration")]
            [ResponseType(typeof(IList<DefectAverangeTime>))]
            public HttpResponseMessage defectAverangeTimeFbydevManufsystemProjectIterations(HttpRequestMessage request, devManufsystemProjectIteration parameters) {
                var indicatorOperDevDao = new IndicatorOperDevDao();
                var list = indicatorOperDevDao.defectAverangeTimeFbydevManufsystemProjectIteration(parameters);
                indicatorOperDevDao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, list);
            }

        #endregion


        #region Reopened

            [HttpGet]
            [Route("indicatorOperDev/defectReopened/fbyProject/{subproject}/{delivery}")]
            [ResponseType(typeof(DefectReopened))]
            public HttpResponseMessage defectReopenedFbyProject(HttpRequestMessage request, string subproject, string delivery) {
                var indicatorOperDevDao = new IndicatorOperDevDao();
                var densityDefects = indicatorOperDevDao.defectReopenedFbyProject(subproject, delivery);
                indicatorOperDevDao.Dispose();
                return request.CreateResponse(HttpStatusCode.OK, densityDefects);
            }

            //[HttpPut]
            //[Route("indicatorOperDev/DefectsReopenedIterations/{subproject}/{delivery}")]
            //[ResponseType(typeof(DefectReopened))]
            //public HttpResponseMessage getDefectReopenedByProjectIterations(HttpRequestMessage request, string subproject, string delivery, List<string> iterations) {
            //    var projectDao = new ProjectDao();
            //    var item = projectDao.getDefectReopenedByProjectIterations(subproject, delivery, iterations);
            //    projectDao.Dispose();
            //    return request.CreateResponse(HttpStatusCode.OK, item);
            //}

            [HttpGet]
            [Route("indicatorOperDev/defectsReopened")]
            public List<DefectReopened> GetReopened() {
                string sql = @"
                    select
	                    --'{' +   'date: ''' + substring(dt_final,4,2) + '/' + substring(dt_final,7,2) + ''', ' +
	                    --'devManuf:''' + fabrica_desenvolvimento + ''', ' +
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
	                    fabrica_desenvolvimento as devManuf,
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
                List<DefectReopened> List = Connection.Executar<DefectReopened>(sql);
                Connection.Dispose();

                return List;
            }

        //   [HttpGet]
        //   [Route("indicatorOperDev/defectsReopened/{dateBegin}/{dateEnd}")]
        //   public List<DefectReopened> getReopenedByDate(string dateBegin, string dateEnd)
        //   {
        //       string sql = @"
        //           select
        //            substring(dt_final,1,2) + '/' + substring(dt_final,4,2) + '/' + substring(dt_final,7,2) as date,
        //            fabrica_desenvolvimento as devManuf,
        //            sistema_defeito as system,
        //            convert(varchar, cast(substring(Subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(Entrega,8,8) as int)) as project,
        //            entrega as delivery,
        //            subprojeto as subproject,
        //            count(*) as qtyTotal,
        //            sum(qtd_reopen) as qty,
        //            round(convert(float,sum(qtd_reopen)) / count(*) * 100,2) as [percent],
        //            5 as percentReference,
        //            round(convert(float,count(*) * 0.05),2) as qtyReference
        //           from 
        //            alm_defeitos 
        //           where 
        //            ciclo in ('TI', 'UAT') and
        //            status_atual = 'CLOSED' and
        //            dt_final <> '' and
        //substring(dt_final,7,2) + substring(dt_final,4,2) + substring(dt_final,1,2) >= '@dateBegin' and
        //substring(dt_final,7,2) + substring(dt_final,4,2) + substring(dt_final,1,2) <= '@dateEnd'
        //           group by
        //   substring(dt_final,1,2),
        //            substring(dt_final,4,2),
        //            substring(dt_final,7,2),
        //            subprojeto,
        //            entrega,
        //            fabrica_desenvolvimento,
        //            sistema_defeito
        //           order by 
        //            substring(dt_final,7,2),
        //            substring(dt_final,4,2),
        //substring(dt_final,1,2),
        //            fabrica_desenvolvimento
        //       ";

        //       sql = sql.Replace("@dateBegin", dateBegin);
        //       sql = sql.Replace("@dateEnd", dateEnd);

        //       var Connection = new Connection(Bancos.Sgq);
        //       List<DefectReopened> List = Connection.Executar<DefectReopened>(sql);
        //       Connection.Dispose();

        //       return List;
        //   }

        #endregion


        #region DetectableInDev

         //   [HttpGet]
         //   [Route("indicatorOperDev/defectsDetectableInDev")]
         //   public List<DetectableInDev2> getDetectableInDev() {
         //       string sql = @"
         //           select 
	        //            --'{' +
	        //            --'date:''' + substring(dt_final,4,2) + '/' + substring(dt_final,7,2) + ''', ' +
	        //            --'devManuf:''' + fabrica_desenvolvimento + ''', ' +
	        //            --'system:''' + sistema_defeito + ''', ' +
	        //            --'project:''' + convert(varchar, cast(substring(subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(entrega,8,8) as int)) + ''', ' +
	        //            --'subproject:''' + subprojeto + ''', ' +
	        //            --'delivery:''' + entrega + ''', ' +
	        //            --'qtyTotal:' + convert(varchar,count(*)) + ',' + 
	        //            --'qty:' + 
	        //            --	convert(varchar,
	        //            --		sum(
	        //            --			case when Erro_Detectavel_Em_Desenvolvimento = 'SIM'
	        //            --				then 1
	        //            --				else 0
	        //            --			end	
	        //            --		)
	        //            --	) + ',' +
	        //            --'percentReference:5,' +
	        //            --'qtyReference:' + convert(varchar,round(convert(float,count(*) * 0.05),2)) + 
	        //            --'}, ' as json,
	        //            substring(dt_final,4,2) + '/' + substring(dt_final,7,2) as date,
	        //            fabrica_desenvolvimento as devManuf,
	        //            sistema_defeito as system,
	        //            convert(varchar, cast(substring(Subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(Entrega,8,8) as int)) as project,
	        //            subprojeto as subproject,
	        //            entrega as delivery,
	        //            count(*) as qtyTotal,
	        //            sum(
		       //             case when Erro_Detectavel_Em_Desenvolvimento = 'SIM'
			      //              then 1
			      //              else 0
		       //             end	
	        //            ) as qty,
	        //            5 as percentReference,
	        //            round(convert(float,count(*) * 0.05),2) as qtyReference
         //           from 
	        //            alm_defeitos 
         //           where 
	        //            ciclo in ('TI', 'UAT') and
	        //            status_atual = 'CLOSED' and 
	        //            dt_final <> ''
         //           group by
	        //            substring(dt_final,4,2),
	        //            substring(dt_final,7,2),
	        //            subprojeto,
	        //            entrega,
	        //            fabrica_desenvolvimento,
	        //            sistema_defeito
         //           order by 
	        //            substring(dt_final,7,2),
	        //            substring(dt_final,4,2),
	        //            fabrica_desenvolvimento
         //       ";

         //       var Connection = new Connection(Bancos.Sgq);
         //       List<DetectableInDev2> List = Connection.Executar<DetectableInDev2>(sql);
         //       Connection.Dispose();

         //       return List;
         //   }

         //   [HttpGet]
         //   [Route("indicatorOperDev/defectsDetectableInDev/{dateBegin}/{dateEnd}")]
         //   public List<DetectableInDev2> getDetectableInDevByDate(string dateBegin, string dateEnd) {
         //       string sql = @"
         //           select 
	        //            substring(dt_final,1,2) + '/' + substring(dt_final,4,2) + '/' + substring(dt_final,7,2) as date,
	        //            fabrica_desenvolvimento as devManuf,
	        //            sistema_defeito as system,
	        //            convert(varchar, cast(substring(Subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(Entrega,8,8) as int)) as project,
	        //            subprojeto as subproject,
	        //            entrega as delivery,
	        //            count(*) as qtyTotal,
	        //            sum(
		       //             case when Erro_Detectavel_Em_Desenvolvimento = 'SIM'
			      //              then 1
			      //              else 0
		       //             end	
	        //            ) as qty,
	        //            5 as percentReference,
	        //            round(convert(float,count(*) * 0.05),2) as qtyReference
         //           from 
	        //            alm_defeitos 
         //           where 
	        //            ciclo in ('TI', 'UAT') and
	        //            status_atual = 'CLOSED' and 
	        //            dt_final <> '' and
					    //substring(dt_final,7,2) + substring(dt_final,4,2) + substring(dt_final,1,2) >= '@dateBegin' and
					    //substring(dt_final,7,2) + substring(dt_final,4,2) + substring(dt_final,1,2) <= '@dateEnd'
         //           group by
				     //   substring(dt_final,1,2),
	        //            substring(dt_final,4,2),
	        //            substring(dt_final,7,2),
	        //            subprojeto,
	        //            entrega,
	        //            fabrica_desenvolvimento,
	        //            sistema_defeito
         //           order by 
	        //            substring(dt_final,7,2),
	        //            substring(dt_final,4,2),
					    //substring(dt_final,1,2),
	        //            fabrica_desenvolvimento
         //       ";

         //       sql = sql.Replace("@dateBegin", dateBegin);
         //       sql = sql.Replace("@dateEnd", dateEnd);

         //       var Connection = new Connection(Bancos.Sgq);
         //       List<DetectableInDev2> List = Connection.Executar<DetectableInDev2>(sql);
         //       Connection.Dispose();

         //       return List;
         //   }


         //   [HttpPut]
         //   [Route("indicatorOperDev/DefectsDetectableInDevIterations/{subproject}/{delivery}")]
         //   [ResponseType(typeof(DetectableInDev2))]
         //   public HttpResponseMessage getDetectableInDevByProjectIterations(HttpRequestMessage request, string subproject, string delivery, List<string> iterations) {
         //       var projectDao = new ProjectDao();
         //       var item = projectDao.getDetectableInDevByProjectIterations(subproject, delivery, iterations);
         //       projectDao.Dispose();
         //       return request.CreateResponse(HttpStatusCode.OK, item);
         //   }

         //   [HttpGet]
         //   [Route("indicatorOperDev/defectsDetectableInDev/{subproject}/{delivery}")]
         //   [ResponseType(typeof(DetectableInDev2))]
         //   public HttpResponseMessage defectsDetectableInDev(HttpRequestMessage request, string subproject, string delivery) {
         //       var indicatorOperDevDao = new IndicatorOperDevDao();
         //       var detectableInDev = indicatorOperDevDao.getDetectableInDevByProject(subproject, delivery);
         //       indicatorOperDevDao.Dispose();
         //       return request.CreateResponse(HttpStatusCode.OK, detectableInDev);
         //   }

         //   [HttpPost]
         //   [Route("indicatorOperDev/defectsDetectableInDev/fbydevManufsystemProject")]
         //   [ResponseType(typeof(IList<DetectableInDev2>))]
         //   public HttpResponseMessage defectsDetectableInDevFbydevManufsystemProject(HttpRequestMessage request, devManufsystemProject parameters) {
         //       var indicatorOperDevDao = new IndicatorOperDevDao();
         //       var list = indicatorOperDevDao.defectsDetectableInDevFbydevManufsystemProject(parameters);
         //       indicatorOperDevDao.Dispose();
         //       return request.CreateResponse(HttpStatusCode.OK, list);
         //   }

        #endregion


        #region WrongRating

            [HttpGet]
            [Route("indicatorOperDev/defectWrongRating")]
            public List<WrongClassif> getWrongClassificationDefectRate()
            {
                string sql = @"
                    select 
	                    --'{' +
	                    --'date:''' + substring(dt_final,4,2) + '/' + substring(dt_final,7,2) + ''',' +
	                    --'devManuf:''' + 
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
	                    end as devManuf,

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
            [Route("indicatorOperDev/defectWrongRating/{dateBegin}/{dateEnd}")]
            public List<WrongClassif> getWrongClassificationDefectRateByDate(string dateBegin, string dateEnd)
            {
                string sql = @"
                    select 
	                     substring(dt_final,1,2) + '/' + substring(dt_final,4,2) + '/' + substring(dt_final,7,2) as date,

	                    case when Aud_FD_Ofensora is not null 
			                    then Aud_FD_Ofensora 
			                    else fabrica_desenvolvimento 
	                    end as devManuf,

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

        #endregion


        #region NoSolutionForecast

            [HttpGet]
            [Route("indicatorOperDev/defectsNoSolutionForecast")]
            public List<noPredictionDefects> getnoPredictionDefects()
            {
                string sql = @"
                    select
	                    --'{' +   'date: ''' + substring(dt_final,4,2) + '/' + substring(dt_final,7,2) + ''', ' +
	                    --'devManuf:''' + fabrica_desenvolvimento + ''', ' +
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
	                    fabrica_desenvolvimento as devManuf,
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
            [Route("indicatorOperDev/defectsNoSolutionForecast/{dateBegin}/{dateEnd}")]
            public List<noPredictionDefects> getnoPredictionDefectsByDate(string dateBegin, string dateEnd)
            {
                string sql = @"
                    select
	                    substring(dt_final,1,2) + '/' + substring(dt_final,4,2) + '/' + substring(dt_final,7,2) as date,
	                    fabrica_desenvolvimento as devManuf,
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

        #endregion


        // ===============================

        [HttpPost]
		[Route("indicatorOperDev/defectInsideSLA/fbydevManufsystemProject")]
        [ResponseType(typeof(IList<DefectInsideSLA>))]
        public HttpResponseMessage defectInsideSLAFbyListTestManufSystemProject(HttpRequestMessage request, devManufsystemProject parameters)
		{
			var IndicatorPerfDevDao = new IndicatorPerfDevDao();
			var list = IndicatorPerfDevDao.defectInsideSLAFbyListTestManufSystemProject(parameters);
            IndicatorPerfDevDao.Dispose();
			return request.CreateResponse(HttpStatusCode.OK, list);
		}

    }

}