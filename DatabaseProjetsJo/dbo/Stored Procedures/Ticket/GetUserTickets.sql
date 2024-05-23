-- =============================================
-- Description:	<Get Offers>
-- =============================================

CREATE PROCEDURE [dbo].[GetUserTickets]
	@accountKey NVARCHAR(MAX)
AS
	BEGIN TRY
		BEGIN TRAN GetUserTickets

			SELECT
				T.[Id],
				T.[QrCode],
				T.[Date]
			FROM Ticket T
			LEFT JOIN dbo.[User] U ON U.Id = T.UserId 
			WHERE U.AccountKey = @accountKey
	
		COMMIT TRAN GetUserTickets
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN GetUserTickets
	END CATCH