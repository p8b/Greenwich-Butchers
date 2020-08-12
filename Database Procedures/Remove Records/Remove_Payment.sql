-- ================================================
-- Remove Payment record
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE RemovePayment(
		@OrderID Int)
AS
BEGIN
	DELETE FROM tblPayment 
		WHERE Order_Id = @OrderID
END
GO