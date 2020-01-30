/****** Object:  Database [PaymentGatewayDemo]    Script Date: 11/09/2019 20:20:39 ******/
CREATE DATABASE [PaymentGatewayDemo]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PaymentGatewayDemo', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\PaymentGatewayDemo.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PaymentGatewayDemo_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\PaymentGatewayDemo_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PaymentGatewayDemo].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [PaymentGatewayDemo] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [PaymentGatewayDemo] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [PaymentGatewayDemo] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [PaymentGatewayDemo] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [PaymentGatewayDemo] SET ARITHABORT OFF 
GO

ALTER DATABASE [PaymentGatewayDemo] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [PaymentGatewayDemo] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [PaymentGatewayDemo] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [PaymentGatewayDemo] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [PaymentGatewayDemo] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [PaymentGatewayDemo] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [PaymentGatewayDemo] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [PaymentGatewayDemo] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [PaymentGatewayDemo] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [PaymentGatewayDemo] SET  DISABLE_BROKER 
GO

ALTER DATABASE [PaymentGatewayDemo] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [PaymentGatewayDemo] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [PaymentGatewayDemo] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [PaymentGatewayDemo] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [PaymentGatewayDemo] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [PaymentGatewayDemo] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [PaymentGatewayDemo] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [PaymentGatewayDemo] SET RECOVERY FULL 
GO

ALTER DATABASE [PaymentGatewayDemo] SET  MULTI_USER 
GO

ALTER DATABASE [PaymentGatewayDemo] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [PaymentGatewayDemo] SET DB_CHAINING OFF 
GO

ALTER DATABASE [PaymentGatewayDemo] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [PaymentGatewayDemo] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [PaymentGatewayDemo] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [PaymentGatewayDemo] SET QUERY_STORE = OFF
GO

ALTER DATABASE [PaymentGatewayDemo] SET  READ_WRITE 
GO

CREATE TABLE [PaymentGatewayDemo].[dbo].[Log] (

   [Id] int IDENTITY(1,1) NOT NULL,
   [Message] nvarchar(max) NULL,
   [MessageTemplate] nvarchar(max) NULL,
   [Level] nvarchar(128) NULL,
   [TimeStamp] datetimeoffset(7) NOT NULL,
   [Exception] nvarchar(max) NULL,
   [Properties] xml NULL,
   [LogEvent] nvarchar(max) NULL

   CONSTRAINT [PK_Log]
     PRIMARY KEY CLUSTERED ([Id] ASC)

)

CREATE TABLE [PaymentGatewayDemo].[dbo].[RefPaymentStatus](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NOT NULL,
 CONSTRAINT [PK_RefOrderStatus] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [PaymentGatewayDemo].[dbo].[Currency](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](250) NOT NULL,
	[Description] [varchar](250) NOT NULL,
 CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [PaymentGatewayDemo].[dbo].[Payment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PaymentCode] [UNIQUEIDENTIFIER] UNIQUE default NEWID(),
	[PaymentStatusId] [int] NOT NULL,
	[Amount] [int] NOT NULL,
	[CurrencyId] [int] NOT NULL,
	[BankPaymentCode] NVARCHAR(50) NULL,
	[CreatedDate] [datetime] NOT NULL,
	[UpdatedDate] [datetime] NOT NULL
	CONSTRAINT [PK_Payment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [PaymentGatewayDemo].[dbo].[PaymentCardDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PaymentCode] [UNIQUEIDENTIFIER] NOT NULL,
	[CardNumber] [int] NOT NULL,
	[ExpiryMonth] [int] NOT NULL,
	[ExpiryYear] [int] NOT NULL,
	[CVV] [int] NOT NULL,
	 CONSTRAINT [PK_PaymentCardDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [PaymentGatewayDemo].[dbo].[Payment]  WITH CHECK ADD FOREIGN KEY([PaymentStatusId])
REFERENCES [PaymentGatewayDemo].[dbo].[RefPaymentStatus] ([Id])
GO

ALTER TABLE [PaymentGatewayDemo].[dbo].[Payment]  WITH CHECK ADD FOREIGN KEY([CurrencyId])
REFERENCES [PaymentGatewayDemo].[dbo].[Currency] ([Id])
GO

ALTER TABLE [PaymentGatewayDemo].[dbo].[PaymentCardDetails]  WITH CHECK ADD FOREIGN KEY([PaymentCode])
REFERENCES [PaymentGatewayDemo].[dbo].[Payment] ([PaymentCode])
GO