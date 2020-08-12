-- ================================================
-- View Customer record(s) 
-- Including information from tblPersons and tblCustomer
-- @ShowAll = 0 (Search By Customer ID)
-- @ShowAll = 1 (Show All CustomerS)
-- ================================================

GO
CREATE OR ALTER PROCEDURE ViewCustomer(
	@CustomerID int,
	@ShowAll bit)
AS
BEGIN
	IF (@ShowAll = 0) -- return specific customer
	BEGIN
		SELECT * FROM tblCustomers, tblPersons
		WHERE tblCustomers.Customer_Id = @CustomerID
		AND tblPersons.Person_Id = tblCustomers.Person_Id
	END
	ELSE IF (@ShowAll = 1) -- Return all customers
	BEGIN
		SELECT * FROM tblCustomers, tblPersons
		WHERE tblPersons.Person_Id = tblCustomers.Person_Id
	END
END
GO