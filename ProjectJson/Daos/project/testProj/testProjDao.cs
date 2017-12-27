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
    public class TestProjDao
    {
        private Connection connection;

        public TestProjDao()
        {
            connection = new Connection(Bancos.Sgq);
        }

        public void Dispose()
        {
            connection.Dispose();
        }

        public IList<simpProject> all()
        {
			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\testProj\all.sql"), Encoding.Default);
			var listProjects = connection.Executar<simpProject>(sql);
            return listProjects;
        }

		public IList<Project> fromTestManufsAndSystems(testManufsAndSystems parameters)
        {
			string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\testProj\fromTestManufsAndSystems.sql"), Encoding.Default);
			sql = sql.Replace("@testManufs", "'" + string.Join("','", parameters.testManufs) + "'");
			sql = sql.Replace("@systems", "'" + string.Join("','", parameters.systems) + "'");
			var result = connection.Executar<Project>(sql);
            return result;
        }

        public IList<simpProject> fbyDevManufsAndSystems(devManufsAndSystems parameters)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\testProj\fbyDevManufsAndSystems.sql"), Encoding.Default);
            sql = sql.Replace("@devManufs", "'" + string.Join("','", parameters.devManufs) + "'");
            sql = sql.Replace("@systems", "'" + string.Join("','", parameters.systems) + "'");
            var result = connection.Executar<simpProject>(sql);
            return result;
        }

        public IList<Project> fromAgentFbyDevManufsAndSystems(devManufsAndSystems parameters)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\testProj\fromAgentFbyDevManufsAndSystems.sql"), Encoding.Default);
            sql = sql.Replace("@devManufs", "'" + string.Join("','", parameters.devManufs) + "'");
            sql = sql.Replace("@systems", "'" + string.Join("','", parameters.systems) + "'");
            var result = connection.Executar<Project>(sql);
            return result;
        }


        //public IList<Project> fbySubprojectDelivery(IList<string> parameter)
        //{
        //    string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\testProj\fbySubprojectDelivery.sql"), Encoding.Default);
        //    sql = sql.Replace("@projects", "'" + string.Join("','", parameter) + "'");
        //    var list = connection.Executar<Project>(sql);
        //    return list;
        //}

        public IList<Project> byIds(string ids)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\testProj\byIds.sql"), Encoding.Default);
            sql = sql.Replace("@ids", ids);
            var result = connection.Executar<Project>(sql);
            return result;
        }

        public Project getProject(string subproject, string delivery)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\testProj\detail.sql"), Encoding.Default);
            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);

            var list = connection.Executar<Project>(sql);

            return list[0];
        }


        //public DefectDensity getDefectsDensityByProject(string subproject, string delivery)
        //{
        //    string sql = @"
        //        select
        //            sum(qte_defeitos) as QtyDefects,
        //            count(*) as QtyCTs,
        //            round(convert(float, sum(qte_defeitos)) / (case when count(*) = 0 then 1 else count(*) end) * 100,2) as Density
        //        from
        //            (select
        //                cts.subprojeto as subproject,
        //                cts.entrega as delivery,
        //                substring(cts.dt_execucao, 4, 2) as monthExecution,
        //                substring(cts.dt_execucao, 7, 2) as yearExecution,
        //                (select count(*)
        //                from alm_defeitos df
        //                where df.subprojeto = cts.subprojeto and
        //                        df.entrega = cts.entrega and
        //                        df.ct = cts.ct and
        //                        df.status_atual = 'CLOSED' and
        //                        df.Origem like '%CONSTRUÇÃO%' and
        //                        df.Ciclo in ('TI', 'UAT')
        //                ) as qte_defeitos
        //                from
        //                    alm_cts cts
        //                where
        //                    subprojeto = '@subproject' and
        //                    entrega = '@delivery' and
        //                    status_exec_ct not in ('CANCELLED', 'NO RUN') and
        //                    cts.fabrica_desenvolvimento is not null and
        //                    cts.massa_Teste <> 'SIM' and
        //                    cts.Ciclo in ('TI', 'UAT') and
        //                    dt_execucao <> ''
        //            ) Aux
        //        ";

        //    sql = sql.Replace("@subproject", subproject);
        //    sql = sql.Replace("@delivery", delivery);

        //    var list = connection.Executar<DefectDensity>(sql);

        //    return list[0];
        //}





        public IList<DefectStatus> getDefectStatusByProject(string subproject, string delivery)
        {
            string sql = @"
            select 
	            name,
	            qtyDefects,
	            totalDefects,
	            round(convert(float,qtyDefects) / (case when totalDefects <> 0 then totalDefects else 1 end) * 100,2) as [percent]
            from
	            (
	            select 
		            'Aberto-Fáb.Teste' as name,
		            (select count(*) from ALM_Defeitos d WITH (NOLOCK)
			            where d.subprojeto = '@subproject' and 
				            d.Entrega = '@delivery' and 
				            d.Status_Atual in ('ON_RETEST','PENDENT (RETEST)','REJECTED')
		            ) as qtyDefects,
		            (select count(*) from ALM_Defeitos d WITH (NOLOCK) where d.subprojeto = '@subproject' and d.Entrega = '@delivery') as totalDefects

	            union all

	            select 
		            'Aberto-Fáb.Desen' as name,
		            (select count(*) from ALM_Defeitos d WITH (NOLOCK)
		             where d.subprojeto = '@subproject' and 
				            d.Entrega = '@delivery' and 
				            d.Status_Atual in ('NEW','IN_PROGRESS','PENDENT (PROGRESS)','REOPEN','MIGRATE')
		            ) as qtyDefects,
		            (select count(*) from ALM_Defeitos d WITH (NOLOCK) where d.subprojeto = '@subproject' and d.Entrega = '@delivery') as totalDefects

	            union all

	            select 
		            'Fechado' as name,
		            (select count(*) from ALM_Defeitos d WITH (NOLOCK)
		             where d.subprojeto = '@subproject' and 
				            d.Entrega = '@delivery' and 
				            d.Status_Atual = 'CLOSED'
		            ) as qtyDefects,
		            (select count(*) from ALM_Defeitos d WITH (NOLOCK) where d.subprojeto = '@subproject' and d.Entrega = '@delivery') as totalDefects

	            union all

	            select 
		            'Cancelado' as name,
		            (select count(*) from ALM_Defeitos d WITH (NOLOCK)
		             where d.subprojeto = '@subproject' and 
				            d.Entrega = '@delivery' and 
				            d.Status_Atual = 'CANCELLED'
		            ) as qtyDefects,
		            (select count(*) from ALM_Defeitos d WITH (NOLOCK) where d.subprojeto = '@subproject' and d.Entrega = '@delivery') as totalDefects
	            ) aux
            ";
            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);

            List<DefectStatus> list = connection.Executar<DefectStatus>(sql);

            return list;
        }

        public IList<DefectStatus> getDefectsGroupOrigin(string subproject, string delivery)
        {
            string sql = @"
                select 
	                UPPER(LEFT(name,1))+LOWER(SUBSTRING(name,2,LEN(name))) as name,
	                qtyDefects,
	                totalDefects,
	                round(convert(float,qtyDefects) / (case when totalDefects <> 0 then totalDefects else 1 end) * 100,2) as [percent]
                from
	                (
	                select 
		                (case when Origem <> '' then Origem else 'INDEFINIDO' end) as name,
		                count(*) as qtyDefects,
		                (select 
			                count(*)
		                from 
			                ALM_Defeitos d WITH (NOLOCK)
		                where
			                subprojeto = '@subproject' and
			                entrega = '@delivery' and
			                Status_Atual = 'CLOSED'
		                ) as totalDefects
	                from 
		                ALM_Defeitos d WITH (NOLOCK)
	                where
		                subprojeto = '@subproject' and
		                entrega = '@delivery' and
		                Status_Atual = 'CLOSED'
	                group by 
		                Origem
	                ) aux
                order by
	                2 desc
            ";
            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);

            List<DefectStatus> list = connection.Executar<DefectStatus>(sql);

            return list;
        }

        public IList<CtsImpactedXDefects> getCtsImpactedXDefects(string subproject, string delivery)
        {
            string sql = @"
            declare @t table (
	            subproject varchar(30),
	            delivery varchar(30),
	            date varchar(8), 
	            dateOrder varchar(8), 
	            qtyDefectsAmb int, 
	            qtyDefectsCons int, 
	            qtyDefectsTot int,
	            qtyCtsImpacted int
            )

            insert into @t (
	            subproject,
	            delivery,
	            date, 
	            dateOrder,
	            qtyDefectsAmb,
	            qtyDefectsCons,
	            qtyDefectsTot,
	            qtyCtsImpacted
            )            
            select
	            subproject,
	            delivery,
	            date,
	            substring(date,7,2)+substring(date,4,2)+substring(date,1,2) as dateOrder,
	            sum(case when name = 'AMBIENTE' then Qtd_Defeitos else 0 end) as qtyDefectsAmb,
	            sum(case when name = 'CONSTRUÇÃO' then Qtd_Defeitos else 0 end) as qtyDefectsCons,
	            sum(Qtd_Defeitos) as qtyDefectsTot,
	            sum(Qtd_CTs_Impactados) as qtyCtsImpacted
            from
	            (
	            select 
		            subprojeto as subproject,
		            entrega as delivery,
		            substring(dt_inicial,1,8) as date,
		            substring(dt_inicial,7,2) + substring(dt_inicial,4,2) + substring(dt_inicial,1,2) as dateOrder,
		            Origem as name,
		            Qtd_CTs_Impactados,
		            1 as Qtd_Defeitos
	            from 
		            alm_defeitos
	            where
		            subprojeto = '@subproject' and
		            entrega = '@delivery' and
		            status_atual not in ('CLOSED', 'CANCELLED') and
		            Origem in ('AMBIENTE','CONSTRUÇÃO') and
		            dt_inicial <> ''
	            ) aux
            group by 
	            subproject,
	            delivery,
	            date,
	            dateOrder
            order by 
	            dateOrder

            select
	            convert(varchar, cast(substring('@subproject',4,8) as int)) + ' ' + convert(varchar,cast(substring('@delivery',8,8) as int)) as project,
	            '@subproject' as subproject,
	            '@delivery' as delivery,
	            t.date,
	            t.qtyDefectsAmb, 
	            t.qtyDefectsCons, 
	            t.qtyDefectsTot, 
	            t.qtyCtsImpacted, 

	            SUM(t2.qtyDefectsAmb) as qtyDefectsAmbAcum,
	            SUM(t2.qtyDefectsCons) as qtyDefectsConsAcum,
	            SUM(t2.qtyDefectsTot) as qtyDefectsTotAcum,
	            SUM(t2.qtyCtsImpacted) as qtyCtsImpactedAcum
            from 
	            @t t 
	            inner join @t t2 
	              on t.dateOrder >= t2.dateOrder
            group by 
	            t.date,
	            t.dateOrder,
	            t.qtyDefectsAmb, 
	            t.qtyDefectsCons, 
	            t.qtyDefectsTot, 
	            t.qtyCtsImpacted
            order by
	            t.dateOrder
            ";
            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);

            List<CtsImpactedXDefects> list = connection.Executar<CtsImpactedXDefects>(sql);

            return list;
        }

        public IList<DefectsOpen> getDefectsOpenInDevManuf(string subproject, string delivery)
        {
            string sql = @"
            select 
	            convert(varchar, cast(substring(Subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(Entrega,8,8) as int)) as project,
	            subprojeto as subproject,
	            entrega as delivery,

	            df.Defeito as defect,

                case df.Status_Atual 
                    when 'NEW' then 'New'
                    when 'IN_PROGRESS' then 'In Progr.'
                    when 'MIGRATE' then 'Migrate'
                    when 'PENDENT (PROGRESS)' then 'Pend.Progr.'
                    when 'REOPEN' then 'Reopen'
                    else 'Indefinido'
                end as status,

	            UPPER(LEFT(left(df.Encaminhado_Para,20),1))+LOWER(SUBSTRING(left(df.Encaminhado_Para,20),2,LEN(left(df.Encaminhado_Para,20)))) as forwardedTo,
	            UPPER(LEFT(left(df.Sistema_Defeito,20),1))+LOWER(SUBSTRING(left(df.Sistema_Defeito,20),2,LEN(left(df.Sistema_Defeito,20)))) as defectSystem,
	            UPPER(LEFT(substring(severidade,3,3),1))+LOWER(SUBSTRING(substring(severidade,3,3),2,LEN(substring(severidade,3,3)))) as severity,
             df.Aging as aging,
	            substring('-  ',2+convert(int,sign(df.aging)),1) + right(convert(varchar, floor(abs(df.aging))), 3) + ':' + right('00' + convert(varchar,round( 60*(abs(df.aging)-floor(abs(df.aging))), 0)), 2) as agingDisplay,
             df.Ping_Pong as pingPong
            from 
	            ALM_Defeitos df WITH (NOLOCK)
            where
	            df.Status_Atual in ('NEW','IN_PROGRESS','PENDENT (PROGRESS)','REOPEN','MIGRATE') and
	            df.subprojeto = '@subproject' and
	            df.entrega = '@delivery'
            order by 
                4
            ";
            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);

            List<DefectsOpen> list = connection.Executar<DefectsOpen>(sql);

            return list;
        }

        public IList<DefectsOpen> getDefectsOpenInTestManuf(string subproject, string delivery)
        {
            string sql = @"
            select 
	            convert(varchar, cast(substring(Subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(Entrega,8,8) as int)) as project,
	            subprojeto as subproject,
	            entrega as delivery,

	            df.Defeito as defect,

                case df.Status_Atual 
                    when 'ON_RETEST' then 'On Retest'
                    when 'PENDENT (RETEST)' then 'Pend.Retest'
                    when 'REJECTED' then 'Reject'
                    else 'Indefinido'
                end as status,

	            UPPER(LEFT(left(df.Encaminhado_Para,20),1))+LOWER(SUBSTRING(left(df.Encaminhado_Para,20),2,LEN(left(df.Encaminhado_Para,20)))) as forwardedTo,
	            UPPER(LEFT(left(df.Sistema_Defeito,20),1))+LOWER(SUBSTRING(left(df.Sistema_Defeito,20),2,LEN(left(df.Sistema_Defeito,20)))) as defectSystem,
	            UPPER(LEFT(substring(df.severidade,3,3),1))+LOWER(SUBSTRING(substring(df.severidade,3,3),2,LEN(substring(df.severidade,3,3)))) as severity,
             df.Aging as aging,
	            substring('-  ',2+convert(int,sign(df.aging)),1) + right(convert(varchar, floor(abs(df.aging))), 3) + ':' + right('00' + convert(varchar,round( 60*(abs(df.aging)-floor(abs(df.aging))), 0)), 2) as agingDisplay,
             df.Ping_Pong as pingPong
            from 
	            ALM_Defeitos df WITH (NOLOCK)
            where
	            df.Status_Atual in ('ON_RETEST','PENDENT (RETEST)','REJECTED') and
	            df.subprojeto = '@subproject' and
	            df.entrega = '@delivery'
            order by 
                4
            ";
            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);

            List<DefectsOpen> list = connection.Executar<DefectsOpen>(sql);

            return list;
        }


        public IList<iteration> iterations(string subproject, string delivery)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\testProj\iterationsFbyProject.sql"), Encoding.Default);
            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);
            var result = connection.Executar<iteration>(sql);
            return result;
        }

        public List<string> iterationsActive(string subproject, string delivery)
        {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\testProj\iterationsActiveFbyProject.sql"), Encoding.Default);
            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);

            string iterations = connection.Get_String(sql);
            string[] stringSeparators = new string[] { "','" };
            var list = iterations.Split(stringSeparators, StringSplitOptions.None);

            return new List<string>(list);
        }

        public List<string> iterationsSelected(string subproject, string delivery) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\testProj\iterationsSelectedFbyProject.sql"), Encoding.Default);
            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);

            string iterations = connection.Get_String(sql);
            string[] stringSeparators = new string[] { "','" };
            var list = iterations.Split(stringSeparators, StringSplitOptions.None);

            return new List<string>(list);
        }


        public DefectDensity getDefectsDensityByProjectIterations(string subproject, string delivery, List<string> iterations)
        {
            string sql = @"
                select
                    sum(qte_defeitos) as QtyDefects,
                    count(*) as QtyCTs,
                    round(convert(float, sum(qte_defeitos)) / (case when count(*) = 0 then 1 else count(*) end) * 100,2) as Density
                from
                    (select
                        cts.subprojeto as subproject,
                        cts.entrega as delivery,
                        substring(cts.dt_execucao, 4, 2) as monthExecution,
                        substring(cts.dt_execucao, 7, 2) as yearExecution,
                        (select count(*)
                        from alm_defeitos df
                        where df.subprojeto = cts.subprojeto and
                                df.entrega = cts.entrega and
                                df.ct = cts.ct and
                                df.status_atual = 'CLOSED' and
                                df.Origem like '%CONSTRUÇÃO%' and
                                df.Ciclo in ('TI', 'UAT')
                        ) as qte_defeitos
                        from
                            alm_cts cts
                        where
                            cts.subprojeto = '@subproject' and
                            cts.entrega = '@delivery' and
                            cts.iterations in (@iterations) and
                            cts.status_exec_ct not in ('CANCELLED', 'NO RUN') and
                            cts.fabrica_desenvolvimento is not null and
                            cts.massa_Teste <> 'SIM' and
                            cts.Ciclo in ('TI', 'UAT') and
                            cts.dt_execucao <> ''
                    ) Aux
                ";

            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);
            sql = sql.Replace("@iterations", "'" + string.Join("','", iterations.ToArray()) + "'");

            var list = connection.Executar<DefectDensity>(sql);

            return list[0];
        }


        public DetectableInDev getDetectableInDevByProjectIterations(string subproject, string delivery, List<string> iterations)
        {
            string sql = @"
                select 
	                    count(*) as qtyTotal,
	                    sum(case when Erro_Detectavel_Em_Desenvolvimento = 'SIM' then 1 else 0 end) as qtyDetectableInDev,
                        round(convert(float,sum(case when Erro_Detectavel_Em_Desenvolvimento = 'SIM' then 1 else 0 end)) / count(*) * 100,2) as percentDetectableInDev,
	                    5 as percentReference,
	                    round(convert(float,count(*) * 0.05),2) as qtyReference
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
	                    df.Ciclo in ('TI', 'UAT') and
	                    df.status_atual = 'CLOSED' and 
	                    df.dt_final <> ''
                ";

            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);
            sql = sql.Replace("@iterations", "'" + string.Join("','", iterations.ToArray()) + "'");

            var list = connection.Executar<DetectableInDev>(sql);

            return list[0];
        }


        public IList<DefectStatus> getDefectStatusByProjectIterations(string subproject, string delivery, List<string> iterations)
        {
            string sql = @"
            select 
	            name,
	            qtyDefects,
	            totalDefects,
	            round(convert(float,qtyDefects) / (case when totalDefects <> 0 then totalDefects else 1 end) * 100,2) as [percent]
            from
	            (
	            select 
		            'Aberto-Fáb.Desen' as name,

		            (select count(*) 
                     from 
					    alm_cts cts WITH (NOLOCK)
					    inner join alm_defeitos df WITH (NOLOCK)
						    on df.subprojeto = cts.subprojeto and
                               df.entrega = cts.entrega and
                               df.ct = cts.ct
			         where 
                        df.subprojeto = '@subproject' and 
				        df.Entrega = '@delivery' and 
                        cts.iterations in (@iterations) and
				        df.Status_Atual in ('ON_RETEST','PENDENT (RETEST)','REJECTED')
		            ) as qtyDefects,

		            (select count(*) 
                     from 
					    alm_cts cts WITH (NOLOCK)
					    inner join alm_defeitos df WITH (NOLOCK)
						    on df.subprojeto = cts.subprojeto and
                               df.entrega = cts.entrega and
                               df.ct = cts.ct
                     where 
                        df.subprojeto = '@subproject' and 
                        df.Entrega = '@delivery' and
                        cts.iterations in (@iterations)
                    ) as totalDefects

	            union all

	            select 
		            'Aberto-Fáb.Teste' as name,

		            (select count(*) 
                     from 
					    alm_cts cts WITH (NOLOCK)
					    inner join alm_defeitos df WITH (NOLOCK)
						    on df.subprojeto = cts.subprojeto and
                               df.entrega = cts.entrega and
                               df.ct = cts.ct
		             where 
                        df.subprojeto = '@subproject' and 
				        df.Entrega = '@delivery' and 
                        cts.iterations in (@iterations) and
				        df.Status_Atual in ('NEW','IN_PROGRESS','PENDENT (PROGRESS)','REOPEN','MIGRATE')
		            ) as qtyDefects,

		            (select count(*) 
                     from 
					    alm_cts cts WITH (NOLOCK)
					    inner join alm_defeitos df WITH (NOLOCK)
						    on df.subprojeto = cts.subprojeto and
                               df.entrega = cts.entrega and
                               df.ct = cts.ct
                     where 
                        df.subprojeto = '@subproject' and 
                        df.Entrega = '@delivery' and
                        cts.iterations in (@iterations)
                    ) as totalDefects

	            union all

	            select 
		            'Fechado' as name,

		            (select count(*) 
                     from 
					    alm_cts cts WITH (NOLOCK)
					    inner join alm_defeitos df WITH (NOLOCK)
						    on df.subprojeto = cts.subprojeto and
                               df.entrega = cts.entrega and
                               df.ct = cts.ct
		             where 
                        df.subprojeto = '@subproject' and 
				        df.Entrega = '@delivery' and 
                        cts.iterations in (@iterations) and
				        df.Status_Atual = 'CLOSED'
		            ) as qtyDefects,

		            (select count(*) 
                     from 
					    alm_cts cts WITH (NOLOCK)
					    inner join alm_defeitos df WITH (NOLOCK)
						    on df.subprojeto = cts.subprojeto and
                               df.entrega = cts.entrega and
                               df.ct = cts.ct
                     where 
                        df.subprojeto = '@subproject' and 
                        df.Entrega = '@delivery' and
                        cts.iterations in (@iterations)
                    ) as totalDefects

	            union all

	            select 
		            'Cancelado' as name,

		            (select count(*) 
                     from 
					    alm_cts cts WITH (NOLOCK)
					    inner join alm_defeitos df WITH (NOLOCK)
						    on df.subprojeto = cts.subprojeto and
                               df.entrega = cts.entrega and
                               df.ct = cts.ct
		             where 
                        df.subprojeto = '@subproject' and 
				        df.Entrega = '@delivery' and 
                        cts.iterations in (@iterations) and
				        df.Status_Atual = 'CANCELLED'
		            ) as qtyDefects,

		            (select count(*) 
                     from 
					    alm_cts cts WITH (NOLOCK)
					    inner join alm_defeitos df WITH (NOLOCK)
						    on df.subprojeto = cts.subprojeto and
                               df.entrega = cts.entrega and
                               df.ct = cts.ct
                    where 
                        df.subprojeto = '@subproject' and 
                        df.Entrega = '@delivery' and
                        cts.iterations in (@iterations)
                    ) as totalDefects
	            ) aux
            ";
            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);
            sql = sql.Replace("@iterations", "'" + string.Join("','", iterations.ToArray()) + "'");

            List<DefectStatus> list = connection.Executar<DefectStatus>(sql);

            return list;
        }

        public IList<DefectStatus> getDefectsGroupOriginIterations(string subproject, string delivery, List<string> iterations)
        {
            string sql = @"
                select 
	                UPPER(LEFT(name,1))+LOWER(SUBSTRING(name,2,LEN(name))) as name,
	                qtyDefects,
	                totalDefects,
	                round(convert(float,qtyDefects) / (case when totalDefects <> 0 then totalDefects else 1 end) * 100,2) as [percent]
                from
	                (
	                select 
		                (case when Origem <> '' then Origem else 'INDEFINIDO' end) as name,
		                count(*) as qtyDefects,
		                (select 
			                count(*)
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
			                df.Status_Atual = 'CLOSED'
		                ) as totalDefects
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
		                df.Status_Atual = 'CLOSED'
	                group by 
		                Origem
	                ) aux
                order by
	                2 desc
            ";
            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);
            sql = sql.Replace("@iterations", "'" + string.Join("','", iterations.ToArray()) + "'");

            List<DefectStatus> list = connection.Executar<DefectStatus>(sql);

            return list;
        }

        public IList<CtsImpactedXDefects> getCtsImpactedXDefectsIterations(string subproject, string delivery, List<string> iterations)
        {
            string sql = @"
            declare @t table (
	            subproject varchar(30),
	            delivery varchar(30),
	            date varchar(8), 
	            dateOrder varchar(8), 
	            qtyDefectsAmb int, 
	            qtyDefectsCons int, 
	            qtyDefectsTot int,
	            qtyCtsImpacted int
            )

            insert into @t (
	            subproject,
	            delivery,
	            date, 
	            dateOrder,
	            qtyDefectsAmb,
	            qtyDefectsCons,
	            qtyDefectsTot,
	            qtyCtsImpacted
            )            
            select
	            subproject,
	            delivery,
	            date,
	            substring(date,7,2)+substring(date,4,2)+substring(date,1,2) as dateOrder,
	            sum(case when name = 'AMBIENTE' then Qtd_Defeitos else 0 end) as qtyDefectsAmb,
	            sum(case when name = 'CONSTRUÇÃO' then Qtd_Defeitos else 0 end) as qtyDefectsCons,
	            sum(Qtd_Defeitos) as qtyDefectsTot,
	            sum(Qtd_CTs_Impactados) as qtyCtsImpacted
            from
	            (
	            select 
		            cts.subprojeto as subproject,
		            cts.entrega as delivery,
		            substring(df.dt_inicial,1,8) as date,
		            substring(df.dt_inicial,7,2) + substring(df.dt_inicial,4,2) + substring(df.dt_inicial,1,2) as dateOrder,
		            df.Origem as name,
		            df.Qtd_CTs_Impactados,
		            1 as Qtd_Defeitos
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
		            df.Origem in ('AMBIENTE','CONSTRUÇÃO') and
		            df.dt_inicial <> ''
	            ) aux
            group by 
	            subproject,
	            delivery,
	            date,
	            dateOrder
            order by 
	            dateOrder

            select
	            convert(varchar, cast(substring('@subproject',4,8) as int)) + ' ' + convert(varchar,cast(substring('@delivery',8,8) as int)) as project,
	            '@subproject' as subproject,
	            '@delivery' as delivery,
	            t.date,
	            t.qtyDefectsAmb, 
	            t.qtyDefectsCons, 
	            t.qtyDefectsTot, 
	            t.qtyCtsImpacted, 

	            SUM(t2.qtyDefectsAmb) as qtyDefectsAmbAcum,
	            SUM(t2.qtyDefectsCons) as qtyDefectsConsAcum,
	            SUM(t2.qtyDefectsTot) as qtyDefectsTotAcum,
	            SUM(t2.qtyCtsImpacted) as qtyCtsImpactedAcum
            from 
	            @t t 
	            inner join @t t2 
	              on t.dateOrder >= t2.dateOrder
            group by 
	            t.date,
	            t.dateOrder,
	            t.qtyDefectsAmb, 
	            t.qtyDefectsCons, 
	            t.qtyDefectsTot, 
	            t.qtyCtsImpacted
            order by
	            t.dateOrder
            ";
            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);
            sql = sql.Replace("@iterations", "'" + string.Join("','", iterations.ToArray()) + "'");

            List<CtsImpactedXDefects> list = connection.Executar<CtsImpactedXDefects>(sql);

            return list;
        }

        public IList<DefectsOpen> getDefectsOpenInDevManufIterations(string subproject, string delivery, List<string> iterations)
        {
            string sql = @"
            select 
	            convert(varchar, cast(substring(cts.Subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(cts.Entrega,8,8) as int)) as project,
	            cts.subprojeto as subproject,
	            cts.entrega as delivery,

	            df.Defeito as defect,

                case df.Status_Atual 
                    when 'NEW' then 'New'
                    when 'IN_PROGRESS' then 'In Progr.'
                    when 'MIGRATE' then 'Migrate'
                    when 'PENDENT (PROGRESS)' then 'Pend.Progr.'
                    when 'REOPEN' then 'Reopen'
                    else 'Indefinido'
                end as status,

	            UPPER(LEFT(left(df.Encaminhado_Para,20),1))+LOWER(SUBSTRING(left(df.Encaminhado_Para,20),2,LEN(left(df.Encaminhado_Para,20)))) as forwardedTo,
	            UPPER(LEFT(left(df.Sistema_Defeito,20),1))+LOWER(SUBSTRING(left(df.Sistema_Defeito,20),2,LEN(left(df.Sistema_Defeito,20)))) as defectSystem,
	            UPPER(LEFT(substring(severidade,3,3),1))+LOWER(SUBSTRING(substring(severidade,3,3),2,LEN(substring(severidade,3,3)))) as severity,
                df.Aging as aging,
	            substring('-  ',2+convert(int,sign(df.aging)),1) + right(convert(varchar, floor(abs(df.aging))), 3) + ':' + right('00' + convert(varchar,round( 60*(abs(df.aging)-floor(abs(df.aging))), 0)), 2) as agingDisplay,
                df.Ping_Pong as pingPong
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
	                df.Status_Atual in ('NEW','IN_PROGRESS','PENDENT (PROGRESS)','REOPEN','MIGRATE')
            order by 
                4
            ";
            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);
            sql = sql.Replace("@iterations", "'" + string.Join("','", iterations.ToArray()) + "'");

            List<DefectsOpen> list = connection.Executar<DefectsOpen>(sql);

            return list;
        }

        public IList<DefectsOpen> getDefectsOpenInTestManufIterations(string subproject, string delivery, List<string> iterations)
        {
            string sql = @"
            select 
	            convert(varchar, cast(substring(cts.Subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(cts.Entrega,8,8) as int)) as project,
	            cts.subprojeto as subproject,
	            cts.entrega as delivery,

	            df.Defeito as defect,

                case df.Status_Atual 
                    when 'ON_RETEST' then 'On Retest'
                    when 'PENDENT (RETEST)' then 'Pend.Retest'
                    when 'REJECTED' then 'Reject'
                    else 'Indefinido'
                end as status,

	            UPPER(LEFT(left(df.Encaminhado_Para,20),1))+LOWER(SUBSTRING(left(df.Encaminhado_Para,20),2,LEN(left(df.Encaminhado_Para,20)))) as forwardedTo,
	            UPPER(LEFT(left(df.Sistema_Defeito,20),1))+LOWER(SUBSTRING(left(df.Sistema_Defeito,20),2,LEN(left(df.Sistema_Defeito,20)))) as defectSystem,
	            UPPER(LEFT(substring(df.severidade,3,3),1))+LOWER(SUBSTRING(substring(df.severidade,3,3),2,LEN(substring(df.severidade,3,3)))) as severity,
                df.Aging as aging,
	            substring('-  ',2+convert(int,sign(df.aging)),1) + right(convert(varchar, floor(abs(df.aging))), 3) + ':' + right('00' + convert(varchar,round( 60*(abs(df.aging)-floor(abs(df.aging))), 0)), 2) as agingDisplay,
                df.Ping_Pong as pingPong
            from 
				            alm_cts cts 
				            inner join alm_defeitos df
					            on df.subprojeto = cts.subprojeto and
                                    df.entrega = cts.entrega and
                                    df.ct = cts.ct
            where
	                df.subprojeto = '@subproject' and
	                df.entrega = '@delivery' and
                    cts.iterations in (@iterations) and
	                df.Status_Atual in ('ON_RETEST','PENDENT (RETEST)','REJECTED')
            order by 
                4
            ";
            sql = sql.Replace("@subproject", subproject);
            sql = sql.Replace("@delivery", delivery);
            sql = sql.Replace("@iterations", "'" + string.Join("','", iterations.ToArray()) + "'");

            List<DefectsOpen> list = connection.Executar<DefectsOpen>(sql);

            return list;
        }

    }
}