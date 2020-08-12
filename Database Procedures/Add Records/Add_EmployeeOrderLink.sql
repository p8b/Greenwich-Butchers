-- ================================================
-- Add Employee Order Link
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE AddEmployeeOrderLink(
		@EmployeeID Int, @OrderID Int)
AS
BEGIN
	DECLARE @InitialEmpStatus Bit = 0

	-- If there is no employee linked to the order
	IF NOT EXISTS (SELECT Employee_Id 
		FROM LinkEmployeeOrders
		WHERE Order_Id = @OrderID
		AND Initial_Employee = 1)
	BEGIN -- Set the Initial Employee status to true
		SET @InitialEmpStatus = 1
	END
	INSERT INTO LinkEmployeeOrders(
		Employee_Id, Order_Id, Initial_Employee)
	VALUES (
		@EmployeeID, @OrderID, @InitialEmpStatus)
END
GO