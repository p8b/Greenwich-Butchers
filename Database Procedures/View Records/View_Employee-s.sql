-- ================================================
-- View Employee record
-- @ShowAll = 0 (Search By Employee ID)
-- @ShowAll = 1 (Show All Employees)
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE ViewEmployee(
		@EmployeeID Int, @ShowAll bit)
AS
BEGIN
	IF (@ShowAll = 0) -- Return selected employee
	BEGIN
		SELECT *
		FROM tblEmployee AS E,tblPersons AS P
		WHERE Employee_Id = @EmployeeID
		AND P.Person_Id = E.Person_Id
	END
	ELSE IF (@ShowAll = 1) -- Return all employees
	BEGIN
		SELECT *
		FROM tblEmployee AS E,tblPersons AS P
		WHERE P.Person_Id = E.Person_Id
	END
END
GO