using Classes;
using ProjectWebApi.Models;
using ProjectWebApi.Models.Project;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace ProjectWebApi.Daos {
    public class TrgDefectDao {
        private Connection connection;

        public TrgDefectDao() {
            connection = new Connection(Bancos.Sgq);
        }

        public void Dispose() {
            connection.Dispose();
        }

        public IList<Defect> DefectsOpen(Release release, IList<string> systems) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\trg\trgDefect\defectOpen.sql"), Encoding.Default);
            sql = sql.Replace("@mmm", release.name.Substring(0, 3));
            sql = sql.Replace("@yyyy", release.year.ToString());
            sql = sql.Replace("@yy", release.year.ToString().Substring(release.year.ToString().Length - 2));
            if (systems.Count > 0) {
                sql = sql.Replace("@systemConditionDefect", "d.Sistema_Pasta_CT in ('" + string.Join("','", systems) + "')");
            } else {
                sql = sql.Replace("@systemConditionDefect", "1=1");
            }
            return connection.Executar<Defect>(sql);
        }


        public List<DefectStatus> DefectStatus(Release release, IList<string> systems) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\trg\trgDefect\defectStatus.sql"), Encoding.Default);
            sql = sql.Replace("@mmm", release.name.Substring(0, 3));
            sql = sql.Replace("@yyyy", release.year.ToString());
            sql = sql.Replace("@yy", release.year.ToString().Substring(release.year.ToString().Length - 2));
            if (systems.Count > 0) {
                sql = sql.Replace("@systemConditionDefect", "d.Sistema_Pasta_CT in ('" + string.Join("','", systems) + "')");
            } else {
                sql = sql.Replace("@systemConditionDefect", "1=1");
            }
            return connection.Executar<DefectStatus>(sql);
        }

        public List<DefectStatus> defectGroupOrigin(Release release, IList<string> systems) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\trg\trgDefect\defectGroupOrigin.sql"), Encoding.Default);
            sql = sql.Replace("@mmm", release.name.Substring(0, 3));
            sql = sql.Replace("@yyyy", release.year.ToString());
            sql = sql.Replace("@yy", release.year.ToString().Substring(release.year.ToString().Length - 2));
            if (systems.Count > 0) {
                sql = sql.Replace("@systemConditionDefect", "d.Sistema_Pasta_CT in ('" + string.Join("','", systems) + "')");
            } else {
                sql = sql.Replace("@systemConditionDefect", "1=1");
            }
            return connection.Executar<DefectStatus>(sql);
        }

        public List<CtImpactedXDefects> ctImpactedXDefects(Release release, IList<string> systems) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\trg\trgDefect\ctImpactedXDefects.sql"), Encoding.Default);
            sql = sql.Replace("@mmm", release.name.Substring(0, 3));
            sql = sql.Replace("@yyyy", release.year.ToString());
            sql = sql.Replace("@yy", release.year.ToString().Substring(release.year.ToString().Length - 2));
            if (systems.Count > 0) {
                sql = sql.Replace("@systemConditionDefect", "d.Sistema_Pasta_CT in ('" + string.Join("','", systems) + "')");
            } else {
                sql = sql.Replace("@systemConditionDefect", "1=1");
            }
            return connection.Executar<CtImpactedXDefects>(sql);
        }
    }
}