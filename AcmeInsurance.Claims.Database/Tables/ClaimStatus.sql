CREATE TABLE [dbo].[ClaimStatus] (
    [Id]    INT           NOT NULL IDENTITY
  , [Value] NVARCHAR(850) NOT NULL

  , CONSTRAINT [PK_ClaimStatus_Id]    PRIMARY KEY ([Id])
  , CONSTRAINT [UQ_ClaimStatus_Value] UNIQUE      ([Value])
)
;

GO

