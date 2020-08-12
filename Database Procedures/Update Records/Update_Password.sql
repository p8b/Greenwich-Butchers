-- ================================================
-- Update Customer password record
-- @ID value of CustomerID or EmployeeID
-- @Person type can be "Customer" or "Employee"
-- @OldPassword of the user
-- @New Password of the user 
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE UpdatePassword(
		@ID Int,
		@PersonType varchar(10),
		@OldPassword varchar(130),
		@NewPassword varchar(130))
AS
BEGIN
	-- Update passed if the person exists and the old password is correct
	IF (@PersonType = 'Customer' AND EXISTS(SELECT * FROM  tblPersons AS P 
			WHERE P.person_Password = HASHBYTES('SHA2_512', @OldPassword)))
	BEGIN
		UPDATE tblPersons SET 
			person_Password = HASHBYTES('SHA2_512', @NewPassword)
		WHERE Person_Id = (SELECT C.Person_Id 
			FROM tblCustomers AS C, tblPersons AS P 
			WHERE Customer_Id = @ID
			AND P.Person_Id = C.Person_Id
			AND P.person_Password = HASHBYTES('SHA2_512', @OldPassword))
	END
	ELSE IF (@PersonType = 'Employee' AND EXISTS(SELECT * FROM  tblPersons AS P 
			WHERE P.person_Password = HASHBYTES('SHA2_512', @OldPassword)))
	BEGIN
		UPDATE tblPersons SET
			person_Password = HASHBYTES('SHA2_512', @NewPassword)
		WHERE Person_Id = (SELECT E.Person_Id 
			FROM tblEmployee AS E, tblPersons AS P
			WHERE Employee_Id = @ID
			AND P.Person_Id = E.Person_Id
			AND P.person_Password = HASHBYTES('SHA2_512', @OldPassword))
	END
	ELSE -- Else the person is not found
	BEGIN
		RAISERROR('Person cannot be found',18,0)
	END
END
GO