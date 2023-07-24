CREATE TABLE [dbo].[Claim] (
    [Id]             INT           NOT NULL IDENTITY
  , [PatientName]    NVARCHAR(850) NOT NULL
  , [ProviderId]     INT           NOT NULL
  , [Amount]         MONEY         NOT NULL
  , [HasPreApproval] BIT           NOT NULL
  , [ClaimStatusId]  INT           NOT NULL

  , CONSTRAINT [PK_Claim_Id]            PRIMARY KEY ([Id])
  , CONSTRAINT [FK_Claim_ProviderId]    FOREIGN KEY ([ProviderId])    REFERENCES [Provider]    ([Id])
  , CONSTRAINT [FK_Claim_ClaimStatusId] FOREIGN KEY ([ClaimStatusId]) REFERENCES [ClaimStatus] ([Id])
)
;

GO
