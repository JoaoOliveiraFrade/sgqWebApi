--if OBJECT_ID('tempdb..#hist') is not null 
--  drop table #hist
--go
create table #hist (
	subproject varchar(11)
	,delivery varchar(15)
	,ct int
	,qtyRejectionTechnique int
	,qtyRejectionClient int
	,qtyRejectionTotal int
)
insert into #hist
select 
	subprojeto as subproject
	,entrega as delivery
	,tabela_id as ct
	,sum(case when campo = '(EVIDÊNCIA) VALIDAÇÃO TÉCNICA' then 1 else 0 end) as qtyRejectionTechnique
	,sum(case when campo = '(EVIDÊNCIA) VALIDAÇÃO CLIENTE' then 1 else 0 end) as qtyRejectionClient
	,count(*) as qtyRejectionTotal
from 
	ALM_Historico_Alteracoes_Campos h WITH (NOLOCK)
where 
	tabela = 'TESTCYCL'
	and campo in ('(EVIDÊNCIA) VALIDAÇÃO TÉCNICA', '(EVIDÊNCIA) VALIDAÇÃO CLIENTE')
	and novo_valor = 'REJEITADO'
	and h.subprojeto + h.delivery collate Latin1_General_CI_AS in (@selectedProjects)

group by
	subprojeto
	,entrega
	,tabela_id

create index idx_r_subproject on #hist(subproject)
create index idx_r_delivery on #hist(delivery)
create index idx_r_ct on #hist(ct)

select
	substring(cts.dt_execucao,4,2) as month
	,substring(cts.dt_execucao,7,2) as year
	,(case when IsNull(cts.fabrica_teste,'') <> '' then cts.fabrica_teste else 'NÃO IDENTIFICADA' end) as testManuf
	,cts.sistema as system
	,convert(varchar, cast(substring(cts.subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(cts.entrega,8,8) as int)) as subprojectDelivery
	,sum(case when cts.evidencia_validacao_tecnica <> 'N/A' then 1 else 0 end) as qtyEvidence
	,sum(case when cts.evidencia_validacao_cliente <> 'N/A' and cts.UAT = 'SIM' then 1 else 0 end) as qtyEvidenceClient
	,isnull(sum(hist.qtyRejectionTechnique),0) as qtyRejectionTechnique
	,isnull(sum(hist.qtyRejectionClient),0) as qtyRejectionClient
	,isnull(sum(hist.qtyRejectionTotal),0) as qtyRejectionTotal
from 
	alm_cts cts WITH (NOLOCK)
	left join #hist hist on
		hist.subproject collate Latin1_General_CI_AS = cts.subprojeto and
		hist.delivery collate Latin1_General_CI_AS = cts.entrega and
		hist.ct = cts.ct
where
	cts.status_exec_ct = 'PASSED'
	and cts.dt_execucao <> ''
	and cts.ciclo = 'TI'
	and cts.subprojeto + cts.delivery collate Latin1_General_CI_AS in (@selectedProjects)
	and cts.sistema in (@selectedSystems)
	and (case when IsNull(cts.fabrica_teste,'') <> '' then cts.fabrica_teste else 'NÃO IDENTIFICADA' end) in (@selectedTestManufs)
group by
	substring(cts.dt_execucao,4,2)
	,substring(cts.dt_execucao,7,2)
	,(case when IsNull(cts.fabrica_teste,'') <> '' then cts.fabrica_teste else 'NÃO IDENTIFICADA' end)
	,cts.sistema
	,convert(varchar, cast(substring(cts.subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(cts.entrega,8,8) as int))
