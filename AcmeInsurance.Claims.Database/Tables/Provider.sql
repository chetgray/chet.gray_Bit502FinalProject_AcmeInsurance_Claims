CREATE TABLE [dbo].[Provider] (
    [Id]          INT           NOT NULL IDENTITY
  , [Code]        NVARCHAR(850) NOT NULL
  , [Name]        NVARCHAR(850) NOT NULL
  , [IsInNetwork] BIT           NOT NULL
  , [IsPreferred] BIT           NOT NULL

  , CONSTRAINT [PK_Provider_Id]   PRIMARY KEY ([Id])
  , CONSTRAINT [UQ_Provider_Code] UNIQUE      ([Code])
)
;

GO

