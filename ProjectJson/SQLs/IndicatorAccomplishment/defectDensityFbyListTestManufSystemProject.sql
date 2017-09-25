select
	month,
	year,
    devManuf,
	system,
	convert(varchar, cast(substring(subproject,4,8) as int)) + ' ' + convert(varchar,cast(substring(delivery,8,8) as int)) as subprojectDelivery,
    sum(qty_defect) as qty_defect,
    sum(qty_ct) as qty_ct,
    convert(varchar,round(convert(float, sum(qty_defect)) / isnull(nullif(sum(qty_ct),0),1) * 100,0)) + '%' as density
from
	(
	select
		substring(df.dt_final,4,2) as month,
		substring(df.dt_final,7,2) as year,
		Fabrica_Desenvolvimento as devManuf,
		left(df.Sistema_Defeito,30) as system,
		subprojeto as subproject,
		entrega as delivery,
		0 as qty_ct,
		1 as qty_defect
	from 
		ALM_Defeitos df WITH (NOLOCK)
	where
		df.Status_Atual = 'CLOSED' and
		df.Origem like '%CONSTRUÇÃO%' and
		(df.Ciclo like '%TI%' or df.Ciclo like '%UAT%') and 
		df. dt_final <> ''

	UNION ALL

	select 
		substring(yearMonth, 3, 2) as month,
		substring(yearMonth, 1, 2) as year,
		devManuf,
		system,
		subproject,
		delivery,
		1 as qty_ct,
		0 as qty_defect
	from
		(
			select 
				Fabrica_Desenvolvimento as devManuf,
				left(ct.Sistema,30) as system,
				subprojeto as subproject,
				entrega as delivery,

				(select
					min(substring(dt_execucao,7,2) + substring(dt_execucao,4,2))
				from 
					alm_execucoes ex WITH (NOLOCK)
				where
					ex.subprojeto = ct.subprojeto and
					ex.entrega = ct.entrega and
					ex.ct = ct.ct and
					ex.status = 'PASSED' and
					ex.dt_execucao <> ''
				) as yearMonth
			from 
				ALM_CTs ct WITH (NOLOCK)
			where
				ct.Status_Exec_CT = 'PASSED' and
				ct.Massa_Teste <> 'SIM' and
				(ct.Ciclo like '%TI%' or ct.Ciclo like '%UAT%')
		) aux1
	)  Aux2
where
	devManuf in (@selectedDevManufs)
	and system in (@selectedSystems)
	and (subproject + delivery) collate Latin1_General_CI_AS in (@selectedProjects)
group by
    devManuf,
	system,
	subproject,
	delivery,
	month,
	year
order by
	year,
	month

