-- ================================================
-- Test Customer Procedures
-- @CustomerID = 0 (Search By Customer ID)
-- @ShowAll = 1 (Show All CustomerS)
-- ================================================
USE [GreenwichButchers]
EXEC ViewCustomer -- Show all customers
@CustomerID = 0, 
@ShowAll = 1

CREATE TABLE #TEMP (Customer_Id int) -- Create Temp table
-- Add new Customer and insert the result in #Temp table
INSERT INTO #Temp EXEC dbo.AddCustomer -- Add new Customer
@Title ='Mr', @Name = 'Test123', @Surname = 'Test123',
@Tel ='335415353123', @Email = '123Test@test.test',
@Password = 'password', @Company =null,
@AddressName = 'Test123', @FirstLineAdd = 'Test123',
@SecondLineAdd =null, @City = 'Test123', @PostCode = 'Test123'

DECLARE @CusID int
SELECT @CusID = Customer_Id FROM #TEMP -- Get the id from Temp table
DROP TABLE #TEMP -- Remove Temp Table

EXEC ViewCustomer -- Show all customers after adding before update
@CustomerID = 0, 
@ShowAll = 1

EXEC dbo.UpdateCustomer -- update customer without password attribute
@CustomerID = @CusID, @Title = 'Miss', @Name = 'Emma',
@Surname = 'Jones', @Tel = '1116545641', 
@Email = 'emma@test.test', @Company ='TEST'

EXEC ViewCustomer -- Select only the new customer before removing
@CustomerID = @CusID, 
@ShowAll = 0

EXEC RemoveCustomer -- remove new customer
@CustomerID = @CusID

EXEC ViewCustomer -- Show all customers after remove
@CustomerID = 0, 
@ShowAll = 1