select 
	name,
	qtyDefects,
	totalDefects,
	round(convert(float,qtyDefects) / (case when totalDefects <> 0 then totalDefects else 1 end) * 100,2) as [percent]
from
	(
	select 
		'Aberto-Fáb.Teste' as name,
		(select count(*) from ALM_Defeitos d WITH (NOLOCK)
			where d.subprojeto = '@subproject' and 
				d.Entrega = '@delivery' and 
				d.Status_Atual in ('ON_RETEST','PENDENT (RETEST)','REJECTED')
		) as qtyDefects,
		(select count(*) from ALM_Defeitos d WITH (NOLOCK) where d.subprojeto = '@subproject' and d.Entrega = '@delivery') as totalDefects

	union all

	select 
		'Aberto-Fáb.Desen' as name,
		(select count(*) from ALM_Defeitos d WITH (NOLOCK)
		    where d.subprojeto = '@subproject' and 
				d.Entrega = '@delivery' and 
				d.Status_Atual in ('NEW','IN_PROGRESS','PENDENT (PROGRESS)','REOPEN','MIGRATE')
		) as qtyDefects,
		(select count(*) from ALM_Defeitos d WITH (NOLOCK) where d.subprojeto = '@subproject' and d.Entrega = '@delivery') as totalDefects

	union all

	select 
		'Fechado' as name,
		(select count(*) from ALM_Defeitos d WITH (NOLOCK)
		    where d.subprojeto = '@subproject' and 
				d.Entrega = '@delivery' and 
				d.Status_Atual = 'CLOSED'
		) as qtyDefects,
		(select count(*) from ALM_Defeitos d WITH (NOLOCK) where d.subprojeto = '@subproject' and d.Entrega = '@delivery') as totalDefects

	union all

	select 
		'Cancelado' as name,
		(select count(*) from ALM_Defeitos d WITH (NOLOCK)
		    where d.subprojeto = '@subproject' and 
				d.Entrega = '@delivery' and 
				d.Status_Atual = 'CANCELLED'
		) as qtyDefects,
		(select count(*) from ALM_Defeitos d WITH (NOLOCK) where d.subprojeto = '@subproject' and d.Entrega = '@delivery') as totalDefects
	) aux