select
	testManuf,
	system,
	convert(varchar, cast(substring(subproject,4,8) as int)) + ' ' + convert(varchar,cast(substring(delivery,8,8) as int)) as subprojectDelivery,
	month + '/' + year as monthYear,
	year + '/' + month as yearMonth,
	sum(evidences ) as evidences,
	sum(rejections) as rejections
from
	( select
		cts.fabrica_teste as testManuf,
		cts.subprojeto as subproject,
		cts.entrega as delivery,
		cts.sistema as system,
		substring(dt_execucao,4,2) as month,
		substring(dt_execucao,7,2) as year,

		(case when cts.evidencia_validacao_tecnica <> 'N/A' and cts.UAT = 'SIM' then 1 else 0 end) as evidences,

		(select count(*) 
		from ALM_Historico_Alteracoes_Campos ac
		where cts.subprojeto = ac.subprojeto and
			    cts.entrega = ac.entrega and
			    cts.ct = ac.Tabela_id and
				ac.tabela = 'TESTCYCL' and
			    ac.campo = '(EVIDÊNCIA) VALIDAÇÃO CLIENTE'  and
				ac.novo_valor = 'REJEITADO'
		) as rejections

	from 
		alm_cts cts WITH (NOLOCK)
	where
		status_exec_ct = 'PASSED' and
		cts.massa_Teste <> 'SIM' and
		cts.ciclo like '%TI%' and
		cts.dt_execucao <> ''
	) Aux
group by
	testManuf,
	system,
	subproject,
	delivery, 
	year,
	month
having
	testManuf in (@selectedTestManufs) and
	system in (@selectedSystems) and
	subproject + delivery in (@selectedProjects) and
	sum(evidences) > 0
order by
	testManuf,
	system,
	subproject,
	delivery, 
	year,
	month