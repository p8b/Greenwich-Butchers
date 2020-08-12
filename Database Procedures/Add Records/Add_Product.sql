-- ================================================
-- Add Product Record
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE AddProduct(
	@ProductName varchar(300),
	@RetailUnit varchar(50),
	@RetailPrice decimal(6,2),
	@Category varchar(80))
AS
BEGIN
	INSERT INTO tblProducts(
		Product_Name,
		product_RetailUnit,
		product_RetailPrice,
		productCategory)
	VALUES(
		@ProductName,
		@RetailUnit,
		@RetailPrice,
		@Category)
END
GO