USE [FlightApplicationDB]
GO

/****** Object:  Table [dbo].[tblBooking]    Script Date: 18-05-2022 19:24:22 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblBooking]') AND type in (N'U'))
DROP TABLE [dbo].[tblBooking]
GO

/****** Object:  Table [dbo].[tblBooking]    Script Date: 18-05-2022 19:24:22 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblBooking](
	[PNR] [int] IDENTITY(1000,1) NOT NULL,
	[UserID] [int] NULL,
	[FlightNo] [varchar](50) NULL,
	[NoOfPassengers] [int] NULL,
	[DepartureDateTime] [datetime] NULL,
	[IsOneWay] [varchar](50) NULL,
	[ArrivalDateTime] [datetime] NULL,
	[StatusCode] [int] NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_tblBooking] PRIMARY KEY CLUSTERED 
(
	[PNR] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tblBooking] ADD  CONSTRAINT [DF_BookingDetails_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[tblBooking] ADD  CONSTRAINT [DF_BookingDetails_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO


