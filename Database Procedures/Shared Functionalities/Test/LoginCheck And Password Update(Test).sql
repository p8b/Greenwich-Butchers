-- ================================================
-- Test "Login Check" procedure
-- Returns True if login is successful
-- ================================================
USE [GreenwichButchers]
-- Create Temp table
CREATE TABLE #TEMP ([Status] bit, ID int, PersonType varchar(50)) 
-- Add new user ID and insert the result in #Temp table
INSERT INTO #Temp EXEC dbo.LoginCheck
@Email ='Majid@Admin.com',
@Password = 'asd'

DECLARE @PID int

SELECT @PID = ID FROM #TEMP -- Get the id from Temp table
DROP TABLE #TEMP -- Remove Temp Table

EXEC UpdatePassword
@ID = @PID,
@PersonType = 'Customer',
@OldPassword = 'password',
@NewPassword = 'drowssap'

-- Password Login With old password (Must Fail)
EXEC dbo.LoginCheck 
@Email ='Majid@Admin.com',
@Password = 'password'

-- Password Login With new password (Must succeed)
EXEC dbo.LoginCheck 
@Email ='Majid@Admin.com',
@Password = 'drowssap'

