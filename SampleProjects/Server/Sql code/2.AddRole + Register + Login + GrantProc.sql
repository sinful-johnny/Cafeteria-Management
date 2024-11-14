USE cafeteriaDB
GO

CREATE OR ALTER PROC Take_GetUserByEmail
@userEmail varchar(50)
AS
BEGIN
    SELECT ID_ADMIN, EMAIL
    FROM ADMIN
    WHERE EMAIL = @userEmail
END
GO

-- EXEC Take_GetUserByEmail 'admin1@example.com'
-- GO

-- SELECT * FROM sys.server_principals WHERE type = 'R';

-- ALTER SERVER ROLE ADMIN DROP MEMBER A052;
-- go

-- select *
-- from admin a
-- WHERE a.EMAIL = 'user456789@example.com'
-- go

-- select *
-- from ADMIN
-- where ID_ADMIN = 'A052'

CREATE OR ALTER PROC Take_UserRoles
@userEmail varchar(50)
AS
BEGIN
    SELECT
    dp2.name AS RoleName
    FROM ADMIN ad,
        sys.database_role_members drm
    JOIN 
        sys.database_principals dp ON drm.member_principal_id = dp.principal_id
    JOIN 
        sys.database_principals dp2 ON drm.role_principal_id = dp2.principal_id
    WHERE dp.name = ad.ID_ADMIN
        and ad.EMAIL = @userEmail
END
GO

--EXEC Take_UserRoles 'admin1@example.com'

-- EXEC sp_addrole 'USER'
-- GO

EXEC sp_addrole 'ADMIN'
GO

CREATE OR ALTER PROCEDURE ADD_ROLE
AS
BEGIN
    DECLARE @ID_TK VARCHAR(5);  -- Adjust data type to match your ID_ADMIN or user ID column

    -- Declare a cursor for selecting all user IDs from the ADMIN table
    DECLARE admin_cursor CURSOR FOR
    SELECT ID_ADMIN
    FROM ADMIN;

    -- Open the cursor
    OPEN admin_cursor;

    -- Fetch the first row from the cursor
    FETCH NEXT FROM admin_cursor INTO @ID_TK;

    -- Loop through all rows in the cursor
    WHILE @@FETCH_STATUS = 0
    BEGIN
        -- Check if the SQL login already exists
        IF NOT EXISTS (SELECT 1 FROM sys.server_principals WHERE name = @ID_TK)
        BEGIN
            -- Create a SQL Server login for this ID
            EXEC sp_addlogin @ID_TK, 'YourDefaultPassword';  -- Replace with a secure default password or generate one
        END

        -- Check if the database user exists, then add to role
        IF NOT EXISTS (SELECT 1 FROM sys.database_principals WHERE name = @ID_TK)
        BEGIN
            EXEC sp_adduser @ID_TK;  -- Create database user
        END

        -- Add the user to the Admin role
        EXEC sp_addrolemember 'ADMIN', @ID_TK;

        FETCH NEXT FROM admin_cursor INTO @ID_TK;
    END

    -- Close and deallocate the cursor
    CLOSE admin_cursor;
    DEALLOCATE admin_cursor;
END
GO

EXEC ADD_ROLE
GO

--drop user

-- DECLARE @sql NVARCHAR(MAX) = N'';

-- -- Generate DROP USER statements for each user
-- SELECT @sql += 'DROP USER [' + name + '];' + CHAR(13)
-- FROM sys.database_principals
-- WHERE type_desc = 'SQL_USER' 
--       AND principal_id > 4  -- Excludes system users like dbo, guest, etc.

-- -- Execute the generated SQL
-- EXEC sp_executesql @sql;
-- go

--ADMIN REGISTER

-- Verify that the stored procedure does not already exist.
IF OBJECT_ID('usp_GetErrorInfo', 'P') IS NOT NULL
    DROP PROCEDURE usp_GetErrorInfo;
GO

-- Create procedure to retrieve error information.
CREATE OR ALTER PROC usp_GetErrorInfo
@ResponseMess varchar(255) output
AS
SELECT ERROR_NUMBER() AS ErrorNumber,
    ERROR_SEVERITY() AS ErrorSeverity,
    ERROR_STATE() AS ErrorState,
    ERROR_PROCEDURE() AS ErrorProcedure,
    ERROR_LINE() AS ErrorLine,
    ERROR_MESSAGE() AS ErrorMessage;

    SET @ResponseMess = ERROR_MESSAGE();
GO


CREATE OR ALTER PROC sp_ADMIN_REGISTER
	@Email VARCHAR(255),  
    @PASSWORD VARCHAR(255),
    @ResponseMessage NVARCHAR(255) OUTPUT
