-- ===========================
-- Add Stock Location Record
-- ===========================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE AddStockLocation(
	@LocationName varchar(30))
AS
BEGIN
	INSERT INTO tblStockLocation(
		stockLocation_Name) 
	VALUES(
		@LocationName)
END
GO