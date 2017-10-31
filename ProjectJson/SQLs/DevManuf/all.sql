select 
	devManuf as id,
	devManuf as name
from
	(
		select distinct
			(case 
				when devManuf in('ACC', 'ACCENTURE') then 'ACCENTURE'
				when devManuf in('LINK', 'LINK CONSULTING', 'LINK  CONSULTING') then 'LINK'
				when devManuf in('SONDA', 'SONDA IT') then 'SONDA'
				when devManuf in('TRIAD SYSTEM', 'TRIAD SYSTEMS') then 'TRIAD SYSTEMS'
				when devManuf = 'SOFTWARE EXPRESS' then 'SOFT.EXPRESS'
				else devManuf
			end) as devManuf
		from
			(
				select distinct (case when IsNull(fabrica_desenvolvimento,'') <> '' then fabrica_desenvolvimento else 'NÃO IDENTIFICADA' end) as devManuf from alm_cts
				union all
				select distinct (case when IsNull(fabrica_desenvolvimento,'') <> '' then fabrica_desenvolvimento else 'NÃO IDENTIFICADA' end) as devManuf from alm_defeitos
			) aux1
	) aux2
order by
    1
