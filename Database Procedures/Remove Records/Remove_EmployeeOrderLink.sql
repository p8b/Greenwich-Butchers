-- ================================================
-- Remove Employee Order Link
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE RemoveEmployeeOrderLink(
		@EmployeeID Int,
		@OrderID Int)
AS
BEGIN
	-- get the "InitialEployee" status of the link
	DECLARE @InitialEmployee bit
	SELECT @InitialEmployee = Initial_Employee 
	FROM LinkEmployeeOrders 
	WHERE Employee_Id = @EmployeeID
	AND Order_Id = @OrderID

	-- If the employee is NOT the initial employee
	IF (@InitialEmployee != 1)
		DELETE FROM LinkEmployeeOrders -- Remove the link
		WHERE Employee_Id = @EmployeeID
		AND Order_Id = @OrderID
	ELSE -- Else if it is then raise customer error
		RAISERROR('Initial Employee Cannot Be Removed!',18,0)
END
GO