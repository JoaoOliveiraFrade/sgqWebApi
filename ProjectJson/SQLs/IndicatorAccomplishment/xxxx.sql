﻿declare @t table (
	devManuf varchar(50), 
	system varchar(50), 
	queue varchar(100)
)
insert into @t 
select distinct
	rtrim(ltrim(substring(q, len(queue) - charindex('-', reverse(q)) + 2, 50))) as devManuf,
	rtrim(ltrim(substring(q, 1, len(queue) - charindex('-', reverse(q))))) as system,
	queue
from
	(
		select distinct
			replace(encaminhado_para,'–', '-') as q,
			encaminhado_para as queue,
			status
		from
			alm_defeitos_tempos
		where
			status in ('IN_PROGRESS', 'PENDENT (PROGRESS)', 'REOPEN') 
	) aux1

select 
	devManuf, system, queue
from
	@t
where
	devManuf not in ('', 'OI','LÍDER TÉCNICO', 'ÁREA DE NEGÓCIOS', 'ÁREA USUÁRIA', 'AUTOMAÇÃO', 'ENGENHARIA', 'OI (API)', 'OI (APLICATIVO)')
order by
	devManuf, system


-----------------------


select 
	month,
	year,
	devManuf,
	system,
	subprojectDelivery,
	queue,
	severity,
	defeito,   
	case 
		when (severity = '3-HIGH' and sum(tempo_util) > 240) or (severity = '2-MEDIUM' and sum(tempo_util) > 480) or (severity = '1-LOW' and sum(tempo_util) > 960)
		then 'N'
		else 'S'
	end as insideSla,
	sum(tempo_util) as tempo_util_min
	--cast (sum(tempo_util) / 60 as varchar) + ':' + cast (sum(tempo_util) - ((sum(tempo_util) / 60) * 60) as varchar) as tempo_util_horas,
	--cast (sum(tempo_util) / 1440 as varchar) as tempo_utild_dias,
from
	(
		select
			substring(dt.dt_de,4,2) as month,
			substring(dt.dt_de,7,2) as year,
			(case when IsNull(d.fabrica_desenvolvimento,'') <> '' then d.fabrica_desenvolvimento else 'NÃO IDENTIFICADA' end) as devManuf,
			d.sistema_ct as system,
			convert(varchar, cast(substring(d.subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(d.subprojeto,8,8) as int)) as subprojectDelivery,
			dt.encaminhado_para,
			case when charindex('-', dt.encaminhado_para) <> 0
				then substring(dt.encaminhado_para, charindex('-', dt.encaminhado_para)+2, 50)
				else dt.encaminhado_para
			end as queue,
			d.severidade as severity,
			d.sla,
			d.defeito,   
			tempo_util
		from
				alm_defeitos d
				left join alm_defeitos_tempos dt
				on dt.subprojeto = d.subprojeto and 
					dt.entrega = d.entrega and 
					dt.defeito = d.defeito
		where
			d.origem like '%Construção%'
			and d.status_atual = 'Closed'
			and (d.ciclo like '%TI%' or d.ciclo like '%UAT%' or d.ciclo like '%TRG%' or d.ciclo like '%DEV%')
			and dt.status in ('IN_PROGRESS', 'PENDENT (PROGRESS)', 'REOPEN')
			--and d.entrega in ('ENTREGA00007456')
	) aux1
group by
	month,
	year,
	devManuf,
	system,
	subprojectDelivery,
	queue,
	severity,
	defeito



select * from alm_defeitos_tempos



select '123-4556-7890'

select CHARINDEX('123-456-7890',REVERSE('-'))-1)
select RIGHT('123-456-7890', CHARINDEX('123-456-7890',REVERSE('-'))-1)

'123-456-7890'

select len('123-4556-7890') - charindex('-', reverse('123-4556-7890'))
SELECT LEN('123-4556-7890') - CHARINDEX('-', REVERSE('123-4556-7890')) + 1;

select 
	'123-4556-7890',
	substring('123-4556-7890', 1, len('123-4556-7890') - charindex('-', reverse('123-4556-7890'))),
	substring('123-4556-7890', len('123-4556-7890') - charindex('-', reverse('123-4556-7890')) + 2, 50)


select distinct
	encaminhado_para,
	substring(encaminhado_para, 1, len(encaminhado_para) - charindex('-', reverse(encaminhado_para))) as sistema,
	substring(encaminhado_para, len(encaminhado_para) - charindex('-', reverse(encaminhado_para)) + 2, 50) as fornecedor
from
	alm_defeitos_tempos




declare @t table (
	Manufacture varchar(50), 
	system varchar(50), 
	queue varchar(100),
	devManuf varchar(1),
	testtManuf varchar(1)
)
insert into @t 
select
	rtrim(ltrim(substring(queue, len(queue) - charindex('-', reverse(queue)) + 2, 50))) as Manufacture,
	rtrim(ltrim(substring(queue, 1, len(queue) - charindex('-', reverse(queue))))) as system,
	queue,
	case when status in ('IN_PROGRESS', 'PENDENT (PROGRESS)', 'REOPEN') then 'S' else 'N' end as devManuf,
	case when status not in ('IN_PROGRESS', 'PENDENT (PROGRESS)', 'REOPEN') then 'S' else 'N' end as testtManuf
from
	(
		select distinct
			replace(encaminhado_para,'–', '-') as queue,
			status
		from
			alm_defeitos_tempos
	) aux1

select 
	Manufacture, system, queue, devManuf, testtManuf
from
	@t
where
	Manufacture not in ('', 'OI',' LÍDER TÉCNICO', 'ÁREA DE NEGÓCIOS', 'ÁREA USUÁRIA', 'AUTOMAÇÃO', 'ENGENHARIA', 'OI (API)', 'OI (APLICATIVO)')
order by
	Manufacture, system
