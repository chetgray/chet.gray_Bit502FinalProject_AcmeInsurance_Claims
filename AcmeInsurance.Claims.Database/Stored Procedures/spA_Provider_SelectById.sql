CREATE PROCEDURE [dbo].[spA_Provider_SelectById]
    @id INT
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
    [Prv].[Id] = @id

RETURN 0
