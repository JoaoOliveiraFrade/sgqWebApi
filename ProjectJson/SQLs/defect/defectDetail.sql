            select 
	            convert(varchar, cast(substring(Subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(Entrega,8,8) as int)) as subDel,
	            subprojeto as subproject,
	            entrega as delivery,

	            Defeito as id,
	            d.Nome as name,
	            Ciclo as cycle,
	            CT,
				(select nome from alm_cts cts where cts.subprojeto = d.subprojeto and cts.entrega = d.entrega and cts.ct = d.CT) as ctName,
	            Sistema_CT as ctSystem,
                Sistema_Defeito as defectSystem,
	            Fabrica_Desenvolvimento as devManuf,
	            Fabrica_Teste as testManuf,
	            Encaminhado_Para as queue,
	            substring(Severidade,3,10) as severity,
	            Origem as origin,
	            natureza as nature,
                Status_Atual as status,
				
				sgqD.auditStatus,
				sgqD.week,
				sgqD.release,
				sgqD.offender,
				sgqD.ruleInfringed,
				sgqD.trafficLight,

                Dt_Inicial as dtOpening,
	            Dt_Prevista_Solucao_Defeito as dtForecastingSolution,

                erro_detectavel_em_desenvolvimento as detectableInDev,

                Qtd_Reopen as qtyReopened,
                Qtd_CTs_Impactados as qtyImpactedCTs,

	            d.Ping_Pong as qtyPingPong,

      	       --round(
		           -- cast(
			          --    (select Sum(Tempo_Util) 
			          --     from ALM_Defeitos_Tempos dt WITH (NOLOCK)
			          --     where dt.Subprojeto = d.Subprojeto and 
			          --           dt.Entrega = d.Entrega and 
					        --     dt.Defeito = d.Defeito)
		           -- as float ) / 60, 2
	            --) as qtyBusinessHours,

                d.aging,
   	            substring('-  ',2+convert(int,sign(d.aging)),1) + right(convert(varchar, floor(abs(d.aging))), 3) + ':' + right('00' + convert(varchar,round( 60*(abs(d.aging)-floor(abs(d.aging))), 0)), 2) as agingFormat,

                (select top 1 novo_valor
                from ALM_Historico_Alteracoes_Campos h WITH(NOLOCK)
                    where
                      h.subprojeto = d.subprojeto and
                      h.entrega = d.entrega and
                      h.tabela_id = d.Defeito and
                      h.tabela = 'BUG' and
                      h.campo = 'COMENTÁRIOS'
                    order by
                      convert(datetime, dt_alteracao, 5) desc) as Comments
            from 
	            ALM_Defeitos d WITH (NOLOCK)
	            left join BITI_Subprojetos sp WITH (NOLOCK)
		            on sp.id = d.subprojeto
	            left join sgqDefects sgqD WITH (NOLOCK)
		            on sgqD.subproject = d.subprojeto and
		               sgqD.delivery = d.entrega and
		               sgqD.id = d.Defeito

            where
                subprojeto = '@subproject' and
                entrega = '@delivery' and
	            Defeito = @defect
                --subprojeto = '@subproject' and
                --entrega = '@delivery' and
	            --Defeito = 63
