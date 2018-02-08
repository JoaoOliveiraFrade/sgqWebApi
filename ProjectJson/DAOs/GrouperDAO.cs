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
    public class GrouperDao
    {
        private Connection connection;

        public GrouperDao()
        {
            connection = new Connection(Bancos.Sgq);
        }

        public void Dispose()
        {
            connection.Dispose();
        }

        public IList<Grouper> LoadData()
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\grouper\loadData.sql"), Encoding.Default);
            return connection.Executar<Grouper>(sql);
        }

        public Grouper LoadById(string id)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\grouper\loadById.sql"), Encoding.Default);
            sql = sql.Replace("@id", id);
            return connection.Executar<Grouper>(sql)[0];
        }

        public Grouper getByName(string name)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\grouper\loadByName.sql"), Encoding.Default);
            sql = sql.Replace("@name", name);
            return connection.Executar<Grouper>(sql)[0];
        }

        public Grouper Create(Grouper item)
        {
            var connection = new Connection(Bancos.Sgq);

            if (item.name == null)
                item.name = "";

            if (item.executiveSummary == null)
                item.executiveSummary = "";

            bool resultado = false;
            if (item == null) throw new ArgumentNullException("item");
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection.connection;
                command.CommandText = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\grouper\create.sql"), Encoding.Default);
                command.Parameters.AddWithValue("name", item.name);
                command.Parameters.AddWithValue("type", item.type);
                command.Parameters.AddWithValue("executiveSummary", item.executiveSummary);
                command.Parameters.AddWithValue("startTiUat", item.startTiUat);
                command.Parameters.AddWithValue("endTiUat", item.endTiUat);
                command.Parameters.AddWithValue("startTRG", item.startTRG);
                command.Parameters.AddWithValue("endTRG", item.endTRG);
                command.Parameters.AddWithValue("startStabilization", item.startStabilization);
                command.Parameters.AddWithValue("endStabilization", item.endStabilization);

                int i = command.ExecuteNonQuery();
                resultado = i > 0;
            }

            var createItem = this.getByName(item.name);
            connection.Dispose();
            return createItem;
        }

        public void Update(string id, Grouper item)
        {
            var connection = new Connection(Bancos.Sgq);

            if (item.name == null)
                item.name = "";

            if (item.executiveSummary == null)
                item.executiveSummary = "";

            bool resultado = false;
            if (item == null) throw new ArgumentNullException("item");
            if (id == "0") throw new ArgumentNullException("id");
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection.connection;
                command.CommandText = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\grouper\update.sql"), Encoding.Default);
                command.Parameters.AddWithValue("id", item.id);
                command.Parameters.AddWithValue("name", item.name);
                command.Parameters.AddWithValue("type", item.type);
                command.Parameters.AddWithValue("executiveSummary", item.executiveSummary);
                command.Parameters.AddWithValue("startTiUat", item.startTiUat);
                command.Parameters.AddWithValue("endTiUat", item.endTiUat);
                command.Parameters.AddWithValue("startTRG", item.startTRG);
                command.Parameters.AddWithValue("endTRG", item.endTRG);
                command.Parameters.AddWithValue("startStabilization", item.startStabilization);
                command.Parameters.AddWithValue("endStabilization", item.endStabilization);

                int i = command.ExecuteNonQuery();
                resultado = i > 0;
            }
            connection.Dispose();
        }

        public void Delete(int id)
        {
            var connection = new Connection(Bancos.Sgq);

            bool resultado = false;
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connection.connection;
                command.CommandText = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\grouper\delete.sql"), Encoding.Default);
                command.Parameters.AddWithValue("id", id);

                int i = command.ExecuteNonQuery();
                resultado = i > 0;
            }
            connection.Dispose();
        }
    }
}