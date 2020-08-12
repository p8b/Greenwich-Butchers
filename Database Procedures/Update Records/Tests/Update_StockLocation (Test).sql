-- ================================================
-- Test "Update Stock Location" Procedure
-- ================================================
USE [GreenwichButchers]
EXEC dbo.UpdateStockLocation
@FromLocation = 'Test Shop',
@ToLocation = 'Test Only'

Select * FROM tblStockLocation