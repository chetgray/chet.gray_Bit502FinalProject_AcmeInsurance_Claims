CREATE PROCEDURE [dbo].[spA_Criteria_DeleteById]
    @id INT
AS

DELETE FROM [Criteria]
WHERE
    [Id] = @id
;

RETURN 0
