-- ================================================
-- Add or Update Order Quote record
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE AddUpdateOrderQuote(
		@ItemID Int,
		@SupplierID Int,
		@QuotePrice decimal(8,2)=null,
		@DeliveryDate varchar(10)=null )
AS
BEGIN
	-- If the item has not quote from the supplier 
	IF NOT EXISTS(SELECT * FROM tblOrderQuotes
	WHERE Item_Id =@ItemID
	AND Supplier_Id = @SupplierID)
		BEGIN -- Add new quote
			INSERT INTO tblOrderQuotes(
				Item_Id,
				Supplier_Id,
				quote_Price,
				quote_DeliveryDate)
			VALUES (
				@ItemID,
				@SupplierID,
				@QuotePrice,
				@DeliveryDate)
		END
	-- Else Update the existing record
	ELSE 
		BEGIN
			UPDATE tblOrderQuotes SET
				quote_Price = @QuotePrice,
				quote_DeliveryDate = @DeliveryDate
			WHERE Item_Id = @ItemID
			AND Supplier_Id =@SupplierID
		END
END
GO