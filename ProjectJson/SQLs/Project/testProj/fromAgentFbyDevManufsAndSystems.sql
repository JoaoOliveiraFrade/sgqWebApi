declare @t table (
	devManuf varchar(50), 
	system varchar(50),
	subproject varchar(11),
	delivery varchar(15)
)
insert into @t 
select distinct
	rtrim(ltrim(substring(queue, len(queue) - charindex('-', reverse(queue)) + 2, 50))) as devManuf
	,rtrim(ltrim(substring(queue, 1, len(queue) - charindex('-', reverse(queue))))) as system
	,subproject
	,delivery
from
	(
		select distinct
			replace(dt.encaminhado_para,'–', '-') as queue
			,d.subprojeto as subproject
			,d.entrega as delivery
		from
			alm_defeitos d
			left join alm_defeitos_tempos dt
			on dt.subprojeto = d.subprojeto and 
				dt.entrega = d.entrega and 
				dt.defeito = d.defeito
		where
			d.origem like '%Construção%'
			and d.status_atual = 'Closed'
			and (d.ciclo like '%TI%' or d.ciclo like '%UAT%' or d.ciclo like '%TRG%' or d.ciclo like '%DEV%')
			and dt.status in ('IN_PROGRESS', 'PENDENT (PROGRESS)', 'REOPEN')
	) aux10


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
        replace(replace(replace(replace(replace(biti_s.estado,'CONSOLIDAÇÃO E APROVAÇÃO DO PLANEJAMENTO','CONS/APROV. PLAN'),'PLANEJAMENTO','PLANEJ.'),'DESENHO DA SOLUÇÃO','DES.SOL'),'VALIDAÇÃO','VALID.'),'AGUARDANDO','AGUAR.') as state,
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
				aux.subproject,
				aux.delivery
			from
				(
					select distinct
						devManuf
						,system
						,subproject
						,delivery
					from
						@t
					where
						devManuf not in ('', 'OI','LÍDER TÉCNICO', 'ÁREA DE NEGÓCIOS', 'ÁREA USUÁRIA', 'AUTOMAÇÃO', 'ENGENHARIA', 'OI (API)', 'OI (APLICATIVO)') 

				) as aux

				inner join ALM_Projetos as alm_p with (NOLOCK) 
					on alm_p.Subprojeto = aux.subproject and
						alm_p.Entrega = aux.delivery

				inner join biti_subprojetos biti_s WITH (NOLOCK)
					on biti_s.id = alm_p.subprojeto
			where
				devManuf in (@devManufs) and
				system in (@systems)
			) aux2

				on aux2.subproject = sgq_p.subproject and
				   aux2.delivery = sgq_p.delivery

    where 
		sgq_p.currentReleaseYear is not null
	) aux
order by
    subproject,
    delivery
