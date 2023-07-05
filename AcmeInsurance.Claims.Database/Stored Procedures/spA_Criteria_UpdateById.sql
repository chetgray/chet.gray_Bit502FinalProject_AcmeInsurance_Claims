CREATE PROCEDURE [dbo].[spA_Criteria_UpdateById]
    @id                          INT
  , @denialMinimumAmount         MONEY
  , @requiresProviderIsInNetwork BIT
  , @requiresProviderIsPreferred BIT
  , @requiresClaimHasPreApproval BIT
AS

UPDATE [Criteria]
SET
    [DenialMinimumAmount]         = @denialMinimumAmount
  , [RequiresProviderIsInNetwork] = @requiresProviderIsInNetwork
  , [RequiresProviderIsPreferred] = @requiresProviderIsPreferred
  , [RequiresClaimHasPreApproval] = @requiresClaimHasPreApproval
WHERE
    [Id] = @id
;

-- Suppress result set if called from a stored procedure that does not want it.
IF OBJECT_ID('tempdb..#__SuppressResults_Criteria_UpdateById') IS NULL
BEGIN
    EXEC [spA_Criteria_SelectById] @id = @id;
END

RETURN 0
