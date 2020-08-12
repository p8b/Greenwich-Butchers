-- ================================================
-- View All Stock locations
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE ViewSockLocations
AS
BEGIN
	SELECT * FROM tblStockLocation
END
GO