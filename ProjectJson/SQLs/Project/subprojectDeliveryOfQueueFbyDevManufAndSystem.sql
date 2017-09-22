declare @t table (
	devManuf varchar(50), 
	system varchar(50),
	subprojectDelivery varchar(26)
)
insert into @t 
select distinct
	rtrim(ltrim(substring(queue, len(queue) - charindex('-', reverse(queue)) + 2, 50))) as devManuf
	,rtrim(ltrim(substring(queue, 1, len(queue) - charindex('-', reverse(queue))))) as system
	,subprojectDelivery
from
	(
		select distinct
			replace(dt.encaminhado_para,'�', '-') as queue
			,(d.subprojeto + d.entrega) as subprojectDelivery
		from
			alm_defeitos d
			left join alm_defeitos_tempos dt
			on dt.subprojeto = d.subprojeto and 
				dt.entrega = d.entrega and 
				dt.defeito = d.defeito
		where
			d.origem like '%Constru��o%'
			and d.status_atual = 'Closed'
			and (d.ciclo like '%TI%' or d.ciclo like '%UAT%' or d.ciclo like '%TRG%' or d.ciclo like '%DEV%')
			and dt.status in ('IN_PROGRESS', 'PENDENT (PROGRESS)', 'REOPEN')
	) aux1

select distinct
	devManuf
	,system
	,subprojectDelivery
from
	@t
where
	devManuf not in ('', 'OI','L�DER T�CNICO', '�REA DE NEG�CIOS', '�REA USU�RIA', 'AUTOMA��O', 'ENGENHARIA', 'OI (API)', 'OI (APLICATIVO)') 
order by
	devManuf, system