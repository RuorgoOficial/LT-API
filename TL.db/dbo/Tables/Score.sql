CREATE TABLE [dbo].[Score]
(
	[Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Score]       DECIMAL(10, 2) NOT NULL,
    [Acronym]     NVARCHAR(3) NOT NULL,
    CONSTRAINT [PK_Score] PRIMARY KEY CLUSTERED ([Id] ASC)
)
