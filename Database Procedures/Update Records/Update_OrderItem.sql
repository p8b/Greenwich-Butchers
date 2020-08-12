-- ================================================
-- Update Order Item records
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE UpdateOrderItem(
		@OrderID Int, @ProductName varchar(300),
		@ItemPrice decimal(8,2), @ItemQuantity decimal(6,2))
AS
BEGIN
	UPDATE tblOrderItems SET
		Item_Price = @ItemPrice,
		Item_Quantity = @ItemQuantity
	WHERE Order_Id = @OrderID
	AND Product_Name = @ProductName
END
GO
