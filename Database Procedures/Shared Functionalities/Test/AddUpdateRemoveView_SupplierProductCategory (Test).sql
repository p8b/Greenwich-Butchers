-- ================================================
-- Test "View Supplier's Product Category" Procedure
-- Filter By Supplier, ProductCategory Or Off (View All)
-- ================================================
USE [GreenwichButchers]

-- Declare a local variable to hold the Supplier_Id received
-- from tblSuppliers
DECLARE @SupID Int
SELECT TOP(1) @SupID = Supplier_Id FROM tblSuppliers

-- View the link before adding a new one
EXEC ViewSupplierProductCategory
@SupplierId = @SupID

EXEC AddSupplierProductCategory
@SupplierId = @SupID,
@ProductCategory ='Lamb'

-- View the link before after adding
EXEC ViewSupplierProductCategory
@SupplierId = @SupID

-- Remove the new link
EXEC dbo.RemoveSupplierProductCategory
@SupplierId = @SupID,
@ProductCategory = 'Lamb'

-- View the link before after removing
EXEC ViewSupplierProductCategory
@SupplierId = @SupID