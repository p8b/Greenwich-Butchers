-- ================================================
-- View Customer Address record
-- @FilterBy CustomerID or AddressID
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE ViewCustomerAddress(
		@FilterValue Int, @FilterBy varchar(11))
AS
BEGIN
	IF (@FilterBy = 'CustomerID')
	BEGIN
		SELECT * From tblAddresses
		WHERE Customer_Id = @FilterValue
	END
	ELSE IF (@FilterBy = 'AddressID')
	BEGIN
		SELECT * From tblAddresses
		WHERE Address_Id = @FilterValue
	END
END
GO