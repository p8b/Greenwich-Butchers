-- ================================================
-- Encrypt Cookie Delete
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE DeleteEncryptCookie(
		@HashCookieID varchar(5000))
AS
BEGIN
	-- Delete record
	DELETE FROM tblCookieEncrypt 
	WHERE Hash_Id = @HashCookieID
END
GO