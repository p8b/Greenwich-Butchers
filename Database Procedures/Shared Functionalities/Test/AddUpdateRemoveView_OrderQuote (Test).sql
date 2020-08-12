-- ================================================
-- Test Order Quote procedures
-- ================================================
USE [GreenwichButchers]
DECLARE @SupID int, @ItmID int, @QutID int
-- Select an item which has no order quote
SELECT TOP(1) @ItmID = I.Item_Id 
FROM  tblOrderItems AS I LEFT JOIN tblOrderQuotes AS Q
ON I.Item_Id = Q.Item_Id
WHERE Q.Item_Id IS NULL

-- Get the a Supplier ID
SELECT TOP(1) @SupID = Supplier_Id FROM tblSuppliers

-- Check if item has any quotes before add
EXEC dbo.ViewOrderQuote 
@IDValue = @ItmID, @FilterBy = 'ItemID'

EXEC dbo.AddUpdateOrderQuote -- Add item quote
@ItemID = @ItmID, @SupplierID = @SupID,
@QuotePrice =  110.00, @DeliveryDate = '21/02/2017'

-- Check if item has any quotes after add
EXEC dbo.ViewOrderQuote 
@IDValue = @ItmID, @FilterBy = 'ItemID'

EXEC dbo.AddUpdateOrderQuote -- Add item Update
@ItemID = @ItmID, @SupplierID = @SupID,
@QuotePrice =  999.00, @DeliveryDate = '02/02/2099'

-- Get the QuoteID to be able to remove it
SELECT @QutID = Quote_Id FROM tblOrderQuotes 
WHERE Item_Id = @ItmID AND Supplier_Id = @SupID

-- Check if item qoute has any quotes after Update
EXEC dbo.ViewOrderQuote 
@IDValue = @QutID, @FilterBy = 'QuoteID'

EXEC dbo.RemoveOrderQuote -- Remove the quote
@QuoteID = @QutID

-- Check if item has any quotes after Update
EXEC dbo.ViewOrderQuote 
@IDValue = @ItmID, @FilterBy = 'ItemID'
