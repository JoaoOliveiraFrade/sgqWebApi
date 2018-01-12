select 
	subprojeto as subproject
	,entrega as delivery
	,step as id
	,teste as test
	,ordem as [order]
	,nome as name
	,descricao as description
	,resultado_esperado as expectedResult
	,(select upper(name) + ': ' + dbo.Remove_HTML(value) + ', '
	from ALM_StepParameters 
	where subproject = s.subprojeto
		and delivery = s.entrega
		and entity = 'TESTCYCL'
		and entityOwnerId = @ct
		and CHARINDEX(upper(name), descricao) > 0
	for xml path('')
	) as [parameters]
from 
	ALM_Steps s
where 
	--subprojeto = 'PRJ00000751'
	--and entrega = 'ENTREGA00000493'
	--and teste = 307
	subprojeto = '@subproject'
	and entrega = '@delivery'
	and teste = @test
order by
	ordem




--select 
--	subprojeto as subproject
--	,entrega as delivery
--	,step as id
--	,teste as test
--	,ordem as [order]
--	,nome as name
--	,descricao as description
--	,resultado_esperado as expectedResult
--	,(select top 1
--		value
--	from 
--		ALM_StepParameters 
--	where 
--		subproject = 'PRJ00000751'
--		and delivery = 'ENTREGA00000493'
--		and entity = 'TESTCYCL'
--		and entityOwnerId = 184
--	) as [parameters]
--from 
--	ALM_Steps
--where 
--	--subprojeto = 'PRJ00001558'
--	--and entrega = 'ENTREGA00000731'
--	--and teste = 36
--	subprojeto = '@subproject'
--	and entrega = '@delivery'
--	and teste = @test
--order by
--	ordem
