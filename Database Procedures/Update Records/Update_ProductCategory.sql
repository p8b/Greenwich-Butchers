-- ================================================
-- Update Product Category
-- @FromCategoryName => old name of the category
-- @ToCategoryName => New name of category 
-- @CatImagePath => Server path of the category image
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE UpdateProductCategory(
	@FromCategoryName varchar(80),
	@ToCategoryName varchar(80),
	@CatImagePath varchar(500))
AS
BEGIN 
	UPDATE tblProductCategories 
	SET productCategory = @ToCategoryName, 
		ProductCategory_imgPath = @CatImagePath
	WHERE productCategory = @FromCategoryName 
END
GO