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
			and ciclo like ('%JAN/18')
			and path like '%2018%'
	) a1
where 
	sytem <> ''
order by 1