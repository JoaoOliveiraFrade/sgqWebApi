select
	name as id,
	name + ' (' + text + ')' as name
from 
	SGQ_TrafficLights
order by 
	[order] desc

--select
-- farol_cor as id,
-- farol_cor + ' (' + sla + ')' as name
--from 
--	SGQ_Defeitos_Farol
--order by farol_imagem