update sgq_projects
set
    IterationsActive = @iterations
where
	subproject = @subproject
	and delivery = @delivery
