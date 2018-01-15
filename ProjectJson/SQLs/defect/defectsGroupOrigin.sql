select 
	UPPER(LEFT(name,1))+LOWER(SUBSTRING(name,2,LEN(name))) as name,
	qtyDefects,
	totalDefects,
	round(convert(float,qtyDefects) / (case when totalDefects <> 0 then totalDefects else 1 end) * 100,2) as [percent]
from
	(
	select 
		(case when Origem <> '' then Origem else 'INDEFINIDO' end) as name,
		count(*) as qtyDefects,
		(select 
			count(*)
		from 
			ALM_Defeitos d WITH (NOLOCK)
		where
			subprojeto = '@subproject' and
			entrega = '@delivery' and
			Status_Atual = 'CLOSED'
		) as totalDefects
	from 
		ALM_Defeitos d WITH (NOLOCK)
	where
		subprojeto = '@subproject' and
		entrega = '@delivery' and
		Status_Atual = 'CLOSED'
	group by 
		Origem
	) aux
order by
	2 desc
