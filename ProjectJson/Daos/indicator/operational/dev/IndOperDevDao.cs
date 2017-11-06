using Classes;
using ProjectWebApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;

namespace ProjectWebApi.Daos
{
	public class IndOperDevDao
	{
		private Connection connection;

		public IndOperDevDao()
		{
			connection = new Connection(Bancos.Sgq);
		}

		public void Dispose()
		{
			connection.Dispose();
		}


        #region Reopened

            public IList<DefectReopened> defectReopenedFbyProject(string subproject, string delivery) {
                string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicator\operational\Dev\defectReopenedFbyProject.sql"), Encoding.Default);
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


        #region DetectableInDev

            public IList<DetectableInDev> defectsDetectableInDevFbydevManufsystemProject(DevManufSystemProject parameters)
		    {
			    string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicatorTest\defectsDetectableInDev.sql"), Encoding.Default);
			    sql = sql.Replace("@selectedTestManuf", "'" + string.Join("','", parameters.selectedDevManuf) + "'");
			    sql = sql.Replace("@selectedSystem", "'" + string.Join("','", parameters.selectedSystem) + "'");
			    sql = sql.Replace("@selectedProject", "'" + string.Join("','", parameters.selectedProject) + "'");
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