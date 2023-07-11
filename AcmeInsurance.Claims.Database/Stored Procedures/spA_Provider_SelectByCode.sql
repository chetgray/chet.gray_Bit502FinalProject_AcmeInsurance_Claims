CREATE PROCEDURE [dbo].[spA_Provider_SelectByCode]
    @code NVARCHAR(850)
AS

SELECT
    [Prv].[Id]          AS [Provider_Id]
  , [Prv].[Code]        AS [Provider_Code]
  , [Prv].[Name]        AS [Provider_Name]
  , [Prv].[IsInNetwork] AS [Provider_IsInNetwork]
  , [Prv].[IsPreferred] AS [Provider_IsPreferred]
FROM
    [Provider] AS [Prv]
WHERE
    [Prv].[Code] = @code

RETURN 0
