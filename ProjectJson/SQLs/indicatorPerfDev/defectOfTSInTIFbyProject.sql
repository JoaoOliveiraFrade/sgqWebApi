select
	substring(dt_final,4,2) as month
	,substring(dt_final,7,2) as year
	,(case when IsNull(fabrica_desenvolvimento,'') <> '' then fabrica_desenvolvimento else 'NÃO IDENTIFICADA' end) as devManuf
	,sistema_defeito as system
	,convert(varchar, cast(substring(subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(entrega,8,8) as int)) as subprojectDelivery
	,sum(case when Erro_Detectavel_Em_Desenvolvimento = 'SIM' then 1 else 0 end) as qtyOfTSInTI
	,count(*) as qtyDefect
	,round(convert(float, sum(case when Erro_Detectavel_Em_Desenvolvimento = 'SIM' then 1 else 0 end)) / (case when count(*) <> 0 then count(*) else 1 end) * 100, 2) as percOfTSInTI
from 
	alm_defeitos WITH (NOLOCK)
where
	status_atual = 'CLOSED'
	and dt_final <> ''
	and ciclo in ('TI', 'UAT')
	and subprojeto = '@subproject'
	and entrega = '@delivery'
group by
	substring(dt_final,7,2)
	,substring(dt_final,4,2)
	,(case when IsNull(fabrica_desenvolvimento,'') <> '' then fabrica_desenvolvimento else 'NÃO IDENTIFICADA' end)
	,sistema_defeito
	,convert(varchar, cast(substring(subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(entrega,8,8) as int))
order by
	2, 1, 3, 4
