select
	substring(dt_inicial,4,2) as month,
	substring(dt_inicial,7,2) as year,
	(case when IsNull(fabrica_teste,'') <> '' then fabrica_teste else 'NÃO IDENTIFICADA' end) as testManuf,
	sistema_ct as system,
	convert(varchar, cast(substring(subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(entrega,8,8) as int)) as subprojectDelivery,
	count(*) as qtyDefect,
	sum(case when ciclo = 'UAT' then 1 else 0 end) qtyDefectUat
from 
	alm_defeitos WITH (NOLOCK)
where
	ciclo in ('TI', 'UAT') and
	status_atual <> 'CANCELLED' and
	fabrica_teste in (@selectedTestManufs) and
	sistema_ct in (@selectedSystems) and
	subprojeto + entrega collate Latin1_General_CI_AS in (@selectedProjects)
group by
	substring(dt_inicial,7,2),
	substring(dt_inicial,4,2),
	(case when IsNull(fabrica_teste,'') <> '' then fabrica_teste else 'NÃO IDENTIFICADA' end),
	sistema_ct,
	convert(varchar, cast(substring(subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(entrega,8,8) as int))
order by
	2, 1, 3, 4
