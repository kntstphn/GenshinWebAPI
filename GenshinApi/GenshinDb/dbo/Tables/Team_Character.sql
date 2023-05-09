CREATE TABLE [dbo].[Team_Character]
(
	[TeamId] INT NOT NULL, 
    [CharacterId] INT NOT NULL,
	CONSTRAINT [PK_Ids] PRIMARY KEY CLUSTERED ([TeamId], [CharacterId]),
	CONSTRAINT [FK_TeamId] FOREIGN KEY ([TeamId]) REFERENCES [dbo].[TeamComposition] ([Id]) ON DELETE CASCADE, 
	CONSTRAINT [FK_CharacterId] FOREIGN KEY ([CharacterId]) REFERENCES [dbo].[Character] ([Id]) ON DELETE CASCADE
)
