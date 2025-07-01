create database Emp_Audit

use Emp_Audit

CREATE TABLE Employees ( 
    EmpID INT PRIMARY KEY, 
    EmpName VARCHAR(100), 
    Department VARCHAR(50), 
    Salary DECIMAL(10, 2) 
); 

CREATE TABLE EmployeeAuditLog ( 
    LogID INT IDENTITY(1,1) PRIMARY KEY, 
    EmpID INT, 
    EmpName VARCHAR(100), 
    Department VARCHAR(50), 
    Salary DECIMAL(10,2), 
    ActionType VARCHAR(10), 
    ActionDate DATETIME DEFAULT GETDATE() 
); 

CREATE TRIGGER trg_AuditEmployeeInsert
ON Employees
AFTER INSERT
AS
BEGIN
    INSERT INTO EmployeeAuditLog (EmpID, EmpName, Department, Salary, ActionType)
    SELECT EmpID, EmpName, Department, Salary, 'INSERT'
    FROM INSERTED;
END;

CREATE TRIGGER trg_AuditEmployeeDelete
ON Employees
AFTER DELETE
AS
BEGIN
    INSERT INTO EmployeeAuditLog (EmpID, EmpName, Department, Salary, ActionType)
    SELECT EmpID, EmpName, Department, Salary, 'DELETE'
    FROM DELETED;
END;

INSERT INTO Employees (EmpID, EmpName, Department, Salary)
VALUES (101, 'Alice Johnson', 'HR', 55000.00),
       (102, 'Bob Smith', 'IT', 72000.00);

DELETE FROM Employees
WHERE EmpID = 101;

SELECT * FROM EmployeeAuditLog;
