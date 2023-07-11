CREATE PROCEDURE [dbo].[spA_Claim_SelectById]
    @id INT
AS

SELECT
    [Clm].[Id]             AS [Claim_Id]
  , [Clm].[PatientName]    AS [Claim_PatientName]
  , [Clm].[ProviderId]     AS [Claim_ProviderId]
  , [Clm].[Amount]         AS [Claim_Amount]
  , [Clm].[HasPreApproval] AS [Claim_HasPreApproval]
  , [Clm].[ClaimStatusId]  AS [Claim_ClaimStatusId]
FROM
    [Claim] AS [Clm]
WHERE
    [Clm].[Id] = @id
;

RETURN 0
