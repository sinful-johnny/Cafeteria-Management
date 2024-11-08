USE cafeteriaDB
GO

CREATE OR ALTER PROC sp_ADMIN_INSERTTABLE
	@ID_CANVA VARCHAR(5),
    @ID_SHAPE VARCHAR(5),
    
    @ResponseMessage NVARCHAR(255) OUTPUT
AS
BEGIN
    DECLARE @LastID NVARCHAR(10);
    DECLARE @NumericPart INT;
    DECLARE @NewID NVARCHAR(10);

    SELECT @LastID = MAX(ID_TABLE) FROM CAFETERIA_TABLE WHERE ID_TABLE LIKE 'T%';

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
    SET @NewID = 'T' + RIGHT('000' + CAST(@NumericPart AS NVARCHAR(3)), 3);

    BEGIN TRY
        -- SELECT *
        -- FROM CAFETERIA_TABLE
        INSERT INTO CAFETERIA_TABLE (ID_TABLE, ID_SHAPE, ID_CANVA, ID_ADMIN, TABLE_STATUS, CREATED_AT, UPDATE_AT)
        VALUES (@NewID, @ID_SHAPE, @ID_CANVA, CURRENT_USER, 'available', GETDATE(), GETDATE());

        SET @ResponseMessage = 'Table successfully created with ID ' + @NewID;
    END TRY
        BEGIN CATCH
            SET @ResponseMessage = 'An error occurred during insertation.';
        END CATCH

END
GO

-- SELECT *
-- FROM CAFETERIA_TABLE
-- GO

-- declare @ResponseMessage NVARCHAR(255);
-- EXEC sp_ADMIN_DELETETABLE 'T002', @ResponseMessage = @ResponseMessage output;

-- select @ResponseMessage;
-- GO

CREATE OR ALTER PROC sp_ADMIN_DELETETABLE
    @ID_TABLE VARCHAR(5),
    @ResponseMessage NVARCHAR(255) OUTPUT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM CAFETERIA_TABLE WHERE ID_TABLE = @ID_TABLE
                                                AND TABLE_STATUS = 'available')
    BEGIN
        DELETE FROM CAFETERIA_TABLE
        WHERE ID_TABLE = @ID_TABLE
        SET @ResponseMessage = 'Table deleted from CAFETERIA_TABLE.';
    END
    ELSE IF EXISTS (SELECT 1 FROM FOOD_TABLE WHERE ID_TABLE = @ID_TABLE)
    BEGIN
        -- Delete all references to the table from FOOD_TABLE
        DELETE FROM FOOD_TABLE WHERE ID_TABLE = @ID_TABLE;

        -- After deleting from FOOD_TABLE, delete the table from CAFETERIA_TABLE
        DELETE FROM CAFETERIA_TABLE WHERE ID_TABLE = @ID_TABLE;
        
        SET @ResponseMessage = 'Table and associated food records deleted.';
    END
    ELSE
    BEGIN
        -- Table not found in CAFETERIA_TABLE or FOOD_TABLE
        SET @ResponseMessage = 'No matching table found to delete.';
    END
END
GO

-- SELECT*
-- FROM CAFETERIA_TABLE
-- GO

CREATE OR ALTER PROC sp_ADMIN_UPDATETABLE
	@ID_TABLE VARCHAR(5),
    @X_COORDINATE FLOAT,
    @Y_COORDINATE FLOAT,
    @ResponseMessage NVARCHAR(255) OUTPUT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM CAFETERIA_TABLE WHERE ID_TABLE = @ID_TABLE)
    BEGIN
        IF (@X_COORDINATE IS NOT NULL AND @Y_COORDINATE IS NOT NULL)
        BEGIN
            UPDATE CAFETERIA_TABLE
            SET X_COORDINATE = @X_COORDINATE, 
                Y_COORDINATE = @Y_COORDINATE,
                ID_ADMIN = CURRENT_USER,
                UPDATE_AT = GETDATE()
            WHERE ID_TABLE = @ID_TABLE
                
        END
        ELSE IF (@X_COORDINATE IS NOT NULL)
        BEGIN
            UPDATE CAFETERIA_TABLE
            SET X_COORDINATE = @X_COORDINATE,
                ID_ADMIN = CURRENT_USER,
                UPDATE_AT = GETDATE()
            WHERE ID_TABLE = @ID_TABLE
        END
        ELSE IF (@Y_COORDINATE IS NOT NULL)
        BEGIN
            UPDATE CAFETERIA_TABLE
            SET Y_COORDINATE = @X_COORDINATE,
                ID_ADMIN = CURRENT_USER,
                UPDATE_AT = GETDATE()
            WHERE ID_TABLE = @ID_TABLE
        END
        SET @ResponseMessage = 'Table with ID ' + @ID_TABLE + ' successfully updated';
        RETURN;
    END
    ELSE
    BEGIN
        SET @ResponseMessage = 'There is no table with ID ' + @ID_TABLE;
    END
