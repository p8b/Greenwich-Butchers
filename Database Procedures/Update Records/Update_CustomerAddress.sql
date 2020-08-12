-- ================================================
-- Update Customer Address record
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE UpdateCustomerAddress(
		@AddressID Int,	@AddressName varchar(100),
		@FirstLineAdd varchar(200),	@SecondLineAdd varchar(200)=null,
		@City varchar(100),	@PostCode varchar(8))
AS
BEGIN
	UPDATE tblAddresses SET
		address_Name = @AddressName,
		address_1stLine = @FirstLineAdd,
		address_2ndLine = @SecondLineAdd,
		address_City = @City,
		address_PostCode = @PostCode
	WHERE
		Address_Id = @AddressID
END
GO