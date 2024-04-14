CREATE TABLE [dbo].[Error]
(
	[Id]					INT	IDENTITY (1, 1) NOT NULL,
	[LogLevel]				NVARCHAR(100) NOT NULL,
	[ThreadId]				INT NOT NULL,  
	[EventId]				INT NOT NULL,
	[EventName]				NVARCHAR(1000) NOT NULL,
	[ExceptionMessage]		NVARCHAR(1000) NULL,  
	[ExceptionStackTrace]	NVARCHAR(MAX) NULL,  
	[ExceptionSource]		NVARCHAR(1000) NULL,
	[CreationDate]			DATETIME NOT NULL DEFAULT(GETDATE())

    CONSTRAINT [PK_ErrorId] PRIMARY KEY CLUSTERED ([Id] ASC)
)
