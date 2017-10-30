--if OBJECT_ID('tempdb..#i') is not null 
--  drop table #i
--go
create table #i (
	subproject varchar(11)
	,delivery varchar(15)
	,defect int
	,qtyRetestHours numeric(10,2)
)
insert into #i
select
	subprojeto as subproject,
	entrega as delivery,
	defeito as defect,
	round(
		cast(
			Sum(Tempo_Util)
		as float) / 60
	,2) as qtyRetestHours
from 
	alm_defeitos_tempos dt
where
	status = 'ON_RETEST'
	and subprojeto + entrega collate Latin1_General_CI_AS in (@selectedProjects)
group by
	subprojeto
	,entrega
	,defeito
order by 1, 2, 3

create index idx_r_subproject on #i(subproject)
create index idx_r_delivery on #i(delivery)
create index idx_r_defect on #i(defect)

select
	substring(date,4,2) as month
	,substring(date,7,2) as year
	,testManuf
	,system
	,subprojectDelivery
	,count(*) as qtyDefect
	,sum(qtyRetestHours) as qtyRetestHour
from
	(
		select
			case when dt_inicial <> '' 
				then dt_inicial
				else 
					case when dt_final <> '' 
						then dt_final
						else dt_ultimo_status
					end
			end as date

			,(case when IsNull(df.fabrica_teste,'') <> '' then df.fabrica_teste else 'NÃO IDENTIFICADA' end) as testManuf
			,sistema_ct as system
			,convert(varchar, cast(substring(df.subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(df.entrega,8,8) as int)) as subprojectDelivery
			,IsNull(qtyRetestHours, 0.0) as qtyRetestHours
		from 
			alm_defeitos df WITH (NOLOCK)
			left join #i i on
				i.subproject collate Latin1_General_CI_AS = df.subprojeto and
				i.delivery collate Latin1_General_CI_AS = df.entrega and
				i.defect = df.defeito
		where
			df.ciclo = 'TI'
			and df.status_atual <> 'CANCELLED'
			and subprojeto + entrega collate Latin1_General_CI_AS in (@selectedProjects)
			and sistema_ct in (@selectedSystems)
			and (case when IsNull(df.fabrica_teste,'') <> '' then df.fabrica_teste else 'NÃO IDENTIFICADA' end) in (@selectedTestManufs)
	) aux1
group by 
	substring(date,4,2)
	,substring(date,7,2)
	,testManuf
	,system
	,subprojectDelivery

order by
	2, 1, 3, 4

drop table #i