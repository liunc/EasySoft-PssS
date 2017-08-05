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
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'Ĭ�Ϸ���', 'Y', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())
GO

INSERT INTO [dbo].[CustomerGroup]([Id], [Name], [IsDefault], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'�к���Դ', 'Y', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())
GO

INSERT INTO [dbo].[CustomerGroup]([Id], [Name], [IsDefault], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'������־�', 'Y', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())
GO

INSERT INTO [dbo].[CustomerGroup]([Id], [Name], [IsDefault], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'�����о�', 'Y', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())
GO

INSERT INTO [dbo].[CustomerGroup]([Id], [Name], [IsDefault], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'�ٺ�ʢ��', 'Y', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())
GO

INSERT INTO [dbo].[CustomerGroup]([Id], [Name], [IsDefault], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'��������', 'Y', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())
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
	[Category] [char](1) NOT NULL,-- ��Ʒ A����װ B
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
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'����', 'HT', 'A', N'��', N'��', 10, 120, 'Y', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())

INSERT INTO [dbo].[PurchaseItem]([Id], [Name], [Code], [Category], [InUnit], [OutUnit], [InOutRate], [Price], [IsValid], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'���ǳ�', 'BTC', 'A', N'��', N'��', 10, 50, 'N', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())

INSERT INTO [dbo].[PurchaseItem]([Id], [Name], [Code], [Category], [InUnit], [OutUnit], [InOutRate], [Price], [IsValid], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'���', 'QC', 'A', N'��', N'��', 10, 60, 'N', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())

INSERT INTO [dbo].[PurchaseItem]([Id], [Name], [Code], [Category], [InUnit], [OutUnit], [InOutRate], [Price], [IsValid], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'����', 'DS', 'A', N'��', N'��', 1, 10, 'N', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())

INSERT INTO [dbo].[PurchaseItem]([Id], [Name], [Code], [Category], [InUnit], [OutUnit], [InOutRate], [Price], [IsValid], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'ֽ��', 'ZX', 'B', N'��', N'', 0, 0, 'Y', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())

INSERT INTO [dbo].[PurchaseItem]([Id], [Name], [Code], [Category], [InUnit], [OutUnit], [InOutRate], [Price], [IsValid], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'������', 'FPW', 'B', N'��', N'', 0, 0, 'Y', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())

INSERT INTO [dbo].[PurchaseItem]([Id], [Name], [Code], [Category], [InUnit], [OutUnit], [InOutRate], [Price], [IsValid], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'����', 'JD', 'B', N'��', N'', 0, 0, 'Y', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())

GO

/****** Object:  Table [dbo].[CostItem]    Script Date: 01/16/2017 22:11:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CostItem]') AND type in (N'U'))
DROP TABLE [dbo].[CostItem]
GO

CREATE TABLE [dbo].[CostItem](
	[Id] [char](32) NOT NULL PRIMARY KEY,
	[Name] [nvarchar](10) NOT NULL,
	[Code] [varchar](10) NOT NULL,
	[Category] [char](1) NOT NULL,-- �ɹ� P������ D
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
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'����', 'HUOKUAN', 'P', 'Y', 1, '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())

INSERT INTO [dbo].[CostItem]([Id], [Name], [Code], [Category], [IsValid], [OrderNumber], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'������ת', 'YUNSHU', 'P', 'Y', 2, '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())

INSERT INTO [dbo].[CostItem]([Id], [Name], [Code], [Category], [IsValid], [OrderNumber], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'��������', 'QITA', 'P', 'Y', 3, '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())

INSERT INTO [dbo].[CostItem]([Id], [Name], [Code], [Category], [IsValid], [OrderNumber], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'��ݷ�', 'KUAIDI', 'D', 'Y', 1, '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())

INSERT INTO [dbo].[CostItem]([Id], [Name], [Code], [Category], [IsValid], [OrderNumber], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'������ת', 'YUNSHU', 'D', 'Y', 2, '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())

INSERT INTO [dbo].[CostItem]([Id], [Name], [Code], [Category], [IsValid], [OrderNumber], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'��������', 'QITA', 'D', 'Y', 3, '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())
GO

/****** Object:  Table [dbo].[Purchase]    Script Date: 01/16/2017 22:11:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Purchase]') AND type in (N'U'))
DROP TABLE [dbo].[Purchase]
GO

CREATE TABLE [dbo].[Purchase](
	[Id] [char](32) NOT NULL PRIMARY KEY,
	[Date] [date] NOT NULL,
	[Category] [char](1) NOT NULL,-- ��Ʒ����װ
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
	[Category] [char](1) NOT NULL, --���ɱ�������ɱ�
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
	[TargetType] [char](1) NOT NULL, --�ɹ��֡����۲�
	[Category] [char](1) NOT NULL, --���桢����
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
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'Բͨ���', 'YUANTONG', 'Y', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())

INSERT INTO [dbo].[ExpressCompany]([Id], [Name], [Code], [IsValid], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'˳����', 'SHUNFENG', 'Y', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())

INSERT INTO [dbo].[ExpressCompany]([Id], [Name], [Code], [IsValid], [Remark], [Creator], [CreateTime], [Mender], [ModifyTime])
VALUES(CAST(LOWER(REPLACE(NEWID(), '-', '')) AS CHAR(32)), N'������', 'TIANTIAN', 'Y', '', 'Admin', GETUTCDATE(), 'Admin', GETUTCDATE())

GO

/****** Object:  Table [dbo].[Delivery]    Script Date: 01/16/2017 22:11:12 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Delivery]') AND type in (N'U'))
DROP TABLE [dbo].[Delivery]
GO

CREATE TABLE [dbo].[Delivery](
	[Id] [char](32) NOT NULL,
	[Date] [date] NOT NULL,
	[ExpressCompany] [varchar](10) NOT NULL, --��ݹ�˾
	[ExpressBill] [varchar](100) NOT NULL, --��ݵ���������ö��Ÿ���
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
	[Status] [char](1) NOT NULL,--S�ѷ�����R�ѵ�����F����ɡ�
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
	[Status] [char](1) NOT NULL,--O���µ���S�ѷ�����R���ջ���
	[Remark] [nvarchar](100) NOT NULL,
	[Creator] [nvarchar](16) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Mender] [nvarchar](16) NOT NULL,
	[ModifyTime] [datetime] NOT NULL
)

GO
