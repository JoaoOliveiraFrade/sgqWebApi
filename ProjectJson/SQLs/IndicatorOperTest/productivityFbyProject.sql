select 
	substring(ex.dt_execucao,4,2) as month
	,substring(ex.dt_execucao,7,2) as year
	,(case when IsNull(cts.fabrica_teste,'') <> '' then cts.fabrica_teste else 'N/A' end) as testManuf
	,cts.sistema as system
	,convert(varchar, cast(substring(cts.subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(cts.entrega,8,8) as int)) as subprojectDelivery
	,count(*) as productivity
	,sum(case when ex.status = 'PASSED' then 1 else 0 end) as passed
	,sum(case when ex.status = 'FAILED' then 1 else 0 end) as failed
from 
	alm_cts cts WITH (NOLOCK)
	inner join alm_execucoes ex WITH (NOLOCK)
		on 	ex.subprojeto = cts.subprojeto and
			ex.entrega = cts.entrega and
			ex.ct = cts.ct
where 
	cts.ciclo = 'TI'
	and ex.status in ('PASSED', 'FAILED')
	and ex.dt_execucao <> ''
	and cts.subprojeto = '@subproject'
	and cts.entrega = '@delivery'
group by
	substring(ex.dt_execucao,4,2)
	,substring(ex.dt_execucao,7,2)
	,(case when IsNull(cts.fabrica_teste,'') <> '' then cts.fabrica_teste else 'N/A' end)
	,cts.sistema
	,convert(varchar, cast(substring(cts.subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(cts.entrega,8,8) as int))
order by
	2, 1, 3, 4

--select 
--	substring(ex.dt_execucao,4,2) + '/' + substring(ex.dt_execucao,7,2) as monthYear,
--	substring(ex.dt_execucao,7,2) + '/' + substring(ex.dt_execucao,4,2) as yearMonth,
--	cts.fabrica_teste as testManuf,
--	cts.sistema as system,
--	convert(varchar, cast(substring(cts.subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(cts.entrega,8,8) as int)) as subprojectDelivery,
--	--cts.subprojeto as subproject,
--	--cts.entrega as delivery,
--	sum(case when ex.status = 'PASSED' then 1 else 0 end) as passed,
--	sum(case when ex.status = 'FAILED' then 1 else 0 end) as failed,
--	count(*) as productivity
--from 
--	alm_cts cts WITH (NOLOCK)
--	left join alm_execucoes ex WITH (NOLOCK)
--		on 	ex.subprojeto = cts.subprojeto and
--			ex.entrega = cts.entrega and
--			ex.ct = cts.ct
--where 
--	cts.ciclo = 'TI' and 
--	cts.fabrica_teste in (@selectedTestManufs) and
--	cts.sistema in (@selectedSystems) and
--	ex.subprojeto + ex.entrega in (@selectedProjects) and
--	ex.status in ('PASSED', 'FAILED') and
--	ex.dt_execucao <> ''
--group by
--	substring(ex.dt_execucao,4,2),
--	substring(ex.dt_execucao,7,2),
--	cts.subprojeto,
--	cts.entrega,
--	cts.fabrica_teste,
--	cts.sistema
--order by
--	2,3,4,6,7