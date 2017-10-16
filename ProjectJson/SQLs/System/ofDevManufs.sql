select distinct
    system as id,
    system as name
from
    (
		select distinct 
			sistema as system, 
			isnull(fabrica_desenvolvimento,'') as devManuf 
		from 
			alm_cts 
		where 
			isnull(sistema,'') <> ''
		
		union all
		
		select distinct 
			sistema_defeito as system, 
			isnull(fabrica_desenvolvimento,'') as devManuf 
		from 
			alm_defeitos 
		where 
			isnull(sistema_defeito,'') <> ''
    ) aux
where
    devManuf in (@listDevManufs)
order by
    1
