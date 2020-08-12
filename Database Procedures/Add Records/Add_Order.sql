-- ================================================
-- Add Order records
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE AddOrder(
		@OrderDate varchar(10),	@OrderDeliveryDate varchar(10),
		@Note varchar(500)=null, @Status varchar(30),
		@OrderType varchar(50),	@CustomerID Int=null,
		@AddressID Int = null)
AS
BEGIN
	INSERT INTO tblOrders(
		order_Date,	order_DeliveryDate,	order_Note,
		order_Status, order_type, Customer_Id, Address_Id)
	OUTPUT INSERTED.Order_Id
	VALUES (
		@OrderDate,	@OrderDeliveryDate,	@Note,
		@Status, @OrderType, @CustomerID, @AddressID)
END
GO