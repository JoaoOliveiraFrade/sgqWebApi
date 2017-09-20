declare @t table (
	devManuf varchar(50)
)
insert into @t 
select distinct
	rtrim(ltrim(substring(queue, len(queue) - charindex('-', reverse(queue)) + 2, 50))) as devManuf
from
	(
		select distinct
			replace(encaminhado_para,'–', '-') as queue
		from
			alm_defeitos_tempos
		where
			status not in ('IN_PROGRESS', 'PENDENT (PROGRESS)', 'REOPEN')
	) aux1

select 
	devManuf as id,
	devManuf as name
from
	@t
where
	devManuf not in ('', 'OI',' LÍDER TÉCNICO', 'ÁREA DE NEGÓCIOS', 'ÁREA USUÁRIA', 'AUTOMAÇÃO', 'ENGENHARIA', 'OI (API)', 'OI (APLICATIVO)')
order by
	1

