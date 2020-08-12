-- ================================================
-- View Stock Records
-- @Filterby "Product" or "Location" or "Off" (View All)
-- and pass the appropriate Value by using the
-- @Value parameter
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE ViewStock(
	@Value varchar(300)=null, 
	@FilterBy varchar(8))
AS 
BEGIN
	IF (@FilterBy = 'Product')
		SELECT * FROM tblStock 
		WHERE tblStock.Product_Name = @Value 
	IF (@FilterBy = 'Location')
		SELECT * FROM tblStock 
		WHERE tblStock.StockLocation_Name = @Value	
	IF (@FilterBy = 'Off') -- View All Stocks
		SELECT * FROM tblStock
END
GO
