            declare @t table (
	            fullWeekNumber varchar(5), 
	            fullWeekNumberOrder varchar(5),
	            weekNumber int,
	            productivity int,
	            realized int,
	            qtyDefectsAmb int,
	            qtyDefectsCons int,
	            qtyDefectsTot int
            )
            insert into @t (
	            fullWeekNumber, 
	            fullWeekNumberOrder, 
	            weekNumber,
	            productivity,
	            realized,
	            qtyDefectsAmb,
	            qtyDefectsCons,
	            qtyDefectsTot
            )
	        select 
		        fullWeekNumber,
		        substring(fullWeekNumber,4,2)+substring(fullWeekNumber,1,2) as fullWeekNumberOrder,
		        convert(int,substring(fullWeekNumber,1,2)) as weekNumber,
		        sum(productivity) as productivity,
		        sum(realized) as realized,
		        sum(qtyDefectsAmb) as qtyDefectsAmb,
		        sum(qtyDefectsCons) as qtyDefectsCons,
		        sum(qtyDefectsTot) as qtyDefectsTot
	        from
		        (
				select 
					right('00' + convert(varchar,datepart(week, convert(datetime, dt_execucao, 5))),2) + '/' + substring(dt_execucao,7,2) as fullWeekNumber,
					1 as productivity,
					0 as realized,
					0 as qtyDefectsAmb,
					0 as qtyDefectsCons,
					0 as qtyDefectsTot
				from alm_execucoes WITH (NOLOCK)
				where 
					subprojeto = '@subproject' and
					entrega = '@delivery' and
					status in ('PASSED', 'FAILED') and
					dt_execucao <> ''

				union all

				select 
					right('00' + convert(varchar,datepart(week, convert(datetime, dt_execucao, 5))),2) + '/' + substring(dt_execucao,7,2) as fullWeekNumber,
					0 as productivity,
					1 as realized,
					0 as qtyDefectsAmb,
					0 as qtyDefectsCons,
					0 as qtyDefectsTot
				from ALM_CTs WITH (NOLOCK)
				where 
					subprojeto = '@subproject' and
					entrega = '@delivery' and
					Status_Exec_CT = 'PASSED' and 
					dt_execucao <> ''

				union all

	            select 
					right('00' + convert(varchar,datepart(week, convert(datetime, dt_inicial, 5))),2) + '/' + substring(dt_inicial,7,2) as fullWeekNumber,
					0 as productivity,
					0 as realized,
					(case when origem = 'AMBIENTE' then 1 else 0 end) as qtyDefectsAmb,
					(case when origem = 'CONSTRUÇÃO' then 1 else 0 end) as qtyDefectsCons,
					1 as qtyDefectsTot
	            from 
		            alm_defeitos
	            where
		            subprojeto = '@subproject' and
		            entrega = '@delivery' and
		            status_atual not in ('CLOSED', 'CANCELLED') and
		            Origem in ('AMBIENTE','CONSTRUÇÃO') and
		            dt_inicial <> ''

		        ) Aux
			group by
				fullWeekNumber
			order by
				2,3

            declare @t1 table (
	            fullWeekNumber varchar(5), 
	            fullWeekNumberOrder varchar(5),
	            productivity int,
	            realized int,
	            realizedAverage float,
	            qtyDefectsAmb int,
	            qtyDefectsCons int,
	            qtyDefectsTot int
            )
            insert into @t1 (
	            fullWeekNumber, 
	            fullWeekNumberOrder, 
	            productivity,
	            realized,
				realizedAverage,
	            qtyDefectsAmb,
	            qtyDefectsCons,
	            qtyDefectsTot
            )
			select 
				a.fullWeekNumber, 
				a.fullWeekNumberOrder,
				a.productivity,
				a.realized,

				round(
					convert(float,a.realized) / 
                        case when (case 
						      when (b.weekNumber - a.weekNumber) > 0 then (b.weekNumber - a.weekNumber) * 7
						      when (b.weekNumber - a.weekNumber) < 0 then ((b.weekNumber - a.weekNumber) * -1 - 51) * 7
					         end) > 1 
                          then 
                             (case 
						        when (b.weekNumber - a.weekNumber) > 0 then (b.weekNumber - a.weekNumber) * 7
						        when (b.weekNumber - a.weekNumber) < 0 then ((b.weekNumber - a.weekNumber) * -1 - 51) * 7
					         end)
                          else 1
                        end,
				2) as realizedAverage,

				a.qtyDefectsAmb,
				a.qtyDefectsCons,
				a.qtyDefectsTot
			from
				(select ROW_NUMBER() OVER(ORDER BY t1.fullWeekNumberOrder) as id, * from @t t1) a
				left join (select ROW_NUMBER() OVER(ORDER BY t1.fullWeekNumberOrder) as id, * from @t t1) b
				  on b.id = a.id + 1
			order by 
				a.fullWeekNumberOrder

            select
	            convert(varchar, cast(substring('@subproject',4,8) as int)) + ' ' + convert(varchar,cast(substring('@delivery',8,8) as int)) as subDel,
	            '@subproject' as subproject,
	            '@delivery' as delivery,
	            t11.fullWeekNumber,
	            t11.productivity,
	            t11.realized,
				         t11.realizedAverage,
	            t11.qtyDefectsAmb,
	            t11.qtyDefectsCons,
	            t11.qtyDefectsTot,

	            SUM(t12.qtyDefectsAmb) as qtyDefectsAmbAcum,
	            SUM(t12.qtyDefectsCons) as qtyDefectsConsAcum,
	            SUM(t12.qtyDefectsTot) as qtyDefectsTotAcum
            from 
	            @t1 t11 inner join @t1 t12 
	              on t11.fullWeekNumberOrder >= t12.fullWeekNumberOrder
            group by 
	            t11.fullWeekNumber,
	            t11.fullWeekNumberOrder, 
	            t11.productivity,
	            t11.realized,
	            t11.realizedAverage,
	            t11.qtyDefectsAmb,
	            t11.qtyDefectsCons,
	            t11.qtyDefectsTot
            order by 
	            t11.fullWeekNumberOrder
