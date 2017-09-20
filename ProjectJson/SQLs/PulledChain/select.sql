select
	id,
	subproject,
	delivery,
	subprojectDelivery,
	name,
	releaseClarity,
	nextRelease,

	priorityGlobal,
	state,
	category,
	productiveChain,
	UN,
	Typification,
	workFrontState,
	topic,
	RT,
	deliveryState,
	statusCategoryORL,

	readyStrategyTestingAndContracting,
	statusStrategyTestingAndContracting,
	dtUpdateStrategyTestingAndContracting,
	dtStartStrategyTestingAndContracting,
	dtEndStrategyTestingAndContracting,
	agingStrategyTestingAndContracting,
	case when agingStrategyTestingAndContracting > 0
		then convert(varchar, agingStrategyTestingAndContracting / 540) + 'd ' + convert(varchar, (agingStrategyTestingAndContracting % 540) / 60) + 'h' 
		else ''
	end as agingStrategyTestingAndContractingFormated,
	case when agingLTStrategyTestingAndContracting > 0
		then convert(varchar, agingLTStrategyTestingAndContracting / 540) + 'd ' + convert(varchar, (agingLTStrategyTestingAndContracting % 540) / 60) + 'h' 
		else ''
	end as agingLTStrategyTestingAndContractingFormated,

	readyTimeline,
	statusTimeline,
	dtUpdateTimeLine,
	dtStartTimeLine,
	dtEndTimeLine,
	agingTimeline,
	case when agingTimeline > 0
		then convert(varchar, agingTimeline / 540) + 'd ' + convert(varchar, (agingTimeline % 540) / 60) + 'h' 
		else ''
	end as agingTimelineFormated,
	case when agingLTTimeline > 0
		then convert(varchar, agingLTTimeline / 540) + 'd ' + convert(varchar, (agingLTTimeline % 540) / 60) + 'h' 
		else ''
	end as agingLTTimelineFormated,

	readyTestPlan,
	statusTestPlan,
	dtUpdateTestPlan,
	dtStartTestPlan,
	dtEndTestPlan,
	agingTestPlan,
	case when agingTestPlan > 0
		then convert(varchar, agingTestPlan / 540) + 'd ' + convert(varchar, (agingTestPlan % 540) / 60) + 'h' 
		else ''
	end as agingTestPlanFormated,
	case when agingLTTestPlan > 0
		then convert(varchar, agingLTTestPlan / 540) + 'd ' + convert(varchar, (agingLTTestPlan % 540) / 60) + 'h' 
		else ''
	end as agingLTTestPlanFormated,

	dtDeliveryTestPlan
