using System;
namespace ProjectWebApi.Models.Project
{
    public class PulledChain
    {
        public int id { get; set; }
        public string subproject { get; set; }
        public string delivery { get; set; }
        public string subprojectDelivery { get; set; }
        public string name { get; set; }

        public string releaseClarity { get; set; }
        public string nextRelease { get; set; }

        public int priorityGlobal { get; set; }
        public string state { get; set; }
        public string category { get; set; }
        public string productiveChain { get; set; }
        public string UN { get; set; }
        public string Typification { get; set; }
        public string workFrontState { get; set; }
        public string topic { get; set; }
        public string RT { get; set; }
        public string deliveryState { get; set; }
        public string statusCategoryORL { get; set; }

        public string readyStrategyTestingAndContracting { get; set; }
        public string statusStrategyTestingAndContracting { get; set; }
        public string dtUpdateStrategyTestingAndContracting { get; set; }
        public string dtStartStrategyTestingAndContracting { get; set; }
        public string dtEndStrategyTestingAndContracting { get; set; }
        public int agingStrategyTestingAndContracting { get; set; }
        public string agingStrategyTestingAndContractingFormated { get; set; }
        
        public string readyTimeline { get; set; }
        public string statusTimeline { get; set; }
        public string dtUpdateTimeLine { get; set; }
        public string dtStartTimeLine { get; set; }
        public string dtEndTimeLine { get; set; }
        public int agingTimeline { get; set; }
        public string agingTimelineFormated { get; set; }

        public string readyTestPlan { get; set; }
        public string statusTestPlan { get; set; }
        public string dtUpdateTestPlan { get; set; }
        public string dtStartTestPlan { get; set; }
        public string dtEndTestPlan { get; set; }
        public int agingTestPlan { get; set; }
        public string agingTestPlanFormated { get; set; }

        public string dtDeliveryTestPlan { get; set; }
    }
}
