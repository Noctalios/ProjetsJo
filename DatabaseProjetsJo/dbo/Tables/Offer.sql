CREATE TABLE [dbo].[Offer] (
    [Id]           INT            NOT NULL  IDENTITY (1, 1),
    [Label]        NVARCHAR (MAX) NOT NULL,
    [TicketNumber] INT            NOT NULL,
    [Price]        DECIMAL (18)   NOT NULL,
    [State]        BIT            NOT NULL  DEFAULT ((1)),
    PRIMARY KEY CLUSTERED ([Id] ASC),
    INDEX [IX_Offer_Id] ([Id] ASC)
);

