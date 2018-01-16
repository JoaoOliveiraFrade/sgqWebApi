select
    subprojeto as subproject
	,entrega as delivery
	,defeito as defect
    ,encaminhado_para as queue
    ,status
    ,dt_de as startDate
    ,dt_ate as endDate
    ,round(cast((tempo_util) as float) / 60, 1) as workTime
from
    alm_defeitos_tempos
where
    subprojeto = '@subproject'
    and entrega = '@delivery'
    and defeito = @defect
