select
	substring(df.dt_final,4,2) as month
	,substring(df.dt_final,7,2) as year
	,(case when IsNull(fabrica_desenvolvimento,'') <> '' then fabrica_desenvolvimento else 'NÃO IDENTIFICADA' end) as devManuf
	,left(df.Sistema_Defeito,30) as system
	,convert(varchar, cast(substring(subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(entrega,8,8) as int)) as subprojectDelivery
	,count(*) as qtyDefect
	,sum(qtd_reopen) as qtyReopened
	,round(convert(float, sum(qtd_reopen)) / (case when count(*) <> 0 then count(*) else 1 end) * 100, 2) as percReopened
from 
	ALM_Defeitos df WITH (NOLOCK)
where
	df.Status_Atual = 'CLOSED'
	and df.Ciclo in ('TI', 'UAT')
	and df.dt_final <> ''
	and df.subprojeto = '@subproject'
	and df.entrega = '@delivery'
group by
	substring(df.dt_final,4,2)
	,substring(df.dt_final,7,2)
	,(case when IsNull(fabrica_desenvolvimento,'') <> '' then fabrica_desenvolvimento else 'NÃO IDENTIFICADA' end)
	,left(df.Sistema_Defeito,30)
	,convert(varchar, cast(substring(subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(entrega,8,8) as int))
order by
	2,1,3,4,5