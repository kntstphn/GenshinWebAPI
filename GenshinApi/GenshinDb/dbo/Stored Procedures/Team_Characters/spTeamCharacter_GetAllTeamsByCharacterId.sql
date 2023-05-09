CREATE PROCEDURE [dbo].[spTeamCharacter_GetAllTeamsByCharacterId]
	@characterId INT
AS
	BEGIN
		SELECT
			* 
		FROM 
			Team_Character tc 
		INNER JOIN 
			TeamComposition t on tc.TeamId = t.Id 
		INNER JOIN 
			Character c on c.Id = tc.CharacterId 
		WHERE 
			tc.CharacterId = @characterId
	END
