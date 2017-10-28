select 
	substring(df.dt_final,4,2) as month
	,substring(df.dt_final,7,2) as year
	,df.Fabrica_Desenvolvimento as devManuf
	,left(df.Sistema_Defeito,30) as system
	,df.subprojeto + df.entrega as subprojectDelivery
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
	and devManuf in (@selectedDevManufs)
	and system in (@selectedSystems)
	and subprojectDelivery collate Latin1_General_CI_AS in (@selectedProjects)
group by
	substring(df.dt_final,4,2)
	,substring(df.dt_final,7,2)
	,df.Fabrica_Desenvolvimento
	,left(df.Sistema_Defeito,30)
	,df.subprojeto + df.entrega
	,substring(df.severidade,3,10)
order by 
	2, 1, 3, 4, 5