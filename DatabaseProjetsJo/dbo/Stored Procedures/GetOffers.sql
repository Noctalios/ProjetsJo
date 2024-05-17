-- =============================================
-- Description:	<Get Offers>
-- =============================================

CREATE PROCEDURE [dbo].[GetOffers]
AS
  BEGIN TRY
	BEGIN TRAN GetOffers

		SELECT
			O.[Id],
			O.[Label],
			O.[Price],
			O.[TicketNumber],
			O.[State],
			COUNT(T.[Id]) AS [Total]
		FROM Offer O
		LEFT JOIN (SELECT * FROM Ticket) T 
			ON T.OfferId = O.Id  
	
	COMMIT TRAN GetOffers
  END TRY
  BEGIN CATCH
	ROLLBACK TRAN GetOffers
  END CATCH
