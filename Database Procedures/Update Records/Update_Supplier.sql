-- ================================================
-- Update Supplier records
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE UpdateSupplier(
		@SupplierID int,
		@Company varchar(200)=null,
		@FullName varchar(250),
		@Tel varchar(15),
		@Email varchar(200)=null,
		@Description varchar(1000)=null)
AS
BEGIN
	UPDATE  tblSuppliers SET
		supplier_Company = @Company,
		supplier_FullName = @FullName,
		supplier_Tel = @Tel,
		supplier_Email = @Email,
		supplier_Description = @Description
	WHERE Supplier_Id = @SupplierID
END
GO