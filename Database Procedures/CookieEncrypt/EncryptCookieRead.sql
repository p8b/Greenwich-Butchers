-- ================================================
-- Encrypt CookieRead
-- Returns HashCookieID and Base64Value
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE ReadEncryptCookie(
		@HashCookieID varchar(max))
AS
BEGIN
	DECLARE @Base64Value varchar(MAX)
	-- Check if the there is a record with the @HashCookieID
	SELECT @Base64Value = StringBase64Value
	FROM tblCookieEncrypt 
	WHERE Hash_Id = @HashCookieID

	SELECT 'HashCookieID' = @HashCookieID, 'Base64Value' = @Base64Value
END
GO