from
	(
		select 
			sgq_p.id,
			sgq_p.subproject,
			sgq_p.delivery,
			convert(varchar, cast(substring(sgq_p.subproject,4,8) as int)) + ' ' + convert(varchar,cast(substring(sgq_p.delivery,8,8) as int)) as subprojectDelivery,
			sub.nome as name,

			case when sgq_p.clarityReleaseYear is not null
				then (select m.sigla from sgq_meses m where m.id = sgq_p.clarityReleaseMonth) + '/' + convert(varchar, sgq_p.clarityReleaseYear)
				else ''
			end as releaseClarity,

			case when  sgq_p.clarityReleaseMonth = month(DATEADD(month, 2, getDate())) and sgq_p.clarityReleaseYear = year(DATEADD(month, 2, getDate()))
				then 'S'
				else 'N' 
			end as nextRelease,

			sgq_p.priorityGlobal,
			sgq_p.state,
			sgq_p.category,
			sub.cadeia_produtiva as productiveChain,
			sgq_p.UN,
			sub.tipificacao as Typification,
			sgq_p.workFrontState,
			sgq_p.topic,
			sgq_p.RT,
			sgq_p.deliveryState,
			sgq_p.statusCategoryORL,

			readyStrategyTestingAndContracting,
			statusStrategyTestingAndContracting,
			dtUpdateStrategyTestingAndContracting,
			dtStartStrategyTestingAndContracting,
			dtEndStrategyTestingAndContracting,
			case when isnull(dtStartStrategyTestingAndContracting,'') <> '' and isnull(dtEndStrategyTestingAndContracting,'') <> ''
					then dbo.WorkTime(convert(datetime, dtStartStrategyTestingAndContracting, 5), convert(datetime, dtEndStrategyTestingAndContracting, 5))
				when isnull(dtStartStrategyTestingAndContracting,'') <> '' and isnull(dtEndStrategyTestingAndContracting,'') = ''
					then dbo.WorkTime(convert(datetime, dtStartStrategyTestingAndContracting, 5), getDate())
				else 0
			end agingStrategyTestingAndContracting,

			case when isnull(dtEndStrategyTestingAndContracting,'') <> '' and isnull(dtUpdateStrategyTestingAndContracting,'') <> '' and statusStrategyTestingAndContracting = 'REALIZADO'
					then dbo.WorkTime(convert(datetime, dtEndStrategyTestingAndContracting, 5), convert(datetime, dtUpdateStrategyTestingAndContracting, 5))
				when isnull(dtEndStrategyTestingAndContracting,'') <> '' and (isnull(dtUpdateStrategyTestingAndContracting,'') = '' or statusStrategyTestingAndContracting <> 'REALIZADO')
					then dbo.WorkTime(convert(datetime, dtEndStrategyTestingAndContracting, 5), getDate())
				else 0
			end agingLTStrategyTestingAndContracting,

			readyTimeline,
			statusTimeline,
			dtUpdateTimeLine,
			dtStartTimeLine,
			dtEndTimeLine,
			case when isnull(dtStartTimeLine,'') <> '' and isnull(dtEndTimeLine,'') <> ''
					then dbo.WorkTime(convert(datetime, dtStartTimeLine, 5), convert(datetime, dtEndTimeLine, 5))
				when isnull(dtStartTimeLine,'') <> '' and isnull(dtEndTimeLine,'') = ''
					then dbo.WorkTime(convert(datetime, dtStartTimeLine, 5), getDate())
				else ''
			end agingTimeline,
			case when isnull(dtEndTimeline,'') <> '' and isnull(dtUpdateTimeline,'') <> '' and statusTimeline = 'REALIZADO'
					then dbo.WorkTime(convert(datetime, dtEndTimeline, 5), convert(datetime, dtUpdateTimeline, 5))
				when isnull(dtEndTimeline,'') <> '' and (isnull(dtUpdateTimeline,'') = '' or statusTimeline <> 'REALIZADO')
					then dbo.WorkTime(convert(datetime, dtEndTimeline, 5), getDate())
				else 0
			end agingLTTimeline,

			readyTestPlan,
			statusTestPlan,
			dtUpdateTestPlan,
			dtStartTestPlan,
			dtEndTestPlan,
			case when isnull(dtStartTestPlan,'') <> '' and isnull(dtEndTestPlan,'') <> '' 
					then dbo.WorkTime(convert(datetime, dtStartTestPlan, 5), convert(datetime, dtEndTestPlan, 5))
				when isnull(dtStartTestPlan,'') <> '' and isnull(dtEndTestPlan,'') = '' 
					then dbo.WorkTime(convert(datetime, dtStartTestPlan, 5),  getDate())
				else 0
			end agingTestPlan,
			case when isnull(dtEndTestPlan,'') <> '' and isnull(dtUpdateTestPlan,'') <> '' and statusTestPlan = 'REALIZADO'
					then dbo.WorkTime(convert(datetime, dtEndTestPlan, 5), convert(datetime, dtUpdateTestPlan, 5))
				when isnull(dtEndTestPlan,'') <> '' and (isnull(dtUpdateTestPlan,'') = '' or statusTestPlan <> 'REALIZADO')
					then dbo.WorkTime(convert(datetime, dtEndTestPlan, 5), getDate())
				else 0
			end agingLTTestPlan,

			sgq_p.dtDeliveryTestPlan
		from 
			sgq_projects sgq_p
			left join BITI_Subprojetos sub
			  on sub.id = sgq_p.subproject
			left join BITI_entregas ent
			  on ent.id = sgq_p.delivery
		where
			--sgq_p.subproject = 'PRJ00015285' and delivery = 'ENTREGA00005264' and
			sub.estado <> 'CANCELADO' and
			ent.estado <> 'ENTREGA CANCELADA' and
			sgq_p.RT in ('CARLOS HENRIQUE', 'SORAIA CASAGRANDE', 'CLAUDIA CARVALHO', '') and
			sgq_p.subproject in (
									select distinct ft.subprojeto
									from BITI_Frentes_Trabalho ft
									where ft.subprojeto = sgq_p.subproject and
											ft.area in ('TESTES E RELEASE', 'SUPORTE E PROJETOS', 'TRANSFORMACAO DE BSS') and
											ft.estado not in ('CANCELADA', 'CANCELADA SEM DESENHO', 'PARTICIPAÇÃO RECUSADA') and
											ft.sistema_nome = 'NÃO INFORMADO'
								)

	) as aux
order by
	priorityGlobal, 
	subproject,
	delivery