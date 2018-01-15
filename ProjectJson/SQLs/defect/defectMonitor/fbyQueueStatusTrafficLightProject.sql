select
	id, trafficLight, provider, subDel, subproject, delivery, system, queue, origin, status, severity, agingHours, timeLastQueueHours, qtyImpactCT, pingPong
from
	(
		select
			case when severity = 'HIG'
				then
					case when round(timeLastQueueHours,0) <= 1  then 'VERDE'
						when round(timeLastQueueHours,0) >= 2 and round(timeLastQueueHours,0) <= 3 then 'AMARELO'
						when round(timeLastQueueHours,0) >= 4 and round(timeLastQueueHours,0) <= 5 then 'VERMELHO'
						when round(timeLastQueueHours,0) >= 6 then 'ROXO'
					end 
				when severity = 'MED'
				then
					case when round(timeLastQueueHours,0) <= 4  then 'VERDE'
						when round(timeLastQueueHours,0) >= 5 and round(timeLastQueueHours,0) <= 7 then 'AMARELO'
						when round(timeLastQueueHours,0) >= 8 and round(timeLastQueueHours,0) <= 10 then 'VERMELHO'
						when round(timeLastQueueHours,0) >= 11 then 'ROXO'
					end 
				when severity = 'LOW'
				then
					case when round(timeLastQueueHours,0) <= 11  then 'VERDE'
						when round(timeLastQueueHours,0) >= 12 and round(timeLastQueueHours,0) <= 15 then 'AMARELO'
						when round(timeLastQueueHours,0) >= 16 and round(timeLastQueueHours,0) <= 19 then 'VERMELHO'
						when round(timeLastQueueHours,0) >= 20 then 'ROXO'
					end 
			end as trafficLight
			,rtrim(ltrim(substring(queue, len(queue) - charindex('-', reverse(queue)) + 2, 50))) as provider
			,convert(varchar, cast(substring(subproject,4,8) as int)) + ' ' + convert(varchar,cast(substring(delivery,9,8) as int)) as subDel
			,subproject
			,delivery
			,id
			,rtrim(ltrim(substring(queue, 1, len(queue) - charindex('-', reverse(queue))))) as system
			,queue
			,origin
			,status
			,severity
			,isnull(round(
				cast(
						(select Sum(Tempo_Util) 
						from ALM_Defeitos_Tempos dt WITH (NOLOCK)
						where 
							dt.Subprojeto = a1.subproject and 
							dt.Entrega = a1.delivery and 
							dt.Defeito = a1.id)
				as float) / 60, 1
			),0) as agingHours
			,timeLastQueueHours
			,qtyImpactCT
			,pingPong
		from
			(
				select
					replace(encaminhado_para,'–', '-') as queue
					,subprojeto as subproject
					,entrega as delivery
					,defeito as id
					,origem as origin
					,status_atual as status
					,substring(severidade,3,3) as severity
					,Qtd_CTs_Impactados as qtyImpactCT
					,isnull(Ping_Pong,0) as pingPong
					,isnull(round(
						cast(
							(select sum(Teste)	
								from(
								select *,
								rank() OVER (ORDER BY convert(datetime, Data, 5) desc) as id2
									from(
									select
											*,     
																
											case when Id = 1 then tempo_util
											else
												(case when Id >=
														(
															select top 1 id
																
															from
															(
															select 
																rank() OVER (ORDER BY convert(datetime, dt_ate, 5) desc) as id,
																DT_ATE as Data,
																status, 
																encaminhado_para, 
																tempo_util
															from
																alm_defeitos_tempos dt
															where
																	dt.Subprojeto = d.Subprojeto and 
																		dt.Entrega = d.Entrega and 
																		dt.Defeito = d.Defeito
																	
															) a
																where  
																a.encaminhado_para = 
																	(select top 1
																			encaminhado_para
																	from
																			alm_defeitos_tempos dt
																	where
																		dt.Subprojeto = d.Subprojeto and 
																			dt.Entrega = d.Entrega and 
																			dt.Defeito = d.Defeito
																			 
																	order by
																			convert(datetime, dt_ate, 5) desc) and
																			status in ('REJECTED','ON_RETEST', 'PENDENT (RETEST)') )
												then '' 
												else tempo_util end)
												end as Teste
		
									from
											(
											select 
												rank() OVER (ORDER BY convert(datetime, dt_ate, 5) desc) as id,
												dt_ate as Data,
												status, 
												encaminhado_para, 
												tempo_util
											from
												alm_defeitos_tempos dt
											where
													dt.Subprojeto = d.Subprojeto and 
														dt.Entrega = d.Entrega and 
														dt.Defeito = d.Defeito
																
											) a
									where  
											a.encaminhado_para = 
												(select top 1
														encaminhado_para
												from
														alm_defeitos_tempos dt
												where
															dt.Subprojeto = d.Subprojeto and 
																dt.Entrega = d.Entrega and 
																dt.Defeito = d.Defeito
																			
												order by
														convert(datetime, dt_ate, 5) desc))b)c
														where id <= id2)
									as float) / 60, 1),0) as timeLastQueueHours
				from 
					alm_defeitos d WITH (NOLOCK)
				where
					status_atual not in('CLOSED', 'CANCELLED')
					@queueFilter
					@statusFilter
					@projectFilter
			) a1
	) a2
	@trafficLightFilter
order by 
	2
	,subDel
	,id
