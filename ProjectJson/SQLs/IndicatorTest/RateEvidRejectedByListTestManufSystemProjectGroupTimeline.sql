--if OBJECT_ID('tempdb..#r') is not null 
--  drop table #r
--go
create table #r (
	subproject varchar(11), 
	delivery varchar(15), 
	month varchar(2), 
	year varchar(4), 
	ct int, 
	rejectionsTechnique int, 
	rejectionsClient int,
	rejectionsTotal int
)
insert into #r
select 
	subprojeto as subproject,
	entrega as delivery,
	substring(dt_alteracao,4,2) as month,
	substring(dt_alteracao,7,2) as year,
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
	substring(dt_alteracao,4,2),
	substring(dt_alteracao,7,2),
	tabela_id

create index idx_r_subproject on #r(subproject)
create index idx_r_delivery on #r(delivery)
create index idx_r_ct on #r(ct)

select
	month,
	year,
	cts.fabrica_teste as testManuf,
	cts.sistema as system,
	convert(varchar, cast(substring(cts.subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(cts.entrega,8,8) as int)) as subprojectDelivery,
	sum(r.rejectionsTechnique) as rejectionsTechnique,
	sum(r.rejectionsClient) as rejectionsClient,
	sum(r.rejectionsTotal) as rejectionsTotal
from 
	alm_cts cts WITH (NOLOCK)
	inner join #r r on
		r.subproject collate Latin1_General_CI_AS = cts.subprojeto and
		r.delivery collate Latin1_General_CI_AS = cts.entrega and
		r.ct = cts.ct
where
	--cts.subprojeto = 'PRJ00000744' and cts.entrega = 'ENTREGA00000179' and cts.fabrica_teste = 'ACCENTURE' and cts.sistema = '(OI R2) SAC' and
	cts.evidencia_validacao_cliente <> 'N/A' and
	cts.status_exec_ct = 'PASSED' and
	cts.dt_execucao <> '' and
	cts.ciclo = 'TI' and
	cts.fabrica_teste in (@selectedTestManufs) and
	cts.sistema in (@selectedSystems) and
	cts.subprojeto + delivery collate Latin1_General_CI_AS in (@selectedProjects)
group by
	r.month,
	r.year,
	convert(varchar, cast(substring(subproject,4,8) as int)) + ' ' + convert(varchar,cast(substring(delivery,8,8) as int)),
	cts.subprojeto,
	cts.entrega,
	cts.fabrica_teste,
	cts.sistema
order by
	r.year,
	r.month,
	cts.fabrica_teste,
	cts.sistema,
	cts.subprojeto,
	cts.entrega

drop table #r

