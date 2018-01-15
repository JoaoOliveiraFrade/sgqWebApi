declare @t table (
	date varchar(5), 
	dateOrder varchar(5), 
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
	substring(date,4,2)+substring(date,1,2) as dateOrder,
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
		substring(dt_criacao,4,5) as date, 
		1 as active,
		case when Evidencia_Validacao_Tecnica <> 'N/A' then 1 else 0 end as activeTechnique,
		case when uat = 'SIM' and Evidencia_Validacao_Cliente <> 'N/A' then 1 else 0 end as activeClient,
		0 as planned,
		0 as realized,
		0 as productivity,
		0 as approvedTI,
		0 as approvedUAT
	from ALM_CTs WITH (NOLOCK)
	where 
		subprojeto = '@subproject' and
		entrega = '@delivery' and
        iterations in (@iterations) and
		Status_Exec_CT <> 'CANCELLED' and
		dt_criacao <> ''

	union all

	select 
		substring(dt_planejamento,4,5) as date, 
		0 as active,
		0 as activeTechnique,
		0 as activeClient,
		1 as planned,
		0 as realized,
		0 as productivity,
		0 as approvedTI,
		0 as approvedUAT
	from ALM_CTs WITH (NOLOCK)
	where 
		subprojeto = '@subproject' and
		entrega = '@delivery' and
        iterations in (@iterations) and
		Status_Exec_CT <> 'CANCELLED' and
		substring(dt_planejamento,7,2) + substring(dt_planejamento,4,2) + substring(dt_planejamento,1,2) <= convert(varchar(6), getdate(), 12) and
		dt_planejamento <> ''

	union all

    select
	    substring(date,4,5) as date, 
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
			    alm_cts cts  WITH (NOLOCK)
			    inner join alm_execucoes ex  WITH (NOLOCK)
				    on ex.subprojeto = cts.subprojeto and
					    ex.entrega = cts.entrega and
					    ex.ct = cts.ct
		    where
			    cts.subprojeto = '@subproject' and
			    cts.entrega = '@delivery' and
                cts.iterations in (@iterations) and
			    ex.status = 'PASSED'
		    group by
			    ex.ct
		    ) aux
	    ) x

	union all

	select 
		substring(ex.dt_execucao,4,5) as date, 
		0 as active,
		0 as activeTechnique,
		0 as activeClient,
		0 as planned,
		0 as realized,
		1 as productivity,
		0 as approvedTI,
		0 as approvedUAT
	from 
		alm_cts cts  WITH (NOLOCK)
		inner join alm_execucoes ex  WITH (NOLOCK)
			on ex.subprojeto = cts.subprojeto and
                ex.entrega = cts.entrega and
                ex.ct = cts.ct
	where 
		cts.subprojeto = '@subproject' and
		cts.entrega = '@delivery' and
        cts.iterations in (@iterations) and
		ex.status in ('PASSED', 'FAILED') and
		ex.dt_execucao <> ''

	union all

	select 
		substring((select top 1
			dt_alteracao
		from 
			ALM_Historico_Alteracoes_Campos h
		where 
			h.subprojeto = cts.subprojeto and
			h.entrega = cts.entrega and
			h.tabela = 'TESTCYCL' and 
			h.tabela_id =  cts.ct and 
			h.campo = '(EVIDÊNCIA) VALIDAÇÃO TÉCNICA' and
			h.novo_valor = 'VALIDADO'
		order by 
			substring(dt_alteracao,7,2)+substring(dt_alteracao,4,2)+substring(dt_alteracao,1,2) desc
		),4,5) as date,
		0 as active,
		0 as activeTechnique,
		0 as activeClient,
		0 as planned,
		0 as realized,
		0 as productivity,
		1 as approvedTI,
		0 as approvedUAT
	from 
		alm_cts cts WITH (NOLOCK)
	where
		subprojeto = '@subproject' and
		entrega = '@delivery' and
        iterations in (@iterations) and
		Status_Exec_CT <> 'CANCELLED' and 
		Evidencia_Validacao_Tecnica = 'VALIDADO'

	union all

	select 
		substring((select top 1
			dt_alteracao
		from 
			ALM_Historico_Alteracoes_Campos h
		where 
			h.subprojeto = cts.subprojeto and
			h.entrega = cts.entrega and
			h.tabela = 'TESTCYCL' and 
			h.tabela_id =  cts.ct and 
			h.campo = '(EVIDÊNCIA) VALIDAÇÃO CLIENTE' and
			h.novo_valor = 'VALIDADO'
		order by 
			substring(dt_alteracao,7,2)+substring(dt_alteracao,4,2)+substring(dt_alteracao,1,2) desc
		),4,5) as date,
		0 as active,
		0 as activeTechnique,
		0 as activeClient,
		0 as planned,
		0 as realized,
		0 as productivity,
		0 as approvedTI,
		1 as approvedUAT
	from 
		alm_cts cts WITH (NOLOCK)
	where
		subprojeto = '@subproject' and
		entrega = '@delivery' and
        iterations in (@iterations) and
		Status_Exec_CT = 'PASSED' and 
		UAT = 'SIM' and
		Evidencia_Validacao_Cliente = 'VALIDADO'
	) Aux
group by
	date
order by
	2

select
	convert(varchar, cast(substring('@subproject',4,8) as int)) + ' ' + convert(varchar,cast(substring('@delivery',8,8) as int)) as subDel,
	'@subproject' as subproject,
	'@delivery' as delivery,
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
	t.dateOrder
