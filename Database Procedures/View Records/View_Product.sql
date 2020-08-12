-- ================================================
-- View Products
-- @FilterBy ProductName, Category, Off(View All)
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE ViewProducts(
	@Value varchar(300)=null,
	@FilterBy varchar(30))
AS
BEGIN
	IF (@FilterBy = 'Off') -- View All Products
		SELECT * FROM tblProducts
	ELSE IF (@FilterBy = 'ProductName')
		SELECT * FROM tblProducts WHERE product_Name = @Value
	ELSE IF (@FilterBy = 'Category')
		SELECT * FROM tblProducts WHERE productCategory = @Value
	ELSE
		RAISERROR('ERR_Invalid Filter Value',0,0)
END
GO
	