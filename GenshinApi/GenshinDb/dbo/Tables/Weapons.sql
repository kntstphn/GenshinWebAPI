CREATE TABLE [dbo].[Weapons]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(50) NULL, 
    [Damage] INT NULL, 
    [WeaponType_Id] INT NULL, 
    [Rarity] INT NULL
)
