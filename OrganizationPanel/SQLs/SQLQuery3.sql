USE [Organization]
CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[SecondName] [nvarchar](50) NOT NULL,
	[MiddleName] [nvarchar](50) NULL,
	[DateOfBirth] [date] NOT NULL,
	[PassportSeries] [nchar](4) NOT NULL,
	[PassportNumber] [nchar](6) NOT NULL,
	[Note] [nvarchar](1000) NULL,
	[OrganizationId] [int] NOT NULL,

	CONSTRAINT PK_EmployeeID PRIMARY KEY CLUSTERED (Id),
	CONSTRAINT FK_Employee_Organization FOREIGN KEY ([OrganizationId]) REFERENCES ORGANIZATION(Id) ON DELETE CASCADE,
 )
GO


