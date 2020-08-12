-- ================================================
-- Remove Customer Address record
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE RemoveCustomerAddress(
	@AddressID Int)
AS
BEGIN
	DELETE FROM tblAddresses 
	WHERE Address_Id = @AddressID
END
GO