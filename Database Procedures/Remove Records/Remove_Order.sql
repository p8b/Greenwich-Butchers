-- ================================================
-- Remove Order
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE RemoveOrder(
		@OrderID Int)
AS
BEGIN
	DELETE FROM tblOrders
	WHERE Order_Id = @OrderID
END
GO