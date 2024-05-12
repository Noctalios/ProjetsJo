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
		  U.[Id],
		  U.[Firstname],
		  U.[LastName],
		  U.[Mail],
		  U.[AccountKey],
		  U.[IsAdmin],
		  T.[Id],
		  T.[Date],
		  T.[QrCode]
		FROM 
		  [User] U
		INNER JOIN Ticket T 
	      ON T.UserId = U.Id
		WHERE U.[Firstname] = @firstName 
		  AND U.[LastName] = @lastName 
		  AND U.[Password] = @password

	COMMIT TRAN GetUser
  END TRY
  BEGIN CATCH
	ROLLBACK TRAN GetUser
  END CATCH
