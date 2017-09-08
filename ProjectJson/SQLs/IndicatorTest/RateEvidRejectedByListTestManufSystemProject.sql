--if OBJECT_ID('tempdb..#r') is not null 
--  drop table #r
--go
create table #r (
	subproject varchar(11), 
	delivery varchar(15), 
	ct int, 
	rejectionsTechnique int, 
	rejectionsClient int,
	rejectionsTotal int
)
insert into #r
select 
	subprojeto as subproject,
	entrega as delivery,
	tabela_id as ct,
	sum(case when campo = '(EVIDÊNCIA) VALIDAÇÃO TÉCNICA' then 1 else 0 end) as rejectionsTechnique,
	sum(case when campo = '(EVIDÊNCIA) VALIDAÇÃO CLIENTE' then 1 else 0 end) as rejectionsClient,
	count(*) as rejectionsTotal
from 
	ALM_Historico_Alteracoes_Campos  WITH (NOLOCK)
where 
	tabela = 'TESTCYCL' and
	campo in ('(EVIDÊNCIA) VALIDAÇÃO TÉCNICA', '(EVIDÊNCIA) VALIDAÇÃO CLIENTE')  and
	novo_valor = 'REJEITADO'
group by
	subprojeto,
	entrega,
	tabela_id;

create index idx_r_subproject on #r(subproject);
create index idx_r_delivery on #r(delivery);
create index idx_r_ct on #r(ct);

select
	cts.fabrica_teste as testManuf,
	cts.sistema as system,
	convert(varchar, cast(substring(cts.subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(cts.entrega,8,8) as int)) as subprojectDelivery,
	count(*) as evidences,
	sum((case when cts.UAT = 'SIM' then 1 else 0 end)) as evidencesClient,
	isnull(sum(r.rejectionsTechnique), 0)  as rejectionsTechnique,
	isnull(sum(r.rejectionsClient), 0)  as rejectionsClient,
	isnull(sum(r.rejectionsTotal), 0)  as rejectionsTotal
from 
	alm_cts cts WITH (NOLOCK)
	left join #r r on
		r.subproject collate Latin1_General_CI_AS = cts.subprojeto and
		r.delivery collate Latin1_General_CI_AS = cts.entrega and
		r.ct = cts.ct
where
	--cts.subprojeto = 'PRJ00000744' and cts.entrega = 'ENTREGA00000179' and cts.fabrica_teste = 'ACCENTURE' and cts.sistema = '(OI R2) SAC' and
	cts.evidencia_validacao_cliente <> 'N/A' and
	cts.status_exec_ct = 'PASSED' and
	cts.dt_execucao <> '' and
	cts.ciclo like '%TI%' and
	cts.fabrica_teste in (@selectedTestManufs) and
	cts.sistema in (@selectedSystems) and
	cts.subprojeto + delivery collate Latin1_General_CI_AS in (@selectedProjects)
group by
	convert(varchar, cast(substring(subproject,4,8) as int)) + ' ' + convert(varchar,cast(substring(delivery,8,8) as int)),
	cts.subprojeto,
	cts.entrega,
	cts.fabrica_teste,
	cts.sistema
order by
	cts.fabrica_teste,
	cts.sistema,
	cts.subprojeto,
	cts.entrega;

drop table #r;