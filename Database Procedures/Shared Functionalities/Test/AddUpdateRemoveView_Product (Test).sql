-- ================================================
-- Test Products Procedures
-- @FilterBy ProductName, Category, Off(View All)
-- ================================================
USE [GreenwichButchers]
-- Check if "Test Product" exists before adding
EXEC dbo.ViewProducts
@Value = 'Test Product',
@FilterBy = 'ProductName'

EXEC dbo.AddProduct -- Add the "Test Product"
	@ProductName = 'Test Product',
	@RetailUnit = 'Kg',
	@RetailPrice = 4.99,
	@Category = 'Lamb'

-- Check if "Test Product" exists after adding before update
EXEC dbo.ViewProducts
@Value = 'Test Product',
@FilterBy = 'ProductName'

EXEC dbo.UpdateProduct -- Update "Test Product"
@ProductName = 'Test Product',
@NewProductName = 'Test Only', 
@RetailUnit = 'Package',
@RetailPrice = 10.10,
@productCategory = 'Pork'

-- Check if "Test Only" exists after Update before Remove
EXEC dbo.ViewProducts
@Value = 'Test Only',
@FilterBy = 'ProductName'

EXEC dbo.RemoveProduct -- Remove "Test Only" record
@ProductName = 'Test Only'

-- Check if "Test Only" exists after Remove
EXEC dbo.ViewProducts
@Value = 'Test Only',
@FilterBy = 'ProductName'
