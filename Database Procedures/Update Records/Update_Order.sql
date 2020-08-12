-- ================================================
-- Update Order
-- ================================================
GO
CREATE OR ALTER PROCEDURE UpdateOrder(
		@OrderID Int, @OrderDate varchar(10),
		@OrderDeliveryDate varchar(10),	@Note varchar(500)=null,
		@Status varchar(30), @orderType varchar(50),
		@AddressID Int)
AS
BEGIN
	UPDATE tblOrders SET
		order_Date = @OrderDate,
		order_DeliveryDate = @OrderDeliveryDate,
		order_Note = @Note,
		order_Status = @Status,
		order_type = @orderType,
		Address_Id = @AddressID
	WHERE
		Order_Id = @OrderID
END
GO