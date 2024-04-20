CREATE TABLE [dbo].[Test] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [Description]      NVARCHAR (MAX) NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)  DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Test] PRIMARY KEY CLUSTERED ([Id] ASC)
);



