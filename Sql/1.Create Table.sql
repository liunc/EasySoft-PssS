USE [EasysoftPssS_Dev]
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
	[Allowance] [decimal](18, 2) NOT NULL,
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


