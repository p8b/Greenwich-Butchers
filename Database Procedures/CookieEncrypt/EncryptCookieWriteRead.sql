-- ================================================
-- Encrypt Cookie Write and Read
-- @HashCookieID is used to identify the record which
--- will always refresh when this procedure is used.
-- @NewBase64Value has value when there is an intent
--- to change the cookie value
-- Returns the new HashCookieID and if NewBase64Value
--- is passed as parameter the it is returned otherwise
--- the @OldBase64Value is returned
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE WriteReadEncryptCookie(
		@HashCookieID varchar(max),	@NewBase64Value varchar(max))
AS
BEGIN
	DECLARE @RecID int = 0, @OldBase64Value varchar(max),@SaltedID varchar(Max)

	-- First get the record ID and the OldBase64Value if HashCookieID exists
	SELECT @RecID = Id, @OldBase64Value = StringBase64Value
	FROM tblCookieEncrypt 
	WHERE Hash_Id = @HashCookieID

	-- If the @RecID is more than 0 then a record is found
	-- and if @NewBase64Value is not empty
	IF (@RecID > 0 AND @NewBase64Value != '')
	BEGIN
		-- Delete the old record
		DELETE FROM tblCookieEncrypt WHERE Id = @RecID
		-- insert a new record with the @NewBase64Value
		INSERT INTO tblCookieEncrypt(StringBase64Value)
		VALUES (@NewBase64Value)
		-- Receive the id of the newly created ID
		SET @RecID = SCOPE_IDENTITY()
	END
	-- Else If the @RecID is found and no @NewBase64Value is given
	ELSE IF (@RecID > 0 AND @NewBase64Value = '')
	BEGIN
		-- Then set the @NewBase64Value to the @OldBase64Value
		-- the HashCookieID will be change at the end of the procedure
		SET @NewBase64Value = @OldBase64Value
	END
	-- else @RecID is invalid So if @NewBase64Value is not empty
	ELSE IF (@NewBase64Value != '')
	BEGIN
		-- Add the @NewBase64Value to the database 
		INSERT INTO tblCookieEncrypt(StringBase64Value)
		VALUES (@NewBase64Value)
		--  Receive the id of the newly created ID
		SET @RecID = SCOPE_IDENTITY()
	END
	-- ELSE @RecID is invalid and @NewBase64Value is not empty
	-- So do nothing so stop executing the code further
	ELSE BEGIN RETURN END
	
	-- if the code continues to here create a has id
	DECLARE @HashID varchar(MAX) = HASHBYTES('SHA2_512', 
		cast(@RecID as varchar(25)) + 
		cast(CONVERT(varchar(255), NEWID()) as varchar(255)))
	
	-- Update the record with the hash ID	
	UPDATE tblCookieEncrypt SET Hash_Id = @HashID WHERE Id = @RecID
	
	-- Return Both the HashID and the 
	SELECT 'HashCookieID' = @HashID, 'Base64Value' = @NewBase64Value
END
GO