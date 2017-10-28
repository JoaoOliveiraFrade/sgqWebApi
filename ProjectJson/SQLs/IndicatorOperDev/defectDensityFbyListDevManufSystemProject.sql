select
	month
	,year
    ,devManuf
	,system
	,convert(varchar, cast(substring(subprojectDelivery,4,8) as int)) + ' ' + convert(varchar,cast(substring(subprojectDelivery,19,8) as int)) as subprojectDelivery
    ,sum(qtyDefect) as qtyDefect
    ,count(*) as qtyCt
    ,round(convert(float, sum(qtyDefect)) / (case when count(*) = 0 then 1 else count(*) end) * 100,2) as density
from
	(
		select 
			substring(yearMonth, 3, 2) as month
			,substring(yearMonth, 1, 2) as year
			,devManuf
			,system
			,subprojeto + entrega as subprojectDelivery
			,(select count(*)
				from alm_defeitos df
				where df.subprojeto = aux1.subprojeto and
						df.entrega = aux1.entrega and
						df.ct = aux1.ct and
						df.status_atual = 'CLOSED' and
						df.Origem like '%CONSTRUÇÃO%' and
						(df.Ciclo like '%TI%' or df.Ciclo like '%UAT%')
			) as qtyDefect
		from
			(
				select 
					ct.fabrica_desenvolvimento as devManuf
					,left(ct.sistema,30) as system
					,ct.subprojeto
					,ct.entrega
					,ct.ct
					,(select
						min(substring(ex.dt_execucao,7,2) + substring(ex.dt_execucao,4,2))
					from 
						alm_execucoes ex WITH (NOLOCK)
					where
						ex.subprojeto = ct.subprojeto
						and ex.entrega = ct.entrega
						and ex.ct = ct.ct
						and ct.fabrica_desenvolvimento is not null
						and ex.status not in ('CANCELLED', 'NO RUN')
						and ex.dt_execucao <> ''
					) as yearMonth
				from 
					ALM_CTs ct WITH (NOLOCK)
				where
					ct.Status_Exec_CT not in ('CANCELLED', 'NO RUN')
					and ct.fabrica_desenvolvimento is not null
					and ct.Ciclo in ('TI', 'UAT')
					and ct.Massa_Teste <> 'SIM'
			) aux1
	) aux2
where
	devManuf in (@selectedDevManufs)
	and system in (@selectedSystems)
	and subprojectDelivery collate Latin1_General_CI_AS in (@selectedProjects)
group by
	month,
	year,
    devManuf,
	system,
	subprojectDelivery
order by
	year,
	month
