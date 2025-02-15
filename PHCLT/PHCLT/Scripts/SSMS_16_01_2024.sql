USE [SSMS]
GO
/****** Object:  Table [dbo].[CustomerProductMaster]    Script Date: 16-01-2024 18:46:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerProductMaster](
	[CustomerProductMasterId] [int] IDENTITY(1,1) NOT NULL,
	[CustomerName] [nvarchar](100) NULL,
	[PurchaseDate] [datetime] NULL,
	[TillWarrantyDate] [datetime] NULL,
	[CustomerAddress] [nvarchar](max) NULL,
	[CustomerTaluka] [int] NULL,
	[CustomerDistrict] [int] NULL,
	[CustomerBillImage] [nvarchar](max) NULL,
	[Products] [nvarchar](max) NULL,
	[CreatedAt] [datetime] NULL,
	[CreatedBy] [nvarchar](100) NULL,
	[UpdatedAt] [datetime] NULL,
	[UpdatedBy] [nvarchar](100) NULL,
	[IsActive] [bit] NULL,
	[Mob1] [nvarchar](100) NULL,
	[Mob2] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[CustomerProductMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
