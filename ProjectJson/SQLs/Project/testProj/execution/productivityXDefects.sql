declare @t table (
	date varchar(8), 
	dateOrder varchar(8), 
	productivity int,
	realized int,
	qtyDefectsAmb int,
	qtyDefectsCons int,
	qtyDefectsTot int
)

insert into @t (
	date, 
	dateOrder,
	productivity,
	realized,
	qtyDefectsAmb,
	qtyDefectsCons,
	qtyDefectsTot
)
select 
	date,
	substring(date,7,2)+substring(date,4,2)+substring(date,1,2) as dateOrder,
	sum(productivity) as productivity,
	sum(realized) as realized,
	sum(qtyDefectsAmb) as qtyDefectsAmb,
	sum(qtyDefectsCons) as qtyDefectsCons,
	sum(qtyDefectsTot) as qtyDefectsTot
from
	(
	select 
		left(dt_execucao,8) as date,
		1 as productivity,
		0 as realized,
		0 as qtyDefectsAmb,
		0 as qtyDefectsCons,
		0 as qtyDefectsTot
	from alm_execucoes WITH (NOLOCK)
	where 
		subprojeto = '@subproject' and
		entrega = '@delivery' and
		status in ('PASSED', 'FAILED') and
		dt_execucao <> ''

	union all

	select 
		left(dt_execucao,8) as date,
		0 as productivity,
		1 as realized,
		0 as qtyDefectsAmb,
		0 as qtyDefectsCons,
		0 as qtyDefectsTot
	from ALM_CTs WITH (NOLOCK)
	where 
		subprojeto = '@subproject' and
		entrega = '@delivery' and
		Status_Exec_CT = 'PASSED' and 
		dt_execucao <> ''

	union all

	select 
		substring(dt_inicial,1,8) as date,
		0 as productivity,
		0 as realized,
		(case when origem = 'AMBIENTE' then 1 else 0 end) as qtyDefectsAmb,
		(case when origem = 'CONSTRUÇÃO' then 1 else 0 end) as qtyDefectsCons,
		1 as qtyDefectsTot
	from 
		alm_defeitos
	where
		subprojeto = '@subproject' and
		entrega = '@delivery' and
		status_atual not in ('CLOSED', 'CANCELLED') and
		Origem in ('AMBIENTE','CONSTRUÇÃO') and
		dt_inicial <> ''

	) Aux
group by
	date
order by
	2

select
	convert(varchar, cast(substring('@subproject',4,8) as int)) + ' ' + convert(varchar,cast(substring('@delivery',8,8) as int)) as subDel,
	'@subproject' as subproject,
	'@delivery' as delivery,
	t.date,
	t.productivity,
	t.realized,
	t.qtyDefectsAmb,
	t.qtyDefectsCons,
	t.qtyDefectsTot,

	SUM(t2.qtyDefectsAmb) as qtyDefectsAmbAcum,
	SUM(t2.qtyDefectsCons) as qtyDefectsConsAcum,
	SUM(t2.qtyDefectsTot) as qtyDefectsTotAcum
from 
	@t t inner join @t t2 
	    on t.dateOrder >= t2.dateOrder
group by 
	t.dateOrder, 
	t.date,
	t.productivity,
	t.realized,
	t.qtyDefectsAmb,
	t.qtyDefectsCons,
	t.qtyDefectsTot
order by 
	t.dateOrder
