USE [Product_Management]
GO

/****** Object:  Table [dbo].[ProductData]    Script Date: 1/11/2021 7:01:57 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ProductData](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nchar](50) NOT NULL,
	[UserId] [int] NOT NULL,
	[Category] [nchar](100) NOT NULL,
	[SmallDescription] [nchar](250) NOT NULL,
	[SmallImage] [nchar](250) NOT NULL,
	[LongDescription] [varchar](max) NOT NULL,
	[LongImage] [nchar](250) NULL,
	[Tags] [nvarchar](max) NULL,
	[Price] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[CreatedDate] [date] NULL,
	[UpdatedDate] [date] NULL,
 CONSTRAINT [PK_ProductData] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[ProductData]  WITH CHECK ADD  CONSTRAINT [FK_ProductData_UserData] FOREIGN KEY([UserId])
REFERENCES [dbo].[UserData] ([Id])
GO

ALTER TABLE [dbo].[ProductData] CHECK CONSTRAINT [FK_ProductData_UserData]
GO

