-- =============================================
-- Description:	<Create Tickets for User>
-- =============================================

CREATE PROCEDURE [dbo].[CreateTicketsForUser]
	@Tickets TicketTableType readonly
AS
  BEGIN TRY
	BEGIN TRAN CreateTicketsForUser

		INSERT INTO Ticket (QrCode, [Date], OfferId, UserId)
		SELECT
			PT.QrCode,
			PT.[Date],
			PT.[OfferId],
			U.Id
		FROM @Tickets PT
		INNER JOIN dbo.[User] U ON U.AccountKey = PT.accountKey

	COMMIT TRAN CreateTicketsForUser
  END TRY
  BEGIN CATCH
	ROLLBACK TRAN CreateTicketsForUser
  END CATCH