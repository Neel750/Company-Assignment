USE [Hotel Management]
GO

/****** Object:  Table [dbo].[Bookings]    Script Date: 1/12/2021 4:08:20 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Bookings](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BookingDate] [date] NOT NULL,
	[RoomId] [int] NOT NULL,
	[Status] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Bookings] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Bookings] ADD  DEFAULT ('Optional') FOR [Status]
GO

ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD  CONSTRAINT [FK_Bookings_Bookings] FOREIGN KEY([RoomId])
REFERENCES [dbo].[Rooms] ([Id])
GO

ALTER TABLE [dbo].[Bookings] CHECK CONSTRAINT [FK_Bookings_Bookings]
GO

ALTER TABLE [dbo].[Bookings]  WITH CHECK ADD CHECK  (([Status]='Deleted' OR [Status]='Cancelled' OR [Status]='Definitive' OR [Status]='Optional'))
GO

