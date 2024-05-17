-- =============================================
-- Description:	<Create New Offer>
-- =============================================

CREATE PROCEDURE [dbo].[CreateOffer]
	@Label	NVARCHAR(Max),
	@TicketNumber	INT,
	@Price			DECIMAL(18, 0)
AS
  BEGIN TRY
	BEGIN TRAN CreateOffer

		IF NOT EXISTS 
		(
		  SELECT 
			O.[Id] 
		  FROM [Offer] O
		  WHERE O.[Label] = @Label
		)
		  INSERT INTO [Offer] ([Label], [Price], [TicketNumber])
		  VALUES (@Label, @Price, @TicketNumber)

	COMMIT TRAN CreateOffer
  END TRY
  BEGIN CATCH
	ROLLBACK TRAN CreateOffer
  END CATCH