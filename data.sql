USE [airline_planner]
GO
/****** Object:  Table [dbo].[airport]    Script Date: 6/12/2017 8:24:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[airport](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[airport_flights]    Script Date: 6/12/2017 8:24:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[airport_flights](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[airport_id] [int] NULL,
	[flight_id] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[flights]    Script Date: 6/12/2017 8:24:15 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[flights](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[arrival_city] [varchar](255) NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[airport] ON 

INSERT [dbo].[airport] ([id], [name]) VALUES (1, N'Milwaukee')
INSERT [dbo].[airport] ([id], [name]) VALUES (2, N'Los Vegas')
INSERT [dbo].[airport] ([id], [name]) VALUES (3, N'Chicago')
INSERT [dbo].[airport] ([id], [name]) VALUES (4, N'Mexico')
INSERT [dbo].[airport] ([id], [name]) VALUES (5, N'LA')
INSERT [dbo].[airport] ([id], [name]) VALUES (6, N'Nyc')
SET IDENTITY_INSERT [dbo].[airport] OFF
SET IDENTITY_INSERT [dbo].[airport_flights] ON 

INSERT [dbo].[airport_flights] ([id], [airport_id], [flight_id]) VALUES (1, 1, 2)
INSERT [dbo].[airport_flights] ([id], [airport_id], [flight_id]) VALUES (2, 1, 1)
INSERT [dbo].[airport_flights] ([id], [airport_id], [flight_id]) VALUES (3, 5, 3)
INSERT [dbo].[airport_flights] ([id], [airport_id], [flight_id]) VALUES (4, 6, 3)
INSERT [dbo].[airport_flights] ([id], [airport_id], [flight_id]) VALUES (5, 6, 1)
SET IDENTITY_INSERT [dbo].[airport_flights] OFF
SET IDENTITY_INSERT [dbo].[flights] ON 

INSERT [dbo].[flights] ([id], [arrival_city]) VALUES (1, N'to Denver')
INSERT [dbo].[flights] ([id], [arrival_city]) VALUES (2, N'to st.paul')
INSERT [dbo].[flights] ([id], [arrival_city]) VALUES (3, N'to Chicago')
SET IDENTITY_INSERT [dbo].[flights] OFF
