CREATE TABLE [dbo].[DailyCollectionEntry](
	[DailyCollectionEntryId] [int] IDENTITY(1,1) NOT NULL,
	[SubCenterEmployeeId] [int] NOT NULL,
	[EntryDate] [datetime] NOT NULL,
	[Daily] [decimal](18, 2) NOT NULL,
	[Hour24] [decimal](18, 2) NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[UpdatedBy] [int] NULL,
	[UpdatedAt] [datetime] NULL,
	[IsActive] [bit] NOT NULL,
	[UserId] [int] NOT NULL,
 CONSTRAINT [PK_DailyCollectionEntry] PRIMARY KEY CLUSTERED 
(
	[DailyCollectionEntryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


