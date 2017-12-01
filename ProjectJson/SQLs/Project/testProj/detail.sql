select
	id,
	subproject,
	delivery,
	subprojectDelivery,
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
        sgq_projects.id,
        sgq_projects.subproject as subproject,
        sgq_projects.delivery as delivery,
        convert(varchar, cast(substring(sgq_projects.subproject,4,8) as int)) + ' ' + convert(varchar,cast(substring(sgq_projects.delivery,8,8) as int)) as subprojectDelivery,
        biti_subprojetos.nome as name,
        biti_subprojetos.objetivo as objective,
        biti_subprojetos.classificacao_nome as classification,
        replace(replace(replace(replace(replace(biti_subprojetos.estado,'CONSOLIDAÇÃO E APROVAÇÃO DO PLANEJAMENTO','CONS/APROV. PLAN'),'PLANEJAMENTO','PLANEJ.'),'DESENHO DA SOLUÇÃO','DES.SOL'),'VALIDAÇÃO','VALID.'),'AGUARDANDO','AGUAR.') as state,
        (select Sigla from sgq_meses m where m.id = SGQ_Releases_Entregas.release_mes) + ' ' + convert(varchar, SGQ_Releases_Entregas.release_ano) as release,
        biti_subprojetos.Gerente_Projeto as GP,
        biti_subprojetos.Gestor_Do_Gestor_LT as N3,
        biti_subprojetos.UN as UN,
        sgq_projects.trafficLight as trafficLight,
        sgq_projects.rootCause as rootCause,
        sgq_projects.actionPlan as actionPlan,
        sgq_projects.informative as informative,
        sgq_projects.attentionPoints as attentionPoints,
        sgq_projects.attentionPointsIndicators as attentionPointsOfIndicators,
        sgq_projects.IterationsActive,
        sgq_projects.IterationsSelected
    from 
        sgq_projects
        inner join alm_projetos WITH (NOLOCK)
        on alm_projetos.subprojeto = sgq_projects.subproject and
            alm_projetos.entrega = sgq_projects.delivery
        left join biti_subprojetos WITH (NOLOCK)
        on biti_subprojetos.id = sgq_projects.subproject
            left join SGQ_Releases_Entregas WITH (NOLOCK)
        on SGQ_Releases_Entregas.subprojeto = sgq_projects.subproject and
            SGQ_Releases_Entregas.entrega = sgq_projects.delivery and
            SGQ_Releases_Entregas.id = (select top 1 re2.id from SGQ_Releases_Entregas re2 
                                        where re2.subprojeto = SGQ_Releases_Entregas.subprojeto and 
                                            re2.entrega = SGQ_Releases_Entregas.entrega 
                                        order by re2.release_ano desc, re2.release_mes desc)
    where 
        sgq_projects.subproject = '@subproject' and
        sgq_projects.delivery = '@delivery'
	) aux
order by
    subproject,
    delivery
