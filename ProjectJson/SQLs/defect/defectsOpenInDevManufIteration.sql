select 
	convert(varchar, cast(substring(cts.Subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(cts.Entrega,8,8) as int)) as subDel,
	cts.subprojeto as subproject,
	cts.entrega as delivery,

	df.Defeito as id,

    case df.Status_Atual 
        when 'NEW' then 'New'
        when 'IN_PROGRESS' then 'In Progr.'
        when 'MIGRATE' then 'Migrate'
        when 'PENDENT (PROGRESS)' then 'Pend.Progr.'
        when 'REOPEN' then 'Reopen'
        else 'Indefinido'
    end as status,

	UPPER(LEFT(left(df.Encaminhado_Para,20),1))+LOWER(SUBSTRING(left(df.Encaminhado_Para,20),2,LEN(left(df.Encaminhado_Para,20)))) as queue,
	UPPER(LEFT(left(df.Sistema_Defeito,20),1))+LOWER(SUBSTRING(left(df.Sistema_Defeito,20),2,LEN(left(df.Sistema_Defeito,20)))) as defectSystem,
	UPPER(LEFT(substring(severidade,3,3),1))+LOWER(SUBSTRING(substring(severidade,3,3),2,LEN(substring(severidade,3,3)))) as severity,
    df.Aging as aging,
	substring('-  ',2+convert(int,sign(df.aging)),1) + right(convert(varchar, floor(abs(df.aging))), 3) + ':' + right('00' + convert(varchar,round( 60*(abs(df.aging)-floor(abs(df.aging))), 0)), 2) as agingFormat,
    df.Ping_Pong as pingPong
from 
				alm_cts cts 
				inner join alm_defeitos df
					on df.subprojeto = cts.subprojeto and
                        df.entrega = cts.entrega and
                        df.ct = cts.ct
where
	    cts.subprojeto = '@subproject' and
	    cts.entrega = '@delivery' and
        cts.iterations in (@iterations) and
	    df.Status_Atual in ('NEW','IN_PROGRESS','PENDENT (PROGRESS)','REOPEN','MIGRATE')
order by 
    4
