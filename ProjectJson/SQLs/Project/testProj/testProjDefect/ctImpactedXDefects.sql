declare @t table (
	subproject varchar(11)
	,delivery varchar(15)
	,subDel varchar(13)
	,date varchar(8)
	,dateOrder varchar(8)
	,qtyDefectsAmb int
	,qtyDefectsCons int
	,qtyDefectsTot int
	,qtyCtsImpacted int
)
insert into @t (
	subproject
	,delivery
	,subDel
	,date
	,dateOrder
	,qtyDefectsAmb
	,qtyDefectsCons
	,qtyDefectsTot
	,qtyCtsImpacted
)            
select
	subproject
	,delivery
	,subDel
	,date
	,substring(date,7,2)+substring(date,4,2)+substring(date,1,2) as dateOrder
	,sum(case when name = 'AMBIENTE' then Qtd_Defeitos else 0 end) as qtyDefectsAmb
	,sum(case when name = 'CONSTRUÇÃO' then Qtd_Defeitos else 0 end) as qtyDefectsCons
	,sum(Qtd_Defeitos) as qtyDefectsTot
	,sum(Qtd_CTs_Impactados) as qtyCtsImpacted
from
	(
	select 
		subprojeto as subproject
		,entrega as delivery
		,convert(varchar, cast(substring(subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(entrega,8,8) as int)) as subDel
		,substring(dt_inicial,1,8) as date
		,substring(dt_inicial,7,2) + substring(dt_inicial,4,2) + substring(dt_inicial,1,2) as dateOrder
		,Origem as name
		,Qtd_CTs_Impactados
		,1 as Qtd_Defeitos
	from 
		alm_defeitos d WITH (NOLOCK)
	where
		d.subprojeto = '@subproject'
		and d.entrega = '@delivery'
		and status_atual not in ('CLOSED', 'CANCELLED')
		and Origem in ('AMBIENTE','CONSTRUÇÃO')
		and dt_inicial <> ''
	) aux
group by 
	subproject
	,delivery
	,subDel
	,date
	,dateOrder
order by 
	dateOrder

select
	t.subproject
	,t.delivery
	,t.subDel
	,t.date
	,t.qtyDefectsAmb
	,t.qtyDefectsCons
	,t.qtyDefectsTot
	,t.qtyCtsImpacted
	,SUM(t2.qtyDefectsAmb) as qtyDefectsAmbAcum
	,SUM(t2.qtyDefectsCons) as qtyDefectsConsAcum
	,SUM(t2.qtyDefectsTot) as qtyDefectsTotAcum
	,SUM(t2.qtyCtsImpacted) as qtyCtsImpactedAcum
from 
	@t t 
	inner join @t t2 
	    on t.dateOrder >= t2.dateOrder
group by 
	t.subproject
	,t.delivery
	,t.subDel
	,t.date
	,t.dateOrder
	,t.qtyDefectsAmb
	,t.qtyDefectsCons
	,t.qtyDefectsTot
	,t.qtyCtsImpacted
order by
	t.dateOrder
