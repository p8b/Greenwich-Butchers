-- ================================================
-- Remove Product Category Record
-- @CategoryName => Category Name
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE RemoveProductCategory(
	@CategoryName varchar(80))
AS
BEGIN
	DELETE FROM tblProductCategories 
	WHERE tblProductCategories.productCategory = @CategoryName
END
GO
