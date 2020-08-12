-- ================================================
-- Test Order procedures
-- @FilterBy CustomerID, OrderID, Status, Type 
-- or Off (View all orders)
-- @FilterValue CustomerID, OrderID 
--				Status (Pending, Complete or Cancelled)
--				Type (Shop, Bulk)
-- ================================================
USE [GreenwichButchers]
-- Select the first customer and their first address
DECLARE @CusID int, @AddID int
SELECT TOP(1) @CusID = Customer_Id FROM tblCustomers
SELECT TOP(1) @AddID = Address_Id FROM tblAddresses

EXEC dbo.ViewOrder -- View all customer orders
@FilterValue = @CusID,
@FilterBy = 'CustomerID'

CREATE TABLE #TEMP (Order_Id int) -- Create Temp table
-- Add new order and insert the result in #Temp table
INSERT INTO #Temp EXEC dbo.AddOrder -- Add new Order
@OrderDate ='19/01/2019', @OrderDeliveryDate ='25/01/2019',
@Note = 'HERE SOME NOTE', @Status = 'Pending',
@orderType = 'Bulk', @CustomerID = @CusID, @AddressID = @AddID

DECLARE @OrdID int-- Get the id from Temp table
SELECT @OrdID = Order_Id FROM #TEMP 
DROP TABLE #TEMP -- Remove Temp Table

EXEC dbo.ViewOrder -- View new order after add
@FilterValue = @OrdID,
@FilterBy = 'OrderID'

EXEC dbo.UpdateOrder
@OrderID = @OrdID, @OrderDate = '12/02/2019',
@OrderDeliveryDate = '12/02/2019', @Note =null,
@Status = 'Complete', @orderType = 'Shop', @AddressID = @AddID

EXEC dbo.ViewOrder -- View new order after update
@FilterValue = @OrdID,
@FilterBy = 'OrderID'

EXEC dbo.RemoveOrder -- Remove order
@OrderID = @OrdID

EXEC dbo.ViewOrder -- View all customer orders after remove
@FilterValue = @CusID,
@FilterBy = 'CustomerID'