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
        VALUES (@NewID, @ID_SHAPE, @ID_CANVA, CURRENT_USER, 'Empty', GETDATE(), GETDATE());

        SET @ResponseMessage = 'Table successfully created with ID ' + @NewID;
    END TRY
        BEGIN CATCH
            SET @ResponseMessage = 'An error occurred during insertation.';
        END CATCH

END
GO

-- SELECT*
-- FROM CAFETERIA_TABLE
-- GO

CREATE OR ALTER PROC sp_ADMIN_UPDATETABLE
	@ID_TABLE VARCHAR(5),
    @X_COORDINATE FLOAT,
    @Y_COORDINATE FLOAT,
    @ID_ADMIN VARCHAR(5),
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

CREATE OR ALTER PROC sp_ADMIN_LOAD
	@ID_CANVA VARCHAR(5),
    @ResponseMessage NVARCHAR(255) OUTPUT
AS
BEGIN
    SELECT CT.ID_TABLE, CT.X_COORDINATE, CT.Y_COORDINATE, 
        CT.TABLE_STATUS, ST.SHAPE_TYPENAME, ST.HEIGHT, ST.WIDTH, ST.RADIUS,
        FTY.ID_FOOD, FTA.AMOUNT_IN_TABLE, FTY.FOOD_NAME, FTY.FOOD_TYPENAME
    FROM CAFETERIA_TABLE CT, CANVA C, SHAPE_TYPE ST, FOOD_TABLE FTA, FOOD_TYPE FTY
    WHERE C.ID_CANVA = @ID_CANVA
        AND C.ID_CANVA = CT.ID_CANVA
        AND CT.ID_SHAPE = ST.ID_SHAPE
        AND FTA.ID_TABLE = CT.ID_TABLE
        AND FTA.ID_FOOD = FTY.ID_FOOD
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