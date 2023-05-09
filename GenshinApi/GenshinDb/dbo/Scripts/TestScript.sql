--print diluc

--SELECT DISTINCT
--*
--FROM Characters C
--JOIN Element E ON C.ElemId = E.Id
--JOIN Weapons W ON C.WeaponId = W.Id
--JOIN Region R ON C.RegionId = R.RegionId
--JOIN ArtifactSet S ON C.PreferredArtifactSet = S.SetId





	SELECT 
		C.*, C.Id, W.*, R.*, S.*, E.*
	FROM
		Character AS C
		INNER JOIN Element AS E ON C.ElemId = E.Id 
        INNER JOIN Weapons AS W ON C.WeaponId = W.Id  
        INNER JOIN Region AS R ON C.RegionId = R.Id  
        INNER JOIN ArtifactSet AS S ON C.PreferredArtifactSet = S.Id 
	WHERE 
		ElemId = @elemId
	ORDER BY
		C.Id