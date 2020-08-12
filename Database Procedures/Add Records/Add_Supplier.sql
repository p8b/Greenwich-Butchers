-- ================================================
-- Add Supplier records
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE AddSupplier(
		@Company varchar(200)=null,
		@FullName varchar(250),
		@Tel varchar(15),
		@Email varchar(200)=null,
		@Description varchar(1000)=null)
AS
BEGIN
	INSERT INTO tblSuppliers(
		supplier_Company, supplier_FullName,
		supplier_Tel, supplier_Email,
		supplier_Description)
	VALUES (
		@Company, @FullName,
		@Tel, @Email,
		@Description)
	SELECT 'Supplier_Id' = SCOPE_IDENTITY()
END
GO