-- =============================================
-- Description:	<GET User Info on connexion>
-- =============================================

CREATE PROCEDURE [dbo].[GetUserGuids]
AS
  BEGIN TRY
	BEGIN TRAN GetUserGuids

		SELECT U.[AccountKey]
		FROM [User] U

	COMMIT TRAN GetUserGuids
  END TRY
  BEGIN CATCH
	ROLLBACK TRAN GetUserGuids
  END CATCH
