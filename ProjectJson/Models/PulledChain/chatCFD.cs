using System;
namespace ProjectWebApi.Models
{
    public class chartCFD
    {
        public string date { get; set; }
        public string activity { get; set; }
        public int backlogNotReady { get; set; }
        public int backlogReady { get; set; }
        public int onGoing { get; set; }
        public int finished { get; set; }
    }
}
