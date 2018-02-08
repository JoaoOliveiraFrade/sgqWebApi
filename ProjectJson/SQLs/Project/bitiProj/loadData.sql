select
	subDel
	,name
	,state
	,classification
	,un

	,releaseSGQ
	,case when releaseSGQ <> '' then right(releaseSGQ,2) + '/' + left(releaseSGQ,4) else '' end as releaseSGQFormat

	,case when releaseClarity <> '' then releaseClarity else releaseClarityEvento end as releaseClarity
	,case when (case when releaseClarity <> '' then releaseClarity else releaseClarityEvento end) <> '' then right((case when releaseClarity <> '' then releaseClarity else releaseClarityEvento end),2) + '/' + left((case when releaseClarity <> '' then releaseClarity else releaseClarityEvento end),4) else '' end as releaseClarityFormat

	,category

	,GP --projectManager
	,substring(GP_N4, 1, charindex(' ', GP_N4)) + substring(GP_N4, len(GP_N4) - charindex(' ', reverse(GP_N4))+2, 50) as GP_N4
	,substring(GP_N3, 1, charindex(' ', GP_N3)) + substring(GP_N3, len(GP_N3) - charindex(' ', reverse(GP_N3))+2, 50) as GP_N3

	,LT --technicalLeader
	,LT_N4 --LTManager
	,substring(LT_N3, 1, charindex(' ', LT_N3)) + substring(LT_N3, len(LT_N3) - charindex(' ', reverse(LT_N3))+2, 50) as LT_N3

	,PMO
	,businessAnalyst
	,substring(testLeader, 1, charindex(' ', testLeader)) + substring(testLeader, len(testLeader) - charindex(' ', reverse(testLeader))+2, 50) as testLeader
	,substring(systemsDev,1,len(systemsDev)-1) as systemsDev
	,substring(systemsTest,1,len(systemsTest)-1) as systemsTest
	,hasTest
	,finishedSystemsInDSOL
	,qualityProposalWithGo
	,approvedSolutionDrawing
	,scheduleApproved
	,preCandidature
	,candidature
	,constructionCompleted
	,hasGMUD
	,favorableOpinionImplantation
