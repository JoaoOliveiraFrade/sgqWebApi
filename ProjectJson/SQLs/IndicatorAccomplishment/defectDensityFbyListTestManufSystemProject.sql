﻿declare @t table (
	month varchar(2),
	year varchar(4),
	devManuf varchar(50), 
	system varchar(50),
	subprojectDelivery varchar(26),
	qtyDefect int,
	qtyCt int
)
insert into @t 
select
	substring(df.dt_final,4,2) as month,
	substring(df.dt_final,7,2) as year,
	Fabrica_Desenvolvimento as devManuf,
	left(df.Sistema_Defeito,30) as system,
	subprojeto + entrega as subprojectDelivery,
	1 as qtyDefect,
	0 as qtyCt
from 
	ALM_Defeitos df WITH (NOLOCK)
where
	df.Status_Atual = 'CLOSED' and
	df.Origem like '%CONSTRUÇÃO%' and
	(df.Ciclo like '%TI%' or df.Ciclo like '%UAT%') and 
	df. dt_final <> ''

UNION ALL

select 
	substring(yearMonth, 3, 2) as month,
	substring(yearMonth, 1, 2) as year,
	devManuf,
	system,
	subprojeto + entrega as subprojectDelivery,
	0 as qtyDefect,
	1 as qtyCt
from
	(
		select 
			Fabrica_Desenvolvimento as devManuf,
			left(ct.Sistema,30) as system,
			subprojeto,
			entrega,

			(select
				min(substring(dt_execucao,7,2) + substring(dt_execucao,4,2))
			from 
				alm_execucoes ex WITH (NOLOCK)
			where
				ex.subprojeto = ct.subprojeto and
				ex.entrega = ct.entrega and
				ex.ct = ct.ct and
				ex.status = 'PASSED' and
				ex.dt_execucao <> ''
			) as yearMonth
		from 
			ALM_CTs ct WITH (NOLOCK)
		where
			ct.Status_Exec_CT = 'PASSED' and
			ct.Massa_Teste <> 'SIM' and
			(ct.Ciclo like '%TI%' or ct.Ciclo like '%UAT%')
	) aux1

select
	month,
	year,
    devManuf,
	system,
	convert(varchar, cast(substring(subprojectDelivery,4,8) as int)) + ' ' + convert(varchar,cast(substring(subprojectDelivery,19,8) as int)) as subprojectDelivery,
    sum(qtyDefect) as qtyDefect,
    sum(qtyCt) as qtyCt,
    convert(varchar,round(convert(float, sum(qtyDefect)) / isnull(nullif(sum(qtyCt),0),1) * 100,0)) + '%' as density
from
	@t
where
	devManuf in (@selectedDevManufs)
	and system in (@selectedSystems)
	and subprojectDelivery collate Latin1_General_CI_AS in (@selectedProjects)
group by
	month,
	year,
    devManuf,
	system,
	subprojectDelivery
order by
	year,
	month

