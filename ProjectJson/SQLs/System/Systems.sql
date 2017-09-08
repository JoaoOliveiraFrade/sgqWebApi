select distinct
	id as id
from
    (select distinct sistema as id from alm_cts where sistema is not null
    union all
    select distinct sistema_defeito as id from alm_defeitos where sistema_defeito is not null 
    ) aux
where
    id <> ''
order by
    1