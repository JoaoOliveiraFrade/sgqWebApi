using Classes;
using ProjectWebApi.Models;
using ProjectWebApi.Models.Project;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace ProjectWebApi.Daos
{
    public class TrgExecutionDao
    {
        private Connection connection;

        public TrgExecutionDao()
        {
            connection = new Connection(Bancos.Sgq);
        }

        public void Dispose()
        {
            connection.Dispose();
        }


        public List<Status> groupDay(Release release, IList<string> systems)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\trg\trgExecution\groupDay.sql"), Encoding.Default);
            sql = sql.Replace("@mmm", release.name.Substring(0, 3));
            sql = sql.Replace("@yyyy", release.year.ToString());
            sql = sql.Replace("@yy", release.year.ToString().Substring(release.year.ToString().Length - 2));
            if (systems.Count > 0)
            {
                sql = sql.Replace("@systemConditionCt", "substring(ct.path, CHARINDEX('" + release.year.ToString() + "', path) + 11, 30) in ('" + string.Join("','", systems) + "')");
            }
            else
            {
                sql = sql.Replace("@systemConditionCt", "substring(ct.path, CHARINDEX('" + release.year.ToString() + "', path) + 11, 30) <> ''");
            }
            List<Status> result = connection.Executar<Status>(sql);
            return result;
        }

        public StatusLastDays lastDays(Release release, IList<string> systems)
        {
            List<Status> status30LastDays = this.groupDay(release, systems);

            if (status30LastDays.Count >= 30)
                status30LastDays = status30LastDays.GetRange(0, 29);

            List<Status> status5LastDays;
            if (status30LastDays.Count > 5)
            {
                status5LastDays = status30LastDays.GetRange(0, 4);
            }
            else
            {
                status5LastDays = status30LastDays;
            }

            status30LastDays.Sort((x, y) => x.dateOrder.CompareTo(y.dateOrder));

            var statusLastDays = new StatusLastDays()
            {
                last05Days = status5LastDays,
                last30Days = status30LastDays
            };
            return statusLastDays;
        }


        public IList<Status> groupMonth(Release release, IList<string> systems)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\trg\trgExecution\groupMonth.sql"), Encoding.Default);
            sql = sql.Replace("@mmm", release.name.Substring(0, 3));
            sql = sql.Replace("@yyyy", release.year.ToString());
            sql = sql.Replace("@yy", release.year.ToString().Substring(release.year.ToString().Length - 2));
            if (systems.Count > 0)
            {
                sql = sql.Replace("@systemConditionCt", "substring(ct.path, CHARINDEX('" + release.year.ToString() + "', path) + 11, 30) in ('" + string.Join("','", systems) + "')");
            }
            else
            {
                sql = sql.Replace("@systemConditionCt", "substring(ct.path, CHARINDEX('" + release.year.ToString() + "', path) + 11, 30) <> ''");
            }
            IList<Status> result = connection.Executar<Status>(sql);
            return result;
        }


        public IList<ProductivityXDefects> productivityXDefects(Release release, IList<string> systems)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\trg\trgExecution\productivityXDefects.sql"), Encoding.Default);
            sql = sql.Replace("@mmm", release.name.Substring(0, 3));
            sql = sql.Replace("@yyyy", release.year.ToString());
            sql = sql.Replace("@yy", release.year.ToString().Substring(release.year.ToString().Length - 2));
            if (systems.Count > 0)
            {
                sql = sql.Replace("@systemConditionCt", "substring(ct.path, CHARINDEX('" + release.year.ToString() + "', path) + 11, 30) in ('" + string.Join("','", systems) + "')");
                sql = sql.Replace("@systemConditionDefect", "d.Sistema_Pasta_CT in ('" + string.Join("','", systems) + "')");
            }
            else
            {
                sql = sql.Replace("@systemConditionCt", "substring(ct.path, CHARINDEX('" + release.year.ToString() + "', path) + 11, 30) <> ''");
                sql = sql.Replace("@systemConditionDefect", "1=1");
            }
            List<ProductivityXDefects> result = connection.Executar<ProductivityXDefects>(sql);
            return result;
        }

        public IList<ProductivityXDefectsGroupWeekly> productivityXDefectsGroupWeekly(Release release, IList<string> systems)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\trg\trgExecution\productivityXDefectsGroupWeekly.sql"), Encoding.Default);
            sql = sql.Replace("@mmm", release.name.Substring(0, 3));
            sql = sql.Replace("@yyyy", release.year.ToString());
            sql = sql.Replace("@yy", release.year.ToString().Substring(release.year.ToString().Length - 2));
            if (systems.Count > 0)
            {
                sql = sql.Replace("@systemConditionCt", "substring(ct.path, CHARINDEX('" + release.year.ToString() + "', path) + 11, 30) in ('" + string.Join("','", systems) + "')");
                sql = sql.Replace("@systemConditionDefect", "d.Sistema_Pasta_CT in ('" + string.Join("','", systems) + "')");
            }
            else
            {
                sql = sql.Replace("@systemConditionCt", "substring(ct.path, CHARINDEX('" + release.year.ToString() + "', path) + 11, 30) <> ''");
                sql = sql.Replace("@systemConditionDefect", "1=1");
            }
            List<ProductivityXDefectsGroupWeekly> result = connection.Executar<ProductivityXDefectsGroupWeekly>(sql);
            return result;
        }

        public IList<GroupSystem> groupSystem(Release release, IList<string> systems) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\trg\trgExecution\groupSystem.sql"), Encoding.Default);
            sql = sql.Replace("@mmm", release.name.Substring(0, 3));
            sql = sql.Replace("@yyyy", release.year.ToString());
            sql = sql.Replace("@yy", release.year.ToString().Substring(release.year.ToString().Length - 2));
            if (systems.Count > 0) {
                sql = sql.Replace("@systemConditionCt", "substring(ct.path, CHARINDEX('" + release.year.ToString() + "', path) + 11, 30) in ('" + string.Join("','", systems) + "')");
            } else {
                sql = sql.Replace("@systemConditionCt", "substring(ct.path, CHARINDEX('" + release.year.ToString() + "', path) + 11, 30) <> ''");
            }
            List<GroupSystem> result = connection.Executar<GroupSystem>(sql);
            return result;
        }
    }
}