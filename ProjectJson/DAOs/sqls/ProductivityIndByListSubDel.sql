select 
	substring(ex.dt_execucao,4,2) + '/' + substring(ex.dt_execucao,7,2) as date,
	substring(ex.dt_execucao,4,2) as month,
	substring(ex.dt_execucao,7,2) as year,
	cts.fabrica_teste as testManuf,
	cts.sistema as system,
	convert(varchar, cast(substring(cts.subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(cts.entrega,8,8) as int)) as project,
	cts.subprojeto as subproject,
	cts.entrega as delivery,
	count(*) as productivity
from 
	alm_cts cts WITH (NOLOCK)
	left join alm_execucoes ex WITH (NOLOCK)
		on 	ex.subprojeto = cts.subprojeto and
			ex.entrega = cts.entrega and
			ex.ct = cts.ct
where 
	ex.subprojeto + ex.entrega in (@list) and
	ex.status in ('PASSED', 'FAILED') and
	ex.dt_execucao <> '' and
	cts.ciclo like '%TI%' -- or cts.Ciclo like '%UAT%'
group by
	substring(ex.dt_execucao,4,2),
	substring(ex.dt_execucao,7,2),
	cts.subprojeto,
	cts.entrega,
	cts.fabrica_teste,
	cts.sistema