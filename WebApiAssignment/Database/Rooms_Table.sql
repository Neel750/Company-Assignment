USE [Hotel Management]
GO

/****** Object:  Table [dbo].[Rooms]    Script Date: 1/12/2021 4:08:02 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Rooms](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HotelId] [int] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Category] [nvarchar](100) NOT NULL,
	[Price] [int] NOT NULL,
	[IsActive] [smallint] NOT NULL,
	[CreatedDate] [date] NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[UpdatedDate] [date] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
 CONSTRAINT [PK_Rooms] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Rooms]  WITH CHECK ADD  CONSTRAINT [FK_Rooms_Hotels] FOREIGN KEY([HotelId])
REFERENCES [dbo].[Hotels] ([Id])
GO

ALTER TABLE [dbo].[Rooms] CHECK CONSTRAINT [FK_Rooms_Hotels]
GO

ALTER TABLE [dbo].[Rooms]  WITH CHECK ADD CHECK  (([Category]='Categories 3' OR [Category]='Categories 2' OR [Category]='Categories 1'))
GO

