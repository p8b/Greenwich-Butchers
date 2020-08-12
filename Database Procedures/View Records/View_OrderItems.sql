-- ================================================
-- View Order Items record
-- View all items of an order by passing the
-- "OrderID"
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE ViewOrderItems(
		@OrderID Int)
AS
BEGIN
	SELECT I.Item_Id, I.Item_Price, I.Item_Quantity,
			P.Product_Name, P.product_RetailUnit, P.productCategory
	FROM tblOrderItems AS I, tblProducts AS P
	WHERE Order_Id = @OrderID
	AND P.Product_Name = I.Product_Name
END
GO