select
	sistema as system,
	SUM(Ativo) as active,
	
	SUM(Prev_Dia) as planned,
	SUM(Real_Dia) as realized,
	SUM(Real_Dia) - SUM(Prev_Dia)  as GAP,
	--cast(round((CONVERT(float, SUM(Prev_Dia)) / (case when SUM(Ativo_Dia) = 0 then 1 else SUM(Ativo_Dia) end) * 100), 1) as varchar) + '%' as Perc_Prev_Dia,
	--cast(round((CONVERT(float, SUM(Real_Dia)) / (case when SUM(Ativo_Dia) = 0 then 1 else SUM(Ativo_Dia) end) * 100), 1) as varchar) + '%' as Perc_Real_Dia,
	cast(round(cast(SUM(Real_Dia) - SUM(Prev_Dia) as float) / (case when SUM(Prev_Dia) = 0 then 1 else SUM(Prev_Dia) end) * 100, 1) as varchar) + '%' as percGAP,

	SUM(Prev_Acum) as plannedAcum,
	SUM(Real_Acum) as realizedAcum,
	SUM(Real_Acum) - SUM(Prev_Acum)  as GAPAcum,
	--cast(round((CONVERT(float, SUM(Prev_Acum)) / (case when SUM(Ativo_Dia) = 0 then 1 else SUM(Ativo_Dia) end) * 100), 1) as varchar) + '%' as Perc_Prev_Acumulado,
	--cast(round((CONVERT(float, SUM(Real_Acum)) / (case when SUM(Ativo_Dia) = 0 then 1 else SUM(Ativo_Dia) end) * 100), 1) as varchar) + '%' as Perc_Real_Acumulado,
	cast(round(cast(SUM(Real_Acum) - SUM(Prev_Acum) as float) / (case when SUM(Prev_Acum) = 0 then 1 else SUM(Prev_Acum) end) * 100, 1) as varchar) + '%' as percGAPAcum
from   
    (
    select 
		substring(path, CHARINDEX('2018', path) + 11,30) as Sistema,
        case when (Status_Exec_CT <> 'CANCELLED')  then 1 else 0 end as Ativo,

		case when (Status_Exec_CT <> 'CANCELLED') and 
					(substring(Dt_Criacao,7,2) + substring(Dt_Criacao,4,2) + substring(Dt_Criacao,1,2)) <=
					right(convert(varchar(30),convert(datetime,convert(varchar(10), getdate(), 103) + ' 23:59:59',103),112),6) 
				then 1 
				else 0 
		end as Ativo_Dia,

		case when (Status_Exec_CT <> 'CANCELLED' and Dt_Planejamento <> '') and 
					(substring(Dt_Planejamento,7,2) + substring(Dt_Planejamento,4,2) + substring(Dt_Planejamento,1,2)) = convert(varchar(6), getdate(), 12)
				then 1 
				else 0 
		end as Prev_Dia,

		case when (Status_Exec_CT = 'PASSED' and Dt_Execucao <> '') and 
					(substring(Dt_Execucao,7,2) + substring(Dt_Execucao,4,2) + substring(Dt_Execucao,1,2)) = convert(varchar(6), getdate(), 12)
				then 1 
				else 0 
		end as Real_Dia,

		case when (Status_Exec_CT <> 'CANCELLED' and Dt_Planejamento <> '') and 
					(substring(Dt_Planejamento,7,2) + substring(Dt_Planejamento,4,2) + substring(Dt_Planejamento,1,2)) <= convert(varchar(6), getdate(), 12)
				then 1 
				else 0 
		end as Prev_Acum,

		case when (Status_Exec_CT = 'PASSED' and Dt_Execucao <> '') and 
				  (substring(Dt_Execucao,7,2) + substring(Dt_Execucao,4,2) + substring(Dt_Execucao,1,2)) <= convert(varchar(6), getdate(), 12)
				then 1 
				else 0 
		end as Real_Acum
    from 
        alm_cts ct
    where
		ct.path like '%@yyyy%'
		and ct.subprojeto = 'TRG2017' 
		--and ct.subprojeto = 'TRG@yyyy' 
		and ct.ciclo like ('%@mmm/@yy%')
		and @systemConditionCt
		and path <> 'lixo'
    ) as Aux
group by
	sistema
order by
	sistema