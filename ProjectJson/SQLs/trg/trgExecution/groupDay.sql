declare @t table (
	date varchar(8), 
	dateOrder varchar(8), 
	active int, 
	activeTechnique int,
	activeClient int, 
	planned int, 
	realized int,
	productivity int,
	approvedTI int,
	approvedUAT int
)
insert into @t (
	date, 
	dateOrder,
	active,
	activeTechnique,
	activeClient, 
	planned, 
	realized,
	productivity,
	approvedTI,
	approvedUAT
)
select 
	date,
	substring(date,7,2)+substring(date,4,2)+substring(date,1,2) as dateOrder,
	sum(active) as active,
	sum(activeTechnique) as activeTechnique,
	sum(activeClient) as activeClient,
	sum(planned) as planned,
	sum(realized) as realized,
	sum(productivity) as productivity,
	sum(approvedTI) as approvedTI,
	sum(approvedUAT) as approvedUAT
from
	(
	select
		left(ct.dt_criacao,8) as date, 
		1 as active,
		case when ct.Evidencia_Validacao_Tecnica <> 'N/A' then 1 else 0 end as activeTechnique,
		case when ct.Evidencia_Validacao_Cliente <> 'N/A' and uat = 'SIM' then 1 else 0 end as activeClient,
		0 as planned,
		0 as realized,
		0 as productivity,
		0 as approvedTI,
		0 as approvedUAT
	from ALM_CTs ct WITH (NOLOCK)
	where
		ct.path like '%@yyyy%'
		and ct.subprojeto = 'TRG2017' 
		--and ct.subprojeto = 'TRG@yyyy' 
		and ct.ciclo like ('%@mmm/@yy%')
		and @systemConditionCt
		and ct.Status_Exec_CT <> 'CANCELLED'
		and ct.dt_criacao <> ''

	union all

	select 
		left(ct.dt_planejamento,8) as date, 
		0 as active,
		0 as activeTechnique,
		0 as activeClient,
		1 as planned,
		0 as realized,
		0 as productivity,
		0 as approvedTI,
		0 as approvedUAT
	from ALM_CTs ct WITH (NOLOCK)
	where 
		ct.path like '%@yyyy%'
		and ct.subprojeto = 'TRG2017' 
		--and ct.subprojeto = 'TRG@yyyy' 
		and ct.ciclo like ('%@mmm/@yy%')
		and @systemConditionCt
		and ct.Status_Exec_CT <> 'CANCELLED'
		and substring(ct.dt_planejamento,7,2) + substring(ct.dt_planejamento,4,2) + substring(ct.dt_planejamento,1,2) <= convert(varchar(6), getdate(), 12)
		and ct.dt_planejamento <> ''

	union all

	select
		date,
		0 as active,
		0 as activeTechnique,
		0 as activeClient,
		0 as planned,
		1 as realized,
		0 as productivity,
		0 as approvedTI,
		0 as approvedUAT
	from
		(select distinct
			ct,
			convert(varchar(8),dateDate, 5) as date
		from
			(select 
				ex.ct,
				min(convert(datetime, ex.dt_execucao,5)) as dateDate
			from 
				alm_cts ct  WITH (NOLOCK)
				inner join alm_execucoes ex  WITH (NOLOCK)
					on ex.subprojeto = ct.subprojeto and
						ex.entrega = ct.entrega and
						ex.ct = ct.ct
			where
				ct.path like '%@yyyy%'
				and ct.subprojeto = 'TRG2017' 
				--and ct.subprojeto = 'TRG@yyyy' 
				and ct.ciclo like ('%@mmm/@yy%')
				and @systemConditionCt
				and ex.status = 'PASSED'
			group by
				ex.ct
			) aux
		) x

	union all

	select 
		left(ex.dt_execucao,8) as date,
		0 as active,
		0 as activeTechnique,
		0 as activeClient,
		0 as planned,
		0 as realized,
		1 as productivity,
		0 as approvedTI,
		0 as approvedUAT
	from 
		alm_cts ct WITH (NOLOCK)
		inner join alm_execucoes ex WITH (NOLOCK)
			on ex.subprojeto = ct.subprojeto
			and ex.entrega = ct.entrega
			and ex.ct = ct.ct
	where 
		ct.path like '%@yyyy%'
		and ct.subprojeto = 'TRG2017' 
		--and ct.subprojeto = 'TRG@yyyy' 
		and ct.ciclo like ('%@mmm/@yy%')
		-- and ex.status <> 'CANCELLED'
		and @systemConditionCt
		and ex.status in ('PASSED', 'FAILED')
		and ex.dt_execucao <> ''

	union all

	select 
		left((select top 1
			dt_alteracao
		from 
			ALM_Historico_Alteracoes_Campos h
		where 
			h.subprojeto = ct.subprojeto and
			h.entrega = ct.entrega and
			h.tabela = 'TESTCYCL' and 
			h.tabela_id =  ct.ct and 
			h.campo = '(EVIDÊNCIA) VALIDAÇÃO TÉCNICA' and
			h.novo_valor = 'VALIDADO' 
		order by 
			substring(dt_alteracao,7,2)+substring(dt_alteracao,4,2)+substring(dt_alteracao,1,2) desc
		),8) as date,
		0 as active,
		0 as activeTechnique,
		0 as activeClient,
		0 as planned,
		0 as realized,
		0 as productivity,
		1 as approvedTI,
		0 as approvedUAT
	from 
		alm_cts ct WITH (NOLOCK)
	where
		ct.path like '%@yyyy%'
		and ct.subprojeto = 'TRG2017' 
		--and ct.subprojeto = 'TRG@yyyy' 
		and ct.ciclo like ('%@mmm/@yy%')
		and @systemConditionCt
		and ct.Status_Exec_CT <> 'CANCELLED'
		and ct.Evidencia_Validacao_Tecnica = 'VALIDADO'

	union all

	select 
		left((select top 1
			dt_alteracao
		from 
			ALM_Historico_Alteracoes_Campos h
		where 
			h.subprojeto = ct.subprojeto and
			h.entrega = ct.entrega and
			h.tabela = 'TESTCYCL' and 
			h.tabela_id =  ct.ct and 
			h.campo = '(EVIDÊNCIA) VALIDAÇÃO CLIENTE' and
			h.novo_valor = 'VALIDADO'
		order by 
			substring(dt_alteracao,7,2)+substring(dt_alteracao,4,2)+substring(dt_alteracao,1,2) desc
		),8) as date,
		0 as active,
		0 as activeTechnique,
		0 as activeClient,
		0 as planned,
		0 as realized,
		0 as productivity,
		0 as approvedTI,
		1 as approvedUAT
	from 
		alm_cts ct WITH (NOLOCK)
	where
		ct.path like '%@yyyy%'
		and ct.subprojeto = 'TRG2017' 
		--and ct.subprojeto = 'TRG@yyyy' 
		and ct.ciclo like ('%@mmm/@yy%')
		and @systemConditionCt
		and ct.Status_Exec_CT = 'PASSED'
		and ct.UAT = 'SIM'
		and ct.Evidencia_Validacao_Cliente = 'VALIDADO'
	) Aux
