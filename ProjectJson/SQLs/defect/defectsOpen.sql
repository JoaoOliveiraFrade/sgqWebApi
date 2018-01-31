select 
	subprojeto as subproject
	,entrega as delivery
	,convert(varchar, cast(substring(Subprojeto,4,8) as int)) + ' ' + convert(varchar,cast(substring(Entrega,8,8) as int)) as subDel

	,defeito as id
	,nome as name
	
	,status_atual as status

	,(case 
		when status_atual in ('NEW','IN_PROGRESS','PENDENT (PROGRESS)','REOPEN','MIGRATE') then 'DESENV'
		when status_atual in ('ON_RETEST','PENDENT (RETEST)','REJECTED') then 'TESTE'
		else 'N/A'
	end) as inFactoryType

	,encaminhado_para as queue
	,sistema_defeito as defectSystem
	,substring(severidade,3,3) as severity

    ,aging as aging
	,substring('-  ',2+convert(int,sign(aging)),1) + right(convert(varchar, floor(abs(aging))), 3) + ':' + right('00' + convert(varchar,round( 60*(abs(aging)-floor(abs(aging))), 0)), 2) as agingFormat
    ,ping_pong as pingPong
from 
	alm_defeitos d WITH (NOLOCK)
where
	d.subprojeto = '@subproject'
	and d.entrega = '@delivery'
	and status_atual not in ('CLOSED', 'CANCELLED')
order by 
	defeito