END
GO

-- SELECT *
-- FROM FOOD_TABLE
-- GO

-- SELECT *
-- FROM FOOD_TYPE
-- GO

CREATE OR ALTER PROC sp_ADMIN_INSERT_TABLEFOOD
	@ID_TABLE VARCHAR(5),
    @ID_FOOD VARCHAR(5),
    @ResponseMessage NVARCHAR(255) OUTPUT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM FOOD_TYPE WHERE ID_FOOD = @ID_FOOD
                                            AND FOOD_TYPE_STATUS = 'available')
    BEGIN
        IF NOT EXISTS (SELECT 1 FROM FOOD_TABLE WHERE ID_TABLE = @ID_TABLE
                                                    AND ID_FOOD = @ID_FOOD)
        BEGIN
            INSERT INTO FOOD_TABLE(ID_FOOD, ID_TABLE, AMOUNT_IN_TABLE, CREATED_AT, UPDATE_AT) VALUES
                                (@ID_FOOD, @ID_TABLE, 1, GETDATE(), GETDATE());
            SET @ResponseMessage = 'Table with ID ' + @ID_TABLE + ' add 1 food with ID ' + @ID_FOOD;
        END
        ELSE
        BEGIN
            UPDATE FOOD_TABLE
            SET AMOUNT_IN_TABLE = AMOUNT_IN_TABLE + 1,
                UPDATE_AT = GETDATE()
            WHERE ID_TABLE = @ID_TABLE
                AND ID_FOOD = @ID_FOOD
        END
        UPDATE FOOD_TYPE
        SET AMOUNT_LEFT = AMOUNT_LEFT - 1,
            UPDATE_AT = GETDATE()
        WHERE ID_FOOD = @ID_FOOD

        UPDATE FOOD_TYPE
        SET FOOD_TYPE_STATUS = 'sold out',
            UPDATE_AT = GETDATE()
        WHERE ID_FOOD = @ID_FOOD
            AND AMOUNT_LEFT = 0;

        UPDATE CAFETERIA_TABLE
        SET TABLE_STATUS = 'occupied',
            UPDATE_AT = GETDATE()
        WHERE ID_TABLE = @ID_TABLE

        SET @ResponseMessage = 'Table with ID ' + @ID_TABLE + ' add one more food with ID ' + @ID_FOOD;
        RETURN;
    END
    ELSE
    BEGIN
        SET @ResponseMessage = 'There is no food with ID ' + @ID_TABLE + 'left';
    END
END
GO

CREATE OR ALTER PROC sp_ADMIN_CLEAR_TABLEFOOD
	@ID_TABLE VARCHAR(5),
    @ID_FOOD VARCHAR(5),
    @ResponseMessage NVARCHAR(255) OUTPUT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM FOOD_TABLE WHERE ID_TABLE = @ID_TABLE
                                            AND ID_FOOD = @ID_FOOD)
    BEGIN
        DELETE FROM FOOD_TABLE
        WHERE ID_TABLE = @ID_TABLE
            AND ID_FOOD = @ID_FOOD
        SET @ResponseMessage = 'Table with ID ' + @ID_TABLE + ' was cleared';
        IF NOT EXISTS (SELECT 1 FROM FOOD_TABLE WHERE ID_TABLE = @ID_TABLE)
        BEGIN
            UPDATE CAFETERIA_TABLE
            SET TABLE_STATUS = 'available',
                UPDATE_AT = GETDATE()
            WHERE ID_TABLE = @ID_TABLE
        END
        SET @ResponseMessage = 'Deleted food with ID ' + @ID_FOOD + ' on the table with ID ' + @ID_TABLE;

    END
    ELSE
    BEGIN
        SET @ResponseMessage = 'There is no food with ID ' + @ID_FOOD + ' on the table with ID ' + @ID_TABLE;
    END
END
GO

CREATE OR ALTER PROC sp_ADMIN_TABLEInCANVA
	@ID_CANVA VARCHAR(5)
AS
BEGIN
    set @ID_CANVA = 'C001'

    SELECT VATIC.X_COORDINATE, VATIC.Y_COORDINATE, VATIC.WIDTH, VATIC.HEIGHT, VATIC.RADIUS,
        VATIC.TABLE_STATUS, VATIC.ID_TABLE, VATIC.ID_SHAPE, ID_CANVA
    FROM V_ADMIN_TABLEInCANVA VATIC
    WHERE VATIC.ID_CANVA = @ID_CANVA
