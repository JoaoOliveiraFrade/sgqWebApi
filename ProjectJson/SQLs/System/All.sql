select distinct
	system as id,
	system as name
from
	(
		select distinct sistema as system from alm_cts where isnull(sistema,'') <> ''
		union all
		select distinct sistema_defeito as system from alm_defeitos where isnull(sistema_defeito,'') <> ''
	) aux
order by
	1
