-- ================================================
-- Test Employee procedures
-- ================================================
USE[GreenwichButchers]
EXEC dbo.ViewEmployee -- Show All employees before add
@EmployeeID = 0, @ShowAll = 1

CREATE TABLE #TEMP (Employee_Id int) -- Create Temp table
-- Add new Employee and insert the result in #Temp table
INSERT INTO #Temp EXEC dbo.AddEmployee -- Add new employee
@Title = 'TEST', @Name = 'TEST',
@Surname = 'TEST', @Tel = '121321321321',
@Email = 'TEST@TEST.TEST', @Password = 'TEST',
@PositionName = 'Manager'

DECLARE @EmpID int
SELECT @EmpID = Employee_Id FROM #TEMP -- Get the id from Temp table
DROP TABLE #TEMP -- Remove Temp Table

EXEC dbo.ViewEmployee -- Select new employees after add
@EmployeeID = @EmpID, @ShowAll = 0

EXEC dbo.UpdateEmployee-- Update employee
@EmployeeID = @EmpID,
@Title  = 'Mr', @Name  = 'Alex',
@Surname  = 'Smith', @Tel  = '53156854',
@Email  = 'AS@ss.com', @PositionName = 'Staff'

EXEC dbo.ViewEmployee -- Select new employees after update
@EmployeeID = @EmpID, @ShowAll = 0

EXEC dbo.RemoveEmployee -- Remove new employee
@EmployeeID = @EmpID 

EXEC dbo.ViewEmployee -- Show All employees after remove
@EmployeeID = 0, @ShowAll = 1