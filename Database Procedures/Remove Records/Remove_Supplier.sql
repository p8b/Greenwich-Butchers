-- ================================================
-- Remove Supplier records
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE RemoveSupplier(
	@SupplierID Int)
AS
BEGIN
	DELETE FROM tblSuppliers 
	WHERE Supplier_Id = @SupplierID
END
GO