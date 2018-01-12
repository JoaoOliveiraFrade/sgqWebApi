select 
	subprojeto as subproject
	,entrega as delivery
	,step as id
	,teste as test
	,ordem as [order]
	,step
	,nome as name
	,descricao as description
	,resultado_esperado as expectedResult
from 
	ALM_Steps
where 
	--subprojeto = 'PRJ00001558'
	--and entrega = 'ENTREGA00000731'
	--and teste = 36
	subprojeto = '@subproject'
	and entrega = '@delivery'
	and teste = '@teste'
order by
	ordem
