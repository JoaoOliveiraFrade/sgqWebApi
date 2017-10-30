--data
declare @t table (
	devManuf varchar(50), 
	system varchar(50),
	subprojectDelivery varchar(26),
	month varchar(2),
	year varchar(2),
	severity varchar(50),
	sla int,
	defeito int,
	tempo_util int
)
insert into @t 
select 
	rtrim(ltrim(substring(queue, len(queue) - charindex('-', reverse(queue)) + 2, 50))) as devManuf
	,rtrim(ltrim(substring(queue, 1, len(queue) - charindex('-', reverse(queue))))) as system
	,subprojectDelivery
	,month
	,year
	,severity
	,sla
	,defeito
	,tempo_util
from
	(
		select 
			replace(dt.encaminhado_para,'–', '-') as queue,
			convert(varchar, cast(substring(d.subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(d.subprojeto,8,8) as int)) as subprojectDelivery,
			substring(dt.dt_de,4,2) as month,
			substring(dt.dt_de,7,2) as year,
			--dt.encaminhado_para,
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
	) aux1

select 
	month,
	year,
	devManuf,
	system,
	subprojectDelivery,
	severity,
	count(*) as qtyDefect,
	sum(insideSla) as insideSla
from
	(
		select 
			month,
			year,
			devManuf,
			system,
			subprojectDelivery,
			severity,
			defeito,   
			case 
				when (severity = '3-HIGH' and sum(tempo_util) > 240) or (severity = '2-MEDIUM' and sum(tempo_util) > 480) or (severity = '1-LOW' and sum(tempo_util) > 960)
				then 0
				else 1
			end as insideSla
			--sum(tempo_util) as tempo_util_min
			--cast (sum(tempo_util) / 60 as varchar) + ':' + cast (sum(tempo_util) - ((sum(tempo_util) / 60) * 60) as varchar) as tempo_util_horas,
			--cast (sum(tempo_util) / 1440 as varchar) as tempo_utild_dias,
		from 
			@t 
		where
			devManuf not in ('', 'OI','LÍDER TÉCNICO', 'ÁREA DE NEGÓCIOS', 'ÁREA USUÁRIA', 'AUTOMAÇÃO', 'ENGENHARIA', 'OI (API)', 'OI (APLICATIVO)') 
		group by
			month,
			year,
			devManuf,
			system,
			subprojectDelivery,
			severity,
			defeito
	) aux1
group by
	month,
	year,
	devManuf,
	system,
	subprojectDelivery,
	severity
order by
	year,
	month