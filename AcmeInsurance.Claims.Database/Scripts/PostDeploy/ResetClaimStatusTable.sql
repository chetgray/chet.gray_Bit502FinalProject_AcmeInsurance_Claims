SET IDENTITY_INSERT [ClaimStatus] ON;

-- 1, pending
-- 2, approved
-- 3, denied

MERGE INTO [ClaimStatus] AS [tgt]
USING (
              SELECT 1,  'pending'
    UNION ALL SELECT 2, 'approved'
    UNION ALL SELECT 3,   'denied'
) AS [src]       ([Id],    [Value])
ON
    [tgt].[Id] = [src].[Id]
WHEN MATCHED THEN
    UPDATE SET
    [tgt].[Value] = [src].[Value]
WHEN NOT MATCHED BY TARGET THEN
    INSERT (      [Id],       [Value])
    VALUES ([src].[Id], [src].[Value])
WHEN NOT MATCHED BY SOURCE THEN
    DELETE
;

SET IDENTITY_INSERT [ClaimStatus] OFF;

GO
