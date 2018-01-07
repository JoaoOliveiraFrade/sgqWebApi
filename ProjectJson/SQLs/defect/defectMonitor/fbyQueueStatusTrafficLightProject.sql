select
	rtrim(ltrim(substring(agent, len(agent) - charindex('-', reverse(agent)) + 2, 50))) as provider
	,convert(varchar, cast(substring(subproject,4,8) as int)) + ' ' + convert(varchar,cast(substring(delivery,9,8) as int)) as subprojectDelivery
	,defect
	,rtrim(ltrim(substring(agent, 1, len(agent) - charindex('-', reverse(agent))))) as system
	,agent
	,origin
	,status
	,severity
	,round(
		cast(
				(select Sum(Tempo_Util) 
				from ALM_Defeitos_Tempos dt WITH (NOLOCK)
				where 
					dt.Subprojeto = a1.subproject and 
					dt.Entrega = a1.delivery and 
					dt.Defeito = a1.defect)
		as float) / 60, 1
	) as agingHours
	,qtyImpactCT
	,pingPong
from
	(
		select
			replace(encaminhado_para,'–', '-') as agent
			,subprojeto as subproject
			,entrega as delivery
			,defeito as defect
			,origem as origin
			,status_atual as status
			,substring(severidade,3,3) as severity
			,Qtd_CTs_Impactados as qtyImpactCT
			,Ping_Pong as pingPong
		from 
			alm_defeitos WITH (NOLOCK)
		where
			status_atual not in('CLOSED', 'CANCELLED') 
	) a1
order by 
	1
	,subprojectDelivery
	,defect
