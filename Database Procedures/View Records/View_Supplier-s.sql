-- ================================================
-- View Supplier records
-- Filter By SupplierID Or Off (View All suppliers)
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE ViewSupplier(
	@FilterValue Varchar(80),
	@FilterBy Varchar(20))
AS 
BEGIN
	IF (@FilterBy = 'Off') -- View All suppliers
	BEGIN
		SELECT * FROM tblSuppliers
	END
	ELSE IF (@FilterBy = 'SupplierID')
	BEGIN
		SELECT * FROM tblSuppliers
		WHERE tblSuppliers.Supplier_Id = @FilterValue
	END
END
GO