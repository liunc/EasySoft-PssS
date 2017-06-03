USE [EasysoftPssS_Dev]
GO

/****** Object:  Table [dbo].[CustomerGroup]    Script Date: 01/16/2017 22:11:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerGroup]') AND type in (N'U'))
DROP TABLE [dbo].[CustomerGroup]
GO

CREATE TABLE [dbo].[CustomerGroup](
	[Id] [char](32) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[IsDefault] [char](1) NOT NULL,
	[Remark] [nvarchar](120) NOT NULL,
	[Creator] [nvarchar](20) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Mender] [nvarchar](20) NOT NULL,
	[ModifyTime] [datetime] NOT NULL
) ON [PRIMARY]

GO

INSERT INTO [dbo].[CustomerGroup]([Id], [Name], [IsDefault], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), '默认分组', 'Y', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())
GO

/****** Object:  Table [dbo].[Customer]    Script Date: 01/16/2017 22:11:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND type in (N'U'))
DROP TABLE [dbo].[Customer]
GO

CREATE TABLE [dbo].[Customer](
	[Id] [char](32) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Nickname] [nvarchar](50) NOT NULL,
	[Mobile] [varchar](20) NOT NULL,
	[WeChatId] [varchar](50) NOT NULL,
	[GroupId] [char](32) NOT NULL,
	[Creator] [nvarchar](20) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Mender] [nvarchar](20) NOT NULL,
	[ModifyTime] [datetime] NOT NULL
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[CustomerAddress]    Script Date: 01/16/2017 22:11:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerAddress]') AND type in (N'U'))
DROP TABLE [dbo].[CustomerAddress]
GO

CREATE TABLE [dbo].[CustomerAddress](
	[Id] [char](32) NOT NULL,
	[CustomerId] [char](32) NOT NULL,
	[Address] [nvarchar](120) NOT NULL,
	[Mobile] [varchar](20) NOT NULL,
	[Linkman] [nvarchar](50) NOT NULL,
	[IsDefault] [char](1) NOT NULL,
	[Creator] [nvarchar](20) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Mender] [nvarchar](20) NOT NULL,
	[ModifyTime] [datetime] NOT NULL
) ON [PRIMARY]

GO


/****** Object:  Table [dbo].[Purchase]    Script Date: 01/16/2017 22:11:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Purchase]') AND type in (N'U'))
DROP TABLE [dbo].[Purchase]
GO

CREATE TABLE [dbo].[Purchase](
	[Id] [char](32) NOT NULL,
	[Date] [date] NOT NULL,
	[Category] [varchar](10) NOT NULL,-- 产品、包装
	[Item] [varchar](20) NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[Unit] [nvarchar](5) NOT NULL,
	[Supplier] [nvarchar](50) NOT NULL,
	[Inventory] [decimal](18, 2) NOT NULL,
	[Cost] [decimal](18, 2) NOT NULL,
	[ProfitLoss] [decimal](18, 2) NOT NULL,
	[Remark] [nvarchar](120) NOT NULL,
	[Status] [smallint] NOT NULL,
	[Creator] [nvarchar](20) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Mender] [nvarchar](20) NOT NULL,
	[ModifyTime] [datetime] NOT NULL
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[Cost]    Script Date: 01/16/2017 22:11:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cost]') AND type in (N'U'))
DROP TABLE [dbo].[Cost]
GO

CREATE TABLE [dbo].[Cost](
	[Id] [char](32) NOT NULL,
	[RecordId] [char](32) NOT NULL,
	[Category] [varchar](10) NOT NULL, --入库成本、出库成本
	[Item] [varchar](20) NOT NULL,
	[Money] [decimal](18, 2) NOT NULL
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[ProfitLoss]    Script Date: 01/16/2017 22:11:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProfitLoss]') AND type in (N'U'))
DROP TABLE [dbo].[ProfitLoss]
GO

CREATE TABLE [dbo].[ProfitLoss](
	[Id] [char](32) NOT NULL,
	[RecordId] [char](32) NOT NULL,
	[TargetType] [varchar](10) NOT NULL, --采购仓、销售仓
	[Category] [varchar](10) NOT NULL, --增益、报损
	[Quantity] [decimal](18, 2) NOT NULL,
	[Remark] [nvarchar](120) NOT NULL,
	[Creator] [nvarchar](20) NOT NULL,
	[CreateTime] [datetime] NOT NULL
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[Outbound]    Script Date: 01/16/2017 22:11:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Outbound]') AND type in (N'U'))
DROP TABLE [dbo].[Outbound]
GO

CREATE TABLE [dbo].[Outbound](
	[Id] [char](32) NOT NULL,
	[Date] [date] NOT NULL,
	[ExpressCompany] [varchar](10) NOT NULL, --快递公司
	[ExpressBill] [varchar](120) NOT NULL, --快递单，多个单用逗号隔开
	[Category] [varchar](10) NOT NULL, --增益、报损
	[Quantity] [decimal](18, 2) NOT NULL,
	[Remark] [nvarchar](120) NOT NULL,
	[Creator] [nvarchar](20) NOT NULL,
	[CreateTime] [datetime] NOT NULL
) ON [PRIMARY]

GO


