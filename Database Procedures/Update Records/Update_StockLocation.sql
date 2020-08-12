-- ================================================
-- Update Stock Location
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE UpdateStockLocation (
	@FromLocation varchar(30), 
	@ToLocation varchar(30))
AS
BEGIN
	UPDATE tblStockLocation 
	SET stockLocation_Name = @ToLocation
	WHERE stockLocation_Name = @FromLocation
END
GO