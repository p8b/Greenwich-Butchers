-- ================================================
-- Test "View Stock" Procedure
-- @Filterby "Product" or "Location" or "Off" (View All)
-- and pass the appropriate Value by using the
-- @Value parameter
-- ================================================
USE [GreenwichButchers]
EXEC dbo.ViewStock
@Value ='Shop', 
@FilterBy = 'Location'

EXEC dbo.ViewStock
@Value ='', 
@FilterBy = 'Off'