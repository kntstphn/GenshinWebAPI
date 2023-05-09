CREATE PROC spCharacter_GetAllByElementName

    @elementName varchar(50)
AS
BEGIN 
    DECLARE
	@elemId INT

    SET @elemId = (
        SELECT Id FROM Element WHERE Name = @elementName
    );

   SELECT
        C.*, W.*, R.*, S.*,E.*
    FROM
        Character AS C
        INNER JOIN Weapons AS W ON C.WeaponId = W.Id  
        INNER JOIN Region AS R ON C.RegionId = R.Id  
        INNER JOIN ArtifactSet AS S ON C.PreferredArtifactSet = S.Id
        INNER JOIN Element AS E ON C.ElemId = E.Id
	WHERE 
		ElemId = @elemId;
END