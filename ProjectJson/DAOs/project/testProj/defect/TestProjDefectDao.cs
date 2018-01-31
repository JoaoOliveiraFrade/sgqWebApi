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
    public class TestProjDefectDao {
        private Connection connection;

        public TestProjDefectDao() {
            connection = new Connection(Bancos.Sgq);
        }

        public void Dispose() {
            connection.Dispose();
        }

        public List<DefectStatus> DefectStatus(SubprojectDelivery subprojectDelivery) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\testProj\testProjDefect\defectStatus.sql"), Encoding.Default);
            sql = sql.Replace("@subproject", subprojectDelivery.subproject);
            sql = sql.Replace("@delivery", subprojectDelivery.delivery);
            return connection.Executar<DefectStatus>(sql);
        }

        public List<DefectStatus> DefectGroupOrigin(SubprojectDelivery subprojectDelivery) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\testProj\testProjDefect\defectGroupOrigin.sql"), Encoding.Default);
            sql = sql.Replace("@subproject", subprojectDelivery.subproject);
            sql = sql.Replace("@delivery", subprojectDelivery.delivery);
            return connection.Executar<DefectStatus>(sql);
        }

        public List<CtImpactedXDefects> CtImpactedXDefects(SubprojectDelivery subprojectDelivery) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\testProj\testProjDefect\ctImpactedXDefects.sql"), Encoding.Default);
            sql = sql.Replace("@subproject", subprojectDelivery.subproject);
            sql = sql.Replace("@delivery", subprojectDelivery.delivery);
            return connection.Executar<CtImpactedXDefects>(sql);
        }

        public IList<Defect> DefectsOpen(SubprojectDelivery subprojectDelivery) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\testProj\testProjDefect\defectOpen.sql"), Encoding.Default);
            sql = sql.Replace("@subproject", subprojectDelivery.subproject);
            sql = sql.Replace("@delivery", subprojectDelivery.delivery);
            return connection.Executar<Defect>(sql);
        }
    }
}