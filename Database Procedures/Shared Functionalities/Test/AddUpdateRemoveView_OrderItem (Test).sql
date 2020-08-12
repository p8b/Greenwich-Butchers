-- ================================================
-- Test "View Order Basket" procedure
-- ===============================================
USE [GreenwichButchers]

DECLARE @OrdID int, @ItmID int, @ProdName varchar(300)

SELECT TOP(1) @OrdID = Order_Id FROM tblOrders -- Select an order
-- Select the first produt from product which is not in the 
-- selected order
SELECT TOP(1) @ProdName = P.Product_Name
FROM tblProducts AS P LEFT JOIN tblOrderItems AS I
ON P.Product_Name = I.Product_Name
WHERE I.Product_Name IS NULL

EXEC dbo.ViewOrderItems -- Show All items in the selected order
@OrderID =@OrdID		-- before adding new item

EXEC dbo.AddOrderItem -- Add a new order item
@OrderID =@OrdID,
@ProductName = @ProdName,
@ItemPrice = 0,
@ItemQuantity = 30

EXEC dbo.ViewOrderItems -- Show All items in the order after add
@OrderID =@OrdID

EXEC dbo.UpdateOrderItem -- update the record
@OrderID =@OrdID,
@ProductName = @ProdName,
@ItemPrice = 199,
@ItemQuantity = 50

EXEC dbo.ViewOrderItems -- Show All items in the order after Update
@OrderID = @OrdID

SELECT @ItmID = Item_Id  -- Select the Item_Id of the new Item 
	FROM tblOrderItems 
	WHERE Order_Id = @OrdID 
	AND Product_Name = @ProdName

EXEC dbo.RemoveOrderItem -- Remove the new item
@ItemID = @ItmID

EXEC dbo.ViewOrderItems -- Show All items in the order after remove
@OrderID = @OrdID
