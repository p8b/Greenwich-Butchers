-- ================================================
-- Test "Login Check" procedure
-- Returns True if login is successful
-- ================================================
USE [GreenwichButchers]

EXEC dbo.LoginCheck -- Login Check Staff
@Email ='Martin@gmail.com',
@Password = 'password'

EXEC dbo.LoginCheck -- Login Check Manager
@Email ='Majid@Admin.com',
@Password = 'drowssap'

EXEC dbo.LoginCheck -- Login Check Customer
@Email ='asasd@Asd',
@Password = 'password'

EXEC dbo.LoginCheck -- Login Check Password Fail
@Email ='Majid@Admin.com',
@Password = 'd'

EXEC dbo.LoginCheck -- Login Check Email Fail
@Email ='Admin.com',
@Password = 'drowssap'

