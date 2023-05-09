CREATE PROCEDURE [dbo].[spWeapons_GetAllWeapons]
AS
BEGIN
	SELECT * FROM Weapons a INNER JOIN WeaponType b on a.WeaponType_Id = b.Id;
END