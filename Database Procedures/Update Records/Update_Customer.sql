-- ===============================================
-- Update Customer record
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE UpdateCustomer(
		@CustomerID Int, @Title varchar(5),
		@Name varchar(100), @Surname varchar(100),
		@Tel varchar(15), @Email varchar(200),
		@Company varchar(200)=null)
AS
BEGIN
	UPDATE tblPersons SET
		person_Title = @Title,
		person_Name = @Name,
		person_Surname = @Surname,
		person_Tel = @Tel,
		person_Email = @Email
	WHERE Person_Id = (
		SELECT Person_Id FROM tblCustomers 
		WHERE Customer_Id = @CustomerID)

	UPDATE tblCustomers SET
		customer_CompanyName = @Company
	WHERE Customer_Id = @CustomerID
END
GO