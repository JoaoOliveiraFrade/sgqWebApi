using Classes;
using ProjectWebApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;

namespace ProjectWebApi.Daos.Ind.Oper.Dev {
    public class DefectReopenedDao
    {
        private Connection connection;

        public DefectReopenedDao() {
            connection = new Connection(Bancos.Sgq);
        }

        public void Dispose() {
            connection.Dispose();
        }

        public IList<DefectReopened> defectReopenedFbyProject(string subproject, string delivery)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicator\operational\Dev\defectReopened\dataFbyProject.sql"), Encoding.Default);
            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);
            var result = connection.Executar<DefectReopened>(sql);
            return result;
        }

        public DefectReopened getDefectReopenedByProjectIterations(string subproject, string delivery, List<string> iterations)
        {
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

    }
}