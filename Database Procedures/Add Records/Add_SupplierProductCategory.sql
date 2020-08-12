-- ================================================
-- Add Supplier's Product Category record
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE AddSupplierProductCategory(
	@SupplierId Int,
	@ProductCategory varchar(80))
AS
BEGIN
	INSERT INTO tblSupplierProductCategories(
		Supplier_Id,
		ProductCategory)
	VALUES (
		@SupplierId,
		@ProductCategory)
END
GO