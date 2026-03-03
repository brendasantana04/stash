USE [master]
GO
/****** Object:  Database [stash]    Script Date: 3/3/2026 2:45:44 PM ******/
CREATE DATABASE [stash]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'stash', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSQL\DATA\stash.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'stash_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSQL\DATA\stash_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [stash] SET COMPATIBILITY_LEVEL = 170
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [stash].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [stash] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [stash] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [stash] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [stash] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [stash] SET ARITHABORT OFF 
GO
ALTER DATABASE [stash] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [stash] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [stash] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [stash] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [stash] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [stash] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [stash] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [stash] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [stash] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [stash] SET  ENABLE_BROKER 
GO
ALTER DATABASE [stash] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [stash] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [stash] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [stash] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [stash] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [stash] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [stash] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [stash] SET RECOVERY FULL 
GO
ALTER DATABASE [stash] SET  MULTI_USER 
GO
ALTER DATABASE [stash] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [stash] SET DB_CHAINING OFF 
GO
ALTER DATABASE [stash] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [stash] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [stash] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [stash] SET OPTIMIZED_LOCKING = OFF 
GO
ALTER DATABASE [stash] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [stash] SET QUERY_STORE = ON
GO
ALTER DATABASE [stash] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [stash]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 3/3/2026 2:45:45 PM ******/
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
/****** Object:  Table [dbo].[account]    Script Date: 3/3/2026 2:45:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[account](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[created_on] [date] NOT NULL,
	[user_id] [bigint] NOT NULL,
	[name] [varchar](40) NOT NULL,
	[funds] [decimal](18, 2) NOT NULL,
	[active] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[card]    Script Date: 3/3/2026 2:45:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[card](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[created_on] [date] NOT NULL,
	[accountId] [bigint] NOT NULL,
	[tier] [int] NOT NULL,
	[limit] [decimal](18, 2) NOT NULL,
	[available_credit] [decimal](18, 2) NOT NULL,
	[issued_at] [datetime2](7) NULL,
	[is_active] [bit] NOT NULL,
	[account_id] [bigint] NULL,
	[credit_limit] [decimal](18, 2) NOT NULL,
	[last_upgrade_at] [datetime2](7) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[changelog]    Script Date: 3/3/2026 2:45:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[changelog](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[type] [tinyint] NULL,
	[version] [varchar](50) NULL,
	[description] [varchar](200) NOT NULL,
	[name] [varchar](300) NOT NULL,
	[checksum] [varchar](32) NULL,
	[installed_by] [varchar](100) NOT NULL,
	[installed_on] [datetime] NOT NULL,
	[success] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SavingsBox]    Script Date: 3/3/2026 2:45:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SavingsBox](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Balance] [decimal](18, 2) NOT NULL,
	[AccountId] [bigint] NOT NULL,
	[Created_On] [datetime2](7) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SavingsBoxes]    Script Date: 3/3/2026 2:45:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SavingsBoxes](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Balance] [decimal](18, 2) NOT NULL,
	[AccountId] [bigint] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[transaction]    Script Date: 3/3/2026 2:45:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[transaction](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[created_on] [date] NOT NULL,
	[accountid] [bigint] NOT NULL,
	[type] [int] NOT NULL,
	[value] [decimal](18, 2) NOT NULL,
	[description] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[transfer]    Script Date: 3/3/2026 2:45:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[transfer](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[created_on] [date] NOT NULL,
	[id_account_to] [bigint] NOT NULL,
	[id_account_from] [bigint] NOT NULL,
	[value] [decimal](18, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 3/3/2026 2:45:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[user](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[created_on] [date] NOT NULL,
	[name] [varchar](40) NOT NULL,
	[email] [varchar](30) NOT NULL,
	[password] [varchar](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_account_user_id]    Script Date: 3/3/2026 2:45:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_account_user_id] ON [dbo].[account]
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_card_account_id]    Script Date: 3/3/2026 2:45:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_card_account_id] ON [dbo].[card]
(
	[account_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_card_accountId]    Script Date: 3/3/2026 2:45:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_card_accountId] ON [dbo].[card]
(
	[accountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [UQ_card_accountId]    Script Date: 3/3/2026 2:45:46 PM ******/
CREATE UNIQUE NONCLUSTERED INDEX [UQ_card_accountId] ON [dbo].[card]
(
	[accountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_transaction_accountid]    Script Date: 3/3/2026 2:45:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_transaction_accountid] ON [dbo].[transaction]
(
	[accountid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_transaction_type]    Script Date: 3/3/2026 2:45:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_transaction_type] ON [dbo].[transaction]
(
	[type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_transfer_from]    Script Date: 3/3/2026 2:45:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_transfer_from] ON [dbo].[transfer]
(
	[id_account_from] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_transfer_to]    Script Date: 3/3/2026 2:45:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_transfer_to] ON [dbo].[transfer]
