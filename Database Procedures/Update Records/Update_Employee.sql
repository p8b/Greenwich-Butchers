-- ================================================
-- Update Employee record
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE UpdateEmployee(
		@EmployeeID Int, @Title varchar(5),
		@Name varchar(100),	@Surname varchar(100),
		@Tel varchar(15), @Email varchar(200),
		@PositionName varchar(50))
AS
BEGIN
	BEGIN TRY
		-- If the Person_Id exists
		IF EXISTS(SELECT Person_Id FROM tblEmployee
			WHERE Employee_Id = @EmployeeID)
		BEGIN
			-- Update person details
			UPDATE tblPersons SET
				person_Title = @Title, person_Name = @Name,
				person_Surname = @Surname, person_Tel = @Tel,
				person_Email = @Email
			WHERE Person_Id = (
				SELECT Person_Id FROM tblEmployee
				WHERE Employee_Id = @EmployeeID)
			
			-- Update Employee details
			UPDATE tblEmployee SET Position_Name = @PositionName
			WHERE Employee_Id = @EmployeeID
		END
	END TRY
	BEGIN CATCH -- use "try catch" to raise custom error
		IF( ERROR_NUMBER() = 2627 )
			RAISERROR ('Email address already registered!',0,0)
		ELSE IF (ERROR_NUMBER() = 547)
			BEGIN
				RAISERROR ('Invalid Position!',0,0)
			END
	END CATCH
END
GO