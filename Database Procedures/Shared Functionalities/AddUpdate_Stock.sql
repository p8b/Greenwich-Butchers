-- ================================================
-- Add or Update Stock Records
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE AddUpdateStock(@ProductName varchar(300),
		@Quantity decimal (6,2)=null,
		@StockLocation varchar(30))
AS
BEGIN -- If the record exists then update the record
	IF EXISTS(SELECT COUNT(*) FROM tblStock 
	WHERE tblStock.Product_Name = @ProductName 
	AND tblStock.StockLocation_Name = @StockLocation)
		BEGIN
		UPDATE tblStock 
		SET stock_Quantity = @Quantity
		WHERE 
			Product_Name = @ProductName AND
			StockLocation_Name = @StockLocation
		END
	ELSE -- else create new record
		BEGIN
			INSERT INTO tblStock(
				stock_Quantity,	StockLocation_Name,Product_Name)
			VALUES (@Quantity, @StockLocation, @ProductName)
		END
END
GO