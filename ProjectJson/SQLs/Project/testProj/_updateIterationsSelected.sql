update sgq_projects
set
    IterationsSelected = @iterations
where
	subproject = @subproject
	and delivery = @delivery
