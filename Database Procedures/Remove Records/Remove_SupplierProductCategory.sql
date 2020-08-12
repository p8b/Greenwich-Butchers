-- ================================================
-- Remove Supplier's Product Category record
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE RemoveSupplierProductCategory(
	@SupplierId Int,
	@ProductCategory varchar(80))
AS
BEGIN
	DELETE FROM tblSupplierProductCategories
	WHERE Supplier_Id = @SupplierId
	AND ProductCategory =@ProductCategory
END
GO