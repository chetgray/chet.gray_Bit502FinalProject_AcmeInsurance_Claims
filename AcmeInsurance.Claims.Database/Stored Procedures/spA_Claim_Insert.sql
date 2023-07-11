CREATE PROCEDURE [dbo].[spA_Claim_Insert]
    @PatientName    NVARCHAR(850)
  , @ProviderId     INT
  , @Amount         MONEY
  , @HasPreApproval BIT
  , @ClaimStatusId  INT
AS

INSERT INTO [Claim]
       ([PatientName], [ProviderId], [Amount], [HasPreApproval], [ClaimStatusId])
VALUES (@PatientName,  @ProviderId,  @Amount,  @HasPreApproval,  @ClaimStatusId)
;

DECLARE @id INT = SCOPE_IDENTITY();

-- Suppress result set if called from a stored procedure that does not want it.
IF OBJECT_ID('tempdb..#__SuppressResults_Claim_Insert') IS NULL
BEGIN
    EXEC [spA_Claim_SelectById] @id = @id;
END

RETURN 0
