select distinct
	uXp.profile as id,
	p.name
from 
	sgq_usersXprofiles uXp
	inner join sgq_profiles p
		on p.id = uXp.profile
where 
	uXp.[user] = @user
order by
	1