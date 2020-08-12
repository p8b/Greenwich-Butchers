-- ================================================
-- View All Mailing List Records
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE ViewMailingList
AS
BEGIN
	SELECT * FROM tblMailingList
END
GO