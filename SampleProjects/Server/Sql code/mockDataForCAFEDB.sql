USE cafeteriaDB
GO

--CANVA DATA
INSERT INTO CANVA (ID_CANVA, WIDTH, HEIGHT) VALUES  
('C001', 800.0, 600.0),  
('C002', 1024.0, 768.0),  
('C003', 1280.0, 720.0);


--ADMIN DATA
INSERT INTO ADMIN (ID_ADMIN, EMAIL, PASSWORD) VALUES  
('A001', 'admin1@example.com', 'password1'),  
('A002', 'admin2@example.com', 'password2'),  
('A003', 'admin3@example.com', 'password3');


--CANVA_ADMIN DATA
INSERT INTO CANVA_ADMIN (ID_CANVA, ID_ADMIN, LOGIN_STATUS) VALUES  
('C001', 'A001', 'active'),  
('C001', 'A002', 'inactive'),  
('C002', 'A003', 'active');


--RECTANGLE_SHAPE DATA
INSERT INTO SHAPE_TYPE (ID_SHAPE, WIDTH, HEIGHT, RADIUS, 
MIDPOINT_X_COORDINATE, MIDPOINT_Y_COORDINATE, SHAPE_TYPENAME) VALUES  
('S001', 100.0, 50.0, 0, 0, 0, 'Rectangle'),  
('S002', 75.0, 75.0, 0, 0, 0, 'Rectangle'),  
('S003', 60.0, 80.0, 0, 0, 0, 'Rectangle'),
('S004', 0, 0, 100.0, 0, 0, 'Circle'),  
('S005', 0, 0, 75.0, 0, 0, 'Circle'),  
('S006', 0, 0, 60.0, 0, 0, 'Circle');

--CAFETERIA_TABLE DATA
INSERT INTO CAFETERIA_TABLE (ID_TABLE, ID_SHAPE, ID_CANVA, ID_ADMIN, TABLE_STATUS) VALUES  
('T001', 'S001', 'C001', 'A001', 'available'),  
('T002', 'S002', 'C001', 'A002', 'occupied'),
('T003', 'S002', 'C002', 'A003', 'occupied'),
('T004', 'S004', 'C002', 'A003', 'available'),
('T005', 'S006', 'C002', 'A003', 'occupied');


--FOOD_TYPE DATA
INSERT INTO FOOD_TYPE (ID_FOOD, FOOD_NAME, AMOUNT_LEFT, PRICE, FOOD_TYPE_STATUS) VALUES  
('F001', 'Burger', 20, 5.99, 'available'),  
('F002', 'Pizza', 15, 8.99, 'available'),  
('F003', 'Salad', 30, 4.99, 'available'),
('F004', 'Coffee', 30, 5.99, 'available');

--FOOD_TABLE DATA
INSERT INTO FOOD_TABLE (ID_FOOD, ID_TABLE, AMOUNT_IN_TABLE) VALUES  
('F001', 'T001', 5),  
('F002', 'T002', 3),  
('F004', 'T003', 10);

INSERT INTO FOOD_TABLE (ID_FOOD, ID_TABLE, AMOUNT_IN_TABLE) VALUES  
('F001', 'T002', 5),  
('F004', 'T002', 3),  
('F001', 'T003', 10);