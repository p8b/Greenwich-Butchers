-- ================================================
-- Add Product Category Record
-- @CategoryName => name of the category
-- @CatImagePath => Server path of the category image
-- @TypeName => Type of category (Meat or Condiment) 
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE AddProductCategory(
	@CategoryName varchar(80), 
	@CatImagePath varchar(500),
	@TypeName varchar(80))
AS 
BEGIN
	INSERT INTO tblProductCategories(
		productCategory,
		ProductCategory_imgPath,
		ProductType) 
	VALUES(
		@CategoryName,
		@CatImagePath, 
		@TypeName)
END
GO
