if OBJECT_ID('tempdb..#hist') is not null 
  drop table #hist
go
create table #hist (
	subproject varchar(11)
	,delivery varchar(15)
	,month varchar(2)
	,year varchar(4)
	,ct int
	,rejectionTechnique int
	,rejectionClient int
	,rejectionTotal int
)
insert into #hist
select 
	subprojeto as subproject
	,entrega as delivery
	,substring(dt_alteracao,4,2) as month
	,substring(dt_alteracao,7,2) as year
	,tabela_id as ct
	,sum(case when campo = '(EVIDÊNCIA) VALIDAÇÃO TÉCNICA' then 1 else 0 end) as rejectionTechnique
	,sum(case when campo = '(EVIDÊNCIA) VALIDAÇÃO CLIENTE' then 1 else 0 end) as rejectionClient
	,count(*) as rejectionTotal
from 
	ALM_Historico_Alteracoes_Campos h WITH (NOLOCK)
where 
	tabela = 'TESTCYCL'
	and campo in ('(EVIDÊNCIA) VALIDAÇÃO TÉCNICA', '(EVIDÊNCIA) VALIDAÇÃO CLIENTE')
	and novo_valor = 'REJEITADO'
	and h.subprojeto = '@subproject' 
	and h.entrega = '@delivery'
group by
	subprojeto
	,entrega
	,substring(dt_alteracao,4,2)
	,substring(dt_alteracao,7,2)
	,tabela_id

create index idx_r_subproject on #hist(subproject)
create index idx_r_delivery on #hist(delivery)
create index idx_r_ct on #hist(ct)

select
	month
	,year
	,cts.fabrica_teste as testManuf
	,cts.sistema as system
	,convert(varchar, cast(substring(cts.subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(cts.entrega,8,8) as int)) as subprojectDelivery
	,sum(hist.rejectionTechnique) as rejectionTechnique
	,sum(hist.rejectionClient) as rejectionClient
	,sum(hist.rejectionTotal) as rejectionTotal
from 
	alm_cts cts WITH (NOLOCK)
	inner join #hist hist on
		hist.subproject collate Latin1_General_CI_AS = cts.subprojeto and
		hist.delivery collate Latin1_General_CI_AS = cts.entrega and
		hist.ct = cts.ct
where
	cts.evidencia_validacao_cliente <> 'N/A'
	and cts.status_exec_ct = 'PASSED'
	and cts.dt_execucao <> ''
	and cts.ciclo = 'TI'
	and cts.subprojeto = '@subproject' 
	and cts.entrega = '@delivery'
group by
	hist.month
	,hist.year
	,cts.subprojeto
	,cts.entrega
	,cts.fabrica_teste
	,cts.sistema
order by
	hist.year
	,hist.month
	,cts.fabrica_teste
	,cts.sistema
	,cts.subprojeto
	,cts.entrega

drop table #hist

