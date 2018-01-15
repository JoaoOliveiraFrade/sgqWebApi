using System;
namespace ProjectWebApi.Models.Project
{
    public class simpProject {
        public Int32 id { get; set; }
        public string subproject { get; set; }
        public string delivery { get; set; }
        public string subDel { get; set; }
        public string name { get; set; }
        public string classification { get; set; }
        public string release { get; set; }
        public string state { get; set; }
        public string trafficLight { get; set; }
    }
}