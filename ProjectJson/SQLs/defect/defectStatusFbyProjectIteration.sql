select 
	name,
	qtyDefects,
	totalDefects,
	round(convert(float,qtyDefects) / (case when totalDefects <> 0 then totalDefects else 1 end) * 100,2) as [percent]
from
	(
	select 
		'Aberto-Fáb.Desen' as name,

		(select count(*) 
            from 
			alm_cts cts WITH (NOLOCK)
			inner join alm_defeitos df WITH (NOLOCK)
				on df.subprojeto = cts.subprojeto and
                    df.entrega = cts.entrega and
                    df.ct = cts.ct
			where 
            df.subprojeto = '@subproject' and 
			df.Entrega = '@delivery' and 
            cts.iterations in (@iterations) and
			df.Status_Atual in ('ON_RETEST','PENDENT (RETEST)','REJECTED')
		) as qtyDefects,

		(select count(*) 
            from 
			alm_cts cts WITH (NOLOCK)
			inner join alm_defeitos df WITH (NOLOCK)
				on df.subprojeto = cts.subprojeto and
                    df.entrega = cts.entrega and
                    df.ct = cts.ct
            where 
            df.subprojeto = '@subproject' and 
            df.Entrega = '@delivery' and
            cts.iterations in (@iterations)
        ) as totalDefects

	union all

	select 
		'Aberto-Fáb.Teste' as name,

		(select count(*) 
            from 
			alm_cts cts WITH (NOLOCK)
			inner join alm_defeitos df WITH (NOLOCK)
				on df.subprojeto = cts.subprojeto and
                    df.entrega = cts.entrega and
                    df.ct = cts.ct
		    where 
            df.subprojeto = '@subproject' and 
			df.Entrega = '@delivery' and 
            cts.iterations in (@iterations) and
			df.Status_Atual in ('NEW','IN_PROGRESS','PENDENT (PROGRESS)','REOPEN','MIGRATE')
		) as qtyDefects,

		(select count(*) 
            from 
			alm_cts cts WITH (NOLOCK)
			inner join alm_defeitos df WITH (NOLOCK)
				on df.subprojeto = cts.subprojeto and
                    df.entrega = cts.entrega and
                    df.ct = cts.ct
            where 
            df.subprojeto = '@subproject' and 
            df.Entrega = '@delivery' and
            cts.iterations in (@iterations)
        ) as totalDefects

	union all

	select 
		'Fechado' as name,

		(select count(*) 
            from 
			alm_cts cts WITH (NOLOCK)
			inner join alm_defeitos df WITH (NOLOCK)
				on df.subprojeto = cts.subprojeto and
                    df.entrega = cts.entrega and
                    df.ct = cts.ct
		    where 
            df.subprojeto = '@subproject' and 
			df.Entrega = '@delivery' and 
            cts.iterations in (@iterations) and
			df.Status_Atual = 'CLOSED'
		) as qtyDefects,

		(select count(*) 
            from 
			alm_cts cts WITH (NOLOCK)
			inner join alm_defeitos df WITH (NOLOCK)
				on df.subprojeto = cts.subprojeto and
                    df.entrega = cts.entrega and
                    df.ct = cts.ct
            where 
            df.subprojeto = '@subproject' and 
            df.Entrega = '@delivery' and
            cts.iterations in (@iterations)
        ) as totalDefects

	union all

	select 
		'Cancelado' as name,

		(select count(*) 
            from 
			alm_cts cts WITH (NOLOCK)
			inner join alm_defeitos df WITH (NOLOCK)
				on df.subprojeto = cts.subprojeto and
                    df.entrega = cts.entrega and
                    df.ct = cts.ct
		    where 
            df.subprojeto = '@subproject' and 
			df.Entrega = '@delivery' and 
            cts.iterations in (@iterations) and
			df.Status_Atual = 'CANCELLED'
		) as qtyDefects,

		(select count(*) 
            from 
			alm_cts cts WITH (NOLOCK)
			inner join alm_defeitos df WITH (NOLOCK)
				on df.subprojeto = cts.subprojeto and
                    df.entrega = cts.entrega and
                    df.ct = cts.ct
        where 
            df.subprojeto = '@subproject' and 
            df.Entrega = '@delivery' and
            cts.iterations in (@iterations)
        ) as totalDefects
	) aux
