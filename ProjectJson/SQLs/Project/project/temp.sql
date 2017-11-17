select 
	subproject,
	delivery,
	name,
	estado,
	qtyCTs
	qtyPlan,
	qtyRealized,
	qtyRealized - qtyPlan as GAP,
	qtyDefect,
	qtyDefectSemOrigem,
	qtyDefectAmbiente,
	qtyDefectAutomacao,
	qtyDefectConstrucao,
	qtyDefectDesenhoSolucao,
	qtyDefectImprocedente,
	qtyDefectProducao
from
	(
		select
			sp.subproject,
			sp.delivery,
			bsp.nome as name,
			bsp.estado,

			(select count(*)
			from ALM_Defeitos WITH (NOLOCK)
			where 
				subprojeto = sp.subproject and
				entrega = sp.delivery
			) as qtyDefect,

			(select count(*)
			from ALM_Defeitos WITH (NOLOCK)
			where 
				subprojeto = sp.subproject and
				entrega = sp.delivery and
				origem = ''
			) as qtyDefectSemOrigem,

			(select count(*)
			from ALM_Defeitos WITH (NOLOCK)
			where 
				subprojeto = sp.subproject and
				entrega = sp.delivery and
				origem = 'AMBIENTE'
			) as qtyDefectAmbiente,

			(select count(*)
			from ALM_Defeitos WITH (NOLOCK)
			where 
				subprojeto = sp.subproject and
				entrega = sp.delivery and
				origem = 'AUTOMA플O'
			) as qtyDefectAutomacao,

			(select count(*)
			from ALM_Defeitos WITH (NOLOCK)
			where 
				subprojeto = sp.subproject and
				entrega = sp.delivery and
				origem = 'CONSTRU플O'
			) as qtyDefectConstrucao,

			(select count(*)
			from ALM_Defeitos WITH (NOLOCK)
			where 
				subprojeto = sp.subproject and
				entrega = sp.delivery and
				origem = 'DESENHO DA SOLU플O'
			) as qtyDefectDesenhoSolucao,

			(select count(*)
			from ALM_Defeitos WITH (NOLOCK)
			where 
				subprojeto = sp.subproject and
				entrega = sp.delivery and
				origem = 'IMPROCEDENTE'
			) as qtyDefectImprocedente,

			(select count(*)
			from ALM_Defeitos WITH (NOLOCK)
			where 
				subprojeto = sp.subproject and
				entrega = sp.delivery and
				origem = 'PRODU플O'
			) as qtyDefectProducao,

			(select count(*)
			from ALM_CTs WITH (NOLOCK)
			where 
				subprojeto = sp.subproject and
				entrega = sp.delivery and
				Status_Exec_CT <> 'CANCELLED'
			) as qtyCTs,

			(select count(*)
			from ALM_CTs WITH (NOLOCK)
			where 
				subprojeto = sp.subproject and
				entrega = sp.delivery and
				Status_Exec_CT <> 'CANCELLED' and
				substring(dt_planejamento,7,2) + substring(dt_planejamento,4,2) + substring(dt_planejamento,1,2) <= convert(varchar(6), getdate(), 12) and
				dt_planejamento <> ''
			) as qtyPlan,

			(select count(*)
			from ALM_CTs WITH (NOLOCK)
			where 
				subprojeto = sp.subproject and
				entrega = sp.delivery and
				Status_Exec_CT = 'PASSED'
			) as qtyRealized

		from 
			sgq_projects sp
			inner join biti_subprojetos bsp
				on bsp.id = sp.subproject
			inner join BITI_entregas ent
				on ent.id = sp.delivery
		where
			clarityReleaseYear = 2017
			and clarityReleaseMonth = 11
			and bsp.estado <> 'CANCELADO'
			and ent.estado <> 'ENTREGA CANCELADA'
	) aux0
order by
	1, 2