from
	(
		select
			--convert(varchar, cast(substring(sp.id,4,8) as int)) + ' ' + convert(varchar,cast(substring(en.id,8,8) as int)) as subDel
			substring(sp.id,7,5) + ' ' + substring(en.id,11,5) as subDel

			,sp.Nome as name

			,(case 
				when sp.Estado = 'EM CONSOLIDAÇÃO E APROVAÇÃO DO DESENHO DA SOLUÇÃO' then 'CONSOL. E APROV. DSOL'
				when sp.Estado = 'EM CONSOLIDAÇÃO E APROVAÇÃO DO PLANEJAMENTO' then 'CONSOL. E APROV. PLAN'
				when sp.Estado = 'AGUARDANDO APROVAÇÃO FINANCEIRA' then 'AGUAR. APROV. FINANC.'
				when sp.Estado = 'SUBPROJETO EM CRIAÇÃO' then 'SUBPRJ. EM CRIAÇÃO'
				when sp.Estado = 'EM DESENHO DA SOLUÇÃO' then 'EM DSOL'
				when sp.Estado = 'AGUARDANDO VALIDAÇÃO DE REQUISITOS' then 'AGUAR. VAL. REQUISITOS'
				when sp.Estado = 'AGUARDANDO VALIDAÇÃO DA MACRO ESTIMATIVA' then 'AGUAR. VAL. MACRO ESTI.'
				when sp.Estado = 'EM PLANEJAMENTO PRELIMINAR' then 'EM PLAN. PRELIMINAR' 
				else sp.Estado 
			end) as state

			,sp.Classificacao_Nome as classification

			,(case 
				when sp.UN = 'MARKETING E VAREJO MOBILIDADE' then 'MKT E VAREJO MOBIL.'
				when sp.UN = 'MARKETING E VAREJO RESIDENCIAL' then 'MKT E VAREJO RES.'
				when sp.UN = 'DESENVOLVIMENTO DE SOLUÇÕES' then 'DESENV. DE SOLUÇÕES'
				when sp.UN = 'PLANEJAMENTO E GOVERNANÇA' then 'PLANEJAMENTO E GOV.' 
				else sp.UN 
			end) as UN

			,isNull((select 
				max(
					convert(varchar, re.release_ano) + right('00' + convert(varchar, re.release_mes), 2)
				)
			from 
				SGQ_Releases_Entregas re
			where
				re.subprojeto = sp.id and 
				re.entrega = en.id
			),'') as releaseSGQ

			,isNull((select top 1
				case when isnull(ex.release_nome,'') <> ''
					then right(ex.release_nome,4) + right('00' + convert(varchar, (select m.id from sgq_meses m where m.nome = (select left(ex.release_nome,len(ex.release_nome)-5)))),2)
					else null
				end
			from 
				biti_Execucoes ex
				inner join biti_Frentes_Trabalho ft
					on ft.subprojeto = sp.id and
						ft.estado not in ('CANCELADA', 'CANCELADA SEM DESENHO', 'PARTICIPAÇÃO RECUSADA', 'REQUISITOS RECUSADOS')
			where 
				ex.subprojeto = sp.id and
				ex.entrega = en.id and
				ex.estado <> 'CANCELADA'
			order by 1 desc
			),'') as releaseClarity

			,isNull((select 
				max(
					case when isnull(ex.dt_evento,'') <> ''
						then '20' + substring(ex.dt_evento, 7,2) + substring(ex.dt_evento, 4,2)
						else null
					end
				)
			from 
				biti_Execucoes ex
				inner join biti_Frentes_Trabalho ft
					on ft.subprojeto = sp.id and
						ft.estado not in ('CANCELADA', 'CANCELADA SEM DESENHO', 'PARTICIPAÇÃO RECUSADA', 'REQUISITOS RECUSADOS')
			where 
				ex.subprojeto = sp.id and
				ex.entrega = en.id and
				ex.estado <> 'CANCELADA'
			),'') as releaseClarityEvento

			,(case 
				when sp.Categoria = 'MELHORIA OPERACIONAL' then 'MO - MELHORIA OPER.'
				when sp.Categoria = 'ORL – OBRIGAÇÃO REGULATÓRIA/LEGAL' then 'ORL - OBRIG. REG./LEGAL'
				when sp.Categoria = 'OT – OBRIGAÇÃO TRIBUTÁRIA' then 'OT – OBRIG. TRIBUTÁRIA'
				else sp.Categoria
			end) as category

			,substring(sp.PMO, 1, charindex(' ', sp.PMO)) + substring(sp.PMO, len(sp.PMO) - charindex(' ', reverse(sp.PMO))+2, 50) as PMO
			,substring(sp.Analista_Negocio, 1, charindex(' ', sp.Analista_Negocio)) + substring(sp.Analista_Negocio, len(sp.Analista_Negocio) - charindex(' ', reverse(sp.Analista_Negocio))+2, 50) as businessAnalyst

			,substring(sp.Gerente_Projeto, 1, charindex(' ', sp.Gerente_Projeto)) + substring(sp.Gerente_Projeto, len(sp.Gerente_Projeto) - charindex(' ', reverse(sp.Gerente_Projeto))+2, 50) as GP --projectManager
			,(select top 1 gestor_n4 from biti_Usuarios where nome = sp.gerente_projeto) as GP_N4
			,(select top 1 gestor_n3 from biti_Usuarios where nome = sp.gerente_projeto) as GP_N3
			,substring(sp.Lider_Tecnico, 1, charindex(' ', sp.Lider_Tecnico)) + substring(sp.Lider_Tecnico, len(sp.Lider_Tecnico) - charindex(' ', reverse(sp.Lider_Tecnico))+2, 50) as LT --technicalLeader
			,substring(sp.Gestor_Direto_LT, 1, charindex(' ', sp.Gestor_Direto_LT)) + substring(sp.Gestor_Direto_LT, len(sp.Gestor_Direto_LT) - charindex(' ', reverse(sp.Gestor_Direto_LT))+2, 50) as LT_N4 --LTManager
			,sp.Gestor_Do_Gestor_LT as LT_N3

			,isNull((select top 1 
				ft.Responsavel_Tecnico 
			from 
				biti_Frentes_Trabalho ft
			where 
				ft.subprojeto = sp.id
				and ft.Area in ('TESTES', 'TESTES E RELEASE', 'QUALIDADE TI', 'SUPORTE E PROJETOS', 'TRANSFORMACAO DE BSS', 'GARANTIA DA QUALIDADE')
				and ft.Estado not in ('CANCELADA', 'CANCELADA SEM DESENHO', 'PARTICIPAÇÃO RECUSADA', 'REQUISITOS RECUSADOS')
				and ft.Responsavel_Tecnico <> ''
			),'') as testLeader

		    ,sp.Sistema_Principal as mainSystem

			,replace(
				replace(
					(select  
						sistema_nome as sistema
						from biti_Frentes_Trabalho ft
						where ft.subprojeto = sp.id and 
							ft.sistema_nome <> 'NÃO INFORMADO' and
							ft.tipo_envolvimento in ('DESENVOLVIMENTO','DESENVOLVIMENTO E TESTES','') and
							ft.Estado not in ('PARTICIPAÇÃO RECUSADA', 'REQUISITOS RECUSADOS', 'CANCELADA SEM DESENHO', 'CANCELADA') and 
							ft.Responsavel_Tecnico <> ''
					for xml path('')
				), '<sistema>', '')
			, '</sistema>', ',  ') as systemsDev

			,replace(
				replace(
					(select  
						sistema_nome as sistema
						from biti_Frentes_Trabalho ft
						where ft.subprojeto = sp.id and 
							ft.sistema_nome <> 'NÃO INFORMADO' and
							ft.tipo_envolvimento = 'TESTES' and
							ft.Estado not in ('PARTICIPAÇÃO RECUSADA', 'REQUISITOS RECUSADOS', 'CANCELADA SEM DESENHO', 'CANCELADA') and 
							ft.Responsavel_Tecnico <> ''
					for xml path('')
					), '<sistema>', '')
			, '</sistema>', ',  ') as systemsTest

			,(case when exists (
					select 1 
					from 
						biti_Frentes_Trabalho ft
					where 
						ft.subprojeto = sp.id
						and ft.Area in ('TESTES', 'TESTES E RELEASE', 'QUALIDADE TI', 'SUPORTE E PROJETOS', 'TRANSFORMACAO DE BSS', 'GARANTIA DA QUALIDADE')
						and ft.Estado not in ('CANCELADA', 'CANCELADA SEM DESENHO', 'PARTICIPAÇÃO RECUSADA', 'REQUISITOS RECUSADOS')
					)
				then 'SIM'
				else 'NÃO'
			end) as hasTest

			,(case when
				sp.Estado not in (
					'Subprojeto em Criação', 
					'Em Avaliação de Arquitetura',
					'Aguardando Validação de Requisitos', 
					'Em Macro Estimativa', 
					'Aguardando Validação da Macro Estimativa', 
					'Em Planejamento Preliminar'
				) and
				not exists (
					select 1
					from BITI_Frentes_Trabalho ft
						left join BITI_Desenhos ds 
							on ds.Frente_Trabalho = ft.id
					where 
						ft.Subprojeto = sp.id and
						ft.Sistema_nome <> 'Não Informado' and
						ds.Estado = 'Em desenho da solução'
				)
				then 'SIM'
				else 'NÃO'
			end) as finishedSystemsInDSOL

			,(case when 
				exists (
					select 1
					from BITI_Frentes_Trabalho ft
						left join BITI_Propostas pt
							on pt.subprojeto = ft.Subprojeto
							and pt.Frente_Trabalho = ft.id
					where 
						ft.Subprojeto = sp.id
						and pt.Estado = 'Proposta com GO'
				)
				then 'SIM'
				else 'NÃO'
			end) as qualityProposalWithGo

			,(case when exists (
					select 1
					from biti_documentos_controlados 
					where 
						Objeto = sp.id
						and Tipo in ('DESENHO DA SOLUÇÃO','DESENHO DA SOLUÇÃO COM ESTRATÉGIA DE TESTES')
						and Estado = 'Aprovado'
					)
				then 'SIM'
				else 'NÃO'
			end) as approvedSolutionDrawing

			,(case when exists (
					select 1
					from biti_Documentos_Controlados 
					where 
						Objeto = sp.id
						and Tipo = 'Cronograma'
						and Estado = 'Aprovado'
					)
				then 'SIM'
				else 'NÃO'
			end) as scheduleApproved

			,(case when exists (
					select 1
					from biti_Execucoes ex
						inner join biti_Frentes_Trabalho ft
							on ft.subprojeto = sp.id
						inner join biti_sistemas si
							on si.id = ft.sistema
					where
						ex.subprojeto = sp.id
						and ft.estado not in ('CANCELADA', 'CANCELADA SEM DESENHO', 'PARTICIPAÇÃO RECUSADA', 'REQUISITOS RECUSADOS')
						and ex.release <> ''
						and ex.evento = ''
						and ex.estado <> 'CANCELADA'
						and si.area_qualidade <> 'SIM'
					)
				then 'NÃO'
				else 'SIM'
			end) as preCandidature


			,(case when exists (
					select 1 
					from biti_Execucoes ex
					where ex.subprojeto = sp.id
						and ex.Entrega = en.id
						and ex.Evento not in ('', 'PRÉ-CANDIDATURA')
					)
				then 'SIM'
				else 'NÃO'
			end) as candidature

			,(case when exists (
					select 1 
					from biti_Execucoes ex
					where 
						ex.subprojeto = sp.id 
						and ex.entrega = en.id
						and ex.Estado in ('AGUARDANDO INÍCIO DOS TESTES', 'IMPLANTADA', 'CANCELADA', 'AGUARDANDO CONFIRMAÇÃO PARA IMPLANTAÇÃO', 'EM TESTE', 'ENCERRADA', 'EM IMPLANTAÇÃO', 'EM  IMPLANTAÇÃO')
					)
				then 'SIM'
				else 'NÃO' -- ('AGUARDANDO INÍCIO DO TRABALHO', 'EM CRIAÇÃO', 'EXECUÇÃO PLANEJADA', 'EM CONSTRUÇÃO')
			end) as constructionCompleted

			,(case when exists (
					select 1 
					from biti_Execucoes ex
					where 
						ex.subprojeto = sp.id 
						and ex.entrega = en.id
						and ex.Estado in ('AGUARDANDO INÍCIO DOS TESTES', 'IMPLANTADA', 'CANCELADA', 'AGUARDANDO CONFIRMAÇÃO PARA IMPLANTAÇÃO', 'EM TESTE', 'ENCERRADA', 'EM IMPLANTAÇÃO', 'EM  IMPLANTAÇÃO')
						and ex.Tem_GMUD = 'SIM'
					)
				then 'SIM'
				else 'NÃO'
			end) as hasGMUD

			,en.Parecer_Favoravel_Implantacao as favorableOpinionImplantation

		from
			biti_Subprojetos sp

			inner join biti_Entregas en
				on en.subprojeto = sp.id
		where
			-- substring(sp.id,7,5) in ('00753','01149','01425','01434', '01442')
			sp.status = 'ATIVO'
			and sp.estado not in ('SUSPENSO','CANCELADO', 'ENCERRADO')
			and en.estado not in('ENTREGA CANCELADA', 'ENCERRADA')
			and exists -- subprojeto e entrega com sistema da área de qualidade
				( 
				select 1
				from biti_Execucoes ex 
					inner join biti_Frentes_Trabalho ft 
						on ft.id = ex.frente_trabalho
				where 
					ex.subprojeto = sp.id
					and ex.entrega = en.id
					and (select si.Area_Qualidade from biti_Sistemas si where si.id = ft.Sistema) = 'SIM' 
					and ft.estado not in ('CANCELADA', 'CANCELADA SEM DESENHO', 'PARTICIPAÇÃO RECUSADA', 'REQUISITOS RECUSADOS')
					and ex.estado not in ('CANCELADA','ENCERRADA', 'IMPLANTADA')
				)
	) a1
order by
	subDel
