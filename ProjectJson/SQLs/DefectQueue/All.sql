select distinct 
	Encaminhado_Para as id, 
	(case when Encaminhado_Para = 'SISRAF - CADASTRO DE FATURAMENTO - ACCENTURE' then 'SISRAF - CAD.FAT - ACCENTURE'
		  when Encaminhado_Para = 'PAINEL DE PORTABILIDADE NUMÉRICA - CLEARTECH' then 'PAINEL PORT.NUM - CLEARTECH'
		  when Encaminhado_Para = '(OI R2) CLARIFY - AMBIENTE OPEN - ACCENTURE' then '(OI R2) CLARIFY-AMB.OP.-ACCENTURE'
		  when Encaminhado_Para = 'SOM - SERVICE ORDER MANAGEMENT - IBM' then 'SOM - SERV.ORDER MAN. - IBM'
		  when Encaminhado_Para = 'INFORMÁTICA - INTEGRAÇÃO - ACCENTURE' then 'INFORMÁTICA-INT.-ACCENTURE'
		  when Encaminhado_Para = 'INFORMÁTICA - INTEGRAÇÃO - IBM' then 'INFORMÁTICA-INTEG.- IBM'
		  when Encaminhado_Para = 'INFORMÁTICA - INTEGRAÇÃO - OI' then 'INFORMÁTICA-INTEG.- OI'
		  when Encaminhado_Para = 'GATEWAY DE PORTABILIDADE - CLEARTECH' then 'GATEWAY DE PORT.- CLEARTECH'
		  when Encaminhado_Para = 'SISTEMAS DA ENGENHARIA - ENGENHARIA' then 'SIST.ENG.-ENGENHARIA'
		  when Encaminhado_Para = 'SIEBEL INFORMACIONAL - ACCENTURE' then 'SIEBEL INFORM.- ACCENTURE'
		  when Encaminhado_Para = 'NOTA FISCAL ELETRONICA - NEOGRID' then 'NOTA FIS.ELET. - NEOGRID'
		  when Encaminhado_Para = 'PRINTCENTER - ÁREA DE NEGÓCIOS' then 'PRINTCENTER - ÁREA DE NEG.'
		  when Encaminhado_Para = 'RMS - TARIFAÇÃO DE REDE MULTISERVIÇO - IBM' then 'RMS - TARIF.REDE MULT.- IBM'
		else Encaminhado_Para
	end) as name

from 
	alm_defeitos 
where 
	status_atual not in('CLOSED', 'CANCELLED') 
order by 1