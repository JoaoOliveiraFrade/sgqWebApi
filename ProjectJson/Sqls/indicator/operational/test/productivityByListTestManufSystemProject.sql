select 
	substring(ex.dt_execucao,4,2) as month
	,substring(ex.dt_execucao,7,2) as year
	,(case when IsNull(cts.fabrica_teste,'') <> '' then cts.fabrica_teste else 'N/A' end) as testManuf
	,cts.sistema as system
	,convert(varchar, cast(substring(cts.subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(cts.entrega,8,8) as int)) as subDel
	,count(*) as productivity
	,sum(case when ex.status = 'PASSED' then 1 else 0 end) as passed
	,sum(case when ex.status = 'FAILED' then 1 else 0 end) as failed
from 
	alm_cts cts WITH (NOLOCK)
	left join alm_execucoes ex WITH (NOLOCK)
		on 	ex.subprojeto = cts.subprojeto and
			ex.entrega = cts.entrega and
			ex.ct = cts.ct
where 
	cts.ciclo = 'TI'
	and ex.status in ('PASSED', 'FAILED')
	and ex.dt_execucao <> ''
	and ex.subprojeto + ex.entrega in (@selectedProject)
	and cts.sistema in (@selectedSystem)
	and (case when IsNull(cts.fabrica_teste,'') <> '' then cts.fabrica_teste else 'N/A' end) in (@selectedTestManuf)
group by
	substring(ex.dt_execucao,4,2)
	,substring(ex.dt_execucao,7,2)
	,(case when IsNull(cts.fabrica_teste,'') <> '' then cts.fabrica_teste else 'N/A' end)
	,cts.sistema
	,convert(varchar, cast(substring(cts.subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(cts.entrega,8,8) as int))
order by
	2, 1, 3, 4
