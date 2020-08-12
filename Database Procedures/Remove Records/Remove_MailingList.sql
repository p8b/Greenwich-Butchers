-- ================================================
-- Remove Mailing List Record
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE RemoveMailingList(@Email varchar(200))
AS
BEGIN
	DELETE FROM tblMailingList 
	WHERE tblMailingList.MailingList_Email = @Email 
END
GO