update sgq_projects
set
    statusStrategyTestingAndContracting = @statusStrategyTestingAndContracting,
    dtUpdateStrategyTestingAndContracting = @dtUpdateStrategyTestingAndContracting,

    statusTimeline = @statusTimeline,
    dtUpdateTimeLine = @dtUpdateTimeLine,

    statusTestPlan = @statusTestPlan,
    dtUpdateTestPlan = @dtUpdateTestPlan,

    dtDeliveryTestPlan = @dtDeliveryTestPlan,
    readyTestPlan = @readyTestPlan,
    dtStartTestPlan = @dtStartTestPlan
where
    id = @id