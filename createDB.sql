USE [master]
GO
/****** Object:  Database [CathedralKitchen]    Script Date: 8/7/2019 9:45:44 AM ******/
CREATE DATABASE [CathedralKitchen]
GO
ALTER DATABASE [CathedralKitchen] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CathedralKitchen].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CathedralKitchen] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CathedralKitchen] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CathedralKitchen] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CathedralKitchen] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CathedralKitchen] SET ARITHABORT OFF 
GO
ALTER DATABASE [CathedralKitchen] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CathedralKitchen] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CathedralKitchen] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CathedralKitchen] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CathedralKitchen] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CathedralKitchen] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CathedralKitchen] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CathedralKitchen] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CathedralKitchen] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CathedralKitchen] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CathedralKitchen] SET ALLOW_SNAPSHOT_ISOLATION ON 
GO
ALTER DATABASE [CathedralKitchen] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CathedralKitchen] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [CathedralKitchen] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CathedralKitchen] SET  MULTI_USER 
GO
ALTER DATABASE [CathedralKitchen] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CathedralKitchen] SET ENCRYPTION ON
GO
ALTER DATABASE [CathedralKitchen] SET QUERY_STORE = ON
GO
ALTER DATABASE [CathedralKitchen] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 100, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO)
GO
USE [CathedralKitchen]
GO
ALTER DATABASE SCOPED CONFIGURATION SET BATCH_MODE_ADAPTIVE_JOINS = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET BATCH_MODE_MEMORY_GRANT_FEEDBACK = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET BATCH_MODE_ON_ROWSTORE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET DEFERRED_COMPILATION_TV = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET ELEVATE_ONLINE = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET ELEVATE_RESUMABLE = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET GLOBAL_TEMPORARY_TABLE_AUTO_DROP = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET INTERLEAVED_EXECUTION_TVF = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET ISOLATE_SECURITY_POLICY_CARDINALITY = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LAST_QUERY_PLAN_STATS = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LIGHTWEIGHT_QUERY_PROFILING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET OPTIMIZE_FOR_AD_HOC_WORKLOADS = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET ROW_MODE_MEMORY_GRANT_FEEDBACK = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET VERBOSE_TRUNCATION_WARNINGS = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET XTP_PROCEDURE_EXECUTION_STATISTICS = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET XTP_QUERY_EXECUTION_STATISTICS = OFF;
GO
USE [CathedralKitchen]
GO
/****** Object:  User [Chase.B]    Script Date: 8/7/2019 9:45:45 AM ******/
CREATE USER [Chase.B] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [Chase.B]
GO
/****** Object:  Table [dbo].[Builder]    Script Date: 8/7/2019 9:45:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Builder](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[CreatedBy] [bigint] NULL,
	[UpdatedBy] [bigint] NULL,
 CONSTRAINT [PK_Builder] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BuilderCommunity]    Script Date: 8/7/2019 9:45:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BuilderCommunity](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[BuilderId] [bigint] NOT NULL,
	[CommunityId] [bigint] NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[UpdatedBy] [bigint] NOT NULL,
	[DeleteBy] [bigint] NULL,
	[DeleteTime] [bigint] NULL,
 CONSTRAINT [PK_BuilderCommunity] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[City]    Script Date: 8/7/2019 9:45:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[City](
	[Id] [bigint] NOT NULL,
	[Name] [nvarchar](80) NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[UpdatedBy] [bigint] NOT NULL,
	[DeleteBy] [bigint] NULL,
	[DeleteTime] [bigint] NULL,
 CONSTRAINT [PK_City] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Community]    Script Date: 8/7/2019 9:45:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Community](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[RegionId] [bigint] NULL,
	[Active] [bit] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[CreatedBy] [bigint] NOT NULL,
	[UpdatedBy] [bigint] NOT NULL,
	[DeleteBy] [bigint] NULL,
	[DeleteTime] [datetime] NULL,
 CONSTRAINT [PK_Community] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MenuItem]    Script Date: 8/7/2019 9:45:45 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuItem](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateBy] [bigint] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateBy] [bigint] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[DeleteBy] [bigint] NULL,
	[DeleteTime] [bigint] NULL,
 CONSTRAINT [PK_MenuItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 8/7/2019 9:45:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CommunityId] [bigint] NULL,
	[AddressLine1] [nvarchar](255) NULL,
	[AddressLine2] [nvarchar](255) NULL,
	[ZipCode] [nvarchar](12) NULL,
	[City] [nvarchar](70) NULL,
	[OrderStatusId] [bigint] NOT NULL,
	[CompleteTime] [datetime] NULL,
	[CustomerEmail] [nvarchar](100) NULL,
	[CustomerLastName] [nvarchar](75) NOT NULL,
	[CustomerFirstName] [nvarchar](75) NOT NULL,
	[Note] [nvarchar](500) NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateBy] [bigint] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[UpdateBy] [bigint] NOT NULL,
	[DeleteTime] [datetime] NULL,
	[DeleteBy] [bigint] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderCode]    Script Date: 8/7/2019 9:45:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderCode](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_OrderCode] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItem]    Script Date: 8/7/2019 9:45:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItem](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[MenuItemId] [bigint] NOT NULL,
	[OrderId] [bigint] NULL,
	[SizeId] [bigint] NULL,
	[Quantity] [int] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[UpdateBy] [bigint] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateBy] [bigint] NOT NULL,
	[DeleteTime] [datetime] NULL,
	[DeleteBy] [bigint] NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_OrderItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItemTopping]    Script Date: 8/7/2019 9:45:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItemTopping](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[OrderItemId] [bigint] NOT NULL,
	[ToppingId] [bigint] NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateBy] [bigint] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[UpdateBy] [bigint] NOT NULL,
	[DeleteTime] [datetime] NULL,
	[DeleteBy] [bigint] NULL,
 CONSTRAINT [PK_OrderItemTopping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderStatus]    Script Date: 8/7/2019 9:45:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderStatus](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Status] [nvarchar](70) NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateBy] [bigint] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[UpdateBy] [bigint] NOT NULL,
	[DeleteTime] [datetime] NULL,
	[DeleteBy] [bigint] NULL,
 CONSTRAINT [PK_OrderStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Person]    Script Date: 8/7/2019 9:45:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Person](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Home] [nvarchar](20) NULL,
	[Work] [nvarchar](20) NULL,
	[Cell] [nvarchar](20) NULL,
	[Email] [nvarchar](160) NULL,
	[SendEmail] [bit] NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateTime] [datetime] NULL,
	[CreateBy] [bigint] NULL,
	[UpdateTime] [datetime] NULL,
	[UpdateBy] [bigint] NULL,
	[DeleteTime] [datetime] NULL,
	[DeleteBy] [bigint] NULL,
 CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Region]    Script Date: 8/7/2019 9:45:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Region](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ParentId] [bigint] NULL,
	[RegionName] [nvarchar](300) NULL,
	[CreatedBy] [bigint] NOT NULL,
	[UpdatedBy] [bigint] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[DeleteTime] [datetime] NULL,
	[DeleteBy] [bigint] NULL,
 CONSTRAINT [PK_Region] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 8/7/2019 9:45:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](100) NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateBy] [bigint] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[UpdateBy] [bigint] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[DeleteBy] [bigint] NULL,
	[DeleteTime] [datetime] NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScheduleConfig]    Script Date: 8/7/2019 9:45:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScheduleConfig](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[CommunityId] [bigint] NULL,
	[Date] [datetime] NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateBy] [bigint] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[UpdateBy] [bigint] NOT NULL,
	[DeleteTime] [datetime] NULL,
	[DeleteBy] [bigint] NULL,
	[ParentId] [bigint] NULL,
 CONSTRAINT [PK_ScheduleConfig] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SystemReference]    Script Date: 8/7/2019 9:45:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemReference](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[MainValue] [nvarchar](100) NOT NULL,
	[AltValue] [nvarchar](100) NULL,
	[Active] [bit] NOT NULL,
	[CreateTime] [datetime] NULL,
	[CreateBy] [bigint] NULL,
	[UpdateTime] [datetime] NULL,
	[UpdateBy] [bigint] NULL,
	[DeleteTime] [datetime] NULL,
	[DeleteBy] [bigint] NULL,
 CONSTRAINT [PK_SystemReference] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Topping]    Script Date: 8/7/2019 9:45:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Topping](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ToppingName] [nvarchar](70) NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateBy] [bigint] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[UpdateBy] [bigint] NOT NULL,
	[DeleteTime] [datetime] NULL,
	[DeleteBy] [bigint] NULL,
 CONSTRAINT [PK_Topping] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ToppingSystemReference]    Script Date: 8/7/2019 9:45:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ToppingSystemReference](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ToppingId] [bigint] NOT NULL,
	[ToppingTypeId] [bigint] NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateBy] [bigint] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[UpdateBy] [bigint] NOT NULL,
	[DeleteTime] [datetime] NULL,
	[DeleteBy] [bigint] NULL,
 CONSTRAINT [PK_ToppingSystemReference] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 8/7/2019 9:45:46 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](255) NULL,
	[Hash] [nvarchar](255) NULL,
	[HasTempPassword] [bit] NOT NULL,
	[PersonId] [bigint] NOT NULL,
	[Active] [bit] NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[CreateBy] [bigint] NOT NULL,
	[UpdateTime] [datetime] NOT NULL,
	[UpdateBy] [bigint] NOT NULL,
	[DeleteTime] [datetime] NULL,
	[DeleteBy] [bigint] NULL,
	[BuilderId] [bigint] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Builder] ON 

INSERT [dbo].[Builder] ([Id], [Name], [Active], [CreateTime], [UpdateTime], [CreatedBy], [UpdatedBy]) VALUES (2, N'Beazer', 1, CAST(N'2019-03-01T03:24:56.750' AS DateTime), CAST(N'2019-03-01T03:24:56.750' AS DateTime), 1, 1)
INSERT [dbo].[Builder] ([Id], [Name], [Active], [CreateTime], [UpdateTime], [CreatedBy], [UpdatedBy]) VALUES (3, N'Lennar', 1, CAST(N'2019-03-01T03:24:56.750' AS DateTime), CAST(N'2019-03-01T03:24:56.750' AS DateTime), 1, 1)
SET IDENTITY_INSERT [dbo].[Builder] OFF
SET IDENTITY_INSERT [dbo].[BuilderCommunity] ON 

INSERT [dbo].[BuilderCommunity] ([Id], [BuilderId], [CommunityId], [Active], [CreateTime], [UpdateTime], [CreatedBy], [UpdatedBy], [DeleteBy], [DeleteTime]) VALUES (1, 2, 3, 1, CAST(N'2019-03-01T03:24:56.750' AS DateTime), CAST(N'2019-03-01T03:24:56.750' AS DateTime), 1, 1, NULL, NULL)
INSERT [dbo].[BuilderCommunity] ([Id], [BuilderId], [CommunityId], [Active], [CreateTime], [UpdateTime], [CreatedBy], [UpdatedBy], [DeleteBy], [DeleteTime]) VALUES (2, 2, 4, 1, CAST(N'2019-03-01T03:24:56.750' AS DateTime), CAST(N'2019-03-01T03:24:56.750' AS DateTime), 1, 1, NULL, NULL)
INSERT [dbo].[BuilderCommunity] ([Id], [BuilderId], [CommunityId], [Active], [CreateTime], [UpdateTime], [CreatedBy], [UpdatedBy], [DeleteBy], [DeleteTime]) VALUES (3, 3, 5, 1, CAST(N'2019-03-01T03:24:56.750' AS DateTime), CAST(N'2019-03-01T03:24:56.750' AS DateTime), 1, 1, NULL, NULL)
INSERT [dbo].[BuilderCommunity] ([Id], [BuilderId], [CommunityId], [Active], [CreateTime], [UpdateTime], [CreatedBy], [UpdatedBy], [DeleteBy], [DeleteTime]) VALUES (4, 3, 3, 1, CAST(N'2019-03-01T03:24:56.750' AS DateTime), CAST(N'2019-03-01T03:24:56.750' AS DateTime), 1, 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[BuilderCommunity] OFF
SET IDENTITY_INSERT [dbo].[Community] ON 

INSERT [dbo].[Community] ([Id], [Name], [RegionId], [Active], [CreateTime], [UpdateTime], [CreatedBy], [UpdatedBy], [DeleteBy], [DeleteTime]) VALUES (2, N'Cathedral', NULL, 0, CAST(N'2019-03-01T03:24:56.750' AS DateTime), CAST(N'2019-03-01T03:24:56.750' AS DateTime), 1, 1, NULL, NULL)
INSERT [dbo].[Community] ([Id], [Name], [RegionId], [Active], [CreateTime], [UpdateTime], [CreatedBy], [UpdatedBy], [DeleteBy], [DeleteTime]) VALUES (3, N'Willow Bend', NULL, 1, CAST(N'2019-03-01T03:24:56.750' AS DateTime), CAST(N'2019-03-01T03:24:56.750' AS DateTime), 1, 1, NULL, NULL)
INSERT [dbo].[Community] ([Id], [Name], [RegionId], [Active], [CreateTime], [UpdateTime], [CreatedBy], [UpdatedBy], [DeleteBy], [DeleteTime]) VALUES (4, N'Beaver', NULL, 1, CAST(N'2019-03-01T03:24:56.750' AS DateTime), CAST(N'2019-03-01T03:24:56.750' AS DateTime), 1, 1, NULL, NULL)
INSERT [dbo].[Community] ([Id], [Name], [RegionId], [Active], [CreateTime], [UpdateTime], [CreatedBy], [UpdatedBy], [DeleteBy], [DeleteTime]) VALUES (5, N'Allanis Crossing', NULL, 1, CAST(N'2019-03-01T03:24:56.750' AS DateTime), CAST(N'2019-03-01T03:24:56.750' AS DateTime), 1, 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Community] OFF
SET IDENTITY_INSERT [dbo].[MenuItem] ON 

INSERT [dbo].[MenuItem] ([Id], [Name], [Active], [CreateBy], [CreateTime], [UpdateBy], [UpdateTime], [DeleteBy], [DeleteTime]) VALUES (2, N'Taco - Beef', 1, 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), NULL, NULL)
INSERT [dbo].[MenuItem] ([Id], [Name], [Active], [CreateBy], [CreateTime], [UpdateBy], [UpdateTime], [DeleteBy], [DeleteTime]) VALUES (3, N'Pizza', 1, 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), NULL, NULL)
INSERT [dbo].[MenuItem] ([Id], [Name], [Active], [CreateBy], [CreateTime], [UpdateBy], [UpdateTime], [DeleteBy], [DeleteTime]) VALUES (4, N'Calzone', 0, 1, CAST(N'2019-03-10T20:39:42.870' AS DateTime), 1, CAST(N'2019-03-10T20:39:42.870' AS DateTime), NULL, NULL)
INSERT [dbo].[MenuItem] ([Id], [Name], [Active], [CreateBy], [CreateTime], [UpdateBy], [UpdateTime], [DeleteBy], [DeleteTime]) VALUES (5, N'Lasagna ', 0, 1, CAST(N'2019-03-11T14:04:05.363' AS DateTime), 1, CAST(N'2019-03-11T14:04:05.363' AS DateTime), NULL, NULL)
INSERT [dbo].[MenuItem] ([Id], [Name], [Active], [CreateBy], [CreateTime], [UpdateBy], [UpdateTime], [DeleteBy], [DeleteTime]) VALUES (6, N'Taco - Pork', 0, 1, CAST(N'2019-04-24T15:11:21.250' AS DateTime), 1, CAST(N'2019-04-24T15:11:21.250' AS DateTime), NULL, NULL)
INSERT [dbo].[MenuItem] ([Id], [Name], [Active], [CreateBy], [CreateTime], [UpdateBy], [UpdateTime], [DeleteBy], [DeleteTime]) VALUES (7, N'Quesadilla', 1, 1, CAST(N'2019-04-24T15:20:31.103' AS DateTime), 1, CAST(N'2019-04-24T15:20:31.103' AS DateTime), NULL, NULL)
INSERT [dbo].[MenuItem] ([Id], [Name], [Active], [CreateBy], [CreateTime], [UpdateBy], [UpdateTime], [DeleteBy], [DeleteTime]) VALUES (8, N'Philly Cheesesteak', 1, 1, CAST(N'2019-04-24T15:21:06.967' AS DateTime), 1, CAST(N'2019-04-24T15:21:06.967' AS DateTime), NULL, NULL)
INSERT [dbo].[MenuItem] ([Id], [Name], [Active], [CreateBy], [CreateTime], [UpdateBy], [UpdateTime], [DeleteBy], [DeleteTime]) VALUES (9, N'Hamburger', 0, 1, CAST(N'2019-04-24T15:33:15.260' AS DateTime), 1, CAST(N'2019-04-24T15:33:15.260' AS DateTime), NULL, NULL)
INSERT [dbo].[MenuItem] ([Id], [Name], [Active], [CreateBy], [CreateTime], [UpdateBy], [UpdateTime], [DeleteBy], [DeleteTime]) VALUES (10, N'Taco - Chicken', 1, 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), NULL, NULL)
INSERT [dbo].[MenuItem] ([Id], [Name], [Active], [CreateBy], [CreateTime], [UpdateBy], [UpdateTime], [DeleteBy], [DeleteTime]) VALUES (11, N'Lobster Tortellini', 0, 1, CAST(N'2019-05-24T18:42:17.563' AS DateTime), 1, CAST(N'2019-05-24T18:42:17.563' AS DateTime), NULL, NULL)
INSERT [dbo].[MenuItem] ([Id], [Name], [Active], [CreateBy], [CreateTime], [UpdateBy], [UpdateTime], [DeleteBy], [DeleteTime]) VALUES (12, N'Taco - Veggie', 1, 1, CAST(N'2019-05-29T15:12:30.857' AS DateTime), 1, CAST(N'2019-05-29T15:12:30.857' AS DateTime), NULL, NULL)
INSERT [dbo].[MenuItem] ([Id], [Name], [Active], [CreateBy], [CreateTime], [UpdateBy], [UpdateTime], [DeleteBy], [DeleteTime]) VALUES (13, N'Bowl', 1, 1, CAST(N'2019-05-31T14:25:44.420' AS DateTime), 1, CAST(N'2019-05-31T14:25:44.420' AS DateTime), NULL, NULL)
INSERT [dbo].[MenuItem] ([Id], [Name], [Active], [CreateBy], [CreateTime], [UpdateBy], [UpdateTime], [DeleteBy], [DeleteTime]) VALUES (14, N'Taco - Egg', 1, 1, CAST(N'2019-05-31T14:26:13.627' AS DateTime), 1, CAST(N'2019-05-31T14:26:13.627' AS DateTime), NULL, NULL)
INSERT [dbo].[MenuItem] ([Id], [Name], [Active], [CreateBy], [CreateTime], [UpdateBy], [UpdateTime], [DeleteBy], [DeleteTime]) VALUES (15, N'Tracy''s Special', 1, 1, CAST(N'2019-05-31T14:27:31.937' AS DateTime), 1, CAST(N'2019-05-31T14:27:31.937' AS DateTime), NULL, NULL)
INSERT [dbo].[MenuItem] ([Id], [Name], [Active], [CreateBy], [CreateTime], [UpdateBy], [UpdateTime], [DeleteBy], [DeleteTime]) VALUES (16, N'Refrigerator Menu #1', 0, 1, CAST(N'2019-06-06T16:47:24.923' AS DateTime), 1, CAST(N'2019-06-06T16:47:24.923' AS DateTime), NULL, NULL)
INSERT [dbo].[MenuItem] ([Id], [Name], [Active], [CreateBy], [CreateTime], [UpdateBy], [UpdateTime], [DeleteBy], [DeleteTime]) VALUES (17, N'Refrigerator Menu #2', 0, 1, CAST(N'2019-06-06T16:47:28.627' AS DateTime), 1, CAST(N'2019-06-06T16:47:28.627' AS DateTime), NULL, NULL)
INSERT [dbo].[MenuItem] ([Id], [Name], [Active], [CreateBy], [CreateTime], [UpdateBy], [UpdateTime], [DeleteBy], [DeleteTime]) VALUES (18, N'Refrigerator Menu #3', 0, 1, CAST(N'2019-06-06T16:47:34.077' AS DateTime), 1, CAST(N'2019-06-06T16:47:34.077' AS DateTime), NULL, NULL)
INSERT [dbo].[MenuItem] ([Id], [Name], [Active], [CreateBy], [CreateTime], [UpdateBy], [UpdateTime], [DeleteBy], [DeleteTime]) VALUES (19, N'Refrigerator Menu #4', 0, 1, CAST(N'2019-06-06T16:47:36.973' AS DateTime), 1, CAST(N'2019-06-06T16:47:36.973' AS DateTime), NULL, NULL)
INSERT [dbo].[MenuItem] ([Id], [Name], [Active], [CreateBy], [CreateTime], [UpdateBy], [UpdateTime], [DeleteBy], [DeleteTime]) VALUES (20, N'Refrigerator Menu #5', 0, 1, CAST(N'2019-06-06T16:47:39.580' AS DateTime), 1, CAST(N'2019-06-06T16:47:39.580' AS DateTime), NULL, NULL)
INSERT [dbo].[MenuItem] ([Id], [Name], [Active], [CreateBy], [CreateTime], [UpdateBy], [UpdateTime], [DeleteBy], [DeleteTime]) VALUES (21, N'Refrigerator Menu #6', 0, 1, CAST(N'2019-06-06T16:47:42.383' AS DateTime), 1, CAST(N'2019-06-06T16:47:42.383' AS DateTime), NULL, NULL)
SET IDENTITY_INSERT [dbo].[MenuItem] OFF
SET IDENTITY_INSERT [dbo].[OrderCode] ON 

INSERT [dbo].[OrderCode] ([Id], [Password], [Active]) VALUES (1, N'Test', 1)
SET IDENTITY_INSERT [dbo].[OrderCode] OFF
SET IDENTITY_INSERT [dbo].[OrderStatus] ON 

INSERT [dbo].[OrderStatus] ([Id], [Status], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (2, N'Pending', 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[OrderStatus] ([Id], [Status], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (3, N'Canceled', 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[OrderStatus] ([Id], [Status], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (4, N'Acknowledged', 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[OrderStatus] ([Id], [Status], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (5, N'Complete', 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[OrderStatus] ([Id], [Status], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (6, N'InProgress', 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[OrderStatus] OFF
SET IDENTITY_INSERT [dbo].[Person] ON 

INSERT [dbo].[Person] ([Id], [Home], [Work], [Cell], [Email], [SendEmail], [LastName], [FirstName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (1, NULL, NULL, NULL, N'chaserbullock@live.com', 0, N'Bullock', N'Chase', 1, CAST(N'2019-07-31T00:14:43.443' AS DateTime), 1, CAST(N'2019-07-31T00:14:43.443' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Person] ([Id], [Home], [Work], [Cell], [Email], [SendEmail], [LastName], [FirstName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (2, NULL, NULL, NULL, N'lksdfjsl@lkjhsdf.com', 0, N'ksdflksd', N'cjaslkfj', 1, CAST(N'2019-07-31T17:09:19.627' AS DateTime), 1, CAST(N'2019-07-31T17:09:19.627' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Person] ([Id], [Home], [Work], [Cell], [Email], [SendEmail], [LastName], [FirstName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (3, NULL, NULL, NULL, N'sdaflkda@dafdas.com', 0, N'lkasjdf', N'sadjkas', 1, CAST(N'2019-07-31T17:11:31.250' AS DateTime), 1, CAST(N'2019-07-31T17:11:31.250' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Person] ([Id], [Home], [Work], [Cell], [Email], [SendEmail], [LastName], [FirstName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (4, NULL, NULL, NULL, N'eric.m@cathedralplumbingtx.com', 0, N'Martin', N'Eric', 1, CAST(N'2019-08-01T14:28:59.040' AS DateTime), 1, CAST(N'2019-08-01T14:28:59.040' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Person] ([Id], [Home], [Work], [Cell], [Email], [SendEmail], [LastName], [FirstName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (5, NULL, NULL, NULL, N'Test@Test.com', 0, N'Test', N'Test', 1, CAST(N'2019-08-01T14:59:31.563' AS DateTime), 1, CAST(N'2019-08-01T14:59:31.563' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Person] ([Id], [Home], [Work], [Cell], [Email], [SendEmail], [LastName], [FirstName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (6, NULL, NULL, NULL, N'askldjasdlsa@dssa.com', 0, N'sdjaskd', N'Chase', 1, CAST(N'2019-08-02T18:17:07.387' AS DateTime), 1, CAST(N'2019-08-02T18:17:07.387' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Person] ([Id], [Home], [Work], [Cell], [Email], [SendEmail], [LastName], [FirstName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (7, NULL, NULL, NULL, N'chaserbullock@gmail.com', 0, N'Bullock', N'Chase', 1, CAST(N'2019-08-02T18:26:19.447' AS DateTime), 1, CAST(N'2019-08-02T18:26:19.447' AS DateTime), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Person] OFF
SET IDENTITY_INSERT [dbo].[SystemReference] ON 

INSERT [dbo].[SystemReference] ([Id], [Name], [MainValue], [AltValue], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (1, N'Pizza', N'Meat', N'Topping', 1, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[SystemReference] ([Id], [Name], [MainValue], [AltValue], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (2, N'Taco', N'Topping', N'Topping', 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[SystemReference] ([Id], [Name], [MainValue], [AltValue], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (3, N'Pizza', N'Other', N'Topping', 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[SystemReference] ([Id], [Name], [MainValue], [AltValue], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (4, N'Taco', N'Sauce', N'Topping', 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[SystemReference] ([Id], [Name], [MainValue], [AltValue], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (5, N'Pizza', N'Small', N'Topping', 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[SystemReference] ([Id], [Name], [MainValue], [AltValue], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (6, N'Pizza', N'Medium', N'Size', 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[SystemReference] ([Id], [Name], [MainValue], [AltValue], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (7, N'Pizza', N'Large', N'Size', 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[SystemReference] ([Id], [Name], [MainValue], [AltValue], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (8, N'Other', N'Other', N'Size', 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[SystemReference] ([Id], [Name], [MainValue], [AltValue], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (9, N'Other', N'Sauce', N'Topping', 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[SystemReference] ([Id], [Name], [MainValue], [AltValue], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10, N'All', N'Other', N'Topping', 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[SystemReference] ([Id], [Name], [MainValue], [AltValue], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (11, N'All', N'Other', N'Topping', 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[SystemReference] OFF
SET IDENTITY_INSERT [dbo].[Topping] ON 

INSERT [dbo].[Topping] ([Id], [ToppingName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (1, N'Pepperoni', 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Topping] ([Id], [ToppingName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (2, N'Extra Cheese', 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Topping] ([Id], [ToppingName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (3, N'Bacon', 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Topping] ([Id], [ToppingName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (4, N'Lettuce', 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Topping] ([Id], [ToppingName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (5, N'Salsa Verde', 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Topping] ([Id], [ToppingName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (6, N'Hot Sauce', 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Topping] ([Id], [ToppingName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10, N'Onion', 1, CAST(N'2019-03-02T18:59:27.510' AS DateTime), 1, CAST(N'2019-03-02T18:59:27.510' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Topping] ([Id], [ToppingName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10008, N'Canadian Bacon', 1, CAST(N'2019-03-10T20:40:10.763' AS DateTime), 1, CAST(N'2019-03-10T20:40:10.763' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Topping] ([Id], [ToppingName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10009, N'Mota', 0, CAST(N'2019-04-24T15:32:48.157' AS DateTime), 1, CAST(N'2019-04-24T15:32:48.157' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Topping] ([Id], [ToppingName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10011, N'Jalapeño', 1, CAST(N'2019-03-10T20:40:10.763' AS DateTime), 1, CAST(N'2019-03-10T20:40:10.763' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Topping] ([Id], [ToppingName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10012, N'Cilantro', 1, CAST(N'2019-05-23T14:19:11.743' AS DateTime), 1, CAST(N'2019-05-23T14:19:11.743' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Topping] ([Id], [ToppingName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10013, N'Mozzarella', 1, CAST(N'2019-05-23T15:54:46.223' AS DateTime), 1, CAST(N'2019-05-23T15:54:46.223' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Topping] ([Id], [ToppingName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10014, N'Feta', 1, CAST(N'2019-05-23T15:55:25.023' AS DateTime), 1, CAST(N'2019-05-23T15:55:25.023' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Topping] ([Id], [ToppingName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10015, N'Tomato', 1, CAST(N'2019-05-23T16:18:04.720' AS DateTime), 1, CAST(N'2019-05-23T16:18:04.720' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Topping] ([Id], [ToppingName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10016, N'Banana Peppers', 0, CAST(N'2019-05-24T18:43:26.980' AS DateTime), 1, CAST(N'2019-05-24T18:43:26.980' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Topping] ([Id], [ToppingName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10017, N'Green Peppers', 1, CAST(N'2019-05-29T15:13:31.937' AS DateTime), 1, CAST(N'2019-05-29T15:13:31.937' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Topping] ([Id], [ToppingName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10018, N'Eggs', 1, CAST(N'2019-05-29T15:13:57.547' AS DateTime), 1, CAST(N'2019-05-29T15:13:57.547' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Topping] ([Id], [ToppingName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10019, N'Brocolli', 1, CAST(N'2019-05-29T15:14:15.277' AS DateTime), 1, CAST(N'2019-05-29T15:14:15.277' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Topping] ([Id], [ToppingName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10020, N'Mushrooms', 1, CAST(N'2019-05-29T15:15:42.473' AS DateTime), 1, CAST(N'2019-05-29T15:15:42.473' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Topping] ([Id], [ToppingName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10021, N'Pineapple', 1, CAST(N'2019-05-29T16:51:15.520' AS DateTime), 1, CAST(N'2019-05-29T16:51:15.520' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Topping] ([Id], [ToppingName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10022, N'Black Olives', 1, CAST(N'2019-05-30T18:35:53.187' AS DateTime), 1, CAST(N'2019-05-30T18:35:53.187' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Topping] ([Id], [ToppingName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10023, N'Beef', 1, CAST(N'2019-05-31T14:27:07.580' AS DateTime), 1, CAST(N'2019-05-31T14:27:07.580' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Topping] ([Id], [ToppingName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10024, N'Chicken', 1, CAST(N'2019-05-31T14:27:13.173' AS DateTime), 1, CAST(N'2019-05-31T14:27:13.173' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Topping] ([Id], [ToppingName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10025, N'Pork', 1, CAST(N'2019-05-31T14:27:22.357' AS DateTime), 1, CAST(N'2019-05-31T14:27:22.357' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Topping] ([Id], [ToppingName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10026, N'Black Olives', 1, CAST(N'2019-06-06T17:34:35.490' AS DateTime), 1, CAST(N'2019-06-06T17:34:35.490' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Topping] ([Id], [ToppingName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10027, N'Jalapeños', 1, CAST(N'2019-06-06T17:36:14.370' AS DateTime), 1, CAST(N'2019-06-06T17:36:14.370' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Topping] ([Id], [ToppingName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10028, N'Sausage', 1, CAST(N'2019-06-13T15:52:01.977' AS DateTime), 1, CAST(N'2019-06-13T15:52:01.977' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Topping] ([Id], [ToppingName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10029, N'Olives', 1, CAST(N'2019-06-13T15:52:33.723' AS DateTime), 1, CAST(N'2019-06-13T15:52:33.723' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[Topping] ([Id], [ToppingName], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10030, N'Jalapeños', 1, CAST(N'2019-06-13T15:52:58.547' AS DateTime), 1, CAST(N'2019-06-13T15:52:58.547' AS DateTime), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Topping] OFF
SET IDENTITY_INSERT [dbo].[ToppingSystemReference] ON 

INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (1, 1, 1, 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (2, 2, 3, 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (3, 3, 1, 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (4, 4, 2, 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (5, 5, 4, 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (6, 6, 4, 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, CAST(N'2019-02-18T21:03:02.933' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (11, 10, 2, 1, CAST(N'2019-03-02T18:59:27.613' AS DateTime), 1, CAST(N'2019-03-02T18:59:27.613' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (12, 10, 3, 1, CAST(N'2019-03-02T18:59:27.627' AS DateTime), 1, CAST(N'2019-03-02T18:59:27.627' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10002, 10008, 1, 1, CAST(N'2019-03-10T20:40:10.783' AS DateTime), 1, CAST(N'2019-03-10T20:40:10.783' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10003, 10009, 2, 1, CAST(N'2019-04-24T15:32:48.177' AS DateTime), 1, CAST(N'2019-04-24T15:32:48.177' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10004, 10011, 2, 1, CAST(N'2019-03-10T20:40:10.763' AS DateTime), 1, CAST(N'2019-03-10T20:40:10.763' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10005, 10012, 2, 1, CAST(N'2019-05-23T14:19:11.790' AS DateTime), 1, CAST(N'2019-05-23T14:19:11.790' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10006, 10012, 3, 1, CAST(N'2019-05-23T14:19:11.790' AS DateTime), 1, CAST(N'2019-05-23T14:19:11.790' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10008, 10013, 2, 1, CAST(N'2019-05-23T15:54:46.240' AS DateTime), 1, CAST(N'2019-05-23T15:54:46.240' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10009, 10013, 3, 1, CAST(N'2019-05-23T15:54:46.243' AS DateTime), 1, CAST(N'2019-05-23T15:54:46.243' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10011, 10014, 2, 1, CAST(N'2019-05-23T15:55:25.030' AS DateTime), 1, CAST(N'2019-05-23T15:55:25.030' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10012, 10014, 3, 1, CAST(N'2019-05-23T15:55:25.030' AS DateTime), 1, CAST(N'2019-05-23T15:55:25.030' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10015, 10015, 2, 1, CAST(N'2019-05-23T16:18:04.723' AS DateTime), 1, CAST(N'2019-05-23T16:18:04.723' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10016, 10015, 3, 1, CAST(N'2019-05-23T16:18:04.723' AS DateTime), 1, CAST(N'2019-05-23T16:18:04.723' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10019, 10016, 2, 1, CAST(N'2019-05-24T18:43:27.003' AS DateTime), 1, CAST(N'2019-05-24T18:43:27.003' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10020, 10016, 3, 1, CAST(N'2019-05-24T18:43:27.007' AS DateTime), 1, CAST(N'2019-05-24T18:43:27.007' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10023, 10017, 2, 1, CAST(N'2019-05-29T15:13:31.957' AS DateTime), 1, CAST(N'2019-05-29T15:13:31.957' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10024, 10017, 3, 1, CAST(N'2019-05-29T15:13:31.960' AS DateTime), 1, CAST(N'2019-05-29T15:13:31.960' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10027, 10018, 2, 1, CAST(N'2019-05-29T15:13:57.550' AS DateTime), 1, CAST(N'2019-05-29T15:13:57.550' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10030, 10019, 2, 1, CAST(N'2019-05-29T15:14:15.283' AS DateTime), 1, CAST(N'2019-05-29T15:14:15.283' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10033, 10020, 2, 1, CAST(N'2019-05-29T15:15:42.480' AS DateTime), 1, CAST(N'2019-05-29T15:15:42.480' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10034, 10020, 3, 1, CAST(N'2019-05-29T15:15:42.480' AS DateTime), 1, CAST(N'2019-05-29T15:15:42.480' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10037, 10021, 2, 1, CAST(N'2019-05-29T16:51:15.523' AS DateTime), 1, CAST(N'2019-05-29T16:51:15.523' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10038, 10021, 3, 1, CAST(N'2019-05-29T16:51:15.527' AS DateTime), 1, CAST(N'2019-05-29T16:51:15.527' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10041, 10022, 3, 1, CAST(N'2019-05-30T18:35:53.203' AS DateTime), 1, CAST(N'2019-05-30T18:35:53.203' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10048, 10026, 3, 1, CAST(N'2019-06-06T17:34:35.510' AS DateTime), 1, CAST(N'2019-06-06T17:34:35.510' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10049, 10027, 3, 1, CAST(N'2019-06-06T17:36:14.377' AS DateTime), 1, CAST(N'2019-06-06T17:36:14.377' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10050, 10028, 1, 1, CAST(N'2019-06-13T15:52:02.003' AS DateTime), 1, CAST(N'2019-06-13T15:52:02.003' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10053, 10029, 3, 1, CAST(N'2019-06-13T15:52:33.730' AS DateTime), 1, CAST(N'2019-06-13T15:52:33.730' AS DateTime), 1, NULL, NULL)
INSERT [dbo].[ToppingSystemReference] ([Id], [ToppingId], [ToppingTypeId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy]) VALUES (10055, 10030, 3, 1, CAST(N'2019-06-13T15:52:58.553' AS DateTime), 1, CAST(N'2019-06-13T15:52:58.553' AS DateTime), 1, NULL, NULL)
SET IDENTITY_INSERT [dbo].[ToppingSystemReference] OFF
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([Id], [Email], [Hash], [HasTempPassword], [PersonId], [Active], [CreateTime], [CreateBy], [UpdateTime], [UpdateBy], [DeleteTime], [DeleteBy], [BuilderId]) VALUES (2, N'STAFF@CATHEDRAL.COM', N'AQAAAAEAACcQAAAAEJfRYQ7QCKaVCTubDXP1Yfxc3NUvNmh96XcVXvsqRfPpCiJzgH/5WfGgxEZE4o539w==', 0, 0, 1, CAST(N'2019-03-01T03:24:56.750' AS DateTime), 0, CAST(N'2019-03-01T03:24:56.750' AS DateTime), 0, NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[User] OFF
ALTER TABLE [dbo].[Builder] ADD  CONSTRAINT [DF_Builder_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[BuilderCommunity] ADD  CONSTRAINT [DF_BuilderCommunity_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[City] ADD  CONSTRAINT [DF_City_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Community] ADD  CONSTRAINT [DF_Community_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[MenuItem] ADD  CONSTRAINT [DF_MenuItem_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Order] ADD  CONSTRAINT [DF_Order_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[OrderCode] ADD  CONSTRAINT [DF_OrderCode_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[OrderItem] ADD  CONSTRAINT [DF_OrderItem_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[OrderItemTopping] ADD  CONSTRAINT [DF_OrderItemTopping_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[OrderStatus] ADD  CONSTRAINT [DF_OrderStatus_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Person] ADD  CONSTRAINT [DF_Person_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Role] ADD  CONSTRAINT [DF_Role_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[ScheduleConfig] ADD  CONSTRAINT [DF_ScheduleConfig_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[SystemReference] ADD  CONSTRAINT [DF_SystemReference_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Topping] ADD  CONSTRAINT [DF_Topping_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[ToppingSystemReference] ADD  CONSTRAINT [DF_ToppingSystemReference_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_HasTempPassword]  DEFAULT ((0)) FOR [HasTempPassword]
GO
ALTER TABLE [dbo].[User] ADD  CONSTRAINT [DF_User_Active]  DEFAULT ((1)) FOR [Active]
GO
ALTER TABLE [dbo].[Community]  WITH CHECK ADD  CONSTRAINT [FK_Community_Region] FOREIGN KEY([RegionId])
REFERENCES [dbo].[Region] ([Id])
GO
ALTER TABLE [dbo].[Community] CHECK CONSTRAINT [FK_Community_Region]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Community] FOREIGN KEY([CommunityId])
REFERENCES [dbo].[Community] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Community]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_OrderStatus] FOREIGN KEY([OrderStatusId])
REFERENCES [dbo].[OrderStatus] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_OrderStatus]
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_MenuItem] FOREIGN KEY([MenuItemId])
REFERENCES [dbo].[MenuItem] ([Id])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_MenuItem]
GO
ALTER TABLE [dbo].[OrderItem]  WITH CHECK ADD  CONSTRAINT [FK_OrderItem_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([Id])
GO
ALTER TABLE [dbo].[OrderItem] CHECK CONSTRAINT [FK_OrderItem_Order]
GO
ALTER TABLE [dbo].[OrderItemTopping]  WITH CHECK ADD  CONSTRAINT [FK_OrderItemTopping_OrderItem] FOREIGN KEY([OrderItemId])
REFERENCES [dbo].[OrderItem] ([Id])
GO
ALTER TABLE [dbo].[OrderItemTopping] CHECK CONSTRAINT [FK_OrderItemTopping_OrderItem]
GO
ALTER TABLE [dbo].[OrderItemTopping]  WITH CHECK ADD  CONSTRAINT [FK_OrderItemTopping_Topping] FOREIGN KEY([ToppingId])
REFERENCES [dbo].[Topping] ([Id])
GO
ALTER TABLE [dbo].[OrderItemTopping] CHECK CONSTRAINT [FK_OrderItemTopping_Topping]
GO
ALTER TABLE [dbo].[ScheduleConfig]  WITH CHECK ADD  CONSTRAINT [FK_ScheduleConfig_Community] FOREIGN KEY([CommunityId])
REFERENCES [dbo].[Community] ([Id])
GO
ALTER TABLE [dbo].[ScheduleConfig] CHECK CONSTRAINT [FK_ScheduleConfig_Community]
GO
ALTER TABLE [dbo].[ScheduleConfig]  WITH CHECK ADD  CONSTRAINT [FK_ScheduleConfig_ScheduleConfig] FOREIGN KEY([ParentId])
REFERENCES [dbo].[ScheduleConfig] ([Id])
GO
ALTER TABLE [dbo].[ScheduleConfig] CHECK CONSTRAINT [FK_ScheduleConfig_ScheduleConfig]
GO
ALTER TABLE [dbo].[ToppingSystemReference]  WITH CHECK ADD  CONSTRAINT [FK_ToppingSystemReference_SystemReference] FOREIGN KEY([ToppingTypeId])
REFERENCES [dbo].[SystemReference] ([Id])
GO
ALTER TABLE [dbo].[ToppingSystemReference] CHECK CONSTRAINT [FK_ToppingSystemReference_SystemReference]
GO
ALTER TABLE [dbo].[ToppingSystemReference]  WITH CHECK ADD  CONSTRAINT [FK_ToppingSystemReference_Topping] FOREIGN KEY([ToppingId])
REFERENCES [dbo].[Topping] ([Id])
GO
ALTER TABLE [dbo].[ToppingSystemReference] CHECK CONSTRAINT [FK_ToppingSystemReference_Topping]
GO
USE [master]
GO
ALTER DATABASE [CathedralKitchen] SET  READ_WRITE 
GO
