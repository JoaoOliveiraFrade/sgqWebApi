select 
	id,
	login,
	name,
	email,
	cpf,
	'' as password
from 
	SGQ_Users
where 
	login = '@login' and 
	cpf = '@cpf'
	--login = 'TR412061' and 
	--cpf = '85812838534'