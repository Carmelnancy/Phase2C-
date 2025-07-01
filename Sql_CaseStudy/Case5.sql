--Case 5
create database Case5
use Case5

CREATE TABLE Orders ( 
    OrderID INT IDENTITY(1,1) PRIMARY KEY, 
    CustomerName VARCHAR(100), 
    OrderDate DATETIME DEFAULT GETDATE() 

); 

CREATE TABLE OrderItems ( 
    OrderItemID INT IDENTITY(1,1) PRIMARY KEY, 
    OrderID INT FOREIGN KEY REFERENCES Orders(OrderID), 
    ProductName VARCHAR(100), 
    Quantity INT, 
    UnitPrice DECIMAL(10,2) 
); 

BEGIN TRY
    BEGIN TRANSACTION;

    INSERT INTO Orders (CustomerName)
    VALUES ('John Doe');
    DECLARE @OrderID INT = SCOPE_IDENTITY();
    INSERT INTO OrderItems (OrderID, ProductName, Quantity, UnitPrice)
    VALUES 
        (@OrderID, 'Laptop', 1, 75000.00),
        (@OrderID, 'Mouse', 2, 500.00),
        (@OrderID, 'Keyboard', 1, 1500.00);
    COMMIT;
    PRINT 'Transaction committed successfully.';
END TRY
BEGIN CATCH
    ROLLBACK;
    PRINT 'Transaction failed. Rolled back.';
    PRINT ERROR_MESSAGE(); 
END CATCH;


BEGIN TRY
    BEGIN TRANSACTION;
    INSERT INTO Orders (CustomerName)
    VALUES ('Jimmy');
    DECLARE @OrderID INT = SCOPE_IDENTITY();
    INSERT INTO OrderItems (OrderID, ProductName, Quantity, UnitPrice)
    VALUES 
        (@OrderID, 'Monitor',2, 8000.00);
    COMMIT;
END TRY
BEGIN CATCH
    ROLLBACK;
    PRINT 'Failed as expected. Rolled back.';
    PRINT ERROR_MESSAGE();
END CATCH;

SELECT * FROM Orders;

SELECT * FROM OrderItems;


