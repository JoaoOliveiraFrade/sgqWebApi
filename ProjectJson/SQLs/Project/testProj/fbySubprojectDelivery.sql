select
	id,
	subproject,
	delivery,
	subDel,
	name,
	objective,
	classification,
	state,
	release,
	GP,
	N3,
	UN,
	trafficLight,
	rootCause,
	actionPlan,
	informative,
	attentionPoints,
	attentionPointsOfIndicators,
	IterationsActive,
	IterationsSelected,

	(select count(*) 
	from ALM_CTs WITH (NOLOCK)
	where 
		subprojeto = aux.subproject and
		entrega = aux.delivery and
		Status_Exec_CT <> 'CANCELLED'
	) as total,

	(select count(*) 
	from ALM_CTs WITH (NOLOCK)
	where 
		subprojeto = aux.subproject and
		entrega = aux.delivery and
		Status_Exec_CT <> 'CANCELLED' and
		substring(dt_planejamento,7,2) + substring(dt_planejamento,4,2) + substring(dt_planejamento,1,2) <= convert(varchar(6), getdate(), 12) and
		dt_planejamento <> ''
	) as planned,

	(select count(*)
	from ALM_CTs WITH (NOLOCK)
	where 
		subprojeto = aux.subproject and
		entrega = aux.delivery and
		Status_Exec_CT = 'PASSED' and 
		dt_execucao <> ''
	) as realized,

	(select 
		(case when sum(planned) - sum(realized) >= 0 then sum(planned) - sum(realized) else 0 end) as GAP
	from
		(
		select 
			substring(dt_planejamento,4,5) as date, 
			1 as planned,
			0 as realized
		from ALM_CTs WITH (NOLOCK)
		where 
			subprojeto = aux.subproject and
			entrega = aux.delivery and
			Status_Exec_CT <> 'CANCELLED' and
			substring(dt_planejamento,7,2) + substring(dt_planejamento,4,2) + substring(dt_planejamento,1,2) <= convert(varchar(6), getdate(), 12) and
			dt_planejamento <> ''

		union all

		select 
			substring(dt_execucao,4,5) as date, 
			0 as planned,
			1 as realized
		from ALM_CTs WITH (NOLOCK)
		where 
			subprojeto = aux.subproject and
			entrega = aux.delivery and
			Status_Exec_CT = 'PASSED' and 
			dt_execucao <> ''
		) Aux
	) as gap
from
	(
    select 
        sgq_p.id,
        sgq_p.subproject as subproject,
        sgq_p.delivery as delivery,
        convert(varchar, cast(substring(sgq_p.subproject,4,8) as int)) + ' ' + convert(varchar,cast(substring(sgq_p.delivery,8,8) as int)) as subDel,
        biti_s.nome as name,
        biti_s.objetivo as objective,
        biti_s.classificacao_nome as classification,
        replace(replace(replace(replace(replace(biti_s.estado,'CONSOLIDA플O E APROVA플O DO PLANEJAMENTO','CONS/APROV. PLAN'),'PLANEJAMENTO','PLANEJ.'),'DESENHO DA SOLU플O','DES.SOL'),'VALIDA플O','VALID.'),'AGUARDANDO','AGUAR.') as state,
        (select Sigla from sgq_meses m where m.id = sgq_p.currentReleaseMonth) + ' ' + convert(varchar, sgq_p.currentReleaseYear) as release,
        biti_s.Gerente_Projeto as GP,
        biti_s.Gestor_Do_Gestor_LT as N3,
        biti_s.UN as UN,
        sgq_p.trafficLight as trafficLight,
        sgq_p.rootCause as rootCause,
        sgq_p.actionPlan as actionPlan,
        sgq_p.informative as informative,
        sgq_p.attentionPoints as attentionPoints,
        sgq_p.attentionPointsIndicators as attentionPointsOfIndicators,
        sgq_p.IterationsActive,
        sgq_p.IterationsSelected
    from 
        sgq_projects sgq_p
        inner join alm_projetos alm_p WITH (NOLOCK)
        on alm_p.subprojeto = sgq_p.subproject and
           alm_p.entrega = sgq_p.delivery

        inner join biti_subprojetos biti_s WITH (NOLOCK)
        on biti_s.id = sgq_p.subproject

		inner join
		(
		select distinct
			aux.Subprojeto,
			aux.Entrega
		from
			(select distinct
				cts.Subprojeto,
				cts.Entrega,
				cts.fabrica_teste as testManuf, 
				cts.Sistema as system 
				from
				ALM_CTs cts with (NOLOCK) 

			union all

			select distinct
				d.Subprojeto,
				d.Entrega,
				d.fabrica_teste as testManuf, 

				d.Sistema_Defeito as system 
			from
				ALM_Defeitos d with (NOLOCK) 
			) as aux
			inner join ALM_Projetos as alm_p with (NOLOCK) 
				on alm_p.Subprojeto = aux.Subprojeto and
					alm_p.Entrega = aux.Entrega
			inner join biti_subprojetos biti_s WITH (NOLOCK)
				on biti_s.id = alm_p.subprojeto
		where
			aux.Subprojeto + aux.Entrega in (@projects)
		) aux2
		on aux2.subprojeto = sgq_p.subproject and
		   aux2.entrega = sgq_p.delivery

    where 
		sgq_p.currentReleaseYear is not null
	) aux
order by
    subproject,
    delivery
