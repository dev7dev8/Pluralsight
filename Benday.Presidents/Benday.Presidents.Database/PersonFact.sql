﻿CREATE TABLE [dbo].[PersonFact]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY(1,1),
	[PersonId] INT NOT NULL, 
	[FactType] NVARCHAR(100) NOT NULL,
	[FactValue] NVARCHAR(100) NOT NULL,
	[StartDate] DATETIME2 NOT NULL,
	[EndDate] DATETIME2 NOT NULL, 
    CONSTRAINT [FK_PersonFact_Person] FOREIGN KEY ([PersonId]) REFERENCES [Person]([Id])
)
