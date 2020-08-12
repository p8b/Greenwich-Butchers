-- ================================================
-- View Product Categories 
-- @CategoryName => if null is passed as paramerter
-- then return all product categories 
-- A value can be passed to check if that category
-- exists in the database
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE ViewProductCategories(
	@CategoryName varchar(80)=null)
AS
BEGIN
	IF (@CategoryName != '')
		SELECT * FROM tblProductCategories 
		WHERE ProductCategory = @CategoryName
	ELSE 
		SELECT * FROM tblProductCategories
END
GO