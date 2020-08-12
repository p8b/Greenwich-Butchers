-- =================================
-- Test "RemoveMailingList" Procedure
-- =================================

USE [GreenwichButchers]
-- Get Mailing list before it is removed
EXEC GreenwichButchers.dbo.ViewMailingList

-- Record is removed
EXEC GreenwichButchers.dbo.RemoveMailingList
@Email = 'test@test.test'

-- Get Mailing List after record is removed
EXEC GreenwichButchers.dbo.ViewMailingList