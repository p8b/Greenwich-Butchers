-- ================================================
-- View Customer Address record TEST
-- @FilterBy CustomerID or AddressID
-- ================================================
USE [GreenwichButchers]
GO
DECLARE @CusID int -- Select the id of the first customer
SELECT Top(1) @CusID = Customer_Id FROM tblCustomers

-- Show all the addresses for selected Customer
EXEC ViewCustomerAddress  
@FilterValue = @CusID, @FilterBy = 'CustomerID'

EXEC AddCustomerAddress -- Add new Address
@CustomerID = @CusID, @AddressName = 'NEW', @FirstLineAdd = 'NEW',
@SecondLineAdd = 'NEW', @City = 'NEW', @PostCode = 'NEW'

DECLARE @AddID int -- Select the new Address ID
SELECT @AddID = Address_Id FROM tblAddresses 
WHERE Customer_Id = @CusID AND address_Name = 'NEW'

-- Show all the addresses for selected Customer 
-- after new address is added before update
EXEC ViewCustomerAddress  
@FilterValue = @CusID, @FilterBy = 'CustomerID'

EXEC dbo.UpdateCustomerAddress -- Update Address
@AddressID = @AddID, @AddressName = 'TEST', @FirstLineAdd = 'TEST', 
@SecondLineAdd = 'TEST', @City = 'TEST', @PostCode = 'TEST'

-- Only select the updated address
EXEC ViewCustomerAddress  
@FilterValue = @AddID, @FilterBy = 'AddressID'

EXEC dbo.RemoveCustomerAddress -- Remove the new address
@AddressID = @AddID

-- Show all the addresses for selected Customer
EXEC ViewCustomerAddress  
@FilterValue = @CusID, @FilterBy = 'CustomerID'