-- ================================================
-- Test Product Category Proceduers
-- ================================================
USE [GreenwichButchers]
-- Check if "Test Cat" record exists before adding
EXEC dbo.ViewProductCategories
@CategoryName = 'Test Cat'

EXEC dbo.AddProductCategory -- Add "Test Cat Record"
@CategoryName= 'Test Cat',
@CatImagePath = '~/Images/Test Cat 1.jpg',
@TypeName = 'Condiment'

-- Check if "Test Cat" record exists after adding and before Update
EXEC dbo.ViewProductCategories
@CategoryName = 'Test Cat'

-- Update the "Test Cat" to "Test Only"
EXEC dbo.UpdateProductCategory
@FromCategoryName = 'Test Cat',
@CatImagePath = '~/Images/Bacon 1.jpg',
@ToCategoryName = 'Test Only'

-- Check if "Test Only" record exists after Update before removing
EXEC dbo.ViewProductCategories
@CategoryName = 'Test Only'

-- Remove the "Test Only" record
EXEC dbo.RemoveProductCategory
@CategoryName = 'Test Only'

-- View All Product Categories
EXEC dbo.ViewProductCategories
@CategoryName = ''