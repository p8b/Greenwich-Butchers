-- ================================================
-- Test Products Procedures
-- @FilterBy ProductName, Category, Off(View All)
-- ================================================
USE [GreenwichButchers]
EXEC dbo.ViewProducts
@Value = 'Lamb',
@FilterBy = 'Category'

EXEC dbo.ViewProducts
@Value = '',
@FilterBy = 'Off'

