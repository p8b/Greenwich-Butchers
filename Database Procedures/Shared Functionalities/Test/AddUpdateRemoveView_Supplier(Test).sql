-- ================================================
-- Test Supplier Procedures
-- Filter By SupplierID Or Off (View All suppliers)
-- ================================================
USE [GreenwichButchers]
-- Show All Suppliers
EXEC dbo.ViewSupplier
@FilterValue = '',
@FilterBy = 'Off'

DECLARE @SupID int

CREATE TABLE #TEMP (Supplier_Id int) -- Create Temp table
-- Add new supplier and insert the result in #Temp table
INSERT INTO #Temp EXEC AddSupplier
@Company ='Test Supplier',
@FullName ='Mr Test',
@Tel = '635464',
@Email = 'a@a.a',
@Description ='cfsdf'

SELECT @SupID = Supplier_Id FROM #TEMP -- Get the id from Temp table
DROP TABLE #TEMP -- Remove Temp Table

EXEC dbo.ViewSupplier -- Select the new supplier record before update
@FilterValue = @SupID,
@FilterBy = 'SupplierID'

EXEC UpdateSupplier -- Update the supplier
@SupplierID = @SupID,
@Company ='Test ONLY',
@FullName = 'TEST ONLY',
@Tel = '635464',
@Email = 'asd@asd.asd',
@Description = ''

EXEC dbo.ViewSupplier -- Select the supplier record after update
@FilterValue = @SupID,
@FilterBy = 'SupplierID'

EXEC RemoveSupplier
@SupplierID = @SupID

EXEC dbo.ViewSupplier -- Select the supplier record after delete
@FilterValue = @SupID,
@FilterBy = 'SupplierID'