(
	[id_account_to] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_user_email]    Script Date: 3/3/2026 2:45:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_user_email] ON [dbo].[user]
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[account] ADD  CONSTRAINT [DF_account_created_on]  DEFAULT (CONVERT([date],sysutcdatetime())) FOR [created_on]
GO
ALTER TABLE [dbo].[account] ADD  CONSTRAINT [DF_account_funds]  DEFAULT ((0.00)) FOR [funds]
GO
ALTER TABLE [dbo].[account] ADD  CONSTRAINT [DF_account_active]  DEFAULT ((1)) FOR [active]
GO
ALTER TABLE [dbo].[card] ADD  CONSTRAINT [DF_card_created_on]  DEFAULT (CONVERT([date],sysutcdatetime())) FOR [created_on]
GO
ALTER TABLE [dbo].[card] ADD  CONSTRAINT [DF_card_limit]  DEFAULT ((0.00)) FOR [limit]
GO
ALTER TABLE [dbo].[card] ADD  CONSTRAINT [DF_card_available_credit]  DEFAULT ((0.00)) FOR [available_credit]
GO
ALTER TABLE [dbo].[card] ADD  CONSTRAINT [DF_card_is_active]  DEFAULT ((1)) FOR [is_active]
GO
ALTER TABLE [dbo].[card] ADD  CONSTRAINT [DF_card_credit_limit]  DEFAULT ((0.00)) FOR [credit_limit]
GO
ALTER TABLE [dbo].[changelog] ADD  DEFAULT (getdate()) FOR [installed_on]
GO
ALTER TABLE [dbo].[SavingsBox] ADD  CONSTRAINT [DF_SavingsBox_Created_On]  DEFAULT (getdate()) FOR [Created_On]
GO
ALTER TABLE [dbo].[transaction] ADD  CONSTRAINT [DF_transaction_created_on]  DEFAULT (CONVERT([date],sysutcdatetime())) FOR [created_on]
GO
ALTER TABLE [dbo].[transfer] ADD  CONSTRAINT [DF_transfer_created_on]  DEFAULT (CONVERT([date],sysutcdatetime())) FOR [created_on]
GO
ALTER TABLE [dbo].[user] ADD  CONSTRAINT [DF_user_created_on]  DEFAULT (CONVERT([date],sysutcdatetime())) FOR [created_on]
GO
ALTER TABLE [dbo].[account]  WITH CHECK ADD  CONSTRAINT [FK_account_user] FOREIGN KEY([user_id])
REFERENCES [dbo].[user] ([id])
GO
ALTER TABLE [dbo].[account] CHECK CONSTRAINT [FK_account_user]
GO
ALTER TABLE [dbo].[card]  WITH CHECK ADD  CONSTRAINT [FK_card_account] FOREIGN KEY([accountId])
REFERENCES [dbo].[account] ([id])
GO
ALTER TABLE [dbo].[card] CHECK CONSTRAINT [FK_card_account]
GO
ALTER TABLE [dbo].[card]  WITH CHECK ADD  CONSTRAINT [FK_card_account_account_id] FOREIGN KEY([account_id])
REFERENCES [dbo].[account] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[card] CHECK CONSTRAINT [FK_card_account_account_id]
GO
ALTER TABLE [dbo].[SavingsBox]  WITH CHECK ADD  CONSTRAINT [FK_SavingsBox_Account] FOREIGN KEY([AccountId])
REFERENCES [dbo].[account] ([id])
GO
ALTER TABLE [dbo].[SavingsBox] CHECK CONSTRAINT [FK_SavingsBox_Account]
GO
ALTER TABLE [dbo].[SavingsBoxes]  WITH CHECK ADD  CONSTRAINT [FK_SavingsBoxes_Account] FOREIGN KEY([AccountId])
REFERENCES [dbo].[account] ([id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SavingsBoxes] CHECK CONSTRAINT [FK_SavingsBoxes_Account]
GO
ALTER TABLE [dbo].[transaction]  WITH CHECK ADD  CONSTRAINT [FK_transaction_account] FOREIGN KEY([accountid])
REFERENCES [dbo].[account] ([id])
GO
ALTER TABLE [dbo].[transaction] CHECK CONSTRAINT [FK_transaction_account]
GO
ALTER TABLE [dbo].[transfer]  WITH CHECK ADD  CONSTRAINT [FK_transfer_account_from] FOREIGN KEY([id_account_from])
REFERENCES [dbo].[account] ([id])
GO
ALTER TABLE [dbo].[transfer] CHECK CONSTRAINT [FK_transfer_account_from]
GO
ALTER TABLE [dbo].[transfer]  WITH CHECK ADD  CONSTRAINT [FK_transfer_account_to] FOREIGN KEY([id_account_to])
REFERENCES [dbo].[account] ([id])
GO
ALTER TABLE [dbo].[transfer] CHECK CONSTRAINT [FK_transfer_account_to]
GO
USE [master]
GO
ALTER DATABASE [stash] SET  READ_WRITE 
GO
