select
	ct.subprojeto as subproject
	,ct.entrega as delivery
	,ct.teste as test
	,ct.ct
	,ct.ciclo as cycle
	,ct.nro_cenario as scenario
	,ct.nro_ct as testCase
	,ct.iterations as iteration
	,ct.macrocenario as macrocenary
	,ct.sistema as system
	,ct.uat
	,ct.pre_requisito as prerequisite
	,ct.ct_sucessor as successorTestCase
	,ct.nome as name
	,ct.detalhamento_funcional as functionalDetailing
	,t.descricao as description
from 
	alm_cts ct
	left join alm_testes t
	  on t.subprojeto = ct.subprojeto 
	  and t.entrega = ct.entrega 
	  and t.teste = ct.teste
where 
	--ct.subprojeto like '%1149%'
    ct.subprojeto = '@subproject'
    and ct.entrega = '@delivery'
order by
	ct.ciclo
	,ct.nro_cenario
	,ct.nro_ct
	,ct.iterations
	,ct.macrocenario