-- ================================================
-- Login Check
-- Returns 
-- Status => true or false
-- ID => Emloyee or Customer
-- PersonType => Staff, Manager or Customer
-- ================================================
CREATE OR ALTER PROCEDURE LoginCheck(
		@Email nchar(200), @Password varchar(MAX))
AS
BEGIN
	-- Declear Local variables
	DECLARE @RecPass nvarchar(32)
	DECLARE @PersonID int, @ID int, @PersonType nchar(50), @LCheck bit
	
	-- Select the password and person ID of the record where
	-- the email addresses match 
	SELECT @RecPass = person_Password , @PersonID = Person_Id
	FROM tblPersons
	WHERE person_Email = @Email

	-- Check if the person is customer, staff or manager
	IF EXISTS (SELECT * FROM tblCustomers WHERE Person_Id = @PersonID)
		-- If the person is Customer
		BEGIN
			SET @PersonType = 'Customer'
			SELECT @ID = Customer_Id 
			FROM tblCustomers WHERE  Person_Id = @PersonID
		END
	ELSE IF EXISTS (SELECT * FROM tblEmployee WHERE Person_Id = @PersonID)
		-- If the person is Employee
		-- The @PersonType value will be set the the position name of the Employee
		BEGIN
			SELECT @ID = Employee_Id, @PersonType = Position_Name 
			FROM tblEmployee WHERE Person_Id = @PersonID
		END	
	ELSE
		BEGIN
			SELECT 'Status' = 0, 'ID' = 0 , 'PersonType' = 'Wrong Email'
			RETURN
		END

	-- Use if satement to check if the hashed passwords are the same
	IF (HASHBYTES('SHA2_512', @Password) = @RecPass)
		BEGIN
			SET @LCheck = 1
			SELECT 'Status' = @LCheck, 'ID' = @ID , 'PersonType' = @PersonType
		END
	ELSE -- Else password is wrong
		BEGIN
			SET @LCheck = 0
			SELECT 'Status' = @LCheck, 'ID' = 0 , 'PersonType' = 'Wrong Password'
		END
END
GO
