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
    public class IndOperDevDefectAverangeTimeDao {
        private Connection connection;

        public IndOperDevDefectAverangeTimeDao() {
            connection = new Connection(Bancos.Sgq);
        }

        public void Dispose() {
            connection.Dispose();
        }

        public IList<DefectAverangeTime> data(DevManufSystemProject parameters) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicator\operational\Dev\defectAverangeTime\data.sql"), Encoding.Default);
            sql = sql.Replace("@selectedDevManuf", "'" + string.Join("','", parameters.selectedDevManuf) + "'");
            sql = sql.Replace("@selectedSystem", "'" + string.Join("','", parameters.selectedSystem) + "'");
            sql = sql.Replace("@selectedProject", "'" + string.Join("','", parameters.selectedProject) + "'");
            var result = connection.Executar<DefectAverangeTime>(sql);
            return result;
        }

        public IList<DefectAverangeTime> dataFbyProject(string subproject, string delivery) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicator\operational\Dev\defectAverangeTime\dataFbyProject.sql"), Encoding.Default);
            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);
            var result = connection.Executar<DefectAverangeTime>(sql);
            return result;
        }

        public IList<DefectAverangeTime> defectAverangeTimeFbyProjectAndListIteration(ProjectAndListIteration parameters) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicator\operational\Dev\defectAverangeTimeFbyProjectAndListIteration.sql"), Encoding.Default);
            sql = sql.Replace("@subproject", parameters.subproject);
            sql = sql.Replace("@delivery", parameters.delivery);
            sql = sql.Replace("@iterations", "'" + string.Join("','", parameters.iterations.ToArray()) + "'");
            var result = connection.Executar<DefectAverangeTime>(sql);
            return result;
        }

        public IList<DefectAverangeTime> defectAverangeTimeFbydevManufsystemProjectIteration(DevManufSystemProjectIteration parameters) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\indicator\operational\Dev\defectAverangeTimeFbydevManufsystemProjectIteration.sql"), Encoding.Default);
            sql = sql.Replace("@selectedTestManuf", "'" + string.Join("','", parameters.selectedDevManuf) + "'");
            sql = sql.Replace("@selectedSystem", "'" + string.Join("','", parameters.selectedSystem) + "'");
            sql = sql.Replace("@selectedProject", "'" + string.Join("','", parameters.selectedProject) + "'");
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
    }
}