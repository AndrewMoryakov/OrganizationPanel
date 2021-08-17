USE [Organization]
GO

CREATE TABLE [dbo].[Organization](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](64) NOT NULL,
	[INN] [char](12) NOT NULL,
	[LegalAddress] [nchar](1000) NOT NULL,
	[PhysicalAdress] [nchar](1000) NOT NULL,
	[Note] [nvarchar](1000) NULL,
 CONSTRAINT PK_OrganizationID PRIMARY KEY CLUSTERED (Id))
GO