select distinct
    sgq_p.id
	,cts.Subprojeto as subproject
	,cts.Entrega as delivery
    
	,convert(varchar, cast(substring(cts.Subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(cts.Entrega,8,8) as int)) as subprojectDelivery

    ,biti_s.nome as name
    ,biti_s.classificacao_nome as classification
    ,(select Sigla from sgq_meses m where m.id = sgq_p.currentReleaseMonth) + '/' + convert(varchar, sgq_p.currentReleaseYear) as release
	,replace(replace(replace(replace(replace(biti_s.estado,'CONSOLIDAÇÃO E APROVAÇÃO DO PLANEJAMENTO','CONS/APROV. PLAN'),'PLANEJAMENTO','PLANEJ.'),'DESENHO DA SOLUÇÃO','DES.SOL'),'VALIDAÇÃO','VALID.'),'AGUARDANDO','AGUAR.') as state
	,trafficLight
from 
	ALM_CTs cts with (NOLOCK) 
    inner join sgq_projects sgq_p
		on sgq_p.subproject = cts.Subprojeto and
		   sgq_p.delivery = cts.Entrega

    inner join alm_projetos alm_p WITH (NOLOCK)
		on alm_p.subprojeto = cts.Subprojeto and
			alm_p.entrega = cts.Entrega and
			alm_p.Ativo = 'Y'

    inner join biti_subprojetos biti_s WITH (NOLOCK)
		on biti_s.id = cts.Subprojeto and
		   biti_s.estado <> 'CANCELADO'
where
	(case when IsNull(cts.fabrica_desenvolvimento,'') <> '' then cts.fabrica_desenvolvimento else 'N/A' end) in (@devManufs)
	and (case when IsNull(cts.Sistema,'') <> '' then cts.Sistema else 'N/A' end) in (@systems)
	and sgq_p.currentReleaseYear is not null
order by
    cts.Subprojeto,
    cts.Entrega

------------------------------------------------------

--declare @t table (
--	subproject varchar(11),
--	delivery varchar(15)
--)
--insert into @t 
--select distinct
--	subproject,
--	delivery
--from
--	(
--		select distinct
--			(case when IsNull(cts.fabrica_desenvolvimento,'') <> '' then cts.fabrica_desenvolvimento else 'N/A' end) as devManuf
--			,(case when IsNull(cts.Sistema,'') <> '' then cts.Sistema else 'N/A' end) as system
--			,cts.Subprojeto as subproject
--			,cts.Entrega as delivery
--		from
--			ALM_CTs cts with (NOLOCK) 
--		where
--			--fabrica_desenvolvimento in ('3CON','ACCENTURE','API','APP CO DIGITAL','APP DIGITAL - SISTEMA EXTERNO','ASGA','BRM - SISTEMA EXTERNO','CAPGEMINI','CLARITY','CLEARTECH','CPQD','DIG','DIGICADE','DIST.SIST.EXTERNO','EXPERIAN','GER.INTERF.MAINFRAME','HP','IBM','META','METATRON','MINHA OI MOBILE','NOVELL','OBJECTIVE','OI PLAY','OMS','OSM','PORTAL M2M','PRINTCENTER','PROCWORK','PROTHEUS PDV','PTI','RAID IP MOVEL','SAC - SISTEMA DE ACOMPANHAMENTO DE CONTRATOS','SIST.ENGENHARIA','SQUADRA','SUPRIMENTOS','TDM','TRIAD','WEDO')
--			--and Sistema in ('(OI R2) CLARIFY - AMBIENTE OPEN','(OI R2) CLARIFY - AMBIENTE WEB','(OI R2) CPM','(OI R2) DATA QUALITY','(OI R2) DETRAF','(OI R2) DOC1','(OI R2) DW','(OI R2) EAI','(OI R2) E-BILLING','(OI R2) EBPP','(OI R2) FIX','(OI R2) GDBO','(OI R2) GECO','(OI R2) GENEVA - MÓDULO DE CONVERSOR','(OI R2) GENEVA - MÓDULO DE INTERFACE COMPLEMENTAR','(OI R2) GENEVA - MÓDULO DE INTERFACE SMP','(OI R2) GENEVA - TARIFAÇÃO E FATURAMENTO','(OI R2) GPP','(OI R2) MASC','(OI R2) OBJECTEL','(OI R2) OMS','(OI R2) RAID PI','(OI R2) SAC','(OI R2) SAF','(OI R2) SAG','(OI R2) SAP','(OI R2) SCB','(OI R2) SCO','(OI R2) SCR','(OI R2) SCVR','(OI R2) SFA','(OI R2) SGFT','(OI R2) SID','(OI R2) SPART','(OI R2) TRANSACT','(OI R2) UNIPRO','(OI R2) URA - OUTRAS','ABC / ABD','ACM','API','APP CO DIGITAL','APP DIGITAL - SISTEMA EXTERNO','ARBOR','ARS CGS','BDA-OI','BLL','BRM - SISTEMA EXTERNO','CBILL','CDI','CIC - ESCADINHA','CLARITY','CLARITYPPM','CLICK','CLICK - MUA','CLICK - PO','CMDB - PORTAL','CMS','CONECTOR PORTABILIDADE','COTAR','DIG','DISTRIBUIDOR - SISTEMA EXTERNO','DW','DW CADASTRO','DW FINANCEIRO','DW GESTÃO AMBIENTE','DW ORACLE','DW TRÁFEGO','EBILLING','E-CHANNEL','ENTIREX','EXPEDITER','FGC - FATURAMENTO','FORTUNA','GATEWAY DE PORTABILIDADE','GCOB','GEOPLEX','GEOSITE','GERENCIADOR DE INTERFACE MAINFRAME','GESTÃO DE TARIFAÇÃO FIXA (GTF)','GF','GRANITE','GRIF','HPFMS FIXA','HPFMS MÓVEL','HYPERION NOVA OI','ICS','INFORMÁTICA - DW','INFORMÁTICA - INTEGRAÇÃO','KANNEL','LSV','MEDIAÇÃO FIXA - MM','MEDIAÇÃO FIXA DE NEGÓCIOS','MEDIAÇÃO FIXA DE TERCEIROS (MAINFRAME)','MEDIAÇÃO MÓVEL - MM','MINHA OI MOBILE','N/A','NDS','NETWIN','NF-PREPAGO','NOTA FISCAL ELETRONICA','OI ATENDE','OI GESTOR','OI LEGAL','OI PLAY','OI VENDE','OM','OMS','OSM','OSVC','PARC','PMS','PORTAL M2M','PORTAL OI','PRINTCENTER','PROTHEUS PDV','PW. SPED PIS COFINS','PW.SATI - SOLUÇÕES FISCAIS','PW.SPED FISCAL','PW.SVA - IN86','RAID FR FIXA','RAID FR MÓVEL','RAID IP FIXA','RAID IP MOVEL','RAID IP MÓVEL','RATV','RECARGA AUTOMÁTICA','ROAMBROKER','SAC - SISTEMA DE ACOMPANHAMENTO DE CONTRATOS','SAP','SCA','SCF1','SEDO','SERVCEL','SFA-SIEBEL8','SGIS','SIAF','SIDARA','SIEBEL','SIEBEL 8','SIEBEL INFORMACIONAL','SIEBEL MARKETING','SIEBEL SFA WEB','SINN DTH','SINNWEB','SIS ATIVAÇÃO FIXA','SIS FALHAS','SIS MÓVEL','SISGEN BRASIL','SISRAF - APROPRIAÇÃO','SISRAF - ARRECADAÇÃO','SISRAF - ATENDIMENTO DE CONTA','SISRAF - CADASTRO DE FATURAMENTO','SISRAF - CO-BILLING FISCAL','SISRAF - COBRANÇA','SISRAF - CONTABILIZAÇÃO','SISRAF - CONTESTAÇÃO','SISRAF - FATURAMENTO','SISRAF - FISCAL','SISRAF - MPN','SISRAF - PARCELAMENTO','SISRAF - SEGUNDA VIA DE CONTA','SISRAF - TARIFAÇÃO','SISRED','SISTEMAS DA ENGENHARIA','SOA','STC DADOS','STC VOZ','SUBSCRITORES','SVOI','TARIFAÇÃO REDE INTELIGENTE','TDM','TERADATA','THS','TRANSACT','TVAS','URA','URA FIXA','URA MÓVEL','VAS','VASPROXY','VITRIA','(OI R2) ASAP')
--			(case when IsNull(cts.fabrica_desenvolvimento,'') <> '' then cts.fabrica_desenvolvimento else 'N/A' end) in (@devManufs)
--			and (case when IsNull(cts.Sistema,'') <> '' then cts.Sistema else 'N/A' end) in (@systems)
--	) aux1
--	inner join ALM_Projetos as alm_p with (NOLOCK) 
--		on alm_p.Subprojeto = aux1.subproject and
--			alm_p.Entrega = aux1.delivery and
--			alm_p.Ativo = 'Y'

--	inner join biti_subprojetos biti_s WITH (NOLOCK)
--	on biti_s.id = aux1.subproject and
--		biti_s.estado <> 'CANCELADO'

--select 
--    sgq_p.id,
--    t.subproject as subproject,
--    t.delivery as delivery,
--    convert(varchar, cast(substring(t.subproject,4,8) as int)) + ' ' + convert(varchar,cast(substring(t.delivery,8,8) as int)) as subprojectDelivery,
--    biti_s.nome as name,
--    biti_s.classificacao_nome as classification,
--    (select Sigla from sgq_meses m where m.id = sgq_p.currentReleaseMonth) + ' ' + convert(varchar, sgq_p.currentReleaseYear) as release,
--	replace(replace(replace(replace(replace(biti_s.estado,'CONSOLIDAÇÃO E APROVAÇÃO DO PLANEJAMENTO','CONS/APROV. PLAN'),'PLANEJAMENTO','PLANEJ.'),'DESENHO DA SOLUÇÃO','DES.SOL'),'VALIDAÇÃO','VALID.'),'AGUARDANDO','AGUAR.') as state,
--	trafficLight
--from 
--	@t t
--    inner join sgq_projects sgq_p
--		on sgq_p.subproject = t.subproject and
--		   sgq_p.delivery = t.delivery

--    inner join alm_projetos alm_p WITH (NOLOCK)
--		on alm_p.subprojeto = t.subproject and
--			alm_p.entrega = t.delivery

--    inner join biti_subprojetos biti_s WITH (NOLOCK)
--		on biti_s.id = t.subproject
--where 
--	sgq_p.currentReleaseYear is not null
--order by
--    t.subproject,
--    t.delivery

------------------------------------------------------

--select
--	id,
--	subproject,
--	delivery,
--	subprojectDelivery,
--	name,
--	objective,
--	classification,
--	state,
--	release,
--	GP,
--	N3,
--	UN,
--	trafficLight,
--	rootCause,
--	actionPlan,
--	informative,
--	attentionPoints,
--	attentionPointsOfIndicators,
--	IterationsActive,
--	IterationsSelected,

--	(select count(*) 
--	from ALM_CTs WITH (NOLOCK)
--	where 
--		subprojeto = aux.subproject and
--		entrega = aux.delivery and
--		Status_Exec_CT <> 'CANCELLED'
--	) as total,

--	(select count(*) 
--	from ALM_CTs WITH (NOLOCK)
--	where 
--		subprojeto = aux.subproject and
--		entrega = aux.delivery and
--		Status_Exec_CT <> 'CANCELLED' and
--		substring(dt_planejamento,7,2) + substring(dt_planejamento,4,2) + substring(dt_planejamento,1,2) <= convert(varchar(6), getdate(), 12) and
--		dt_planejamento <> ''
--	) as planned,

--	(select count(*)
--	from ALM_CTs WITH (NOLOCK)
--	where 
--		subprojeto = aux.subproject and
--		entrega = aux.delivery and
--		Status_Exec_CT = 'PASSED' and 
--		dt_execucao <> ''
--	) as realized,

--	(select 
--		(case when sum(planned) - sum(realized) >= 0 then sum(planned) - sum(realized) else 0 end) as GAP
--	from
--		(
--		select 
--			substring(dt_planejamento,4,5) as date, 
--			1 as planned,
--			0 as realized
--		from ALM_CTs WITH (NOLOCK)
--		where 
--			subprojeto = aux.subproject and
--			entrega = aux.delivery and
--			Status_Exec_CT <> 'CANCELLED' and
--			substring(dt_planejamento,7,2) + substring(dt_planejamento,4,2) + substring(dt_planejamento,1,2) <= convert(varchar(6), getdate(), 12) and
--			dt_planejamento <> ''

--		union all

--		select 
--			substring(dt_execucao,4,5) as date, 
--			0 as planned,
--			1 as realized
--		from ALM_CTs WITH (NOLOCK)
--		where 
--			subprojeto = aux.subproject and
--			entrega = aux.delivery and
--			Status_Exec_CT = 'PASSED' and 
--			dt_execucao <> ''
--		) Aux
--	) as gap
--from
--	(
--    select 
--        sgq_p.id,
--        sgq_p.subproject as subproject,
--        sgq_p.delivery as delivery,
--        convert(varchar, cast(substring(sgq_p.subproject,4,8) as int)) + ' ' + convert(varchar,cast(substring(sgq_p.delivery,8,8) as int)) as subprojectDelivery,
--        biti_s.nome as name,
--        biti_s.objetivo as objective,
--        biti_s.classificacao_nome as classification,
--        replace(replace(replace(replace(replace(biti_s.estado,'CONSOLIDAÇÃO E APROVAÇÃO DO PLANEJAMENTO','CONS/APROV. PLAN'),'PLANEJAMENTO','PLANEJ.'),'DESENHO DA SOLUÇÃO','DES.SOL'),'VALIDAÇÃO','VALID.'),'AGUARDANDO','AGUAR.') as state,
--        (select Sigla from sgq_meses m where m.id = sgq_p.currentReleaseMonth) + ' ' + convert(varchar, sgq_p.currentReleaseYear) as release,
--        biti_s.Gerente_Projeto as GP,
--        biti_s.Gestor_Do_Gestor_LT as N3,
--        biti_s.UN as UN,
--        sgq_p.trafficLight as trafficLight,
--        sgq_p.rootCause as rootCause,
--        sgq_p.actionPlan as actionPlan,
--        sgq_p.informative as informative,
--        sgq_p.attentionPoints as attentionPoints,
--        sgq_p.attentionPointsIndicators as attentionPointsOfIndicators,
--        sgq_p.IterationsActive,
--        sgq_p.IterationsSelected
--    from 
--        sgq_projects sgq_p
--        inner join alm_projetos alm_p WITH (NOLOCK)
--        on alm_p.subprojeto = sgq_p.subproject and
--           alm_p.entrega = sgq_p.delivery

--        inner join biti_subprojetos biti_s WITH (NOLOCK)
--        on biti_s.id = sgq_p.subproject

--		inner join
--		(
--		select distinct
--			aux.Subprojeto,
--			aux.Entrega
--		from
--			(select distinct
--				cts.Subprojeto,
--				cts.Entrega,
--				cts.fabrica_desenvolvimento as devManuf, 
--				cts.Sistema as system 
--				from
--				ALM_CTs cts with (NOLOCK) 

--			union all

--			select distinct
--				d.Subprojeto,
--				d.Entrega,
--				d.fabrica_desenvolvimento as devManuf, 

--				d.Sistema_Defeito as system 
--			from
--				ALM_Defeitos d with (NOLOCK) 
--			) as aux
--			inner join ALM_Projetos as alm_p with (NOLOCK) 
--				on alm_p.Subprojeto = aux.Subprojeto and
--					alm_p.Entrega = aux.Entrega and
--					alm_p.Ativo = 'Y'

--			inner join biti_subprojetos biti_s WITH (NOLOCK)
--			on biti_s.id = alm_p.subprojeto and
--				biti_s.estado <> 'CANCELADO'
--		where
--			devManuf in (@devManufs) and
--			system in (@systems)
--		) aux2
--		on aux2.subprojeto = sgq_p.subproject and
--		   aux2.entrega = sgq_p.delivery

--    where 
--		sgq_p.currentReleaseYear is not null
--	) aux
--order by
--    subproject,
--    delivery
