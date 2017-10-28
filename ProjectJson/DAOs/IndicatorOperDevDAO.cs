using Classes;
using ProjectWebApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;

namespace ProjectWebApi.DAOs
{
	public class IndicatorOperDevDAO
	{
		private Connection connection;

		public IndicatorOperDevDAO()
		{
			connection = new Connection(Bancos.Sgq);
		}

		public void Dispose()
		{
			connection.Dispose();
		}

        #region Density

            public IList<DefectDensity> defectDensityFbyProject(string subproject, string delivery) {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicatorOperDev\defectDensityFbyProject.sql"), Encoding.Default);
                sql = sql.Replace("@subproject", subproject);
                sql = sql.Replace("@delivery", delivery);
                var result = connection.Executar<DefectDensity>(sql);
                return result;
            }

            public IList<DefectDensity> defectDensityFbyListDevManufSystemProject(ListDevManufSystemProject parameters) {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicatorOperDev\defectDensityFbyListDevManufSystemProject.sql"), Encoding.Default);
                sql = sql.Replace("@selectedDevManufs", "'" + string.Join("','", parameters.selectedDevManufs) + "'");
                sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
                sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
                var result = connection.Executar<DefectDensity>(sql);
                return result;
            }

        #endregion

        #region AverangeTime

            public IList<DefectAverangeTime> defectAverangeTimeFbyProject(string subproject, string delivery) {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicatorOperDev\defectAverangeTimeFbyProject.sql"), Encoding.Default);
                sql = sql.Replace("@subproject", subproject);
                sql = sql.Replace("@delivery", delivery);
                var result = connection.Executar<DefectAverangeTime>(sql);
                return result;
            }

            public IList<DefectAverangeTime> defectAverangeTimeFbyProjectAndListIteration(ProjectAndListIteration parameters) {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicatorOperDev\defectAverangeTimeFbyProjectAndListIteration.sql"), Encoding.Default);
                sql = sql.Replace("@subproject", parameters.subproject);
                sql = sql.Replace("@delivery", parameters.delivery);
                sql = sql.Replace("@iterations", "'" + string.Join("','", parameters.iterations.ToArray()) + "'");
                var result = connection.Executar<DefectAverangeTime>(sql);
                return result;
            }

