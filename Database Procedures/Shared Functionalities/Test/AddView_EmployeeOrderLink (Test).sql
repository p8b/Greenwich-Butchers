-- ================================================
-- Test Employee Order Link procedures
-- @FilterBy EmployeeID or OrderID
-- ================================================
USE [GreenwichButchers]

-- Get the first Order where there are no employee links
DECLARE @OID int
SELECT TOP(1) @OID = O.Order_Id 
FROM tblOrders AS O LEFT JOIN LinkEmployeeOrders AS L 
ON O.Order_Id = L.Order_Id 
WHERE L.Order_Id IS NULL

EXEC dbo.ViewEmployeeOrderLink -- Check the result before add 
@FilterValue = @OID,
@FilterBy = 'OrderID'

DECLARE @EmpID int -- Select the first employee id
SELECT TOP(1) @EmpID = Employee_Id FROM tblEmployee

EXEC dbo.AddEmployeeOrderLink -- Add the link
@EmployeeID = @EmpID,
@OrderID = @OID

EXEC dbo.ViewEmployeeOrderLink -- Check the result after add
@FilterValue = @OID,
@FilterBy = 'OrderID' -- filter by order ID

EXEC dbo.ViewEmployeeOrderLink -- Check the result 
@FilterValue = @EmpID,
@FilterBy = 'EmployeeID' -- filter by Employee ID