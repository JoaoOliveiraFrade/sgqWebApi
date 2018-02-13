select 
	id
	,name
    ,type
	,executiveSummary

	,case 
		when 'VERMELHO' in (
			select p.trafficLight
			from SGQ_ProjectsXGroupers pg
				left join sgq_projects p
				    on p.subproject = pg.subproject
			where pg.Grouper = g.id
		) then 'VERMELHO'

		when 'AMARELO' in (
			select p.trafficLight
			from SGQ_ProjectsXGroupers pg
				left join sgq_projects p
				    on p.subproject = pg.subproject
			where pg.Grouper = g.id
		) then 'AMARELO'

		when 'VERDE' in (
			select p.trafficLight
			from SGQ_ProjectsXGroupers pg
				left join sgq_projects p
				    on p.subproject = pg.subproject
			where pg.Grouper = g.id
		) then 'VERDE'
	end as trafficLight

	,startTiUat
	,endTiUat
	,startTRG
	,endTRG
	,startStabilization
	,endStabilization
from	 
	SGQ_Groupers g
where 
	g.id in (select distinct grouper from SGQ_ProjectsXGroupers)
order by 
	name