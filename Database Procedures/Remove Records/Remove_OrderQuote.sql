-- ================================================
-- Remove Order Quote record
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE RemoveOrderQuote(
		@QuoteID Int)
AS
BEGIN
	DELETE FROM tblOrderQuotes
	WHERE Quote_Id = @QuoteID
END
GO