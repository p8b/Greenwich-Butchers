-- ================================================
-- Add or Update Payment record
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE AddUpdatePayment(
	@paymentTotal decimal(9,2),	@paymentSupplier decimal(9,2),
	@paymentProfit decimal(9,2), @paymentProfitMargin int,
	@paymentDate varchar(10), @OrderId int)
AS
BEGIN
	-- If order has not payment amount
	IF NOT EXISTS (SELECT * FROM tblPayment 
		WHERE Order_Id = @OrderId)
	BEGIN
		INSERT INTO tblPayment( -- Add new payment
				payment_Total, payment_Supplier ,
				payment_Profit,	payment_ProfitMargin,
				payment_Date, Order_Id)
		VALUES (
				@paymentTotal, @paymentSupplier,
				@paymentProfit,	@paymentProfitMargin,
				@paymentDate, @OrderId)
	END
	ELSE -- Update existing record
	BEGIN
		UPDATE tblPayment SET
			payment_Total = @paymentTotal,
			payment_Supplier = @paymentSupplier,
			payment_Profit = @paymentProfit,
			payment_ProfitMargin = @paymentProfitMargin,
			Payment_Date = @paymentDate
		WHERE Order_Id = @OrderId
	END
END
GO