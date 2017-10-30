--if OBJECT_ID('tempdb..#i') is not null 
--  drop table #i
--go
create table #i (
	subproject varchar(11), 
	delivery varchar(15), 
	defeito int,
	yearMonth varchar(4)
)
insert into #i
select
	subprojeto as subproject,
	entrega as delivery,
	tabela_id as defeito,
	max(substring(dt_alteracao,7,2) + substring(dt_alteracao,4,2)) as yearMonth
from ALM_Historico_Alteracoes_Campos 
where 
	tabela = 'BUG' and
	campo = 'ORIGEM DO ERRO' and
	novo_valor = 'IMPROCEDENTE'
group by
	subprojeto,
	entrega,
	tabela_id

create index idx_r_subproject on #i(subproject)
create index idx_r_delivery on #i(delivery)
create index idx_r_defeito on #i(defeito)

select
	IsNull(right(i.yearMonth,2),'') as month,
	IsNull(left(i.yearMonth,2),'') as year,
	(case when IsNull(df.fabrica_teste,'') <> '' then df.fabrica_teste else 'NÃO IDENTIFICADA' end) as testManuf,
	sistema_ct as system,
	convert(varchar, cast(substring(df.subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(df.entrega,8,8) as int)) as subprojectDelivery,
	count(*) as qtyDefect,
	sum(case when df.status_atual = 'CANCELLED' and origem = 'IMPROCEDENTE' then 1 else 0 end) as qtyUnfounded
from 
	alm_defeitos df WITH (NOLOCK)
	left join #i i on
		i.subproject collate Latin1_General_CI_AS = df.subprojeto and
		i.delivery collate Latin1_General_CI_AS = df.entrega and
		i.defeito = df.defeito
where
	df.ciclo = 'TI'
	and df.subprojeto + df.entrega collate Latin1_General_CI_AS in (@selectedProjects)
	and df.sistema_ct in (@selectedSystems)
	and (case when IsNull(df.fabrica_teste,'') <> '' then df.fabrica_teste else 'NÃO IDENTIFICADA' end) in (@selectedTestManufs)
group by
	i.yearMonth,
	(case when IsNull(df.fabrica_teste,'') <> '' then df.fabrica_teste else 'NÃO IDENTIFICADA' end),
	sistema_ct,
	convert(varchar, cast(substring(df.subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(df.entrega,8,8) as int))
order by
	i.yearMonth, 3, 4