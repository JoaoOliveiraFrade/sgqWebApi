declare @t table (
	testManuf varchar(50), 
	system varchar(50)
)
insert into @t 
select distinct
	(case when IsNull(testManuf,'') <> '' then testManuf else 'N/A' end) as testManuf
	,(case when IsNull(system,'') <> '' then system else 'N/A' end) as system
from
	(
		select distinct
			rtrim(ltrim(substring(agent, len(agent) - charindex('-', reverse(agent)) + 2, 50))) as testManuf
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
					and dt.status in ('NEW', 'ON_RETEST', 'PENDENT', 'PENDENT (RETEST)', 'REJECTED')
			) a1
	) a2
order by 1,2


select distinct 
	t.testManuf
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
	testManuf not in ('N/A', 'OI','LÍDER TÉCNICO', 'ÁREA DE NEGÓCIOS', 'ÁREA USUÁRIA', 'AUTOMAÇÃO', 'ENGENHARIA', 'OI (API)', 'OI (APLICATIVO)')
order by 
	1