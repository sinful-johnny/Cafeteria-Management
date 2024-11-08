USE cafeteria_DB
GO

-- Check if Canva_Admin exist
CREATE or alter PROCEDURE CheckCanva_AdminExists
    @ID_CANVA VARCHAR(5),
    @ID_ADMIN VARCHAR(5)
AS
BEGIN
    -- Check if the specified ID_CANVA exists in the CANVA table
    IF EXISTS ( SELECT 1 
                FROM CANVA_ADMIN 
                WHERE ID_CANVA = @ID_CANVA
                    and ID_ADMIN = @ID_ADMIN)
    BEGIN
        RETURN 1;  -- Return 1 to indicate the Canva exists
    END
    ELSE
    BEGIN
        RETURN 0;  -- Return 0 to indicate the Canva does not exist
    END
END
GO

CREATE OR ALTER PROC sp_CANVA_LOGINSTATUS
	@ID_CANVA VARCHAR(5),
    @ResponseMessage NVARCHAR(255) OUTPUT
AS
BEGIN
    IF NOT EXISTS ( SELECT 1 
                    FROM CANVA_ADMIN 
                    WHERE ID_CANVA = @ID_CANVA
                        AND ID_ADMIN = CURRENT_USER
                        OR CURRENT_USER = 'dbo')
    BEGIN

        INSERT INTO CANVA_ADMIN (ID_CANVA, ID_ADMIN, LOGIN_STATUS, CREATED_AT, UPDATE_AT) VALUES 
                    (@ID_CANVA, CURRENT_USER, 'Login', GETDATE(), GETDATE());
    END
    ELSE
    BEGIN
        UPDATE CANVA_ADMIN
        SET LOGIN_STATUS = 'Login',
            UPDATE_AT = GETDATE()
        WHERE ID_CANVA = @ID_CANVA
            AND ID_ADMIN = CURRENT_USER
    END
    SET @ResponseMessage = 'User with ID ' + CURRENT_USER + 'has logged into Canvas with ID ' + @ID_CANVA;
END
GO

CREATE OR ALTER PROC sp_CANVA_LOGOUTSTATUS
	@ID_CANVA VARCHAR(5)
AS
BEGIN
    IF EXISTS ( SELECT 1 
                    FROM CANVA_ADMIN 
                    WHERE ID_CANVA = @ID_CANVA
                        AND ID_ADMIN = CURRENT_USER)
    BEGIN
        UPDATE CANVA_ADMIN
        SET LOGIN_STATUS = 'Logout',
            UPDATE_AT = GETDATE()
        WHERE ID_CANVA = @ID_CANVA
            AND ID_ADMIN = CURRENT_USER
    END
END
GO

GRANT EXECUTE ON OBJECT::sp_CANVA_LOGINSTATUS
    TO ADMIN; 
GO

GRANT EXECUTE ON OBJECT::sp_CANVA_LOGOUTSTATUS
    TO ADMIN; 
GO