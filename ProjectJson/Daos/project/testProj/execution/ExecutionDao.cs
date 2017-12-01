using Classes;
using ProjectWebApi.Models;
using ProjectWebApi.Models.Project;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;

namespace ProjectWebApi.Daos
{
    public class ExecutionDao
    {
        private Connection connection;

        public ExecutionDao()
        {
            connection = new Connection(Bancos.Sgq);
        }

        public void Dispose()
        {
            connection.Dispose();
        }


        public List<Status> groupDay(string subproject, string delivery)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\execution\groupDay.sql"), Encoding.Default);
            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);
            List<Status> result = connection.Executar<Status>(sql);
            return result;
        }

        public StatusLastDays lastDays(string subproject, string delivery)
        {
            List<Status> status30LastDays = this.groupDay(subproject, delivery);

            if (status30LastDays.Count >= 30)
                status30LastDays = status30LastDays.GetRange(0, 29);

            List<Status> status5LastDays;
            if (status30LastDays.Count > 5)
            {
                status5LastDays = status30LastDays.GetRange(0, 4);
            }
            else
            {
                status5LastDays = status30LastDays;
            }

            status30LastDays.Sort((x, y) => x.dateOrder.CompareTo(y.dateOrder));

            var statusLastDays = new StatusLastDays()
            {
                last05Days = status5LastDays,
                last30Days = status30LastDays
            };
            return statusLastDays;
        }


        public IList<Status> groupMonth(string subproject, string delivery)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\execution\groupMonth.sql"), Encoding.Default);
            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);
            IList<Status> result = connection.Executar<Status>(sql);
            return result;
        }



        public IList<ProductivityXDefects> productivityXDefects(string subproject, string delivery)
        {
            string sql = @"
            declare @t table (
	            date varchar(8), 
	            dateOrder varchar(8), 
	            productivity int,
	            realized int,
	            qtyDefectsAmb int,
	            qtyDefectsCons int,
	            qtyDefectsTot int
            )

            insert into @t (
	            date, 
	            dateOrder,
	            productivity,
	            realized,
	            qtyDefectsAmb,
	            qtyDefectsCons,
	            qtyDefectsTot
            )
	        select 
		        date,
		        substring(date,7,2)+substring(date,4,2)+substring(date,1,2) as dateOrder,
		        sum(productivity) as productivity,
		        sum(realized) as realized,
		        sum(qtyDefectsAmb) as qtyDefectsAmb,
		        sum(qtyDefectsCons) as qtyDefectsCons,
		        sum(qtyDefectsTot) as qtyDefectsTot
	        from
		        (
				select 
					left(dt_execucao,8) as date,
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
					left(dt_execucao,8) as date,
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
		            substring(dt_inicial,1,8) as date,
					0 as productivity,
					0 as realized,
					(case when origem = 'AMBIENTE' then 1 else 0 end) as qtyDefectsAmb,
					(case when origem = 'CONSTRU플O' then 1 else 0 end) as qtyDefectsCons,
					1 as qtyDefectsTot
	            from 
		            alm_defeitos
	            where
		            subprojeto = '@subproject' and
		            entrega = '@delivery' and
		            status_atual not in ('CLOSED', 'CANCELLED') and
		            Origem in ('AMBIENTE','CONSTRU플O') and
		            dt_inicial <> ''

		        ) Aux
			group by
				date
			order by
				2

            select
	            convert(varchar, cast(substring('@subproject',4,8) as int)) + ' ' + convert(varchar,cast(substring('@delivery',8,8) as int)) as project,
	            '@subproject' as subproject,
	            '@delivery' as delivery,
	            t.date,
	            t.productivity,
	            t.realized,
	            t.qtyDefectsAmb,
	            t.qtyDefectsCons,
	            t.qtyDefectsTot,

	            SUM(t2.qtyDefectsAmb) as qtyDefectsAmbAcum,
	            SUM(t2.qtyDefectsCons) as qtyDefectsConsAcum,
	            SUM(t2.qtyDefectsTot) as qtyDefectsTotAcum
            from 
	            @t t inner join @t t2 
	              on t.dateOrder >= t2.dateOrder
            group by 
	            t.dateOrder, 
	            t.date,
	            t.productivity,
	            t.realized,
	            t.qtyDefectsAmb,
	            t.qtyDefectsCons,
	            t.qtyDefectsTot
            order by 
	            t.dateOrder
            ";
            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);

            List<ProductivityXDefects> list = connection.Executar<ProductivityXDefects>(sql);

            return list;
        }

        public IList<ProductivityXDefectsGroupWeekly> productivityXDefectsGroupWeekly(string subproject, string delivery)
        {
            string sql = @"
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
					(case when origem = 'CONSTRU플O' then 1 else 0 end) as qtyDefectsCons,
					1 as qtyDefectsTot
	            from 
		            alm_defeitos
	            where
		            subprojeto = '@subproject' and
		            entrega = '@delivery' and
		            status_atual not in ('CLOSED', 'CANCELLED') and
		            Origem in ('AMBIENTE','CONSTRU플O') and
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
	            convert(varchar, cast(substring('@subproject',4,8) as int)) + ' ' + convert(varchar,cast(substring('@delivery',8,8) as int)) as project,
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
            ";
            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);

            List<ProductivityXDefectsGroupWeekly> list = connection.Executar<ProductivityXDefectsGroupWeekly>(sql);

            return list;
        }


        // Iteration
        // =================================================


        public List<Status> groupDayByIteration(string subproject, string delivery, List<string> iteration)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\execution\groupDayByIteration.sql"), Encoding.Default);
            string sql1 = sql.Replace("@subproject", subproject);
            string sql2 = sql1.Replace("@delivery", delivery);
            string sql3 = sql2.Replace("@iteration", "'" + string.Join("','", iteration.ToArray()) + "'");

            List<Status> List = connection.Executar<Status>(sql3);

            return List;
        }
        
        public StatusLastDays lastDaysByIteration(string subproject, string delivery, List<string> iteration)
        {
            List<Status> status30LastDays = this.groupDayByIteration(subproject, delivery, iteration);

            if (status30LastDays.Count >= 30)
                status30LastDays = status30LastDays.GetRange(0, 29);

            List<Status> status5LastDays;
            if (status30LastDays.Count > 5)
            {
                status5LastDays = status30LastDays.GetRange(0, 4);
            }
            else
            {
                status5LastDays = status30LastDays;
            }

            status30LastDays.Sort((x, y) => y.dateOrder.CompareTo(x.dateOrder));

            var statusLastDays = new StatusLastDays()
            {
                last05Days = status5LastDays,
                last30Days = status30LastDays
            };
            return statusLastDays;
        }


        public IList<Status> groupMonthByIteration(string subproject, string delivery, List<string> iteration)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\execution\groupMonthByIteration.sql"), Encoding.Default);
            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);
            sql = sql.Replace("@iteration", "'" + string.Join("','", iteration.ToArray()) + "'");
            IList<Status> result = connection.Executar<Status>(sql);
            return result;
        }


        public IList<ProductivityXDefects> productivityXDefectsIterations(string subproject, string delivery, List<string> iterations)
        {
            string sql = @"
            declare @t table (
	            date varchar(8), 
	            dateOrder varchar(8), 
	            productivity int,
	            realized int,
	            qtyDefectsAmb int,
	            qtyDefectsCons int,
	            qtyDefectsTot int
            )

            insert into @t (
	            date, 
	            dateOrder,
	            productivity,
	            realized,
	            qtyDefectsAmb,
	            qtyDefectsCons,
	            qtyDefectsTot
            )
	        select 
		        date,
		        substring(date,7,2)+substring(date,4,2)+substring(date,1,2) as dateOrder,
		        sum(productivity) as productivity,
		        sum(realized) as realized,
		        sum(qtyDefectsAmb) as qtyDefectsAmb,
		        sum(qtyDefectsCons) as qtyDefectsCons,
		        sum(qtyDefectsTot) as qtyDefectsTot
	        from
		        (
				select 
					left(ex.dt_execucao,8) as date,
					1 as productivity,
					0 as realized,
					0 as qtyDefectsAmb,
					0 as qtyDefectsCons,
					0 as qtyDefectsTot
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
					left(dt_execucao,8) as date,
					0 as productivity,
					1 as realized,
					0 as qtyDefectsAmb,
					0 as qtyDefectsCons,
					0 as qtyDefectsTot
				from 
                    ALM_CTs WITH (NOLOCK)
				where 
					subprojeto = '@subproject' and
					entrega = '@delivery' and
                    iterations in (@iterations) and
					Status_Exec_CT = 'PASSED' and 
					dt_execucao <> ''

				union all

	            select 
		            substring(df.dt_inicial,1,8) as date,
					0 as productivity,
					0 as realized,
					(case when df.origem = 'AMBIENTE' then 1 else 0 end) as qtyDefectsAmb,
					(case when df.origem = 'CONSTRU플O' then 1 else 0 end) as qtyDefectsCons,
					1 as qtyDefectsTot
	            from 
					alm_cts cts 
					inner join alm_defeitos df
						on df.subprojeto = cts.subprojeto and
                           df.entrega = cts.entrega and
                           df.ct = cts.ct
	            where
		            cts.subprojeto = '@subproject' and
		            cts.entrega = '@delivery' and
                    cts.iterations in (@iterations) and
		            df.status_atual not in ('CLOSED', 'CANCELLED') and
		            df.Origem in ('AMBIENTE','CONSTRU플O') and
		            df.dt_inicial <> ''
		        ) Aux
			group by
				date
			order by
				2

            select
	            convert(varchar, cast(substring('@subproject',4,8) as int)) + ' ' + convert(varchar,cast(substring('@delivery',8,8) as int)) as project,
	            '@subproject' as subproject,
	            '@delivery' as delivery,
	            t.date,
	            t.productivity,
	            t.realized,
	            t.qtyDefectsAmb,
	            t.qtyDefectsCons,
	            t.qtyDefectsTot,

	            SUM(t2.qtyDefectsAmb) as qtyDefectsAmbAcum,
	            SUM(t2.qtyDefectsCons) as qtyDefectsConsAcum,
	            SUM(t2.qtyDefectsTot) as qtyDefectsTotAcum
            from 
	            @t t inner join @t t2 
	              on t.dateOrder >= t2.dateOrder
            group by 
	            t.dateOrder, 
	            t.date,
	            t.productivity,
	            t.realized,
	            t.qtyDefectsAmb,
	            t.qtyDefectsCons,
	            t.qtyDefectsTot
            order by 
	            t.dateOrder
            ";
            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);
            sql = sql.Replace("@iterations", "'" + string.Join("','", iterations.ToArray()) + "'");

            List<ProductivityXDefects> list = connection.Executar<ProductivityXDefects>(sql);

            return list;
        }

        public IList<ProductivityXDefectsGroupWeekly> productivityXDefectsGroupWeeklyIterations(string subproject, string delivery, List<string> iterations)
        {
            string sql = @"
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
					right('00' + convert(varchar,datepart(week, convert(datetime, ex.dt_execucao, 5))),2) + '/' + substring(ex.dt_execucao,7,2) as fullWeekNumber,
					1 as productivity,
					0 as realized,
					0 as qtyDefectsAmb,
					0 as qtyDefectsCons,
					0 as qtyDefectsTot
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
                    iterations in (@iterations) and
					Status_Exec_CT = 'PASSED' and 
					dt_execucao <> ''

				union all

	            select 
					right('00' + convert(varchar,datepart(week, convert(datetime, df.dt_inicial, 5))),2) + '/' + substring(df.dt_inicial,7,2) as fullWeekNumber,
					0 as productivity,
					0 as realized,
					(case when origem = 'AMBIENTE' then 1 else 0 end) as qtyDefectsAmb,
					(case when origem = 'CONSTRU플O' then 1 else 0 end) as qtyDefectsCons,
					1 as qtyDefectsTot
	            from 
					alm_cts cts 
					inner join alm_defeitos df
						on df.subprojeto = cts.subprojeto and
                           df.entrega = cts.entrega and
                           df.ct = cts.ct
	            where
		            cts.subprojeto = '@subproject' and
		            cts.entrega = '@delivery' and
                    cts.iterations in (@iterations) and
		            df.status_atual not in ('CLOSED', 'CANCELLED') and
		            df.origem in ('AMBIENTE','CONSTRU플O') and
		            df.dt_inicial <> ''

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
	            convert(varchar, cast(substring('@subproject',4,8) as int)) + ' ' + convert(varchar,cast(substring('@delivery',8,8) as int)) as project,
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
            ";
            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);
            sql = sql.Replace("@iterations", "'" + string.Join("','", iterations.ToArray()) + "'");

            List<ProductivityXDefectsGroupWeekly> list = connection.Executar<ProductivityXDefectsGroupWeekly>(sql);

            return list;
        }

    }
}