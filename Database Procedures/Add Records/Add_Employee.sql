-- ================================================
-- Add Employee record
-- ================================================
USE [GreenwichButchers]
GO 
CREATE OR ALTER PROCEDURE AddEmployee(
		@Title nchar(5), @Name nchar(100),
		@Surname nchar(100), @Tel nchar(15),
		@Email nchar(200), @Password nchar(130),
		@PositionName nchar(50))
AS
BEGIN
	-- Create a local VarChar(Max) to hold the hash password value
	DECLARE @HashPassword nvarchar(MAX)
	-- Set the "HassPassword" variable to the value returned from
	-- "HASHBYTES" method by passing the name of the has algorithm
	-- and the "Password" value received which returns the a 
	-- "SHA2_512" Hashed value to be kept in database
	SET @HashPassword = HASHBYTES('SHA2_512', @Password)
	BEGIN TRY
		-- Add person details
		INSERT INTO tblPersons(
			person_Title, person_Name, person_Surname,
			person_Tel,	person_Email, person_Password)
		VALUES(
			@Title,	@Name, @Surname,
			@Tel, @Email, @HashPassword)
		DECLARE @PersonID Int
		SET @PersonID = SCOPE_IDENTITY()
		
		-- Add Employee details
		INSERT INTO tblEmployee(
			Position_Name, Person_Id)
		VALUES(
			@PositionName, @PersonID)
		-- return the new Employee ID
		SELECT 'Employee_Id' = SCOPE_IDENTITY()
	END TRY
	BEGIN CATCH -- use "try catch" to raise custom error
		IF( ERROR_NUMBER() = 2627 )
			RAISERROR ('Email address already registered!',18,0)
		ELSE IF (ERROR_NUMBER() = 547)
			BEGIN -- if invalid position is given as parameter
				-- then delete the new person record created
				DELETE FROM tblPersons WHERE Person_Id = @PersonID
				RAISERROR ('Invalid Position!',18,0)
			END
	END CATCH
END
GO