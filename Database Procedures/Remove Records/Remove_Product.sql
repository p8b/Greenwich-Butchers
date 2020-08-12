-- ================================================
-- Remove Product Record
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE RemoveProduct(
	@ProductName varchar(300))
AS
BEGIN
	DELETE FROM tblProducts 
	WHERE Product_Name = @ProductName
END
GO