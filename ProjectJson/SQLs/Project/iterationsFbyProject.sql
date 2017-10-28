select distinct 
	iterations as id,
	iterations as name
from 
	alm_cts
where
	subprojeto = '@subproject' and
	entrega = '@delivery' and
    status_exec_ct <> 'CANCELLED'
order by 
	1