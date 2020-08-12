-- ================================================
-- Remove Employee record
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE RemoveEmployee(
		@EmployeeID Int)
AS
BEGIN
	DELETE FROM tblPersons 
	WHERE Person_Id = (
	SELECT Person_Id FROM tblEmployee 
    WHERE Employee_Id = @EmployeeID)
END
GO