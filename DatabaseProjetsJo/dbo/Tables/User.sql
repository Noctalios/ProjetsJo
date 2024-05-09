﻿CREATE TABLE [dbo].[User]
(
	[Id]		INT			  NOT NULL IDENTITY (1 ,1),
	[Firstname] NVARCHAR(100) NOT NULL,
	[LastName]	NVARCHAR(100) NOT NULL,
	[Mail]		NVARCHAR(MAX) NOT NULL,
	[Password]	NVARCHAR(100) NOT NULL,
	[Key]		NVARCHAR(MAX) NOT NULL,
	[IsAdmin]	BIT			  NOT NULL DEFAULT(0),	
	PRIMARY KEY CLUSTERED ([Id]),
);

