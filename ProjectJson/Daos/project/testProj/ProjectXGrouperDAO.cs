using Classes;
using ProjectWebApi.Models;
using System;
using System.Collections;
using System.Collections.Generic;


namespace ProjectWebApi.Daos
{
    public class ProjectXGrouperDao
    {
        private Connection connection;

        public ProjectXGrouperDao()
        {
            connection = new Connection(Bancos.Sgq);
        }

        public void Dispose()
        {
            connection.Dispose();
        }

        public IList<ProjectXGrouper> Data()
        {
            string sql = @"select * from SGQ_ProjectsXGroupers order by project";

            var listGroups = connection.Executar<ProjectXGrouper>(sql);

            return listGroups;
        }
        public IList<ProjectXGrouper> GetByProject(string project)
        {
            string sql = @"select * from SGQ_ProjectsXGroupers where project = @project order by grouper";
            sql = sql.Replace("@project", project);

            var listGroups = connection.Executar<ProjectXGrouper>(sql);

            return listGroups;
        }
        public IList<ProjectXGrouper> GetByProject(string subproject, string delivery)
        {
            string sql = @"
                select * 
                from SGQ_ProjectsXGroupers 
                where 
                    subproject = '@subproject' and  
                    delivery = '@delivery'  
                order by grouper";

            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);

            var listGroups = connection.Executar<ProjectXGrouper>(sql);

            return listGroups;
        }
        public IList<ProjectXGrouper> GetByGroup(string group)
        {
            string sql = @"select * from SGQ_ProjectsXGroupers where grouper = @grouper order by project";
            sql = sql.Replace("@grouper", group);

            var listGroups = connection.Executar<ProjectXGrouper>(sql);

            return listGroups;
        }
        public void Delete(string GrouperId, string projectId)
        {
            string sql = @"delete SGQ_ProjectsXGroupers where grouper = @grouper and project = @project";
            sql = sql.Replace("@grouper", GrouperId);
            sql = sql.Replace("@project", projectId);
            connection.Executar(sql);
        }
        public void Create(string GrouperId, string projectId, string subproject, string delivery)
        {
            string sql = @"insert into SGQ_ProjectsXGroupers (project, subproject, delivery, grouper) values (@project, '@subproject', '@delivery', @grouper)";

            sql = sql.Replace("@project", projectId);
            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);
            sql = sql.Replace("@grouper", GrouperId);
            connection.Executar(sql);
        }

        //public ProjectXGrouper getOne(string id)
        //{
        //    string sql = @"select * from SGQ_ProjectsXGroupers where id = " + id;

        //    var list = connection.Executar<ProjectXGrouper>(sql);

        //    return list[0];
        //}

    }
}