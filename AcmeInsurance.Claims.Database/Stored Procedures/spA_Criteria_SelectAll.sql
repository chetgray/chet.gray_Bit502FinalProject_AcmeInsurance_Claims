CREATE PROCEDURE [dbo].[spA_Criteria_SelectAll]
AS

SELECT
    [Crt].[Id]                          AS [Criteria_Id]
  , [Crt].[DenialMinimumAmount]         AS [Criteria_DenialMinimumAmount]
  , [Crt].[RequiresProviderIsInNetwork] AS [Criteria_RequiresProviderIsInNetwork]
  , [Crt].[RequiresProviderIsPreferred] AS [Criteria_RequiresProviderIsPreferred]
  , [Crt].[RequiresClaimHasPreApproval] AS [Criteria_RequiresClaimHasPreApproval]
FROM
    [Criteria] AS [Crt]
;

RETURN 0
