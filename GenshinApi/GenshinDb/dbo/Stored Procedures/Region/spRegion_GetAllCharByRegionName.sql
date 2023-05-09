CREATE PROC spRegion_GetAllCharByRegionName

    @regionName varchar(50)
AS
DECLARE
	@regionId INT
BEGIN 
    
    SET @regionId = (
        SELECT Id FROM Region WHERE Name = @regionName
    );



   SELECT
        C.*, W.*, R.*, S.*, E.*
    FROM
        Character AS C
        INNER JOIN Weapons AS W ON C.WeaponId = W.Id  
        INNER JOIN Region AS R ON C.RegionId = R.Id  
        INNER JOIN ArtifactSet AS S ON C.PreferredArtifactSet = S.Id
        INNER JOIN Element AS E ON C.ElemId = E.Id
	WHERE 
		RegionId = @regionId;
END
