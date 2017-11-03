select distinct
	system.devManuf
	,system.id as id
	,system.id as name
	,(case when IsNull(tor.nome,'') <> '' then tor.nome else 'N/A' end) as tower
from
	(
		select distinct 
			fabrica_desenvolvimento as devManuf
			,(case when IsNull(sistema,'') <> '' then sistema else 'N/A' end) as id 
		from 
			alm_cts

		union all

		select distinct 
			fabrica_desenvolvimento as devManuf
			,(case when IsNull(sistema_defeito,'') <> '' then sistema_defeito else 'N/A' end) as id 
		from 
			alm_defeitos
	) system

	left join SGQ_Sistemas_Arquitetura sa 
		on sa.Nome = system.id

	left join SGQ_Torres tor
		on tor.id = sa.torre
order by
	1, 2