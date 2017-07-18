using Classes;
using ProjectWebApi.Models.TestManuf;
using System.Collections.Generic;


namespace ProjectWebApi.DAOs
{
    public class TestManufDAO
    {
        private Connection _connection;

        public TestManufDAO()
        {
            _connection = new Connection(Bancos.Sgq);
        }

        public void Dispose()
        {
            _connection.Dispose();
        }

        public IList<TestManuf> getAll()
        {
            var list = _connection.Executar<TestManuf>("sp_test_manufacturers");
            return list;
       }
    }
}
