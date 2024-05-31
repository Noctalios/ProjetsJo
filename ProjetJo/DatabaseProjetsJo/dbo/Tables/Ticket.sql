CREATE TABLE [dbo].[Ticket] (
    [Id]      INT    IDENTITY NOT NULL,
    [Date]    DATETIME2(7)        NOT NULL,
    [QrCode]  NVARCHAR(MAX)     NOT NULL,
    [OfferId] INT             NOT NULL,
    [UserId]  INT             NOT NULL, 
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Ticket__Offer] FOREIGN KEY ([OfferId]) REFERENCES [dbo].[Offer] ([Id]),
    CONSTRAINT [FK_Ticketd__User]   FOREIGN KEY ([UserId])  REFERENCES [dbo].[User]  ([Id])
);

GO
CREATE NONCLUSTERED INDEX [IX_Ticket_OfferId]
    ON [dbo].[Ticket]([Id] ASC)
    INCLUDE([OfferId]);

