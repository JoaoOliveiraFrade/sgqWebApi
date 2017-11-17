
declare @t table (
	devManuf varchar(50), 
	system varchar(50)
)
insert into @t 
select distinct
	(case when IsNull(devManuf,'') <> '' then devManuf else 'N/A' end) as devManuf
	,(case when IsNull(system,'') <> '' then system else 'N/A' end) as system
from
	(
		select distinct
			rtrim(ltrim(substring(agent, len(agent) - charindex('-', reverse(agent)) + 2, 50))) as devManuf
			,rtrim(ltrim(substring(agent, 1, len(agent) - charindex('-', reverse(agent))))) as system
		from
			(
				select distinct
					replace(dt.encaminhado_para,'–', '-') as agent
					,(d.subprojeto + d.entrega) as subprojectDelivery
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
			) a1
	) a2
order by 1,2


select distinct 
	t.devManuf
	,t.system as id
	,t.system as name
	,(case when IsNull(tor.nome,'') <> '' then tor.nome else 'N/A' end) as tower
from 
	@t t
	left join SGQ_Sistemas_Arquitetura sa 
		on sa.Nome = t.system

	left join SGQ_Torres tor
		on tor.id = sa.torre
where 
	devManuf not in ('N/A', 'OI','LÍDER TÉCNICO', 'ÁREA DE NEGÓCIOS', 'ÁREA USUÁRIA', 'AUTOMAÇÃO', 'ENGENHARIA', 'OI (API)', 'OI (APLICATIVO)')
order by 
	1, 4, 2

