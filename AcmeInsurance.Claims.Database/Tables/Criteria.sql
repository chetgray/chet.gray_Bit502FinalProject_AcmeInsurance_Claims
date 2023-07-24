CREATE TABLE [dbo].[Criteria] (
    [Id]                          INT   NOT NULL IDENTITY
  , [DenialMinimumAmount]         MONEY NOT NULL
  , [RequiresProviderIsInNetwork] BIT   NOT NULL
  , [RequiresProviderIsPreferred] BIT   NOT NULL
  , [RequiresClaimHasPreApproval] BIT   NOT NULL

  , CONSTRAINT [PK_Criteria_Id] PRIMARY KEY ([Id])
)
;

GO

