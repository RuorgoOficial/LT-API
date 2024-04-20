CREATE TABLE [dbo].[Score] (
    [Id]               INT             IDENTITY (1, 1) NOT NULL,
    [Score]            DECIMAL (10, 2) NOT NULL,
    [Acronym]          NVARCHAR (3)    NOT NULL,
    [CreatedTimestamp] DATETIME2 (7)   DEFAULT (getdate()) NOT NULL,
    [UpdatedTimestamp] DATETIME2 (7)   DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Score] PRIMARY KEY CLUSTERED ([Id] ASC)
);


