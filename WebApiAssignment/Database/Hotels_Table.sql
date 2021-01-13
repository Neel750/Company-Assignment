USE [Hotel Management]
GO

/****** Object:  Table [dbo].[Hotels]    Script Date: 1/11/2021 11:48:03 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Hotels](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[City] [nvarchar](250) NOT NULL,
	[Pincode] [int] NOT NULL,
	[ContactNumber] [bigint] NOT NULL,
	[ContactPerson] [nvarchar](100) NOT NULL,
	[Website] [nvarchar](250) NULL,
	[Facebook] [nvarchar](250) NULL,
	[Twitter] [nvarchar](250) NULL,
	[IsActive] [smallint] NOT NULL,
	[CreatedDate] [date] NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[UpdatedDate] [date] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
 CONSTRAINT [PK_Hotel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

