USE [IBMDB]
GO

DECLARE	@return_value Int

EXEC	@return_value = [dbo].[sp_UpdateSalaryById]
		@Ec = NULL,
		@Sal = NULL

SELECT	@return_value as 'Return Value'

GO
