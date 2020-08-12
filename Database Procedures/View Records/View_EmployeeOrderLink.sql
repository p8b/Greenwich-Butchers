-- ================================================
-- View Employee Order Link
-- @FilterBy EmployeeID or OrderID
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE ViewEmployeeOrderLink(
		@FilterValue Int,
		@FilterBy varchar(10))
AS
BEGIN
	IF(@FilterBy = 'EmployeeID')
	BEGIN
		SELECT L.Employee_Id, L.Order_Id, L.Initial_Employee,
				P.person_Title, P.person_Name, P.person_Surname
		FROM LinkEmployeeOrders as L, tblPersons as P, tblEmployee as E
		WHERE L.Employee_Id = @FilterValue
		AND  L.Employee_Id = E.Employee_Id
		AND E.Person_Id = P.Person_Id
	END
	ELSE IF(@FilterBy = 'OrderID')
	BEGIN
		SELECT L.Employee_Id, L.Order_Id, L.Initial_Employee,
				P.person_Title, P.person_Name, P.person_Surname
		FROM LinkEmployeeOrders as L, tblPersons as P, tblEmployee as E
		WHERE L.Order_Id = @FilterValue
		AND  L.Employee_Id = E.Employee_Id
		AND E.Person_Id = P.Person_Id
	END
END
GO