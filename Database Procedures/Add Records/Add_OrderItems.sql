-- ================================================
-- Add Order Item records
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE AddOrderItem(
		@OrderID Int, @ProductName varchar(300),
		@ItemPrice decimal(8,2) =null,
		@ItemQuantity decimal(6,2))
AS
BEGIN
	-- If record does not exists then add new record
	IF NOT EXISTS(SELECT * FROM tblOrderItems 
	   WHERE Order_Id = @OrderID 
	   AND Product_Name = @ProductName)
	BEGIN -- Then add the order item
		INSERT INTO tblOrderItems(
			Order_Id, Product_Name,
			Item_Price, Item_Quantity)
		VALUES(
			@OrderID, @ProductName,
			@ItemPrice,	@ItemQuantity)
	END
END
GO