END
GO



CREATE OR ALTER PROC sp_ADMIN_FOODsOnTABLE
    --@ResponseMessage NVARCHAR(255) OUTPUT
AS
BEGIN
    SELECT *
    FROM V_ADMIN_FOODsOnTABLE

    -- Close and deallocate the cursor
    CLOSE admin_cursor;
    DEALLOCATE admin_cursor;

    UPDATE FOOD_TYPE
    SET AMOUNT_LEFT = AMOUNT_LEFT - FTA.AMOUNT_IN_TABLE
    FROM FOOD_TYPE FTY, FOOD_TABLE FTA
    WHERE FTY.ID_FOOD = FTA.ID_FOOD

    UPDATE FOOD_TYPE
    SET FOOD_TYPE_STATUS = 'sold out',
        UPDATE_AT = GETDATE()
    WHERE AMOUNT_LEFT <= 0;
    
END
GO

CREATE OR ALTER PROC sp_ADMIN_SAVE_TABLEinCANVA
	@X_COORDINATE FLOAT,
    @Y_COORDINATE FLOAT,
    @TABLE_STATUS VARCHAR(50),
    @ID_TABLE VARCHAR(5),
    @ID_SHAPE VARCHAR(5),
    @ID_CANVA VARCHAR(5)
AS
BEGIN

    set @ID_CANVA = 'C001'

    DECLARE @ResponseMessage NVARCHAR(255);

    EXEC sp_CANVA_LOGINSTATUS @ID_CANVA, @ResponseMessage OUTPUT;

    INSERT INTO CAFETERIA_TABLE(X_COORDINATE, Y_COORDINATE, TABLE_STATUS, ID_TABLE, 
                                ID_SHAPE, ID_CANVA, ID_ADMIN, CREATED_AT, UPDATE_AT)
    VALUES (@X_COORDINATE, @Y_COORDINATE, @TABLE_STATUS, @ID_TABLE,
            @ID_SHAPE, @ID_CANVA, CURRENT_USER, GETDATE(), GETDATE())
END
GO

-- EXEC DBO.sp_ADMIN_SAVE_TABLEinCANVA
--         0, 0, 'he', '1', 'S002', ''
-- go

-- EXEC DBO.sp_ADMIN_SAVE_TABLEinCANVA
--         0, 0, 'he', '2', 'S002', ''
-- go

-- EXEC DBO.sp_ADMIN_SAVE_FOODonTABLE
--         '1', 'F003', 5
-- go


CREATE OR ALTER PROC sp_ADMIN_SAVE_FOODonTABLE
    @ID_TABLE VARCHAR(5),
    @ID_FOOD VARCHAR(5),
    @AMOUNT_IN_TABLE INT
AS
BEGIN
    --TRUNCATE TABLE CAFETERIA_TABLE;
    --DELETE FROM CAFETERIA_TABLE;

    INSERT INTO FOOD_TABLE(ID_FOOD, ID_TABLE, AMOUNT_IN_TABLE, CREATED_AT, UPDATE_AT)
    VALUES (@ID_FOOD, @ID_TABLE, @AMOUNT_IN_TABLE, GETDATE(), GETDATE())
END
GO

GRANT EXECUTE ON OBJECT::sp_ADMIN_INSERTTABLE
    TO ADMIN; 
GO

GRANT EXECUTE ON OBJECT::sp_ADMIN_UPDATETABLE
    TO ADMIN; 
GO

GRANT EXECUTE ON OBJECT::sp_ADMIN_INSERT_TABLEFOOD
    TO ADMIN; 
GO

GRANT EXECUTE ON OBJECT::sp_ADMIN_CLEAR_TABLEFOOD
    TO ADMIN; 
GO

GRANT EXECUTE ON OBJECT::sp_ADMIN_LOAD
    TO ADMIN; 
GO

GRANT EXECUTE ON OBJECT::sp_ADMIN_DELETETABLE
    TO ADMIN; 
GO

GRANT EXECUTE ON OBJECT::sp_ADMIN_TABLEInCANVA
    TO ADMIN; 
GO

GRANT EXECUTE ON OBJECT::sp_ADMIN_FOODsOnTABLE
    TO ADMIN; 
GO

GRANT EXECUTE ON OBJECT::sp_ADMIN_SAVE_TABLEinCANVA
    TO ADMIN; 
GO

GRANT EXECUTE ON OBJECT::sp_ADMIN_SAVE_FOODonTABLE
    TO ADMIN; 
GO