            public IList<DefectAverangeTime> defectAverangeTimeFbyListDevManufSystemProject(ListDevManufSystemProject parameters) {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicatorOperDev\defectAverangeTimeFbyListDevManufSystemProject.sql"), Encoding.Default);
                sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedDevManufs) + "'");
                sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
                sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
                var result = connection.Executar<DefectAverangeTime>(sql);
                return result;
            }

            public IList<DefectAverangeTime> defectAverangeTimeFbyListDevManufSystemProjectIteration(ListDevManufSystemProjectIteration parameters) {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicatorOperDev\defectAverangeTimeFbyListDevManufSystemProjectIteration.sql"), Encoding.Default);
                sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedDevManufs) + "'");
                sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
                sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
                sql = sql.Replace("@iterations", "'" + string.Join("','", parameters.iterations.ToArray()) + "'");

                var result = connection.Executar<DefectAverangeTime>(sql);
                return result;
            }

            public DefectAverangeTime getDefectsAverageTimeByProjectIterations(string subproject, string delivery, string severity, List<string> iterations) {
                string sql = @"
                    select 
	                    count(*) as qtyDefects,
	                    round(sum(Aging),2) as qtyHours,
	                    round(sum(Aging) / count(*),2) as AverageHours
                    from 
					                alm_cts cts 
					                inner join alm_defeitos df
						                on df.subprojeto = cts.subprojeto and
                                           df.entrega = cts.entrega and
                                           df.ct = cts.ct
                    where 
	                    cts.subprojeto = '@subproject' and
	                    cts.entrega = '@delivery' and
                        cts.iterations in (@iterations) and
                        df.severidade = '@severity' and
	                    df.Ciclo in ('TI', 'UAT') and
	                    df.status_atual = 'CLOSED' and
	                    df.dt_final <> ''
                    ";

                sql = sql.Replace("@subproject", subproject);
                sql = sql.Replace("@delivery", delivery);
                sql = sql.Replace("@severity", severity);
                sql = sql.Replace("@iterations", "'" + string.Join("','", iterations.ToArray()) + "'");

                var list = connection.Executar<DefectAverangeTime>(sql);

                return list[0];
            }

        #endregion

        #region Reopened

            public IList<DefectReopened> defectReopenedFbyProject(string subproject, string delivery) {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicatorOperDev\defectReopenedFbyProject.sql"), Encoding.Default);
                sql = sql.Replace("@subproject", subproject);
                sql = sql.Replace("@delivery", delivery);
                var result = connection.Executar<DefectReopened>(sql);
                return result;
            }

            public DefectReopened getDefectReopenedByProjectIterations(string subproject, string delivery, List<string> iterations) {
                string sql = @"
                    select
	                        count(*) as qtyTotal,
	                        sum(qtd_reopen) as qtyReopened,
	                        round(convert(float,sum(qtd_reopen)) / count(*) * 100,2) as percentReopened,
	                        5 as percentReference,
	                        round(convert(float,count(*) * 0.05),2) as qtyReference
                    from 
					                    alm_cts cts 
					                    inner join alm_defeitos df
						                    on df.subprojeto = cts.subprojeto and
                                               df.entrega = cts.entrega and
                                               df.ct = cts.ct
                    where 
	                        cts.subprojeto = '@subproject' and
	                        cts.entrega = '@delivery' and
                            cts.iterations in (@iterations) and
	                        df.Ciclo in ('TI', 'UAT') and
	                        df.status_atual = 'CLOSED' and
	                        df.dt_final <> ''
                    ";

                sql = sql.Replace("@subproject", subproject);
                sql = sql.Replace("@delivery", delivery);
                sql = sql.Replace("@iterations", "'" + string.Join("','", iterations.ToArray()) + "'");

                var list = connection.Executar<DefectReopened>(sql);

                return list[0];
            }

        #endregion

        #region TSInTI

            public IList<DefectOfTSInTI> defectOfTSInTIFbyProject(string subproject, string delivery) {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicatorOperDev\defectOfTSInTIFbyProject.sql"), Encoding.Default);
                sql = sql.Replace("@subproject", subproject);
                sql = sql.Replace("@delivery", delivery);
                var result = connection.Executar<DefectOfTSInTI>(sql);
                return result;
            }

            public IList<defectOfTSInTI> defectOfTSInTI_fbyListDevManufSystemProject(ListDevManufSystemProject parameters) {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicatorOperDev\defectOfTSInTI_fbyListDevManufSystemProject.sql"), Encoding.Default);
                sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedDevManufs) + "'");
                sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
                sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
                var list = connection.Executar<defectOfTSInTI>(sql);
                return list;
            }

        #endregion

        #region DetectableInDev

            public IList<DetectableInDev> defectsDetectableInDevFbyListDevManufSystemProject(ListDevManufSystemProject parameters)
		    {
			    string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicatorTest\defectsDetectableInDev.sql"), Encoding.Default);
			    sql = sql.Replace("@selectedTestManufs", "'" + string.Join("','", parameters.selectedDevManufs) + "'");
			    sql = sql.Replace("@selectedSystems", "'" + string.Join("','", parameters.selectedSystems) + "'");
			    sql = sql.Replace("@selectedProjects", "'" + string.Join("','", parameters.selectedProjects) + "'");
			    var list = connection.Executar<DetectableInDev>(sql);
			    return list;
		    }

            public DetectableInDev getDetectableInDevByProject(string subproject, string delivery)
            {
                string sql = @"
                    select 
	                    count(*) as qtyTotal,
	                    sum(case when Erro_Detectavel_Em_Desenvolvimento = 'SIM' then 1 else 0 end) as qtyDetectableInDev,
                        round(convert(float,sum(case when Erro_Detectavel_Em_Desenvolvimento = 'SIM' then 1 else 0 end)) / count(*) * 100,2) as percentDetectableInDev,
	                    5 as percentReference,
	                    round(convert(float,count(*) * 0.05),2) as qtyReference
                    from 
	                    alm_defeitos 
                    where 
	                    subprojeto = '@subproject' and
	                    entrega = '@delivery' and
	                    ciclo in ('TI', 'UAT') and
	                    status_atual = 'CLOSED' and 
	                    dt_final <> ''
                    ";

                sql = sql.Replace("@subproject", subproject);
                sql = sql.Replace("@delivery", delivery);

                var list = connection.Executar<DetectableInDev>(sql);

                return list[0];
            }

        #endregion
    }
}