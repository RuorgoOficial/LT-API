CREATE TABLE [dbo].[Test] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Description] NVARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_TestId] PRIMARY KEY CLUSTERED ([Id] ASC)
);

