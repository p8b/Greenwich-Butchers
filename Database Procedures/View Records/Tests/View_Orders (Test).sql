-- ================================================
-- Test Order procedures
-- @FilterBy CustomerID, OrderID, Status, Type 
-- or Off (View all orders)
-- @FilterValue CustomerID, OrderID 
--				Status (Pending, Complete or Cancelled)
--				Type (Shop, Bulk)
-- ================================================
USE [GreenwichButchers]

EXEC dbo.ViewOrder -- View all customer orders
@FilterValue = 'Pending',
@FilterBy = 'Status'

EXEC dbo.ViewOrder -- View all customer orders after remove
@FilterValue = 'Bulk',
@FilterBy = 'Type'