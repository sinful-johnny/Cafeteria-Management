use cafeteria_DB
GO

-- Take all Canva
CREATE OR ALTER PROCEDURE SelectAllCanva
AS
BEGIN
    SELECT *
    FROM CANVA C
END
GO

-- Take id Canva
CREATE OR ALTER PROCEDURE SelectIdCanva
    @ID_CANVA VARCHAR(5)
AS
BEGIN
    SELECT c.HEIGHT, c.WIDTH
    FROM CANVA C
    where c.ID_CANVA = @ID_CANVA
END
GO

-- Insert Canva

CREATE OR ALTER PROCEDURE InsertCanva
    @WIDTH FLOAT,
    @HEIGHT FLOAT
AS
BEGIN
    DECLARE @NewID VARCHAR(5);
    DECLARE @LastID VARCHAR(5);
    DECLARE @NextID INT;

    -- Step 1: Get the latest ID_CANVA
    SELECT @LastID = MAX(ID_CANVA) FROM CANVA;

    -- Step 2: If there is no existing ID, start with "C001"
    IF @LastID IS NULL
    BEGIN
        SET @NewID = 'C001';
    END
    ELSE
    BEGIN
        -- Step 3: Extract the numeric part, increment it, and format it with leading zeros
        SET @NextID = CAST(SUBSTRING(@LastID, 2, 3) AS INT) + 1;
        SET @NewID = 'C' + RIGHT('000' + CAST(@NextID AS VARCHAR), 3);
    END

    -- Step 4: Insert the new record with the generated ID
    INSERT INTO CANVA (ID_CANVA, WIDTH, HEIGHT, CREATED_AT, UPDATE_AT)
    VALUES (@NewID, @WIDTH, @HEIGHT, GETDATE(), GETDATE());

    -- Return the new ID
    SELECT @NewID AS NewID;
END
GO



--Delete Canva

CREATE OR ALTER PROCEDURE DeleteCanva
    @ID_CANVA VARCHAR(5)
AS
BEGIN
    -- Check if the record exists
    IF EXISTS (SELECT 1 FROM CANVA WHERE ID_CANVA = @ID_CANVA)
    BEGIN
        -- Delete the record
        DELETE FROM CANVA WHERE ID_CANVA = @ID_CANVA;

        PRINT 'Canva deleted successfully.';
    END
    ELSE
    BEGIN
        PRINT 'Canva ID not found.';
    END
END
GO