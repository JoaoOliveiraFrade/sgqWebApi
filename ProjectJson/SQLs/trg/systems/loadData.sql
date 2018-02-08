select 
	 sytem as id
	,sytem as name
from
	(
		select distinct 
			substring(path, CHARINDEX('2018', path) + 11,30)  as sytem
		from alm_cts 
		where 
			subprojeto = 'TRG2017' 
			--and subprojeto = 'TRG2018' 
			and ciclo like ('%@mmm/@yy')
			and path like '%@yyyy%'
	) a1
where 
	sytem <> ''
order by 1