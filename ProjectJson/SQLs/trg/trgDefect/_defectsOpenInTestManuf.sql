select 
	convert(varchar, cast(substring(Subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(Entrega,8,8) as int)) as subDel,
	subprojeto as subproject,
	entrega as delivery,

	d.Defeito as id,

    case d.Status_Atual 
        when 'ON_RETEST' then 'On Retest'
        when 'PENDENT (RETEST)' then 'Pend.Retest'
        when 'REJECTED' then 'Reject'
        else 'Indefinido'
    end as status,

	UPPER(LEFT(left(d.Encaminhado_Para,20),1))+LOWER(SUBSTRING(left(d.Encaminhado_Para,20),2,LEN(left(d.Encaminhado_Para,20)))) as queue,
	UPPER(LEFT(left(d.Sistema_Defeito,20),1))+LOWER(SUBSTRING(left(d.Sistema_Defeito,20),2,LEN(left(d.Sistema_Defeito,20)))) as defectSystem,
	UPPER(LEFT(substring(d.severidade,3,3),1))+LOWER(SUBSTRING(substring(d.severidade,3,3),2,LEN(substring(d.severidade,3,3)))) as severity,
    d.Aging as aging,
	substring('-  ',2+convert(int,sign(d.aging)),1) + right(convert(varchar, floor(abs(d.aging))), 3) + ':' + right('00' + convert(varchar,round( 60*(abs(d.aging)-floor(abs(d.aging))), 0)), 2) as agingFormat,
    d.Ping_Pong as pingPong
from 
	ALM_Defeitos d WITH (NOLOCK)
where
	d.subprojeto = 'TRG2017' 
	--and d.subprojeto = 'TRG@yyyy' 
	and d.ciclo like ('%@mmm/@yy%')
	and @systemConditionDefect
	and d.Status_Atual in ('ON_RETEST','PENDENT (RETEST)','REJECTED')
order by 
    4