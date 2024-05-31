CREATE TYPE [dbo].[TicketTableType] AS TABLE (
    [QrCode]     VARCHAR (MAX)  NOT NULL,
    [Date]       DATETIME2(7)  NOT NULL,
    [OfferId]    INT            NOT NULL,
    [accountKey] NVARCHAR (MAX) NOT NULL);

