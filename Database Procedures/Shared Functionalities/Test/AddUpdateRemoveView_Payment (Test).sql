-- ================================================
-- Test Payment Procedures
-- ================================================
USE [GreenwichButchers]
-- Get the Order ID where there is no payment
DECLARE @OrdID int 
SELECT @OrdID = O.Order_Id
FROM tblOrders AS O LEFT JOIN tblPayment AS P
ON O.Order_Id = P.Order_Id
WHERE P.Order_Id IS NULL

EXEC ViewPayment -- Check if payment exists before adding
@FilterBy = 'OrderID', @OrderID = @OrdID,
@FromDate = null, @ToDate = null

EXEC AddUpdatePayment -- Add new Payment
@OrderId = @OrdID,
@paymentTotal = 200.00,
@paymentSupplier = 180.00,
@paymentProfit = 20.00,
@paymentProfitMargin = 10,
@paymentDate = '11/01/2021'

EXEC ViewPayment -- Check if payment exists After adding
@FilterBy = 'OrderID', @OrderID = @OrdID,
@FromDate = null, @ToDate = null

EXEC AddUpdatePayment -- update Payment
@OrderId = @OrdID,
@paymentTotal = 555.00,
@paymentSupplier = 444.00,
@paymentProfit = 111.00,
@paymentProfitMargin = 20,
@paymentDate = '11/01/2021'

EXEC ViewPayment -- Check if payment exists After update
@FilterBy = 'OrderID', @OrderID = @OrdID,
@FromDate = null, @ToDate = null

EXEC RemovePayment
@OrderID = @OrdID

EXEC ViewPayment -- Check if payment exists After update
@FilterBy = 'OrderID', @OrderID = @OrdID,
@FromDate = null, @ToDate = null