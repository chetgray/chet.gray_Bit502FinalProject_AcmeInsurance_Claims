SET IDENTITY_INSERT [Provider] ON;

-- 1, ANT, Anthem, true, true
-- 2, HUM, Humana, true, false
-- 3, UHC, United Healthcare, false, false
-- 4, AC1, ACA Insurance 1, true, false

INSERT INTO [Provider]
                          ([Id],       [Code],              [Name],       [IsInNetwork],       [IsPreferred])
              SELECT [src].[Id], [src].[Code],        [src].[Name], [src].[IsInNetwork], [src].[IsPreferred]
FROM (        SELECT          1,        'ANT',            'Anthem',                   1,                   1
    UNION ALL SELECT          2,        'HUM',            'Humana',                   1,                   0
    UNION ALL SELECT          3,        'UHC', 'United Healthcare',                   0,                   0
    UNION ALL SELECT          4,        'AC1',   'ACA Insurance 1',                   1,                   0
) AS [src]                ([Id],       [Code],              [Name],       [IsInNetwork],       [IsPreferred])
WHERE NOT EXISTS (
    SELECT 1
    FROM [Provider]
)
;

SET IDENTITY_INSERT [Provider] OFF;

GO
