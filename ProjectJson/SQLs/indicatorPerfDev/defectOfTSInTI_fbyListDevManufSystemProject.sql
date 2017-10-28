select
	month
	,year
	,devManuf
	,system
	,subprojectDelivery
	,qtyDetectableInTS
	,qtyTotal
	,round(convert(float, qtyDetectableInTS) / (case when qtyTotal <> 0 then qtyTotal else 1 end) * 100, 2) as percDetectableInTS
from 
	(
		select
			substring(dt_final,4,2) as month
			,substring(dt_final,7,2) as year
			,(case when IsNull(fabrica_desenvolvimento,'') <> '' then fabrica_desenvolvimento else 'NÃO IDENTIFICADA' end) as devManuf
			,sistema_defeito as system
			,convert(varchar, cast(substring(subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(entrega,8,8) as int)) as subprojectDelivery
			,sum(case when Erro_Detectavel_Em_Desenvolvimento = 'SIM' then 1 else 0 end) as qtyDetectableInTS
			,count(*) as qtyTotal
		from 
			alm_defeitos WITH (NOLOCK)
		where
			ciclo in ('TI', 'UAT')
			and status_atual = 'CLOSED'
			and dt_final <> ''
			and fabrica_desenvolvimento in (@selectedTestManufs)
			and sistema_defeito in (@selectedSystems)
			and subprojeto + entrega collate Latin1_General_CI_AS in (@selectedProjects)
		group by
			substring(dt_final,7,2)
			,substring(dt_final,4,2)
			,(case when IsNull(fabrica_desenvolvimento,'') <> '' then fabrica_desenvolvimento else 'NÃO IDENTIFICADA' end)
			,sistema_defeito
			,convert(varchar, cast(substring(subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(entrega,8,8) as int))
	) aux1
order by
	2, 1, 3, 4
