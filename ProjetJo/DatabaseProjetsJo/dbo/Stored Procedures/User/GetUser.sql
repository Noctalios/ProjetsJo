-- =============================================
-- Description:	<GET User Info on connexion>
-- =============================================

CREATE PROCEDURE [dbo].[GetUser]
	@firstName NVARCHAR(100),
	@lastName  NVARCHAR(100),
	@password  NVARCHAR(100)
AS
  BEGIN TRY
	BEGIN TRAN GetUser

		SELECT
		  U.[Firstname],
		  U.[LastName],
		  U.[Mail],
		  U.[AccountKey],
		  U.[IsAdmin]
		FROM [User] U
		WHERE U.[Firstname] = @firstName 
		  AND U.[LastName] = @lastName 
		  AND U.[Password] = @password

	COMMIT TRAN GetUser
  END TRY
  BEGIN CATCH
	ROLLBACK TRAN GetUser
  END CATCH
