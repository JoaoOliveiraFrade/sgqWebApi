using Classes;
using ProjectWebApi.Models;
using ProjectWebApi.Models.Project;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;

namespace ProjectWebApi.Daos {
    public class TestProjDao {
        private Connection connection;

        public TestProjDao() {
            connection = new Connection(Bancos.Sgq);
        }

        public void Dispose() {
            connection.Dispose();
        }

        public IList<simpProject> LoadData() {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\testProj\loadData.sql"), Encoding.Default);
            var listProjects = connection.Executar<simpProject>(sql);
            return listProjects;
        }

        public IList<Project> fromTestManufsAndSystems(testManufsAndSystems parameters) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\testProj\fromTestManufsAndSystems.sql"), Encoding.Default);
            sql = sql.Replace("@testManufs", "'" + string.Join("','", parameters.testManufs) + "'");
            sql = sql.Replace("@systems", "'" + string.Join("','", parameters.systems) + "'");
            var result = connection.Executar<Project>(sql);
            return result;
        }

        public IList<simpProject> fbyDevManufsAndSystems(devManufsAndSystems parameters) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\testProj\fbyDevManufsAndSystems.sql"), Encoding.Default);
            sql = sql.Replace("@devManufs", "'" + string.Join("','", parameters.devManufs) + "'");
            sql = sql.Replace("@systems", "'" + string.Join("','", parameters.systems) + "'");
            var result = connection.Executar<simpProject>(sql);
            return result;
        }

        public IList<Project> fromAgentFbyDevManufsAndSystems(devManufsAndSystems parameters) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\testProj\fromAgentFbyDevManufsAndSystems.sql"), Encoding.Default);
            sql = sql.Replace("@devManufs", "'" + string.Join("','", parameters.devManufs) + "'");
            sql = sql.Replace("@systems", "'" + string.Join("','", parameters.systems) + "'");
            var result = connection.Executar<Project>(sql);
            return result;
        }


        //public IList<Project> fbyproject(IList<string> parameter)
        //{
        //    string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\testProj\fbyproject.sql"), Encoding.Default);
        //    sql = sql.Replace("@projects", "'" + string.Join("','", parameter) + "'");
        //    var list = connection.Executar<Project>(sql);
        //    return list;
        //}

        public IList<Project> byIds(string ids) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\testProj\byIds.sql"), Encoding.Default);
            sql = sql.Replace("@ids", ids);
            var result = connection.Executar<Project>(sql);
            return result;
        }

        public Project getProject(string subproject, string delivery) {
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
        //                        df.Origem like '%CONSTRU플O%' and
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


        public IList<CtImpactedXDefects> getCtImpactedXDefects(string subproject, string delivery) {
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
	            sum(case when name = 'CONSTRU플O' then Qtd_Defeitos else 0 end) as qtyDefectsCons,
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
		            Origem in ('AMBIENTE','CONSTRU플O') and
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
	            convert(varchar, cast(substring('@subproject',4,8) as int)) + ' ' + convert(varchar,cast(substring('@delivery',8,8) as int)) as subDel,
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

            List<CtImpactedXDefects> list = connection.Executar<CtImpactedXDefects>(sql);

            return list;
        }


        public DefectDensity getDefectsDensityByProjectIterations(string subproject, string delivery, List<string> iterations) {
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
                                df.Origem like '%CONSTRU플O%' and
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


        public DetectableInDev getDetectableInDevByProjectIterations(string subproject, string delivery, List<string> iterations) {
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


        public IList<CtImpactedXDefects> getCtImpactedXDefectsIterations(string subproject, string delivery, List<string> iterations) {
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
	            sum(case when name = 'CONSTRU플O' then Qtd_Defeitos else 0 end) as qtyDefectsCons,
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
		            df.Origem in ('AMBIENTE','CONSTRU플O') and
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
	            convert(varchar, cast(substring('@subproject',4,8) as int)) + ' ' + convert(varchar,cast(substring('@delivery',8,8) as int)) as subDel,
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

            List<CtImpactedXDefects> list = connection.Executar<CtImpactedXDefects>(sql);

            return list;
        }

        #region iterations

        public IList<iteration> LoadIterations(SubprojectDelivery subprojectDelivery) {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\testProj\loadIterationsFbyProject.sql"), Encoding.Default);
            sql = sql.Replace("@subproject", subprojectDelivery.subproject);
            sql = sql.Replace("@delivery", subprojectDelivery.delivery);
            var result = connection.Executar<iteration>(sql);
            return result;
        }

        //public List<string> LoadIterationsActive(SubprojectDelivery subprojectDelivery) {
        //    string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\testProj\loadIterationsActiveFbyProject.sql"), Encoding.Default);
        //    sql = sql.Replace("@subproject", subprojectDelivery.subproject);
        //    sql = sql.Replace("@delivery", subprojectDelivery.delivery);

        //    string iterations = connection.Get_String(sql);
        //    string[] stringSeparators = new string[] { "','" };
        //    var list = iterations.Split(stringSeparators, StringSplitOptions.None);

        //    return new List<string>(list);
        //}

        //public List<string> LoadIterationsSelected(SubprojectDelivery subprojectDelivery) {
        //    string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\testProj\loadIterationsSelectedFbyProject.sql"), Encoding.Default);
        //    sql = sql.Replace("@subproject", subprojectDelivery.subproject);
        //    sql = sql.Replace("@delivery", subprojectDelivery.delivery);

        //    string iterations = connection.Get_String(sql);
        //    string[] stringSeparators = new string[] { "','" };
        //    var list = iterations.Split(stringSeparators, StringSplitOptions.None);

        //    return new List<string>(list);
        //}

        //public bool UpdateIterationsActive(ProjectAndListIteration projectAndListIteratio) {
        //    //string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\testProj\updateIterationsActive.sql"), Encoding.Default);
        //    //sql = sql.Replace("@subproject", projectAndListIteratio.subproject);
        //    //sql = sql.Replace("@delivery", projectAndListIteratio.delivery);
        //    //sql = sql.Replace("@iterations", "'" + string.Join("','", projectAndListIteratio.iterations) + "'");
        //    //var result = connection.Executar(sql);
        //    return true;
        //}

        //public bool UpdateIterationsSelected(ProjectAndListIteration projectAndListIteratio) {
        //    //string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\testProj\updateIterationsSelected.sql"), Encoding.Default);
        //    //sql = sql.Replace("@subproject", projectAndListIteratio.subproject);
        //    //sql = sql.Replace("@delivery", projectAndListIteratio.delivery);
        //    //sql = sql.Replace("@iterations", "'" + string.Join("','", projectAndListIteratio.iterations) + "'");
        //    //var result = connection.Executar(sql);
        //    return true;
        //}

        #endregion

        
        public IList<IdName> LoadTestStatus() {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\testProj\loadTestStatus.sql"), Encoding.Default);
            return connection.Executar<IdName>(sql); ;
        }

        public IList<IdName> LoadReleasesLossReason() {
            string sql = File.ReadAllText(HttpContext.Current.Server.MapPath(@"~\sqls\project\testProj\loadReleasesLossReason.sql"), Encoding.Default);
            return connection.Executar<IdName>(sql); ;
        }

    }
}