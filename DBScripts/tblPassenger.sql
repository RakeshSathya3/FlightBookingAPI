USE [FlightApplicationDB]
GO

ALTER TABLE [dbo].[tblPassenger] DROP CONSTRAINT [DF_UserBookingDetails_ModifiedDate]
GO

ALTER TABLE [dbo].[tblPassenger] DROP CONSTRAINT [DF_UserBookingDetails_CreatedDate]
GO

ALTER TABLE [dbo].[tblPassenger] DROP CONSTRAINT [DF_UserBookingDetails_IsMealOpted]
GO

/****** Object:  Table [dbo].[tblPassenger]    Script Date: 19-05-2022 18:43:06 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPassenger]') AND type in (N'U'))
DROP TABLE [dbo].[tblPassenger]
GO

/****** Object:  Table [dbo].[tblPassenger]    Script Date: 19-05-2022 18:43:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblPassenger](
	[PNR] [int] NOT NULL,
	[PassengerID] [int] IDENTITY(1,1) NOT NULL,
	[PassengerName] [varchar](50) NULL,
	[PassengerAge] [int] NULL,
	[PassengerGender] [varchar](50) NULL,
	[PassengerSeatNo] [varchar](50) NULL,
	[IsMealOpted] [varchar](50) NULL,
	[MealType] [varchar](50) NULL,
	[Price] [decimal](18, 0) NULL,
	[StatusCode] [int] NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_tblPassenger] PRIMARY KEY CLUSTERED 
(
	[PassengerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblPassenger] ADD  CONSTRAINT [DF_UserBookingDetails_IsMealOpted]  DEFAULT ('N') FOR [IsMealOpted]
GO

ALTER TABLE [dbo].[tblPassenger] ADD  CONSTRAINT [DF_UserBookingDetails_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[tblPassenger] ADD  CONSTRAINT [DF_UserBookingDetails_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO


