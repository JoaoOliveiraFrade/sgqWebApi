select 
	testManuf as id,
	testManuf as name
from
	(
		select distinct
			(case 
				when testManuf in('ACC', 'ACCENTURE') then 'ACCENTURE'
				when testManuf in('LINK', 'LINK CONSULTING', 'LINK  CONSULTING') then 'LINK CONSULTING'
				when testManuf in('SONDA', 'SONDA IT') then 'SONDA'
				when testManuf in('TRIAD SYSTEM', 'TRIAD SYSTEMS') then 'TRIAD SYSTEMS'
				else testManuf
			end) as testManuf
		from
			(
				 select distinct fabrica_teste as testManuf from alm_cts where isnull(fabrica_teste,'') <> ''
				 union all
				 select distinct fabrica_teste as testManuf from alm_defeitos where isnull(fabrica_teste,'') <> ''
			) aux1
	) aux2
order by
    1