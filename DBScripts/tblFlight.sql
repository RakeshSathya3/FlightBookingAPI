USE [FlightApplicationDB]
GO

ALTER TABLE [dbo].[tblFlight] DROP CONSTRAINT [DF__tblFlight__Modif__52593CB8]
GO

ALTER TABLE [dbo].[tblFlight] DROP CONSTRAINT [DF__tblFlight__Creat__5165187F]
GO

ALTER TABLE [dbo].[tblFlight] DROP CONSTRAINT [DF__tblFlight__IsAct__5070F446]
GO

ALTER TABLE [dbo].[tblFlight] DROP CONSTRAINT [DF__tblFlight__MealO__4F7CD00D]
GO

/****** Object:  Table [dbo].[tblFlight]    Script Date: 19-05-2022 20:02:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblFlight]') AND type in (N'U'))
DROP TABLE [dbo].[tblFlight]
GO

/****** Object:  Table [dbo].[tblFlight]    Script Date: 19-05-2022 20:02:31 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblFlight](
	[FlightNo] [varchar](50) NOT NULL,
	[FlightName] [varchar](50) NULL,
	[FromLocation] [varchar](50) NULL,
	[ToLocation] [varchar](50) NULL,
	[DepartureDateTime] [datetime] NULL,
	[ArrivalDateTime] [datetime] NULL,
	[PriceBC] [decimal](18, 0) NULL,
	[NoOfBusinessClassSeats] [int] NULL,
	[PriceNonBC] [decimal](18, 0) NULL,
	[NoOfNonBusinessClassSeats] [int] NULL,
	[MealOption] [varchar](50) NULL,
	[IsActive] [varchar](50) NULL,
	[Remarks] [varchar](50) NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_tblFlight] PRIMARY KEY CLUSTERED 
(
	[FlightNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblFlight] ADD  DEFAULT ('N') FOR [MealOption]
GO

ALTER TABLE [dbo].[tblFlight] ADD  DEFAULT ('Y') FOR [IsActive]
GO

ALTER TABLE [dbo].[tblFlight] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO

ALTER TABLE [dbo].[tblFlight] ADD  DEFAULT (getdate()) FOR [ModifiedDate]
GO


