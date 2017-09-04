--if OBJECT_ID('tempdb..#i') is not null 
--  drop table #i
--go
create table #i (
	subproject varchar(11), 
	delivery varchar(15), 
	defect int,
	year varchar(2),
	month varchar(2),
	qtyRetestHours float
)
insert into #i
select
	subprojeto as subproject,
	entrega as delivery,
	defeito as defect,
	substring(dt_de,7,2) as year,
	substring(dt_de,4,2) as month,
	round(
		cast(
			Sum(Tempo_Util)
		as float) / 60
	,2) as qtyRetestHours
from 
	alm_defeitos_tempos dt
where
	status = 'ON_RETEST'
group by
	subprojeto,
	entrega,
	defeito,
	substring(dt_de,7,2),
	substring(dt_de,4,2)

create index idx_r_subproject on #i(subproject)
create index idx_r_delivery on #i(delivery)
create index idx_r_defect on #i(defect)

select
	IsNull(month,'') as month,
	IsNull(year,'') as year,
	(case when IsNull(fabrica_teste,'') <> '' then fabrica_teste else 'NÃO IDENTIFICADA' end) as testManuf,
	sistema_ct as system,
	convert(varchar, cast(substring(df.subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(df.entrega,8,8) as int)) as subprojectDelivery,
	count(*) as qtyDefect,
	convert(float, round(sum(IsNull(qtyRetestHours,0)),2)) as qtyRetestHours
from 
	alm_defeitos df WITH (NOLOCK)
	left join #i i on
		i.subproject collate Latin1_General_CI_AS = df.subprojeto and
		i.delivery collate Latin1_General_CI_AS = df.entrega and
		i.defect = df.defeito
where
	df.ciclo like '%TI%' and
	df.status_atual <> 'CANCELLED' and
	fabrica_teste in (@selectedTestManufs) and
	sistema_ct in (@selectedSystems) and
	subprojeto + entrega collate Latin1_General_CI_AS in (@selectedProjects)
group by
	IsNull(month,''),
	IsNull(year,''),
	(case when IsNull(fabrica_teste,'') <> '' then fabrica_teste else 'NÃO IDENTIFICADA' end),
	sistema_ct,
	convert(varchar, cast(substring(df.subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(df.entrega,8,8) as int))
order by
	2, 1, 3, 4

drop table #i