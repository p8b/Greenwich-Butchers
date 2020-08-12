-- ================================================
-- Update Product
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE UpdateProduct (
				@ProductName varchar(300),
				@NewProductName varchar(300),
				@RetailUnit varchar(50),
				@RetailPrice decimal (6,2),
				@productCategory varchar(80))
AS
BEGIN
	Update tblProducts
	SET Product_Name = @NewProductName,
		product_RetailUnit = @RetailUnit,
		product_RetailPrice = @RetailPrice,
		productCategory = @productCategory
	WHERE Product_Name = @ProductName
END
GO