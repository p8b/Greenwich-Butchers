-- ================================================
-- Test "Remove Stock Location" Procedure
-- ================================================
USE [GreenwichButchers]

-- Get Stock locations before the record is removed
EXEC dbo.ViewSockLocations

-- Remove the record
EXEC dbo.RemoveStockLocation
@LocationName = 'Test Only'

-- Get Stock Locations after the record is removed
EXEC dbo.ViewSockLocations