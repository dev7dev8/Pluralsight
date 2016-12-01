CREATE TABLE [dbo].[Feature]
(
    [Id] INT NOT NULL PRIMARY KEY IDENTITY(1, 1), 
    [Name] NVARCHAR(50) NOT NULL, 
    [IsEnabled] BIT NOT NULL, 
    [Username] NVARCHAR(50) NOT NULL
)