group by
	date
order by
	2

select top 30
	t.date,
    t.dateOrder,
	t.active, 
	t.activeTechnique, 
	t.activeClient, 
	t.planned, 
	t.realized,
	t.productivity,
	(case when t.realized > t.planned then 0 else t.planned - t.realized end) as GAP,
	t.approvedTI,
	t.approvedUAT,

	SUM(t2.active) as activeAcum,
	SUM(t2.activeTechnique) as activeTechniqueAcum,
	SUM(t2.activeClient) as activeClientAcum,
	SUM(t2.planned) as plannedAcum,
	SUM(t2.realized) as realizedAcum,
	SUM(t2.productivity) as productivityAcum,
	(case when sum(t2.realized) > sum(t2.planned) then 0 else sum(t2.planned) - sum(t2.realized) end) as GAPAcum,
	SUM(t2.approvedTI) as approvedTIAcum,
	SUM(t2.approvedUAT) as approvedUATAcum,

	round(convert(float, SUM(t2.planned)) / (case when SUM(t2.active) <> 0 then SUM(t2.active) else 1 end) * 100,2) as percPlanned,

	round(convert(float, SUM(t2.realized)) / (case when SUM(t2.active) <> 0 then SUM(t2.active) else 1 end) * 100,2) as percRealized,

	round(convert(float, (case when sum(t2.realized) > sum(t2.planned) then 0 else sum(t2.planned) - sum(t2.realized) end)) / 
							(case when SUM(t2.planned) <> 0 then SUM(t2.planned) else 1 end) * 100,2) as percGAP,

	round(convert(float, SUM(t2.approvedTI)) / (case when SUM(t2.activeTechnique) <> 0 then SUM(t2.activeTechnique) else 1 end) * 100,2) as percApprovedTI,

	round(convert(float, SUM(t2.approvedUAT)) / (case when SUM(t2.activeClient) <> 0 then SUM(t2.activeClient) else 1 end) * 100,2) as percApprovedUAT
from 
	@t t inner join @t t2 
	    on t.dateOrder >= t2.dateOrder
group by 
	t.dateOrder, 
	t.date,
	t.active, 
	t.activeTechnique, 
	t.activeClient, 
	t.planned, 
	t.realized,
	t.productivity,
	t.approvedTI,
	t.approvedUAT
order by 
	t.dateOrder desc
