using cafeteriaDB;
GO;

-- Check if Canva_Admin exist
CREATE PROCEDURE CheckCanva_AdminExists
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
END;
GO;

-- Insert Canva

CREATE OR ALTER PROCEDURE InsertCanva_Admin
    @ID_CANVA VARCHAR(5),
    @ID_ADMIN VARCHAR(5)
AS
BEGIN
    DECLARE @loginStatus NVARCHAR(50);
    SET @loginStatus = 'Login';
    INSERT INTO CANVA_ADMIN (ID_CANVA, ID_ADMIN, LOGIN_STATUS)
    VALUES (@ID_CANVA, @ID_ADMIN, @loginStatus);
END;
GO;

-- Update status

CREATE OR ALTER PROCEDURE UpdateLoginStatus_Canva_Admin
    @ID_CANVA VARCHAR(5),
    @ID_ADMIN VARCHAR(5)
AS
BEGIN
    DECLARE @Exists BIT;
    EXEC CheckCanva_AdminExists @ID_CANVA, @ID_ADMIN;

    IF @Exists = 1
    BEGIN
        DECLARE @loginStatus NVARCHAR(50);
        SET @loginStatus = 'Login';
        INSERT INTO CANVA_ADMIN (ID_CANVA, ID_ADMIN, LOGIN_STATUS)
        VALUES (@ID_CANVA, @ID_ADMIN, @loginStatus);
    END;
    ELSE
        RETURN 0;
END;
GO;

CREATE OR ALTER PROCEDURE UpdateLogoutStatus_Canva_Admin
    @ID_CANVA VARCHAR(5),
    @ID_ADMIN VARCHAR(5)
AS
BEGIN
    DECLARE @Exists BIT;
    EXEC CheckCanva_AdminExists @ID_CANVA, @ID_ADMIN;

    IF @Exists = 1
    BEGIN
        DECLARE @loginStatus NVARCHAR(50);
        SET @loginStatus = 'Logout';
        INSERT INTO CANVA_ADMIN (ID_CANVA, ID_ADMIN, LOGIN_STATUS)
        VALUES (@ID_CANVA, @ID_ADMIN, @loginStatus);
    END;
    ELSE
        RETURN 0;
END;
