-- ================================================
-- Test "Add Stock Records" Procedure
-- ================================================
USE [GreenwichButchers]
-- Get the "Lamb Leg" stock records before adding
EXEC dbo.ViewStock
@Value ='Lamb Leg', 
@FilterBy = 'Product'

-- Adding stock record for "Lamb Leg" in "Shop" Location
EXEC dbo.AddUpdateStock
@ProductName ='Lamb Leg',
@Quantity = 100,
@StockLocation = 'Shop'

-- Get the "Lamb Leg" stock records after adding and
-- before updating
EXEC dbo.ViewStock
@Value ='Lamb Leg', 
@FilterBy = 'Product'

-- Updating stock record for "Lamb Leg" in "Shop" Location
EXEC dbo.AddUpdateStock
@ProductName ='Lamb Leg',
@Quantity = 555,
@StockLocation = 'Shop'

-- Get the "Lamb Leg" stock records after updating
EXEC dbo.ViewStock
@Value ='Lamb Leg', 
@FilterBy = 'Product'