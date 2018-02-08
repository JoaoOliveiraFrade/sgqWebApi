update SGQ_Groupers
set
    name = @name
    ,type = @type
    ,executiveSummary = @executiveSummary
	,startTiUat = @startTiUat
	,endTiUat = @endTiUat
	,startTRG = @startTRG
	,endTRG = @endTRG
	,startStabilization = @startStabilization
	,endStabilization = @endStabilization
where
    id = @id
