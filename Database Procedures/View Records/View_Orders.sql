-- ================================================
-- View Orders
-- @FilterBy CustomerID, OrderID, Status, Type 
-- or Off (View all orders)
-- @FilterValue CustomerID, OrderID 
--				Status (Pending, Complete or Cancelled)
--				Type (Shop, Bulk)
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE ViewOrder(
		@FilterValue varchar(30),
		@FilterBy varchar(30))
AS
BEGIN
	IF (@FilterBy = 'Off')
		SELECT * FROM tblOrders
	IF (@FilterBy = 'CustomerID')
		SELECT * FROM tblOrders WHERE Customer_Id = @FilterValue
	IF (@FilterBy = 'Status')
		SELECT * FROM tblOrders WHERE order_Status = @FilterValue
	IF (@FilterBy = 'OrderID')
		SELECT * FROM tblOrders WHERE Order_Id = @FilterValue
	IF (@FilterBy = 'Type')
		SELECT * FROM tblOrders WHERE order_Type = @FilterValue		
		
END
GO