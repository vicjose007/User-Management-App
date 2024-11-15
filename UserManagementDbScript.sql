USE [master]
GO
/****** Object:  Database [UserManagementAppDb]    Script Date: 11/10/2024 10:43:46 AM ******/
CREATE DATABASE [UserManagementAppDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'UserManagementAppDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\UserManagementAppDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'UserManagementAppDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\UserManagementAppDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [UserManagementAppDb] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [UserManagementAppDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [UserManagementAppDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [UserManagementAppDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [UserManagementAppDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [UserManagementAppDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [UserManagementAppDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [UserManagementAppDb] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [UserManagementAppDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [UserManagementAppDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [UserManagementAppDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [UserManagementAppDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [UserManagementAppDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [UserManagementAppDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [UserManagementAppDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [UserManagementAppDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [UserManagementAppDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [UserManagementAppDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [UserManagementAppDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [UserManagementAppDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [UserManagementAppDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [UserManagementAppDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [UserManagementAppDb] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [UserManagementAppDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [UserManagementAppDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [UserManagementAppDb] SET  MULTI_USER 
GO
ALTER DATABASE [UserManagementAppDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [UserManagementAppDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [UserManagementAppDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [UserManagementAppDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [UserManagementAppDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [UserManagementAppDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [UserManagementAppDb] SET QUERY_STORE = ON
GO
ALTER DATABASE [UserManagementAppDb] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [UserManagementAppDb]
GO
/****** Object:  User [VicjoseMatos]    Script Date: 11/10/2024 10:43:47 AM ******/
CREATE USER [VicjoseMatos] FOR LOGIN [VicjoseMatos] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [VicjoseMatos]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 11/10/2024 10:43:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Phones]    Script Date: 11/10/2024 10:43:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phones](
	[Id] [nvarchar](450) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[Number] [nvarchar](max) NOT NULL,
	[CityCode] [nvarchar](max) NOT NULL,
	[ContryCode] [nvarchar](max) NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedToken] [nvarchar](max) NULL,
	[Updated] [datetimeoffset](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
 CONSTRAINT [PK_Phones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 11/10/2024 10:43:47 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[Role] [int] NOT NULL,
	[Created] [datetimeoffset](7) NOT NULL,
	[CreatedBy] [nvarchar](max) NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedToken] [nvarchar](max) NULL,
	[Updated] [datetimeoffset](7) NULL,
	[UpdatedBy] [nvarchar](max) NULL,
	[IsActive] [bit] NOT NULL,
	[LastLogin] [datetime2](7) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241110022805_InitialMigration', N'8.0.10')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241110041221_LastLoginAndIsActiveProperties', N'8.0.10')
GO
INSERT [dbo].[Phones] ([Id], [UserId], [Number], [CityCode], [ContryCode], [Created], [CreatedBy], [IsDeleted], [DeletedToken], [Updated], [UpdatedBy]) VALUES (N'285e52ef-cb4f-4b0f-8b94-47c2c0f354e7', N'da6347ca-e59b-4d14-92cf-ecc3f701e74f', N'8097765544', N'01', N'01', CAST(N'2024-11-10T14:14:30.9654616+00:00' AS DateTimeOffset), N'', 0, N'00000000-0000-0000-0000-000000000000', NULL, N'')
INSERT [dbo].[Phones] ([Id], [UserId], [Number], [CityCode], [ContryCode], [Created], [CreatedBy], [IsDeleted], [DeletedToken], [Updated], [UpdatedBy]) VALUES (N'8bc2bd85-d9c1-479a-b90e-8af07ab64a68', N'5750884a-edac-4908-8154-1738fa1b96cb', N'8097764432', N'01', N'01', CAST(N'2024-11-10T14:13:52.5044014+00:00' AS DateTimeOffset), N'', 0, N'00000000-0000-0000-0000-000000000000', NULL, N'')
INSERT [dbo].[Phones] ([Id], [UserId], [Number], [CityCode], [ContryCode], [Created], [CreatedBy], [IsDeleted], [DeletedToken], [Updated], [UpdatedBy]) VALUES (N'ae09215e-1722-449f-9418-adc7d10d53a9', N'b742d277-2e4d-4f55-bb59-5f34b2390267', N'8097746655', N'01', N'01', CAST(N'2024-11-10T14:22:13.0887262+00:00' AS DateTimeOffset), N'', 0, N'00000000-0000-0000-0000-000000000000', NULL, N'')
INSERT [dbo].[Phones] ([Id], [UserId], [Number], [CityCode], [ContryCode], [Created], [CreatedBy], [IsDeleted], [DeletedToken], [Updated], [UpdatedBy]) VALUES (N'bb6bd25d-c477-4a73-baa9-077f4d15d65e', N'b742d277-2e4d-4f55-bb59-5f34b2390267', N'8097745533', N'01', N'01', CAST(N'2024-11-10T04:16:58.6856771+00:00' AS DateTimeOffset), N'', 0, N'00000000-0000-0000-0000-000000000000', NULL, N'')
INSERT [dbo].[Phones] ([Id], [UserId], [Number], [CityCode], [ContryCode], [Created], [CreatedBy], [IsDeleted], [DeletedToken], [Updated], [UpdatedBy]) VALUES (N'dce5a759-7c58-49f4-9dfd-de8eafdbe7f7', N'8c29be67-fc55-4e39-b59c-d0f8debb320e', N'8097765544', N'01', N'01', CAST(N'2024-11-10T14:15:09.9987060+00:00' AS DateTimeOffset), N'', 0, N'00000000-0000-0000-0000-000000000000', NULL, N'')
INSERT [dbo].[Phones] ([Id], [UserId], [Number], [CityCode], [ContryCode], [Created], [CreatedBy], [IsDeleted], [DeletedToken], [Updated], [UpdatedBy]) VALUES (N'ff8ac3ed-27d1-4af9-8323-984fcad5a1a8', N'8c29be67-fc55-4e39-b59c-d0f8debb320e', N'8099990077', N'01', N'01', CAST(N'2024-11-10T14:21:37.2335016+00:00' AS DateTimeOffset), N'', 0, N'00000000-0000-0000-0000-000000000000', NULL, N'')
GO
INSERT [dbo].[Users] ([Id], [Name], [Email], [Password], [Role], [Created], [CreatedBy], [IsDeleted], [DeletedToken], [Updated], [UpdatedBy], [IsActive], [LastLogin]) VALUES (N'5750884a-edac-4908-8154-1738fa1b96cb', N'Jose Reyes', N'jose@gmail.com', N'Jose0101+', 0, CAST(N'2024-11-10T14:13:52.3877005+00:00' AS DateTimeOffset), N'', 0, N'00000000-0000-0000-0000-000000000000', NULL, N'', 0, CAST(N'2024-11-10T10:13:52.3872292' AS DateTime2))
INSERT [dbo].[Users] ([Id], [Name], [Email], [Password], [Role], [Created], [CreatedBy], [IsDeleted], [DeletedToken], [Updated], [UpdatedBy], [IsActive], [LastLogin]) VALUES (N'8c29be67-fc55-4e39-b59c-d0f8debb320e', N'User Admin', N'useradmin@gmail.com', N'Admin009008++', 1, CAST(N'2024-11-10T14:15:09.9984070+00:00' AS DateTimeOffset), N'', 0, N'00000000-0000-0000-0000-000000000000', NULL, N'', 0, CAST(N'2024-11-10T10:15:09.9984015' AS DateTime2))
INSERT [dbo].[Users] ([Id], [Name], [Email], [Password], [Role], [Created], [CreatedBy], [IsDeleted], [DeletedToken], [Updated], [UpdatedBy], [IsActive], [LastLogin]) VALUES (N'b742d277-2e4d-4f55-bb59-5f34b2390267', N'Victor Matos', N'vicjose007@gmail.com', N'VicKH1HZ', 1, CAST(N'2024-11-10T04:16:58.0207942+00:00' AS DateTimeOffset), N'', 0, N'00000000-0000-0000-0000-000000000000', CAST(N'2024-11-10T14:00:59.7235327+00:00' AS DateTimeOffset), N'', 1, CAST(N'2024-11-10T10:00:59.7229014' AS DateTime2))
INSERT [dbo].[Users] ([Id], [Name], [Email], [Password], [Role], [Created], [CreatedBy], [IsDeleted], [DeletedToken], [Updated], [UpdatedBy], [IsActive], [LastLogin]) VALUES (N'da6347ca-e59b-4d14-92cf-ecc3f701e74f', N'Nicole Esmeralda', N'nicole@gmail.com', N'Nicole0101+', 0, CAST(N'2024-11-10T14:14:30.9651125+00:00' AS DateTimeOffset), N'', 0, N'00000000-0000-0000-0000-000000000000', NULL, N'', 0, CAST(N'2024-11-10T10:14:30.9651008' AS DateTime2))
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_Phones_UserId]    Script Date: 11/10/2024 10:43:47 AM ******/
CREATE NONCLUSTERED INDEX [IX_Phones_UserId] ON [dbo].[Phones]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsActive]
GO
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD  CONSTRAINT [FK_Phones_Users_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[Users] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Phones] CHECK CONSTRAINT [FK_Phones_Users_UserId]
GO
USE [master]
GO
ALTER DATABASE [UserManagementAppDb] SET  READ_WRITE 
GO
