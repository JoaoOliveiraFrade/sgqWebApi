select distinct
    system as id,
    system as name
from
    (
		select distinct sistema as system, isnull(fabrica_teste,'') as testManuf from alm_cts where isnull(sistema,'') <> ''
		union all
		select distinct sistema_defeito as system, isnull(fabrica_teste,'') as testManuf from alm_defeitos where isnull(sistema_defeito,'') < >''
    ) aux
where
    testManuf in (@listTestManufs)
order by
    1