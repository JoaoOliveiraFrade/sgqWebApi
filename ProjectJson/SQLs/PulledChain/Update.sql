update sgq_projects
set
    statusStrategyTestingAndContracting = '@statusStrategyTestingAndContracting',
    dtUpdateStrategyTestingAndContracting = '@dtUpdateStrategyTestingAndContracting',
	dtEndStrategyTestingAndContracting = '@dtEndStrategyTestingAndContracting',

    statusTimeline = '@statusTimeline',
    dtUpdateTimeline = '@dtUpdateTimeline',
	dtEndTimeline = '@dtEndTimeline',

    statusTestPlan = '@statusTestPlan',
    dtUpdateTestPlan = '@dtUpdateTestPlan',
	dtEndTestPlan = '@dtEndTestPlan',

    dtDeliveryTestPlan = '@dtDeliveryTestPlan',
    readyTestPlan = '@readyTestPlan',
    dtStartTestPlan = '@dtStartTestPlan'
where
    id = @id