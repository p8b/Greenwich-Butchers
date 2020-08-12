-- ================================================
-- View Order Quote record
-- @FilterBy ItemID , QuoteID
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE ViewOrderQuote(
		@FilterBy varchar(10),
		@IDValue Int )
AS
BEGIN
	IF (@FilterBy = 'ItemID')
	BEGIN
		SELECT * FROM tblOrderQuotes, tblSuppliers
		WHERE Item_Id = @IDValue
		AND tblOrderQuotes.Supplier_Id = tblSuppliers.Supplier_Id
	END
	ELSE IF (@FilterBy = 'QuoteID')
	BEGIN
		SELECT * FROM tblOrderQuotes, tblSuppliers
		WHERE Quote_Id = @IDValue
		AND tblOrderQuotes.Supplier_Id = tblSuppliers.Supplier_Id
	END
END
GO