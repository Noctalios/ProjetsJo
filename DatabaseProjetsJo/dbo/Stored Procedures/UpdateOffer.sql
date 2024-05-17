-- =============================================
-- Description:	<Update Offer>
-- =============================================

CREATE PROCEDURE [dbo].[UpdateOffer]
	@OfferId		INT,
	@Label			NVARCHAR(MAX),
	@TicketNumber	INT
AS
  BEGIN TRY
	BEGIN TRAN UpdateOffer

		UPDATE Offer
		SET 
		  [Label] = @Label,
		  [TicketNumber] = @TicketNumber
		WHERE Id = @OfferId
	COMMIT TRAN UpdateOffer
  END TRY
  BEGIN CATCH
	ROLLBACK TRAN UpdateOffer
  END CATCH
