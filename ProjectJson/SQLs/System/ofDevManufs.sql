select distinct
    system as id,
    system as name
from
    (
		select distinct 
			(case when IsNull(sistema,'') <> '' then sistema else 'NÃO IDENTIFICADO' end) as system, 
			(case when IsNull(fabrica_desenvolvimento,'') <> '' then fabrica_desenvolvimento else 'NÃO IDENTIFICADA' end) as devManuf 
		from 
			alm_cts 
		
		union all
		
		select distinct 
			(case when IsNull(sistema_defeito,'') <> '' then sistema_defeito else 'NÃO IDENTIFICADO' end) as system, 
			(case when IsNull(fabrica_desenvolvimento,'') <> '' then fabrica_desenvolvimento else 'NÃO IDENTIFICADA' end) as devManuf 
		from 
			alm_defeitos 
    ) aux
where
    devManuf in (@listDevManufs)
order by
    1
