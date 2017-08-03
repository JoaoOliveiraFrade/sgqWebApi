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
	password = '@password'
	--login = 'TR412061' and 
	--password = ''