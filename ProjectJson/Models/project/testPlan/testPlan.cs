using System;
namespace ProjectWebApi.Models.Project
{
    public class testPlan
    {
        public string cycle { get; set; }
        public Int32 scenario { get; set; }
        public Int32 testCase { get; set; }
        public string iteration { get; set; }
        public string macrocenary { get; set; }
        public string system { get; set; }
        public string uat { get; set; }
        public string prerequisite { get; set; }
        public string successorTestCase { get; set; }
        public string name { get; set; }
        public string functionalDetailing { get; set; }
        public string description { get; set; }
    }
}