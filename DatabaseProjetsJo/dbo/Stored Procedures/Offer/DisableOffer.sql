-- =============================================
-- Description:	<Disable Offer>
-- =============================================

CREATE PROCEDURE [dbo].[DisableOffer]
	@OfferId INT
AS
  BEGIN TRY
	BEGIN TRAN DisableOffer
		UPDATE Offer
		SET [State] = 0
		WHERE Id = @OfferId
	COMMIT TRAN DisableOffer
  END TRY
  BEGIN CATCH
	ROLLBACK TRAN DisableOffer
  END CATCH
