-- =================================
-- Test "AddMailingList" Procedure
-- =================================
USE [GreenwichButchers]
EXEC GreenwichButchers.dbo.AddMailingList
@Email = 'test@test.test'

SELECT * FROM tblMailingList
