using System;
namespace ProjectWebApi.Models
{
    public class Step
    {
        public string subproject { get; set; }
        public string delivery { get; set; }
        public Int32 id { get; set; }
        public Int32 test { get; set; }
        public Int32 order { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string expectedResult { get; set; }
        public string parameters { get; set; }
    }
}