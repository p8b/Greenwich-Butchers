-- ================================================
-- Remove Order Item record
-- ================================================
USE [GreenwichButchers]
GO
CREATE OR ALTER PROCEDURE RemoveOrderItem(
		@ItemID Int)
AS
BEGIN
	DELETE FROM tblOrderItems 
	WHERE Item_Id = @ItemID
END
GO
