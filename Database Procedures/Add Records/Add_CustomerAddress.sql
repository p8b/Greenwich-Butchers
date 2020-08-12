-- ================================================
-- Add Customer Address record
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE AddCustomerAddress(
		@CustomerID Int, @AddressName varchar(100),
		@FirstLineAdd varchar(200), @SecondLineAdd varchar(200)=null,
		@City varchar(100),	@PostCode varchar(8))
AS
BEGIN
	IF EXISTS(SELECT * FROM tblCustomers 
		WHERE Customer_Id = @CustomerID)
	BEGIN
		INSERT INTO tblAddresses(
			Customer_Id, address_Name,
			address_1stLine, address_2ndLine,
			address_City, address_PostCode)
		VALUES(
			@CustomerID, @AddressName,
			@FirstLineAdd, @SecondLineAdd,
			@City, @PostCode)
	END
	ELSE
	BEGIN
		RAISERROR('No Customer Found!',18,0)
	END
END
GO