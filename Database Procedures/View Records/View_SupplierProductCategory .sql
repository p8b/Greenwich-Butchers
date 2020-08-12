-- ================================================
-- View Supplier's Product Category records
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE ViewSupplierProductCategory(
	@SupplierId Int)
AS
BEGIN
	SELECT * FROM tblSupplierProductCategories
	WHERE Supplier_Id = @SupplierId
END
GO