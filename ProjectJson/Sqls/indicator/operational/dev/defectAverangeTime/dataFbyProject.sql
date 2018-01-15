select 
	substring(df.dt_final,4,2) as month
	,substring(df.dt_final,7,2) as year
	,(case when IsNull(df.fabrica_desenvolvimento,'') <> '' then df.fabrica_desenvolvimento else 'N/A' end) as devManuf
	,(case when IsNull(df.sistema_defeito,'') <> '' then df.sistema_defeito else 'N/A' end) as system
	,df.subprojeto + df.entrega as subDel
	,substring(df.severidade,3,10) as severity
	,count(*) as qtyDefect
	,round(sum(df.Aging),2) as qtyHour
	,round(sum(df.Aging) / count(*),2) as averangeHour
from 
	alm_defeitos df
where 
	df.ciclo in ('TI', 'UAT')
	and df.status_atual = 'CLOSED'
	and df.dt_final <> ''
	and df.subprojeto = '@subproject'
	and df.entrega = '@delivery'
group by
	substring(df.dt_final,4,2)
	,substring(df.dt_final,7,2)
	,(case when IsNull(df.fabrica_desenvolvimento,'') <> '' then df.fabrica_desenvolvimento else 'N/A' end)
	,(case when IsNull(df.sistema_defeito,'') <> '' then df.sistema_defeito else 'N/A' end)
	,df.subprojeto + df.entrega
	,substring(df.severidade,3,10)
order by 
	2, 1, 3, 4, 5