AS
BEGIN
    -- Check if the email is already registered
    IF EXISTS (SELECT 1 FROM ADMIN WHERE EMAIL = @Email)
    BEGIN
        SET @ResponseMessage = 'Email is already registered.';
        RETURN;
    END

    -- Basic email format check (for illustration, not full validation)
    IF @Email LIKE '%_@__%.__%'  
            AND @Email NOT LIKE '%..%'         -- Prevents consecutive dots
            AND @Email NOT LIKE '%@%@%'        -- Ensures only one '@' symbol
            AND LEN(@Email) <= 254
    BEGIN
        DECLARE @LastID NVARCHAR(10);
        DECLARE @NumericPart INT;
        DECLARE @NewID NVARCHAR(10);

        
        DECLARE @Salt VARBINARY(16);           -- To store the salt
        DECLARE @PasswordHash VARBINARY(64);   -- To store the hashed password

        -- Generate a random salt (16 bytes)
        SET @Salt = CONVERT(VARBINARY(16), NEWID());

        -- Get the highest existing ID in the format A###
        SELECT @LastID = MAX(ID_ADMIN) FROM ADMIN WHERE ID_ADMIN LIKE 'A%';

        -- Extract the numeric part and increment it
        IF @LastID IS NOT NULL
        BEGIN
            SET @NumericPart = CAST(SUBSTRING(@LastID, 2, 3) AS INT) + 1;
        END
        ELSE
        BEGIN
            -- If no IDs exist yet, start with 1
            SET @NumericPart = 1;
        END

        -- Format the new ID as 'A' followed by the incremented number, zero-padded to 3 digits
        SET @NewID = 'A' + RIGHT('000' + CAST(@NumericPart AS NVARCHAR(3)), 3);

        -- Hash the password with the salt using SHA2_256
        SET @PasswordHash = HASHBYTES('SHA2_256', @Salt + CONVERT(VARBINARY(255), @Password));

        -- Insert the new admin record with the generated ID and hashed password
        BEGIN TRY

            EXEC sp_addlogin @Email, @PASSWORD;
            declare @cmd varchar(200);
            set @cmd = ' 

		                USE cafeteria_DB
		                CREATE USER [' + @NewID + '] FOR LOGIN '+ '[' + @Email + ']';
            EXEC (@cmd);

            EXEC sp_addrolemember 'ADMIN', @NewID;
            INSERT INTO ADMIN (ID_ADMIN, EMAIL, PASSWORDHASH, SALT, CREATED_AT, UPDATE_AT)
            VALUES (@NewID, @Email, @PasswordHash, @Salt, GETDATE(), GETDATE());

            SET @ResponseMessage = 'User registered successfully';
            return;
        END TRY
        BEGIN CATCH

            declare @ResponseMess nvarchar(255);

            EXECUTE usp_GetErrorInfo @ResponseMess = @ResponseMess output;

            SET @ResponseMessage = @ResponseMess;

        END CATCH
    END
    ELSE
    BEGIN
        SET @ResponseMessage = 'Invalid email format.';
        RETURN;
    END
END
GO

declare @ResponseMessage NVARCHAR(255);

EXEC sp_ADMIN_REGISTER '1143243@gmail.com', '132424', @ResponseMessage = @ResponseMessage output;

select @ResponseMessage;
GO



-- declare @ResponseMessage NVARCHAR(255);

-- EXEC sp_ADMIN_LOGIN 'aHAHAHAHA@gmail.com', '123456', @ResponseMessage = @ResponseMessage output;
-- select @ResponseMessage;
GO
--ADMIN LOGIN
CREATE OR ALTER PROCEDURE sp_ADMIN_LOGIN
    @Email VARCHAR(255),
    @Password VARCHAR(255),
    @ResponseMessage NVARCHAR(255) OUTPUT
AS
BEGIN
    DECLARE @StoredSalt VARBINARY(16);
    DECLARE @StoredPasswordHash VARBINARY(64);
    DECLARE @ComputedHash VARBINARY(64);

    -- Step 1: Retrieve the stored salt and password hash for the given email
    SELECT @StoredSalt = Salt, @StoredPasswordHash = PasswordHash
    FROM ADMIN
    WHERE Email = @Email;

    -- Check if the email exists
    IF @StoredSalt IS NULL OR @StoredPasswordHash IS NULL
    BEGIN
        SET @ResponseMessage = 'Invalid email or password.';
        RETURN;
    END

    -- Step 2: Hash the entered password with the retrieved salt
    SET @ComputedHash = HASHBYTES('SHA2_256', @StoredSalt + CONVERT(VARBINARY(255), @Password));

    -- Step 3: Compare the computed hash with the stored password hash
    IF @ComputedHash = @StoredPasswordHash
    BEGIN
        SET @ResponseMessage = 'Login successful.';
    END
    ELSE
    BEGIN
        SET @ResponseMessage = 'Invalid email or password.';
    END
END
GO

GRANT EXECUTE ON OBJECT::sp_ADMIN_REGISTER
    TO ADMIN; 
GO

GRANT EXECUTE ON OBJECT::sp_ADMIN_LOGIN
    TO ADMIN; 
GO