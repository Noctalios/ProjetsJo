-- =============================================
-- Description:	<Create New User>
-- =============================================

CREATE PROCEDURE [dbo].[CreateUser]
	@firstName	NVARCHAR(100),
	@lastName	NVARCHAR(100),
	@email		NVARCHAR(MAX),
	@password	NVARCHAR(100),
	@accountKey NVARCHAR(MAX)
AS
  BEGIN TRY
	BEGIN TRAN CreateUser

		IF NOT EXISTS 
		(
		  SELECT 
			U.[Id] 
		  FROM [dbo].[User] U 
		  WHERE U.Firstname = @firstName AND U.LastName = @lastName AND U.[Password] = @password 
		)
		INSERT INTO [User] ([FirstName], [LastName], [Mail], [Password], [AccountKey])
		VALUES (@firstName, @lastName, @email, @password, @accountKey)


	COMMIT TRAN CreateUser
  END TRY
  BEGIN CATCH
	ROLLBACK TRAN CreateUser
  END CATCH