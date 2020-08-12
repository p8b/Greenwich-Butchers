-- ================================================
-- View Payment
-- @FilterBy OrderID ,Date or Off (View All)
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE ViewPayment(
		@FilterBy varchar(10), @OrderID Int = null,
		@FromDate varchar(10)= null, @ToDate varchar(10)= null)
AS
BEGIN
	IF (@FilterBy = 'OrderID')
	BEGIN
		SELECT * FROM tblPayment
		WHERE Order_Id = @OrderID
	END
	ELSE IF (@FilterBy = 'Date')
	BEGIN
		SELECT * FROM tblPayment
		WHERE Convert(DATE, payment_Date) >= @FromDate
		AND CONVERT(DATE, payment_date) <= @ToDate
	END
	ELSE IF (@FilterBy = 'Off') -- View All payments
	BEGIN
		SELECT * FROM tblPayment
	END
END
GO