CREATE TABLE [dbo].[ItemTransaction] (
    [Id]               INT              IDENTITY (1, 1) NOT NULL,
    [ItemId]           INT              NOT NULL,
    [TransactionGuid]  UNIQUEIDENTIFIER NOT NULL,
    [Quantity]         INT              NOT NULL,
    [EntityItemId]     INT              NULL,
    [CreatedTimestamp] DATETIME2 (7)    DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)    DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_ItemTransaction] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ItemTransaction_Item_EntityItemId] FOREIGN KEY ([EntityItemId]) REFERENCES [dbo].[Item] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_ItemTransaction_EntityItemId]
    ON [dbo].[ItemTransaction]([EntityItemId] ASC);

