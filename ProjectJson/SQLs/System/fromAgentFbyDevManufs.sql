declare @t table (
	devManuf varchar(50), 
	system varchar(50)
)
insert into @t 
select distinct
	rtrim(ltrim(substring(queue, len(queue) - charindex('-', reverse(queue)) + 2, 50))) as devManuf
	,rtrim(ltrim(substring(queue, 1, len(queue) - charindex('-', reverse(queue))))) as system
from
	(
		select distinct
			replace(dt.encaminhado_para,'–', '-') as queue
			,(d.subprojeto + d.entrega) as subprojectDelivery
		from
			alm_defeitos d
			left join alm_defeitos_tempos dt
			on dt.subprojeto = d.subprojeto and 
				dt.entrega = d.entrega and 
				dt.defeito = d.defeito
		where
			d.origem like '%Construção%'
			and d.status_atual = 'Closed'
			and (d.ciclo like '%TI%' or d.ciclo like '%UAT%' or d.ciclo like '%TRG%' or d.ciclo like '%DEV%')
			and dt.status in ('IN_PROGRESS', 'PENDENT (PROGRESS)', 'REOPEN')
	) aux1

select distinct
	devManuf
	,system
from
	@t
where
	devManuf not in ('', 'OI','LÍDER TÉCNICO', 'ÁREA DE NEGÓCIOS', 'ÁREA USUÁRIA', 'AUTOMAÇÃO', 'ENGENHARIA', 'OI (API)', 'OI (APLICATIVO)')
where
    devManuf in (@devManufs)
	

order by
	devManuf, system
