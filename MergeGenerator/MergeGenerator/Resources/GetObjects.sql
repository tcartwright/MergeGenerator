DECLARE @sql NVARCHAR(MAX);
		
SELECT @sql = (
    SELECT CHAR(10) + ' UNION ALL 
	SELECT database_name = ''' + d.name + ''',
		schema_name = s.name COLLATE DATABASE_DEFAULT,
        table_name = t.name COLLATE DATABASE_DEFAULT,
		identity_column = (
			SELECT c.name COLLATE DATABASE_DEFAULT 
			FROM ' + QUOTENAME(d.name) + '.sys.columns c 
			WHERE c.object_id = t.object_id AND c.is_identity = 1
		) 
    FROM ' + QUOTENAME(d.name) + '.sys.tables t
    JOIN ' + QUOTENAME(d.name) + '.sys.schemas s
        on s.schema_id = t.schema_id'
    FROM sys.databases d
    WHERE d.state = 0
        AND d.database_id > 4
        AND HAS_DBACCESS(d.name) = 1
    ORDER BY [name]
    FOR XML PATH(''), TYPE
).value('.', 'nvarchar(max)');

SET @sql = STUFF(@sql, 1, 13, '') + N' order by database_name, schema_name, table_name'

--PRINT @sql;
EXECUTE (@sql);



