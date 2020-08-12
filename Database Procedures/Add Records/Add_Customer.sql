-- ================================================
-- Add Customer record
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE AddCustomer(
		@Title varchar(5), @Name varchar(100),
		@Surname varchar(100), @Tel varchar(15),
		@Email varchar(200), @Password varchar(130)=null,
		@Company varchar(200)=null, @AddressName varchar(100),
		@FirstLineAdd varchar(200),	@SecondLineAdd varchar(200)=null,
		@City varchar(100),	@PostCode varchar(8))
AS
BEGIN
	If EXISTS (SELECT * FROM tblPersons WHERE person_Email = @Email)
	BEGIN RAISERROR('Email Already Registered!',18,4) RETURN END

	-- Add Person Record 
	-- Password is hashed before writing to the databse
	INSERT INTO tblPersons(
		person_Title, person_Name, person_Surname,
		person_Tel,	person_Email, person_Password)
	VALUES (
		@Title,	@Name, @Surname,
		@Tel, @Email, HASHBYTES('SHA2_512', @Password))
	DECLARE @PersonID numeric(18,0)
	SET @PersonID = SCOPE_IDENTITY() -- Receive the Person ID

	-- Add Customer Record
	INSERT INTO tblCustomers(customer_CompanyName, Person_Id)
	VALUES(	@Company, @PersonID)
	DECLARE @CustomerID numeric(18,0)
	SET @CustomerID = SCOPE_IDENTITY() -- Receive the Cusromer ID

	-- Add Address Record by executing the "AddCustomerAddress"
	-- Procedure
	EXEC dbo.AddCustomerAddress
		@CustomerID,
		@AddressName,
		@FirstLineAdd,
		@SecondLineAdd,
		@City,
		@PostCode
	
	-- return the new Customer ID
	SELECT 'Customer_Id' = @CustomerID
END
GO