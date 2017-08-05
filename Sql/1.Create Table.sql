USE [EasysoftPssS_Dev]
GO

/****** Object:  Table [dbo].[CustomerGroup]    Script Date: 01/16/2017 22:11:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerGroup]') AND type in (N'U'))
DROP TABLE [dbo].[CustomerGroup]
GO

CREATE TABLE [dbo].[CustomerGroup](
	[Id] [char](32) NOT NULL PRIMARY KEY,
	[Name] [nvarchar](10) NOT NULL,
	[IsDefault] [char](1) NOT NULL,
	[Remark] [nvarchar](100) NOT NULL,
	[Creator] [varchar](16) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Mender] [varchar](16) NOT NULL,
	[ModifyTime] [datetime] NOT NULL
) 

GO

INSERT INTO [dbo].[CustomerGroup]([Id], [Name], [IsDefault], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'默认分组', 'Y', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())
GO

INSERT INTO [dbo].[CustomerGroup]([Id], [Name], [IsDefault], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'承翰陶源', 'Y', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())
GO

INSERT INTO [dbo].[CustomerGroup]([Id], [Name], [IsDefault], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'万科清林径', 'Y', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())
GO

INSERT INTO [dbo].[CustomerGroup]([Id], [Name], [IsDefault], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'国香尚居', 'Y', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())
GO

INSERT INTO [dbo].[CustomerGroup]([Id], [Name], [IsDefault], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'百合盛世', 'Y', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())
GO

INSERT INTO [dbo].[CustomerGroup]([Id], [Name], [IsDefault], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'五联其它', 'Y', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())
GO

/****** Object:  Table [dbo].[Customer]    Script Date: 01/16/2017 22:11:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Customer]') AND type in (N'U'))
DROP TABLE [dbo].[Customer]
GO

CREATE TABLE [dbo].[Customer](
	[Id] [char](32) NOT NULL PRIMARY KEY,
	[Name] [nvarchar](10) NOT NULL,
	[Nickname] [nvarchar](10) NOT NULL,
	[Mobile] [varchar](16) NOT NULL,
	[WeChatId] [varchar](30) NOT NULL,
	[GroupId] [char](32) NOT NULL,
	[Creator] [varchar](16) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Mender] [varchar](16) NOT NULL,
	[ModifyTime] [datetime] NOT NULL
)

GO

/****** Object:  Table [dbo].[CustomerAddress]    Script Date: 01/16/2017 22:11:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CustomerAddress]') AND type in (N'U'))
DROP TABLE [dbo].[CustomerAddress]
GO

CREATE TABLE [dbo].[CustomerAddress](
	[Id] [char](32) NOT NULL PRIMARY KEY,
	[CustomerId] [char](32) NOT NULL,
	[Address] [nvarchar](100) NOT NULL,
	[Mobile] [varchar](16) NOT NULL,
	[Linkman] [nvarchar](10) NOT NULL,
	[IsDefault] [char](1) NOT NULL,
	[Creator] [nvarchar](16) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Mender] [nvarchar](16) NOT NULL,
	[ModifyTime] [datetime] NOT NULL
) 

GO

/****** Object:  Table [dbo].[PurchaseItem]    Script Date: 01/16/2017 22:11:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PurchaseItem]') AND type in (N'U'))
DROP TABLE [dbo].[PurchaseItem]
GO

CREATE TABLE [dbo].[PurchaseItem](
	[Id] [char](32) NOT NULL PRIMARY KEY,
	[Name] [nvarchar](10) NOT NULL,
	[Code] [varchar](10) NOT NULL,
	[Category] [char](1) NOT NULL,-- 产品 A、包装 B
	[InUnit] [nvarchar](2) NOT NULL,
	[OutUnit] [nvarchar](2) NOT NULL,
	[InOutRate] [decimal](4,2) NOT NULL,
	[Price] [decimal](6,2) NOT NULL,
	[IsValid] [char](1) NOT NULL,
	[Remark] [nvarchar](100) NOT NULL,
	[Creator] [varchar](16) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Mender] [varchar](16) NOT NULL,
	[ModifyTime] [datetime] NOT NULL
)

GO

INSERT INTO [dbo].[PurchaseItem]([Id], [Name], [Code], [Category], [InUnit], [OutUnit], [InOutRate], [Price], [IsValid], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'黄桃', 'HT', 'A', N'斤', N'箱', 10, 120, 'Y', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())

INSERT INTO [dbo].[PurchaseItem]([Id], [Name], [Code], [Category], [InUnit], [OutUnit], [InOutRate], [Price], [IsValid], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'冰糖橙', 'BTC', 'A', N'斤', N'箱', 10, 50, 'N', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())

INSERT INTO [dbo].[PurchaseItem]([Id], [Name], [Code], [Category], [InUnit], [OutUnit], [InOutRate], [Price], [IsValid], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'脐橙', 'QC', 'A', N'斤', N'箱', 10, 60, 'N', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())

INSERT INTO [dbo].[PurchaseItem]([Id], [Name], [Code], [Category], [InUnit], [OutUnit], [InOutRate], [Price], [IsValid], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'冬笋', 'DS', 'A', N'斤', N'斤', 1, 10, 'N', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())

INSERT INTO [dbo].[PurchaseItem]([Id], [Name], [Code], [Category], [InUnit], [OutUnit], [InOutRate], [Price], [IsValid], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'纸箱', 'ZX', 'B', N'个', N'', 0, 0, 'Y', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())

INSERT INTO [dbo].[PurchaseItem]([Id], [Name], [Code], [Category], [InUnit], [OutUnit], [InOutRate], [Price], [IsValid], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'发泡网', 'FPW', 'B', N'批', N'', 0, 0, 'Y', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())

INSERT INTO [dbo].[PurchaseItem]([Id], [Name], [Code], [Category], [InUnit], [OutUnit], [InOutRate], [Price], [IsValid], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'胶带', 'JD', 'B', N'个', N'', 0, 0, 'Y', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())

GO

/****** Object:  Table [dbo].[CostItem]    Script Date: 01/16/2017 22:11:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CostItem]') AND type in (N'U'))
DROP TABLE [dbo].[CostItem]
GO

CREATE TABLE [dbo].[CostItem](
	[Id] [char](32) NOT NULL PRIMARY KEY,
	[Name] [nvarchar](10) NOT NULL,
	[Code] [varchar](10) NOT NULL,
	[Category] [char](1) NOT NULL,-- 采购 P、出库 D
	[IsValid] [char](1) NOT NULL,
	[OrderNumber] [smallint] NOT NULL,
	[Remark] [nvarchar](100) NOT NULL,
	[Creator] [varchar](16) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Mender] [varchar](16) NOT NULL,
	[ModifyTime] [datetime] NOT NULL
) 

GO

INSERT INTO [dbo].[CostItem]([Id], [Name], [Code], [Category], [IsValid], [OrderNumber], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'货款', 'HUOKUAN', 'P', 'Y', 1, '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())

INSERT INTO [dbo].[CostItem]([Id], [Name], [Code], [Category], [IsValid], [OrderNumber], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'运输中转', 'YUNSHU', 'P', 'Y', 2, '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())

INSERT INTO [dbo].[CostItem]([Id], [Name], [Code], [Category], [IsValid], [OrderNumber], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'其它费用', 'QITA', 'P', 'Y', 3, '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())

INSERT INTO [dbo].[CostItem]([Id], [Name], [Code], [Category], [IsValid], [OrderNumber], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'快递费', 'KUAIDI', 'D', 'Y', 1, '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())

INSERT INTO [dbo].[CostItem]([Id], [Name], [Code], [Category], [IsValid], [OrderNumber], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'运输中转', 'YUNSHU', 'D', 'Y', 2, '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())

INSERT INTO [dbo].[CostItem]([Id], [Name], [Code], [Category], [IsValid], [OrderNumber], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'其它费用', 'QITA', 'D', 'Y', 3, '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())
GO

/****** Object:  Table [dbo].[Purchase]    Script Date: 01/16/2017 22:11:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Purchase]') AND type in (N'U'))
DROP TABLE [dbo].[Purchase]
GO

CREATE TABLE [dbo].[Purchase](
	[Id] [char](32) NOT NULL PRIMARY KEY,
	[Date] [date] NOT NULL,
	[Category] [char](1) NOT NULL,-- 产品、包装
	[Item] [varchar](10) NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[Unit] [nvarchar](2) NOT NULL,
	[Supplier] [nvarchar](10) NOT NULL,
	[Inventory] [decimal](18, 2) NOT NULL,
	[Cost] [decimal](18, 2) NOT NULL,
	[ProfitLoss] [decimal](18, 2) NOT NULL,
	[Remark] [nvarchar](100) NOT NULL,
	[Status] [char](1) NOT NULL,
	[Creator] [nvarchar](16) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Mender] [nvarchar](16) NOT NULL,
	[ModifyTime] [datetime] NOT NULL
)

GO

/****** Object:  Table [dbo].[Cost]    Script Date: 01/16/2017 22:11:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Cost]') AND type in (N'U'))
DROP TABLE [dbo].[Cost]
GO

CREATE TABLE [dbo].[Cost](
	[Id] [char](32) NOT NULL PRIMARY KEY,
	[RecordId] [char](32) NOT NULL,
	[Category] [char](1) NOT NULL, --入库成本、出库成本
	[Item] [varchar](10) NOT NULL,
	[Money] [decimal](18, 2) NOT NULL
) 

GO

/****** Object:  Table [dbo].[ProfitLoss]    Script Date: 01/16/2017 22:11:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ProfitLoss]') AND type in (N'U'))
DROP TABLE [dbo].[ProfitLoss]
GO

CREATE TABLE [dbo].[ProfitLoss](
	[Id] [char](32) NOT NULL PRIMARY KEY,
	[RecordId] [char](32) NOT NULL,
	[TargetType] [char](1) NOT NULL, --采购仓、销售仓
	[Category] [char](1) NOT NULL, --增益、报损
	[Quantity] [decimal](18, 2) NOT NULL,
	[Remark] [nvarchar](100) NOT NULL,
	[Creator] [nvarchar](16) NOT NULL,
	[CreateTime] [datetime] NOT NULL
) 

GO

/****** Object:  Table [dbo].[ExpressCompany]    Script Date: 01/16/2017 22:11:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExpressCompany]') AND type in (N'U'))
DROP TABLE [dbo].[ExpressCompany]
GO

CREATE TABLE [dbo].[ExpressCompany](
	[Id] [char](32) NOT NULL PRIMARY KEY,
	[Name] [nvarchar](10) NOT NULL,
	[Code] [varchar](10) NOT NULL,
	[IsValid] [char](1) NOT NULL,
	[Remark] [nvarchar](100) NOT NULL,
	[Creator] [varchar](16) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Mender] [varchar](16) NOT NULL,
	[ModifyTime] [datetime] NOT NULL
) 

GO

INSERT INTO [dbo].[ExpressCompany]([Id], [Name], [Code], [IsValid], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'圆通快递', 'YUANTONG', 'Y', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())

INSERT INTO [dbo].[ExpressCompany]([Id], [Name], [Code], [IsValid], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'顺丰快递', 'SHUNFENG', 'Y', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())

INSERT INTO [dbo].[ExpressCompany]([Id], [Name], [Code], [IsValid], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'天天快递', 'TIANTIAN', 'Y', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())

GO

/****** Object:  Table [dbo].[Delivery]    Script Date: 01/16/2017 22:11:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Delivery]') AND type in (N'U'))
DROP TABLE [dbo].[Delivery]
GO

CREATE TABLE [dbo].[Delivery](
	[Id] [char](32) NOT NULL,
	[Date] [date] NOT NULL,
	[ExpressCompany] [varchar](10) NOT NULL, --快递公司
	[ExpressBill] [varchar](100) NOT NULL, --快递单，多个单用逗号隔开
	[IncludeOrder] [char](1) NOT NULL, 
	[Cost] [decimal](18, 2) NOT NULL,
	[Summary] [nvarchar](100) NOT NULL,
	[Remark] [nvarchar](100) NOT NULL,
	[Creator] [nvarchar](16) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Mender] [varchar](16) NOT NULL,
	[ModifyTime] [datetime] NOT NULL
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[DeliveryDetail]    Script Date: 01/16/2017 22:11:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DeliveryDetail]') AND type in (N'U'))
DROP TABLE [dbo].[DeliveryDetail]
GO

CREATE TABLE [dbo].[DeliveryDetail](
	[Id] [char](32) NOT NULL,
	[DeliveryId] [char](32) NOT NULL,
	[PurchaseId] [char](32) NOT NULL,
	[PurchaseCategory] [char](1) NOT NULL,
	[DeliveryQuantity] [decimal](18, 2) NOT NULL,
	[PackQuantity] [decimal](18, 2) NOT NULL,
	[PackUnit] [nvarchar](2) NOT NULL,
	[Creator] [nvarchar](16) NOT NULL,
	[CreateTime] [datetime] NOT NULL
) ON [PRIMARY]

GO

/****** Object:  Table [dbo].[Sale]    Script Date: 01/16/2017 22:11:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Sale]') AND type in (N'U'))
DROP TABLE [dbo].[Sale]
GO

CREATE TABLE [dbo].[Sale](
	[Id] [char](32) NOT NULL PRIMARY KEY,
	[DeliveryId] [char](32) NOT NULL,
	[Item] [varchar](10) NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[Unit] [nvarchar](2) NOT NULL,
	[Inventory] [decimal](18, 2) NOT NULL,
	[ProfitLoss] [decimal](18, 2) NOT NULL,
	[Status] [char](1) NOT NULL,--S已发货，R已到货，F已完成。
	[Creator] [nvarchar](16) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Mender] [nvarchar](16) NOT NULL,
	[ModifyTime] [datetime] NOT NULL
)

GO

/****** Object:  Table [dbo].[SaleOrder]    Script Date: 01/16/2017 22:11:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SaleOrder]') AND type in (N'U'))
DROP TABLE [dbo].[SaleOrder]
GO

CREATE TABLE [dbo].[SaleOrder](
	[Id] [char](32) NOT NULL PRIMARY KEY,
	[CustomerId] [char](32) NOT NULL,
    [Date] [date] NOT NULL,
	[Address] [nvarchar](100) NOT NULL,
	[Mobile] [varchar](16) NOT NULL,
	[Linkman] [nvarchar](10) NOT NULL,
	[Item] [varchar](10) NOT NULL,
	[Quantity] [decimal](18, 2) NOT NULL,
	[Unit] [nvarchar](2) NOT NULL,
	[NeedExpress] [char](1) NOT NULL,
	[RecordId] [varchar](32) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[ActualAmount] [decimal](18, 2) NOT NULL,
	[Discount] [decimal](18, 2) NOT NULL,
	[Status] [char](1) NOT NULL,--O已下单，S已发货，R已收货。
	[Remark] [nvarchar](100) NOT NULL,
	[Creator] [nvarchar](16) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Mender] [nvarchar](16) NOT NULL,
	[ModifyTime] [datetime] NOT NULL
)

GO
