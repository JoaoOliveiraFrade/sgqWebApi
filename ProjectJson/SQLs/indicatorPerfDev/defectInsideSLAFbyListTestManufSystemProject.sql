declare @t table (
	devManuf varchar(50), 
	system varchar(50),
	subprojectDelivery varchar(26),
	month varchar(2),
	year varchar(4),
	severity varchar(50),
	slq int,
	defect int,
	timeMin int
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
	,defect
	,timeMin
from
	(
		select
			replace(dt.encaminhado_para,'–', '-') as queue
			,(case when IsNull(d.fabrica_desenvolvimento,'') <> '' then d.fabrica_desenvolvimento else 'NÃO IDENTIFICADA' end) as devManuf
			,d.sistema_ct as system
			,(d.subprojeto + d.entrega) as subprojectDelivery
			,substring(dt.dt_de,4,2) as month
			,substring(dt.dt_de,7,2) as year
			,d.severidade as severity
			,d.sla
			,d.defeito as defect
			,tempo_util as timeMin

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
	month
	,year
	,devManuf
	,system
	,convert(varchar, cast(substring(subprojectDelivery,4,8) as int)) + ' ' + convert(varchar,cast(substring(subprojectDelivery,19,8) as int)) as subprojectDelivery
	,severity
	,count(*) as qtyDefect
	,sum(insideSLA) as qtyInsideSLA
	,round(convert(float, sum(insideSLA)) / (case when count(*) <> 0 then count(*) else 1 end) * 100, 2) as percInsideSLA
from
	(

		select
			month
			,year
			,devManuf
			,system
			,subprojectDelivery
			,severity
			,defect
			,case 
				when (severity = '3-HIGH' and sum(timeMin) > 240) or (severity = '2-MEDIUM' and sum(timeMin) > 480) or (severity = '1-LOW' and sum(timeMin) > 960)
				then 0
				else 1
			end as insideSLA
		from
			@t
		where
			devManuf in (@selectedDevManufs)
			and system in (@selectedSystems)
			and subprojectDelivery collate Latin1_General_CI_AS in (@selectedProjects)
		group by
			devManuf
			,system
			,subprojectDelivery
			,month
			,year
			,severity
			,defect
	) aux1
group by
	devManuf
	,system
	,subprojectDelivery
	,month
	,year
	,severity
order by
	year
	,month
	,devManuf
	,system
