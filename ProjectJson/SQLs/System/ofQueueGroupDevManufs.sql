declare @t table (
	devManuf varchar(50), 
	system varchar(50)
)
insert into @t 
select distinct
	rtrim(ltrim(substring(q, len(queue) - charindex('-', reverse(q)) + 2, 50))) as devManuf,
	rtrim(ltrim(substring(q, 1, len(queue) - charindex('-', reverse(q))))) as system
from
	(
		select distinct
			replace(encaminhado_para,'–', '-') as q,
			encaminhado_para as queue,
			status
		from
			alm_defeitos_tempos
		where
			status in ('IN_PROGRESS', 'PENDENT (PROGRESS)', 'REOPEN') 
	) aux1

select 
	devManuf, system
from
	@t
where
	devManuf not in ('', 'OI','LÍDER TÉCNICO', 'ÁREA DE NEGÓCIOS', 'ÁREA USUÁRIA', 'AUTOMAÇÃO', 'ENGENHARIA', 'OI (API)', 'OI (APLICATIVO)')
order by
	devManuf, system