select 
	data as date,
	sum([Backlog Not Ready]) as backlogNotReady,
	sum([Backlog Ready]) as backlogReady,
	sum([On Going]) as onGoing,
	sum([Finalizado]) as finished
from 
	sgq_pulledChainHistoric 
group by
	data
order by 
	convert(datetime, data, 5)

--select 
--	data as date,
--	atividade as activity,
--	[Backlog Not Ready] as backlogNotReady,
--	[Backlog Ready] as backlogReady,
--	[On Going] as onGoing,
--	[Finalizado] as finished
--from 
--	sgq_pulledChainHistoric 
--order by 
--	convert(datetime, data, 5)
