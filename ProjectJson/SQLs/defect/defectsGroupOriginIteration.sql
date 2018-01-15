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
			alm_cts cts 
			inner join alm_defeitos df
				on df.subprojeto = cts.subprojeto and
                    df.entrega = cts.entrega and
                    df.ct = cts.ct
		where
			cts.subprojeto = '@subproject' and
			cts.entrega = '@delivery' and
            cts.iterations in (@iterations) and
			df.Status_Atual = 'CLOSED'
		) as totalDefects
	from 
		alm_cts cts 
		inner join alm_defeitos df
			on df.subprojeto = cts.subprojeto and
                df.entrega = cts.entrega and
                df.ct = cts.ct
	where
		cts.subprojeto = '@subproject' and
		cts.entrega = '@delivery' and
        cts.iterations in (@iterations) and
		df.Status_Atual = 'CLOSED'
	group by 
		Origem
	) aux
order by
	2 desc
