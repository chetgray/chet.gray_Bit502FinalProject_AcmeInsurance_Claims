CREATE PROCEDURE [dbo].[spA_Claim_UpdateClaimStatus]
    @id             INT
  , @claimStatusId  INT

AS

UPDATE [Claim]
SET
    [ClaimStatusId] = @claimStatusId
WHERE
    [Id] = @id

-- Suppress result set if called from a stored procedure that does not want it.
IF OBJECT_ID('tempdb..#__SuppressResults_Claim_UpdateClaimStatus') IS NULL
BEGIN
    EXEC [spA_Claim_SelectById] @id = @id;
END

RETURN 0
