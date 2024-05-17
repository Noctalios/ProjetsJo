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
				T.Total
			FROM Offer O
			LEFT JOIN 
			(
				SELECT 
					T.OfferId,
					COUNT(T.OfferId) AS [Total]
				FROM Ticket T
				GROUP BY OfferId
			) AS T 
			ON O.Id = T.OfferId
	
		COMMIT TRAN GetOffers
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN GetOffers
	END CATCH
