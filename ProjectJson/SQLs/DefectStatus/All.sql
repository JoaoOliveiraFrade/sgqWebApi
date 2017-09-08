select 
	name as id,
	name + ' (' + substring(activity,1,6) + ')' as name 
from 
	SGQ_StatusDefects
