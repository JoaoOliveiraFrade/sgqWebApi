select distinct
	system as id,
	system as name
from
	(
		select distinct (case when IsNull(sistema,'') <> '' then sistema else 'NÃO IDENTIFICADO' end) as system from alm_cts
		union all
		select distinct (case when IsNull(sistema_defeito,'') <> '' then sistema_defeito else 'NÃO IDENTIFICADO' end) as system from alm_defeitos
	) aux
order by
	1
