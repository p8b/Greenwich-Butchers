-- ========================== --
-- Add Mailing list record
-- ========================== --
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE AddMailingList (@Email varchar(200))
AS
BEGIN
	INSERT INTO tblMailingList(
		MailingList_Email) 
	VALUES(
		@Email)
END
GO