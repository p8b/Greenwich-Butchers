-- ================================================
-- Remove Stock Location Record
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE RemoveStockLocation(
	@LocationName varchar(30))
AS
BEGIN
	DELETE FROM tblStockLocation 
	WHERE tblStockLocation.stockLocation_Name = @LocationName
END
GO