select 
	substring(yearMonth, 3, 2) as month
	,substring(yearMonth, 1, 2) as year
	,yearMonth
    ,devManuf
	,system
	,subDel
    ,sum(qtyCt) as qtyCt
    ,sum(qtyDefect) as qtyDefect
    ,round(convert(float, sum(qtyDefect)) / (case when sum(qtyCt) = 0 then 1 else sum(qtyCt) end) * 100,2) as density
	
from
	(
		select
			devManuf
			,system
			,convert(varchar, cast(substring(subDel,4,8) as int)) + ' ' + convert(varchar,cast(substring(subDel,19,8) as int)) as subDel
			,yearMonth
			,count(*) as qtyCt
			,0 as qtyDefect
		from 
			(
				select 
					(case when IsNull(ct.fabrica_desenvolvimento,'') <> '' then fabrica_desenvolvimento else 'N/A' end) as devManuf
					,(case when IsNull(ct.sistema,'') <> '' then ct.sistema else 'N/A' end) as system
					,ct.subprojeto + ct.entrega as subDel
					,(
						select
							min(substring(ex.dt_execucao,7,2) + substring(ex.dt_execucao,4,2))
						from 
							alm_execucoes ex WITH (NOLOCK)
						where
							ex.subprojeto = ct.subprojeto
							and ex.entrega = ct.entrega
							and ex.ct = ct.ct
							and ex.status in ('PASSED', 'NOT COMPLETED', 'FAILED', 'BLOCKED')
							and ex.dt_execucao <> ''
					) as yearMonth
					,ct.ct
				from 
					ALM_CTs ct WITH (NOLOCK)
				where
					ct.Massa_Teste <> 'SIM'
					and ct.Status_Exec_CT not in ('CANCELLED', 'NO RUN')
					and ct.Ciclo in ('TI', 'UAT')
					and ct.subprojeto = '@subproject'
					and ct.entrega = '@delivery'
			) a1
		where 
			yearMonth is not null
		group by
			devManuf
			,system
			,subDel
			,yearMonth

		union all

		select
			devManuf
			,system
			,convert(varchar, cast(substring(subDel,4,8) as int)) + ' ' + convert(varchar,cast(substring(subDel,19,8) as int)) as subDel
			,yearMonth
			,0 as qtyCt
			,count(*) as qtyDefect
		from 
			(
				select 
					(case when IsNull(fabrica_desenvolvimento,'') <> '' then fabrica_desenvolvimento else 'N/A' end) as devManuf
					,(case when IsNull(Sistema_Defeito,'') <> '' then Sistema_Defeito else 'N/A' end) as system
					,subprojeto + entrega as subDel
					,substring(dt_final,7,2) + substring(dt_final,4,2) as yearMonth
					,defeito as defect
					,status_atual
					,origem
				from alm_defeitos d
				where 
					status_atual = 'CLOSED'
					and Origem like '%CONSTRUÇÃO%'
					and Ciclo in ('TI', 'UAT')
					and subprojeto = '@subproject'
					and entrega = '@delivery'
			) a1
		group by
			devManuf
			,system
			,subDel
			,yearMonth
	) a2
group by
	yearMonth
    ,devManuf
	,system
	,subDel
order by
	yearMonth
