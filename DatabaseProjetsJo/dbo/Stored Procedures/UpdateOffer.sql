-- =============================================
-- Description:	<Update Offer>
-- =============================================

CREATE PROCEDURE [dbo].[UpdateOffer]
	@OfferId		INT,
	@label			NVARCHAR(MAX),
	@ticketNumber	INT
AS
  BEGIN TRY
	BEGIN TRAN UpdateOffer

		UPDATE Offer
		SET 
		  [Label] = @label,
		  [TicketNumber] = @TicketNumber
		WHERE Id = @OfferId
	COMMIT TRAN UpdateOffer
  END TRY
  BEGIN CATCH
	ROLLBACK TRAN UpdateOffer
  END CATCH
