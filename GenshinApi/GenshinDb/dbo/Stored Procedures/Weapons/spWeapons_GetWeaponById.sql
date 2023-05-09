CREATE PROCEDURE [dbo].[spWeapons_GetWeaponById]
	@id int
AS
	SELECT * FROM Weapons a INNER JOIN WeaponType b on a.WeaponType_Id = b.Id WHERE a.Id = @id
RETURN 0
