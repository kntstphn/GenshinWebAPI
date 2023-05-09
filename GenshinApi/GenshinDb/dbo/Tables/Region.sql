CREATE TABLE [dbo].[Region]
(
	[Id] INT NOT NULL IDENTITY(1,1), 
    [Name] VARCHAR(50) NULL, 
    [RegionInspiredFrom] VARCHAR(50) NULL, 
    [RegionDescription] TEXT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
)
