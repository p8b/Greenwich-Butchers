-- ================================================
-- Remove Customer record
-- This procedure will also delete the corresponding 
-- records in tblCustomer and tblAddresses
-- The records in tblOrders related to the customer,
-- will be kept without any customer information  
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE RemoveCustomer(@CustomerID Int)
AS
BEGIN
	-- Delete the Person record to delete all 
	-- the related records for customer
	DELETE FROM tblPersons 
	WHERE Person_Id = (
		SELECT Person_Id FROM tblCustomers
		WHERE Customer_Id = @CustomerID)
END
GO