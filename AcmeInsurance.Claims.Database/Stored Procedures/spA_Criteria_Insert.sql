CREATE PROCEDURE [dbo].[spA_Criteria_Insert]
    @denialMinimumAmount         MONEY
  , @requiresProviderIsInNetwork BIT
  , @requiresProviderIsPreferred BIT
  , @requiresClaimHasPreApproval BIT
AS

INSERT INTO [Criteria]
       ([DenialMinimumAmount], [RequiresProviderIsInNetwork], [RequiresProviderIsPreferred], [RequiresClaimHasPreApproval])
VALUES (@denialMinimumAmount,  @requiresProviderIsInNetwork,  @requiresProviderIsPreferred , @requiresClaimHasPreApproval)
;

DECLARE @id INT = SCOPE_IDENTITY();

-- Suppress result set if called from a stored procedure that does not want it.
IF OBJECT_ID('tempdb..#__SuppressResults_Criteria_Insert') IS NULL
BEGIN
    SELECT
        [Cr].[Id]                          AS [CriteriaId]
      , [Cr].[DenialMinimumAmount]         AS [CriteriaDenialMinimumAmount]
      , [Cr].[RequiresProviderIsInNetwork] AS [CriteriaRequiresProviderIsInNetwork]
      , [Cr].[RequiresProviderIsPreferred] AS [CriteriaRequiresProviderIsPreferred]
      , [Cr].[RequiresClaimHasPreApproval] AS [CriteriaRequiresClaimHasPreApproval]
    FROM
        [Criteria] AS [Cr]
    WHERE
        [Cr].[Id] = @id
    ;
END

RETURN 0
