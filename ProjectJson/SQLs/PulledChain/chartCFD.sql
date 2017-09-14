select 
	data as date,
	atividade as activity,
	[Backlog Not Ready] as backlogNotReady,
	[Backlog Ready] as backlogReady,
	[On Going] as onGoing,
	[Finalizado] as finished
from 
	sgq_pulledChainHistoric 
order by 
	convert(datetime, data, 5)
