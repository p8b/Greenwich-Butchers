-- ================================================
-- Test Encrypt Cookie
-- ================================================
USE [GreenwichButchers]
GO
-- Show all records with test value as StringBase64Value
SELECT * FROM tblCookieEncrypt
WHERE StringBase64Value = 'testVALUE'

-- Create Temp table to Add new record and insert the result in #Temp table
CREATE TABLE #TEMP (HashCookieID varchar(MAX) ,Base64Value varchar(MAX)) 
INSERT INTO #TEMP EXEC WriteReadEncryptCookie
@HashCookieID = '',
@NewBase64Value = 'STRING64VALUE'

DECLARE @HashCID varchar(MAX)
SELECT @HashCID = HashCookieID FROM #TEMP -- Get the id from Temp table
DROP TABLE #TEMP -- Remove Temp Table

EXEC ReadEncryptCookie -- Read Only
@HashCookieID = @HashCID

SELECT * FROM tblCookieEncrypt --  After Read
WHERE Hash_Id = @HashCID

-- Create Temp table
CREATE TABLE #TEMP1 (HashCookieID varchar(MAX) ,Base64Value varchar(MAX)) 
INSERT INTO #TEMP1 EXEC WriteReadEncryptCookie -- Just refresh hash id
@HashCookieID = @HashCID,
@NewBase64Value = ''

SELECT @HashCID = HashCookieID FROM #TEMP1 -- Get the new id from Temp table
DROP TABLE #TEMP1 -- Remove Temp Table

SELECT * FROM tblCookieEncrypt-- Get cookie After refresh
WHERE Hash_Id = @HashCID


-- Create Temp table
CREATE TABLE #TEMP2 (HashCookieID varchar(MAX) ,Base64Value varchar(MAX)) 
INSERT INTO #TEMP2 EXEC WriteReadEncryptCookie -- Refresh hash id and update value
@HashCookieID = @HashCID,
@NewBase64Value = 'UpdateValue'

SELECT @HashCID = HashCookieID FROM #TEMP2 -- Get the new id from Temp table
DROP TABLE #TEMP2 -- Remove Temp Table

SELECT * FROM tblCookieEncrypt -- After refresh and Update
WHERE Hash_Id = @HashCID

EXEC DeleteEncryptCookie -- Delete
@HashCookieID = @HashCID

-- Show all records with test value as StringBase64Value
-- After remove
SELECT * FROM tblCookieEncrypt
WHERE StringBase64Value = 'testVALUE' 
OR StringBase64Value = 'UpdateValue'