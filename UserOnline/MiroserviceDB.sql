USE [master]
GO
/****** Object:  Database [microservice]    Script Date: 11/17/2021 1:04:26 PM ******/
CREATE DATABASE [microservice]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'microservice', FILENAME = N'E:\SQLDatabase\microservice.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'microservice_log', FILENAME = N'E:\SQLDatabase\microservice_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [microservice] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [microservice].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [microservice] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [microservice] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [microservice] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [microservice] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [microservice] SET ARITHABORT OFF 
GO
ALTER DATABASE [microservice] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [microservice] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [microservice] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [microservice] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [microservice] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [microservice] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [microservice] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [microservice] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [microservice] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [microservice] SET  DISABLE_BROKER 
GO
ALTER DATABASE [microservice] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [microservice] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [microservice] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [microservice] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [microservice] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [microservice] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [microservice] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [microservice] SET RECOVERY FULL 
GO
ALTER DATABASE [microservice] SET  MULTI_USER 
GO
ALTER DATABASE [microservice] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [microservice] SET DB_CHAINING OFF 
GO
ALTER DATABASE [microservice] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [microservice] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [microservice] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'microservice', N'ON'
GO
ALTER DATABASE [microservice] SET QUERY_STORE = OFF
GO
USE [microservice]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [microservice]
GO
/****** Object:  Table [dbo].[Mic_log]    Script Date: 11/17/2021 1:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mic_log](
	[logID] [int] IDENTITY(1,1) NOT NULL,
	[logDate] [datetime] NULL,
	[logTitle] [nvarchar](30) NULL,
	[logContent] [nvarchar](1000) NULL,
 CONSTRAINT [PK_Mic_log] PRIMARY KEY CLUSTERED 
(
	[logID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MicPost_Post]    Script Date: 11/17/2021 1:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MicPost_Post](
	[PostId] [int] NOT NULL,
	[PostTitle] [nvarchar](30) NULL,
	[PostContent] [nvarchar](30) NULL,
	[PostUserID] [int] NULL,
 CONSTRAINT [PK_MicPost_Post] PRIMARY KEY CLUSTERED 
(
	[PostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MicPost_User]    Script Date: 11/17/2021 1:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MicPost_User](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](30) NULL,
 CONSTRAINT [PK_MicPost_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MicUser_User]    Script Date: 11/17/2021 1:04:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MicUser_User](
	[ID] [int] NOT NULL,
	[Name] [nvarchar](30) NULL,
	[Mail] [nvarchar](30) NULL,
	[OtherData] [nvarchar](30) NULL,
 CONSTRAINT [PK_UserService] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[MicPost_Post] ([PostId], [PostTitle], [PostContent], [PostUserID]) VALUES (1, N'post tile one', N'post content one', 1)
INSERT [dbo].[MicPost_Post] ([PostId], [PostTitle], [PostContent], [PostUserID]) VALUES (2, N'post title two', N'post content two', 2)
INSERT [dbo].[MicPost_User] ([ID], [Name]) VALUES (1, N'abbas')
INSERT [dbo].[MicPost_User] ([ID], [Name]) VALUES (2, N'ali')
INSERT [dbo].[MicUser_User] ([ID], [Name], [Mail], [OtherData]) VALUES (1, N'abbas', N'a@a.com', N'ooo')
INSERT [dbo].[MicUser_User] ([ID], [Name], [Mail], [OtherData]) VALUES (2, N'ali', N'b@b.com', N'bb')
ALTER TABLE [dbo].[Mic_log] ADD  CONSTRAINT [DF_Mic_log_logDate]  DEFAULT (getdate()) FOR [logDate]
GO
USE [master]
GO
ALTER DATABASE [microservice] SET  READ_WRITE 
GO
