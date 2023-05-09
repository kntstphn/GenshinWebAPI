CREATE TABLE [dbo].[Character]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),  
    [Name] VARCHAR(50) NULL, 
    [Rarity] VARCHAR(50) NULL, 
    [Gender] VARCHAR(50) NULL, 
    [WeaponId] INT NOT NULL, 
    [RegionId] INT NOT NULL, 
    [PreferredArtifactSet] INT NULL, 
    [ElemId] INT NOT NULL, 
    CONSTRAINT [FK_WeaponCharacter] FOREIGN KEY ([WeaponId]) REFERENCES [dbo].[Weapons] ([Id]), 
    CONSTRAINT [FK_RegionCharacter] FOREIGN KEY ([RegionId]) REFERENCES [dbo].[Region] ([Id]),
    CONSTRAINT [FK_CharacterElement] FOREIGN KEY ([ElemId]) REFERENCES [dbo].[Element] (Id),
)
