USE [FlightApplicationDB]
GO

/****** Object:  Table [dbo].[tblUser]    Script Date: 18-05-2022 19:22:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUser]') AND type in (N'U'))
DROP TABLE [dbo].[tblUser]
GO

/****** Object:  Table [dbo].[tblUser]    Script Date: 18-05-2022 19:22:32 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[tblUser](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [varchar](50) NULL,
	[Password] [varchar](1000) NULL,
	[EmailID] [varchar](50) NULL,
	[ContactNo] [varchar](50) NULL,
	[Gender] [varchar](50) NULL,
	[Age] [int] NULL,
	[RoleID] [int] NULL,
	[IsActive] [varchar](50) NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_tblUser] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tblUser] ADD  CONSTRAINT [DF_tblUser_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[tblUser] ADD  CONSTRAINT [DF_tblUser_ModifiedDate]  DEFAULT (getdate()) FOR [ModifiedDate]
GO


