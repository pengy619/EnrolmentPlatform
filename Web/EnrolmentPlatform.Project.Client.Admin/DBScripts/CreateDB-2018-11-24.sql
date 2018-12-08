USE [EnrolmentPlatform]
GO
/****** Object:  Table [dbo].[T_AccountBasic]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_AccountBasic](
	[Id] [uniqueidentifier] NOT NULL,
	[AccountNo] [varchar](100) NOT NULL,
	[IsMaster] [bit] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[DepartmentId] [uniqueidentifier] NOT NULL,
	[JobID] [uniqueidentifier] NOT NULL,
	[AddressId] [uniqueidentifier] NULL,
	[Phone] [nvarchar](max) NULL,
	[ID_Card] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[PassWord] [nvarchar](max) NULL,
	[Classify] [int] NOT NULL,
	[Picture] [nvarchar](100) NULL,
	[Sex] [nvarchar](10) NULL,
	[Nickname] [varchar](100) NOT NULL,
	[Status] [int] NOT NULL,
	[RealName] [varchar](100) NULL,
	[EnterpriseId] [uniqueidentifier] NOT NULL,
	[Birthday] [varchar](100) NULL,
	[Remark] [nvarchar](max) NULL,
	[IsAllowMobileLogin] [bit] NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatorTime] [datetime] NOT NULL,
	[CreatorUserId] [uniqueidentifier] NOT NULL,
	[CreatorAccount] [nvarchar](max) NULL,
	[LastModifyTime] [datetime] NOT NULL,
	[LastModifyUserId] [uniqueidentifier] NOT NULL,
	[DeleteTime] [datetime] NOT NULL,
	[DeleteUserId] [uniqueidentifier] NOT NULL,
	[Unix] [bigint] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_dbo.T_AccountBasic] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_Address]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Address](
	[Id] [uniqueidentifier] NOT NULL,
	[ChinaName] [varchar](100) NOT NULL,
	[EnglishName] [varchar](100) NOT NULL,
	[Pinyin] [varchar](100) NOT NULL,
	[ShortPinyin] [varchar](100) NOT NULL,
	[Classify] [int] NOT NULL,
	[ChinaRoute] [varchar](200) NULL,
	[PinyinRoute] [varchar](200) NULL,
	[IsMunicipality] [bit] NOT NULL,
	[ZipCode] [varchar](500) NULL,
	[AreaCode] [varchar](50) NULL,
	[ParentId] [uniqueidentifier] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatorTime] [datetime] NOT NULL,
	[CreatorUserId] [uniqueidentifier] NOT NULL,
	[LastModifyTime] [datetime] NOT NULL,
	[LastModifyUserId] [uniqueidentifier] NOT NULL,
	[DeleteTime] [datetime] NOT NULL,
	[DeleteUserId] [uniqueidentifier] NOT NULL,
	[Unix] [bigint] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
	[CreatorAccount] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_T_Address] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_ChargeStrategy]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_ChargeStrategy](
	[Id] [uniqueidentifier] NOT NULL,
	[SchoolMajorId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](200) NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[InstitutionCharge] [decimal](18, 2) NOT NULL,
	[CenterCharge] [decimal](18, 2) NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatorTime] [datetime] NOT NULL,
	[CreatorUserId] [uniqueidentifier] NOT NULL,
	[CreatorAccount] [nvarchar](max) NULL,
	[LastModifyTime] [datetime] NOT NULL,
	[LastModifyUserId] [uniqueidentifier] NOT NULL,
	[DeleteTime] [datetime] NOT NULL,
	[DeleteUserId] [uniqueidentifier] NOT NULL,
	[Unix] [bigint] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_T_CHARGESTRATEGY] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[T_Department]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Department](
	[Id] [uniqueidentifier] NOT NULL,
	[DepartmentName] [nvarchar](200) NULL,
	[Sort] [int] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatorTime] [datetime] NOT NULL,
	[CreatorUserId] [uniqueidentifier] NOT NULL,
	[CreatorAccount] [nvarchar](max) NULL,
	[LastModifyTime] [datetime] NOT NULL,
	[LastModifyUserId] [uniqueidentifier] NOT NULL,
	[DeleteTime] [datetime] NOT NULL,
	[DeleteUserId] [uniqueidentifier] NOT NULL,
	[Unix] [bigint] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_dbo.T_Department] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[T_Enterprise]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Enterprise](
	[Id] [uniqueidentifier] NOT NULL,
	[EnterpriseName] [nvarchar](200) NULL,
	[BusinessType] [int] NULL,
	[BusinessRang] [nvarchar](max) NULL,
	[TaxNo] [nvarchar](max) NULL,
	[Fax] [nvarchar](max) NULL,
	[Contact] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[AddressId] [uniqueidentifier] NOT NULL,
	[Address] [nvarchar](max) NULL,
	[SettlementCycle] [int] NOT NULL,
	[Rate] [decimal](18, 2) NOT NULL,
	[DepositAmount] [decimal](18, 2) NOT NULL,
	[Classify] [int] NOT NULL,
	[CashPassWord] [nvarchar](max) NULL,
	[Remark] [nvarchar](max) NULL,
	[Status] [int] NOT NULL,
	[LegalPerson] [nvarchar](max) NULL,
	[Balance] [decimal](18, 2) NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatorTime] [datetime] NOT NULL,
	[CreatorUserId] [uniqueidentifier] NOT NULL,
	[CreatorAccount] [nvarchar](max) NULL,
	[LastModifyTime] [datetime] NOT NULL,
	[LastModifyUserId] [uniqueidentifier] NOT NULL,
	[DeleteTime] [datetime] NOT NULL,
	[DeleteUserId] [uniqueidentifier] NOT NULL,
	[Unix] [bigint] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
	[LastSettlementDate] [datetime] NULL,
	[NextSettlementDate] [datetime] NULL,
	[BusinessEndDate] [datetime] NULL CONSTRAINT [DF__T_Enterpr__Busin__2739D489]  DEFAULT (getdate()),
	[LicenseNo] [varchar](200) NULL CONSTRAINT [DF__T_Enterpr__Licen__282DF8C2]  DEFAULT (getdate()),
	[SupplierType] [int] NOT NULL,
 CONSTRAINT [PK_dbo.T_Enterprise] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_Exam]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Exam](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatorTime] [datetime] NOT NULL,
	[CreatorUserId] [uniqueidentifier] NOT NULL,
	[CreatorAccount] [nvarchar](max) NULL,
	[LastModifyTime] [datetime] NOT NULL,
	[LastModifyUserId] [uniqueidentifier] NOT NULL,
	[DeleteTime] [datetime] NOT NULL,
	[DeleteUserId] [uniqueidentifier] NOT NULL,
	[Unix] [bigint] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_T_EXAM] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[T_ExamInfo]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_ExamInfo](
	[Id] [uniqueidentifier] NOT NULL,
	[StudentId] [uniqueidentifier] NOT NULL,
	[ExamId] [uniqueidentifier] NOT NULL,
	[StudentNo] [varchar](50) NULL,
	[UserName] [nvarchar](200) NULL,
	[ExamPlace] [nvarchar](200) NULL,
	[MailAddress] [nvarchar](200) NULL,
	[ReturnAddress] [nvarchar](200) NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatorTime] [datetime] NOT NULL,
	[CreatorUserId] [uniqueidentifier] NOT NULL,
	[CreatorAccount] [nvarchar](max) NULL,
	[LastModifyTime] [datetime] NOT NULL,
	[LastModifyUserId] [uniqueidentifier] NOT NULL,
	[DeleteTime] [datetime] NOT NULL,
	[DeleteUserId] [uniqueidentifier] NOT NULL,
	[Unix] [bigint] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_T_EXAMINFO] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_File]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_File](
	[Id] [uniqueidentifier] NOT NULL,
	[ForeignKeyId] [uniqueidentifier] NOT NULL,
	[FilePath] [nvarchar](max) NULL,
	[FileName] [nvarchar](max) NULL,
	[FileClassify] [int] NOT NULL,
	[ForeignKeyClassify] [int] NOT NULL,
	[FileBusinessType] [int] NOT NULL,
	[Iscover] [bit] NOT NULL,
	[IsFocus] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatorTime] [datetime] NOT NULL,
	[CreatorUserId] [uniqueidentifier] NOT NULL,
	[CreatorAccount] [nvarchar](max) NULL,
	[LastModifyTime] [datetime] NOT NULL,
	[LastModifyUserId] [uniqueidentifier] NOT NULL,
	[DeleteTime] [datetime] NOT NULL,
	[DeleteUserId] [uniqueidentifier] NOT NULL,
	[Unix] [bigint] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_dbo.T_File] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[T_Job]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Job](
	[Id] [uniqueidentifier] NOT NULL,
	[JobName] [nvarchar](200) NULL,
	[Sort] [int] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatorTime] [datetime] NOT NULL,
	[CreatorUserId] [uniqueidentifier] NOT NULL,
	[CreatorAccount] [nvarchar](max) NULL,
	[LastModifyTime] [datetime] NOT NULL,
	[LastModifyUserId] [uniqueidentifier] NOT NULL,
	[DeleteTime] [datetime] NOT NULL,
	[DeleteUserId] [uniqueidentifier] NOT NULL,
	[Unix] [bigint] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_dbo.T_Job] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[T_LogSetting]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_LogSetting](
	[Id] [uniqueidentifier] NOT NULL,
	[TableName] [nvarchar](max) NULL,
	[BusinessName] [nvarchar](max) NULL,
	[PrimaryKey] [uniqueidentifier] NOT NULL,
	[ModuleKey] [uniqueidentifier] NOT NULL,
	[Url] [nvarchar](max) NULL,
	[IP] [nvarchar](max) NULL,
	[OperationType] [nvarchar](max) NULL,
	[OldContent] [nvarchar](max) NULL,
	[NewContent] [nvarchar](max) NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatorTime] [datetime] NOT NULL,
	[CreatorUserId] [uniqueidentifier] NOT NULL,
	[CreatorAccount] [nvarchar](max) NULL,
	[LastModifyTime] [datetime] NOT NULL,
	[LastModifyUserId] [uniqueidentifier] NOT NULL,
	[DeleteTime] [datetime] NOT NULL,
	[DeleteUserId] [uniqueidentifier] NOT NULL,
	[Unix] [bigint] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_dbo.T_LogSetting] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[T_LogSettingDetail]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_LogSettingDetail](
	[Id] [uniqueidentifier] NOT NULL,
	[LogId] [uniqueidentifier] NOT NULL,
	[ColumnName] [nvarchar](max) NULL,
	[OldColumnValue] [nvarchar](max) NULL,
	[NewColumnValue] [nvarchar](max) NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatorTime] [datetime] NOT NULL,
	[CreatorUserId] [uniqueidentifier] NOT NULL,
	[CreatorAccount] [nvarchar](max) NULL,
	[LastModifyTime] [datetime] NOT NULL,
	[LastModifyUserId] [uniqueidentifier] NOT NULL,
	[DeleteTime] [datetime] NOT NULL,
	[DeleteUserId] [uniqueidentifier] NOT NULL,
	[Unix] [bigint] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_dbo.T_LogSettingDetail] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[T_Metadata]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Metadata](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Type] [int] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatorTime] [datetime] NOT NULL,
	[CreatorUserId] [uniqueidentifier] NOT NULL,
	[CreatorAccount] [nvarchar](max) NULL,
	[LastModifyTime] [datetime] NOT NULL,
	[LastModifyUserId] [uniqueidentifier] NOT NULL,
	[DeleteTime] [datetime] NOT NULL,
	[DeleteUserId] [uniqueidentifier] NOT NULL,
	[Unix] [bigint] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_T_METADATA] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[T_Order]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Order](
	[Id] [uniqueidentifier] NOT NULL,
	[BatchId] [uniqueidentifier] NOT NULL,
	[StudentName] [nvarchar](max) NULL,
	[IDCardNo] [nvarchar](max) NULL,
	[Native] [nvarchar](max) NULL,
	[SchoolId] [uniqueidentifier] NOT NULL,
	[LevelId] [uniqueidentifier] NOT NULL,
	[MajorId] [uniqueidentifier] NOT NULL,
	[Phone] [nvarchar](max) NULL,
	[TencentNo] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Address] [nvarchar](max) NULL,
	[Remark] [nvarchar](max) NULL,
	[Status] [int] NOT NULL,
	[EnrollTime] [datetime] NULL,
	[ToLearningCenterTime] [datetime] NULL,
	[LeaveTime] [datetime] NULL,
	[JoinTime] [datetime] NULL,
	[FromChannelId] [uniqueidentifier] NULL,
	[FromTypeName] [nvarchar](max) NULL,
	[ToLearningCenterId] [uniqueidentifier] NULL,
	[GraduateSchool] [nvarchar](max) NULL,
	[HighesDegree] [nvarchar](max) NULL,
	[WorkUnit] [nvarchar](max) NULL,
	[EnrollAddress] [nvarchar](max) NULL,
	[ExamSubject] [nvarchar](max) NULL,
	[ExamDate] [datetime] NULL,
	[AllOrderImageUpload] [bit] NOT NULL,
	[AllZSZhongXinAmountPayed] [bit] NOT NULL,
	[AllQuDaoAmountPayed] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatorTime] [datetime] NOT NULL,
	[CreatorUserId] [uniqueidentifier] NOT NULL,
	[CreatorAccount] [nvarchar](max) NULL,
	[LastModifyTime] [datetime] NOT NULL,
	[LastModifyUserId] [uniqueidentifier] NOT NULL,
	[DeleteTime] [datetime] NOT NULL,
	[DeleteUserId] [uniqueidentifier] NOT NULL,
	[Unix] [bigint] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_dbo.T_Order] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[T_OrderAmount]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_OrderAmount](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderId] [uniqueidentifier] NOT NULL,
	[PaymentSource] [int] NOT NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
	[PayedAmount] [decimal](18, 2) NOT NULL,
	[ApprovalAmount] [decimal](18, 2) NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatorTime] [datetime] NOT NULL,
	[CreatorUserId] [uniqueidentifier] NOT NULL,
	[CreatorAccount] [nvarchar](max) NULL,
	[LastModifyTime] [datetime] NOT NULL,
	[LastModifyUserId] [uniqueidentifier] NOT NULL,
	[DeleteTime] [datetime] NOT NULL,
	[DeleteUserId] [uniqueidentifier] NOT NULL,
	[Unix] [bigint] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_dbo.T_OrderAmount] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[T_OrderImage]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_OrderImage](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderId] [uniqueidentifier] NOT NULL,
	[IDCard1] [nvarchar](max) NULL,
	[IDCard2] [nvarchar](max) NULL,
	[LiangCunLanDiImg] [nvarchar](max) NULL,
	[BiYeZhengImg] [nvarchar](max) NULL,
	[MianKaoYingYuImg] [nvarchar](max) NULL,
	[MianKaoJiSuanJiImg] [nvarchar](max) NULL,
	[XueXinWangImg] [nvarchar](max) NULL,
	[QiTa] [nvarchar](max) NULL,
	[TouXiang] [nvarchar](max) NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatorTime] [datetime] NOT NULL,
	[CreatorUserId] [uniqueidentifier] NOT NULL,
	[CreatorAccount] [nvarchar](max) NULL,
	[LastModifyTime] [datetime] NOT NULL,
	[LastModifyUserId] [uniqueidentifier] NOT NULL,
	[DeleteTime] [datetime] NOT NULL,
	[DeleteUserId] [uniqueidentifier] NOT NULL,
	[Unix] [bigint] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_dbo.T_OrderImage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[T_PaymentInfo]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_PaymentInfo](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderId] [uniqueidentifier] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[PaymentRecordId] [uniqueidentifier] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatorTime] [datetime] NOT NULL,
	[CreatorUserId] [uniqueidentifier] NOT NULL,
	[CreatorAccount] [nvarchar](max) NULL,
	[LastModifyTime] [datetime] NOT NULL,
	[LastModifyUserId] [uniqueidentifier] NOT NULL,
	[DeleteTime] [datetime] NOT NULL,
	[DeleteUserId] [uniqueidentifier] NOT NULL,
	[Unix] [bigint] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_dbo.T_PaymentInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[T_PaymentRecord]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_PaymentRecord](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Type] [int] NOT NULL,
	[TotalAmount] [decimal](18, 2) NOT NULL,
	[FilePath] [nvarchar](max) NULL,
	[PaymentSource] [int] NOT NULL,
	[PaymentSourceId] [uniqueidentifier] NULL,
	[Status] [int] NOT NULL,
	[Auditor] [nvarchar](max) NULL,
	[AuditorId] [uniqueidentifier] NULL,
	[AuditTime] [datetime] NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatorTime] [datetime] NOT NULL,
	[CreatorUserId] [uniqueidentifier] NOT NULL,
	[CreatorAccount] [nvarchar](max) NULL,
	[LastModifyTime] [datetime] NOT NULL,
	[LastModifyUserId] [uniqueidentifier] NOT NULL,
	[DeleteTime] [datetime] NOT NULL,
	[DeleteUserId] [uniqueidentifier] NOT NULL,
	[Unix] [bigint] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_dbo.T_PaymentRecord] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[T_Permissions]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_Permissions](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Level] [int] NOT NULL,
	[Area] [nvarchar](max) NULL,
	[Controller] [nvarchar](max) NULL,
	[Action] [nvarchar](max) NULL,
	[Param] [nvarchar](max) NULL,
	[Classify] [int] NOT NULL,
	[ParentId] [uniqueidentifier] NOT NULL,
	[Sort] [int] NOT NULL,
	[Icon] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.T_Permissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[T_Role]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Role](
	[Id] [uniqueidentifier] NOT NULL,
	[RoleName] [varchar](200) NULL,
	[Status] [int] NOT NULL,
	[EnterpriseId] [uniqueidentifier] NOT NULL,
	[Classify] [int] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatorTime] [datetime] NOT NULL,
	[CreatorUserId] [uniqueidentifier] NOT NULL,
	[CreatorAccount] [nvarchar](max) NULL,
	[LastModifyTime] [datetime] NOT NULL,
	[LastModifyUserId] [uniqueidentifier] NOT NULL,
	[DeleteTime] [datetime] NOT NULL,
	[DeleteUserId] [uniqueidentifier] NOT NULL,
	[Unix] [bigint] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_dbo.T_Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_RolePermissionsRelation]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_RolePermissionsRelation](
	[Id] [uniqueidentifier] NOT NULL,
	[RoleId] [uniqueidentifier] NOT NULL,
	[PermissionsId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.T_RolePermissionsRelation] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[T_SchoolLevelMajor]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_SchoolLevelMajor](
	[Id] [uniqueidentifier] NOT NULL,
	[ItemId] [uniqueidentifier] NOT NULL,
	[Type] [int] NOT NULL,
	[ParentId] [uniqueidentifier] NULL,
	[IsEnabled] [bit] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatorTime] [datetime] NOT NULL,
	[CreatorUserId] [uniqueidentifier] NOT NULL,
	[CreatorAccount] [nvarchar](max) NULL,
	[LastModifyTime] [datetime] NOT NULL,
	[LastModifyUserId] [uniqueidentifier] NOT NULL,
	[DeleteTime] [datetime] NOT NULL,
	[DeleteUserId] [uniqueidentifier] NOT NULL,
	[Unix] [bigint] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_T_SCHOOLLEVELMAJOR] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[T_SystemBasicSetting]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_SystemBasicSetting](
	[Id] [uniqueidentifier] NOT NULL,
	[Key] [int] NOT NULL,
	[Value] [nvarchar](max) NULL,
	[Remark] [varchar](500) NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatorTime] [datetime] NOT NULL,
	[CreatorUserId] [uniqueidentifier] NOT NULL,
	[CreatorAccount] [nvarchar](max) NULL,
	[LastModifyTime] [datetime] NOT NULL,
	[LastModifyUserId] [uniqueidentifier] NOT NULL,
	[DeleteTime] [datetime] NOT NULL,
	[DeleteUserId] [uniqueidentifier] NOT NULL,
	[Unix] [bigint] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_dbo.T_SystemBasicSetting] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_SystemLoginLog]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_SystemLoginLog](
	[Id] [uniqueidentifier] NOT NULL,
	[Account] [nvarchar](200) NULL,
	[AccountId] [uniqueidentifier] NOT NULL,
	[Ip] [nvarchar](max) NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatorTime] [datetime] NOT NULL,
	[CreatorUserId] [uniqueidentifier] NOT NULL,
	[CreatorAccount] [nvarchar](200) NULL,
	[LastModifyTime] [datetime] NOT NULL,
	[LastModifyUserId] [uniqueidentifier] NOT NULL,
	[DeleteTime] [datetime] NOT NULL,
	[DeleteUserId] [uniqueidentifier] NOT NULL,
	[Unix] [bigint] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
 CONSTRAINT [PK_dbo.T_SystemLoginLog] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[T_SystemMessage]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_SystemMessage](
	[Id] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NULL,
	[Content] [text] NULL,
	[BusinessName] [nvarchar](max) NULL,
	[Status] [int] NOT NULL,
	[IsDelete] [bit] NOT NULL,
	[CreatorTime] [datetime] NOT NULL,
	[CreatorUserId] [uniqueidentifier] NOT NULL,
	[CreatorAccount] [nvarchar](max) NULL,
	[LastModifyTime] [datetime] NOT NULL,
	[LastModifyUserId] [uniqueidentifier] NOT NULL,
	[DeleteTime] [datetime] NOT NULL,
	[DeleteUserId] [uniqueidentifier] NOT NULL,
	[Unix] [bigint] NOT NULL,
	[RowVersion] [timestamp] NOT NULL,
	[EnterpriseId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_T_SystemMessage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  StoredProcedure [dbo].[P_CopyProduct]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[P_CopyProduct]    
	-- Add the parameters for the stored procedure here
 @ProductId uniqueidentifier,   
 @NewProductId uniqueidentifier,   
 @Classify int,     
 @CreatorAccount VARCHAR(50), 
 @CreatorUserId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @NewProductNumber varchar(50)
  
	if @Classify=1 /*农产品*/
	begin 

		set @NewProductNumber='S'+ REPLACE(REPLACE(REPLACE(CONVERT(varchar(100),GETDATE(),120),'-',''),' ',''),':','')

		/*复制[T_Product]信息*/   
		insert into [dbo].[T_ProductBasic]([Id],[ProductName],[ProductCategoriesId],[ProductNumber],[MinMarkPrice],[MinDistributionPrice],[MinSettlementPrice],[InventoryClassify],[SupplierId],
										   [Status],[Reason],[Classify],[Sort],[OffSaleTime],[IsDelete],[CreatorTime],[CreatorUserId],[CreatorAccount],[LastModifyTime],[LastModifyUserId],[DeleteTime],[DeleteUserId],[Unix]) 
		select @NewProductId [Id],[ProductName],[ProductCategoriesId],@NewProductNumber [ProductNumber],[MinMarkPrice],[MinDistributionPrice],[MinSettlementPrice],[InventoryClassify],[SupplierId],
										   1 [Status],'' [Reason],[Classify],[Sort],[OffSaleTime],[IsDelete],getdate() [CreatorTime],@CreatorUserId [CreatorUserId],@CreatorAccount [CreatorAccount],getdate() [LastModifyTime],@CreatorUserId [LastModifyUserId],[DeleteTime],[DeleteUserId],[Unix]
		from [dbo].[T_ProductBasic] with(nolock) where Id=@ProductId and isdelete=0 

		/*农产品信息*/
		insert into [dbo].T_ProductForSpecialty([Id],[ProductId],[VarietiesId],[SpecsStr],[SpecialtyExplain],[SalesModel],[DepositRatio],[SaleslUnit],[Deliveries],[ExpressFee],[SupplierTime],[SaleStartTime],[SaleEndTime],[MinBuyQuantity],[IsDelete],[CreatorTime],
												[CreatorUserId],[CreatorAccount],[LastModifyTime],[LastModifyUserId],[DeleteTime],[DeleteUserId],[Unix] ,[IsMarket]) 
		select newid() [Id],@NewProductId [ProductId],[VarietiesId],[SpecsStr],[SpecialtyExplain],[SalesModel],[DepositRatio],[SaleslUnit],[Deliveries],[ExpressFee],[SupplierTime],[SaleStartTime],[SaleEndTime],[MinBuyQuantity],[IsDelete],getdate() [CreatorTime],
												@CreatorUserId [CreatorUserId],@CreatorAccount [CreatorAccount],getdate() [LastModifyTime],@CreatorUserId [LastModifyUserId],[DeleteTime],[DeleteUserId],[Unix] ,[IsMarket]
		from [dbo].T_ProductForSpecialty with(nolock) where ProductId=@ProductId and isdelete=0

		/*农产品价格*/
		insert into [dbo].[T_Price]([Id],[PriceName],[ForeignKeyId],[ForeignKeyClassify],[PriceClassify],[Price],[Remark],[IsDelete],[CreatorTime],[CreatorUserId],[CreatorAccount],[LastModifyTime],[LastModifyUserId],[DeleteTime],[DeleteUserId],[Unix]) 
		select newid() [Id],[PriceName],@NewProductId [ForeignKeyId],[ForeignKeyClassify],[PriceClassify],[Price],[Remark],[IsDelete],getdate() [CreatorTime],@CreatorUserId [CreatorUserId],@CreatorAccount [CreatorAccount],getdate() [LastModifyTime],@CreatorUserId [LastModifyUserId],[DeleteTime],[DeleteUserId],[Unix]
		from [dbo].[T_Price] with(nolock) where [ForeignKeyId]=@ProductId and isdelete=0

		/*农产品库存*/
		insert into [dbo].[T_Inventory]([Id],[InventoryName],[ForeignKeyId],[ForeignKeyClassify],[IsInventory],[Inventory],[SoldInventory],[Remark],[IsDelete],[CreatorTime],[CreatorUserId],[CreatorAccount],[LastModifyTime],[LastModifyUserId],[DeleteTime],[DeleteUserId],[Unix]) 
		select newid() [Id],[InventoryName],@NewProductId [ForeignKeyId],[ForeignKeyClassify],[IsInventory],[Inventory],[SoldInventory],[Remark],[IsDelete],getdate() [CreatorTime],@CreatorUserId [CreatorUserId],@CreatorAccount [CreatorAccount],getdate() [LastModifyTime],@CreatorUserId [LastModifyUserId],[DeleteTime],[DeleteUserId],[Unix]
		from [dbo].[T_Inventory] with(nolock) where [ForeignKeyId]=@ProductId and isdelete=0
		 

	end

	if @Classify=2 /*门票*/
	begin
						
		set @NewProductNumber='T'+ REPLACE(REPLACE(REPLACE(CONVERT(varchar(100),GETDATE(),120),'-',''),' ',''),':','')
						   
		/*复制[T_Product]信息*/   
		insert into [dbo].[T_ProductBasic]([Id],[ProductName],[ProductCategoriesId],[ProductNumber],[MinMarkPrice],[MinDistributionPrice],[MinSettlementPrice],[InventoryClassify],[SupplierId],
										   [Status],[Reason],[Classify],[Sort],[OffSaleTime],[IsDelete],[CreatorTime],[CreatorUserId],[CreatorAccount],[LastModifyTime],[LastModifyUserId],[DeleteTime],[DeleteUserId],[Unix]) 
		select @NewProductId [Id],('[复制]'+[ProductName]) [ProductName],[ProductCategoriesId],@NewProductNumber [ProductNumber],[MinMarkPrice],[MinDistributionPrice],[MinSettlementPrice],[InventoryClassify],[SupplierId],
										   1 [Status],'' [Reason],[Classify],[Sort],[OffSaleTime],[IsDelete],getdate() [CreatorTime],@CreatorUserId [CreatorUserId],@CreatorAccount [CreatorAccount],getdate() [LastModifyTime],@CreatorUserId [LastModifyUserId],[DeleteTime],[DeleteUserId],[Unix]
		from [dbo].[T_ProductBasic] with(nolock) where Id=@ProductId and isdelete=0 

		/*门票信息*/
		insert into [dbo].[T_ProductForTicket]([Id],[ProductId],[ThemeId],[ScenicSpotIdStr],[ScenicSpotNameStr],[AmusementProjectIdStr],[AmusementProjectNameStr],[AdultNumber],[ChildNumber],[BuyMaxAge]
					,[BuyMinAge],[BookTimeLimitDay],[BookTimeLimitHour],[BookTimeLimitMinute],[RefundRule],IsBefore,[RefundDay],[BookDescription],[UseDescription],[IsDelete]
					,[CreatorTime],[CreatorUserId],[CreatorAccount],[LastModifyTime],[LastModifyUserId],[DeleteTime],[DeleteUserId],[Unix],[TicketClassify]) 
		select newid() [Id],@NewProductId [ProductId],[ThemeId],[ScenicSpotIdStr],[ScenicSpotNameStr],[AmusementProjectIdStr],[AmusementProjectNameStr],[AdultNumber],[ChildNumber],[BuyMaxAge]
					,[BuyMinAge],[BookTimeLimitDay],[BookTimeLimitHour],[BookTimeLimitMinute],[RefundRule],IsBefore,[RefundDay],[BookDescription],[UseDescription],[IsDelete]
					,getdate() [CreatorTime],@CreatorUserId [CreatorUserId],[CreatorAccount],getdate() [LastModifyTime],@CreatorUserId [LastModifyUserId],getdate() [DeleteTime],[DeleteUserId],[Unix],[TicketClassify]
		from [dbo].[T_ProductForTicket] with(nolock) where ProductId=@ProductId and isdelete=0
		    
	end

	
	if @Classify=3 /*餐饮*/
	begin
						
		set @NewProductNumber='C'+ REPLACE(REPLACE(REPLACE(CONVERT(varchar(100),GETDATE(),120),'-',''),' ',''),':','')
						   
		/*复制[T_Product]信息*/   
		insert into [dbo].[T_ProductBasic]([Id],[ProductName],[ProductCategoriesId],[ProductNumber],[MinMarkPrice],[MinDistributionPrice],[MinSettlementPrice],[InventoryClassify],[SupplierId],
										   [Status],[Reason],[Classify],[Sort],[OffSaleTime],[IsDelete],[CreatorTime],[CreatorUserId],[CreatorAccount],[LastModifyTime],[LastModifyUserId],[DeleteTime],[DeleteUserId],[Unix]) 
		select @NewProductId [Id],('[复制]'+[ProductName]) [ProductName],@NewProductId [ProductCategoriesId],@NewProductNumber [ProductNumber],[MinMarkPrice],[MinDistributionPrice],[MinSettlementPrice],[InventoryClassify],[SupplierId],
										   1 [Status],'' [Reason],[Classify],[Sort],[OffSaleTime],[IsDelete],getdate() [CreatorTime],@CreatorUserId [CreatorUserId],@CreatorAccount [CreatorAccount],getdate() [LastModifyTime],@CreatorUserId [LastModifyUserId],[DeleteTime],[DeleteUserId],[Unix]
		from [dbo].[T_ProductBasic] with(nolock) where Id=@ProductId and isdelete=0 

		/*门票信息*/
		insert into [dbo].[T_ProductForCateringPackage]([Id],[ProductId],[ShopId],[Description],[UsePeople],[MaxUsePeople],[IsSubscribe],[RefundRule],[StartExpiryDate],[EndExpiryDate],[UseTime],
					[UseRule],[PackageDetailInfo],[IsDelete],[CreatorTime],[CreatorUserId],[CreatorAccount],[LastModifyTime],[LastModifyUserId],[DeleteTime],[DeleteUserId],[Unix]) 
		select newid() [Id],@NewProductId [ProductId],[ShopId],[Description],[UsePeople],[MaxUsePeople],[IsSubscribe],[RefundRule],[StartExpiryDate],[EndExpiryDate],[UseTime]
					,[UseRule],[PackageDetailInfo],[IsDelete],getdate() [CreatorTime],@CreatorUserId [CreatorUserId],[CreatorAccount],getdate() [LastModifyTime],@CreatorUserId [LastModifyUserId],
					getdate() [DeleteTime],[DeleteUserId],[Unix]
		from [dbo].[T_ProductForCateringPackage] with(nolock) where ProductId=@ProductId and isdelete=0
		    
	end
	 
	/*产品图片*/
	insert into [dbo].[T_File]([Id],[ForeignKeyId],[FilePath],[FileName],[FileClassify],[ForeignKeyClassify],[FileBusinessType],[Iscover],[IsFocus],[IsDelete],[CreatorTime],[CreatorUserId],[CreatorAccount],[LastModifyTime],[LastModifyUserId],[DeleteTime],[DeleteUserId],[Unix]) 
	select newid() [Id],@NewProductId [ForeignKeyId],[FilePath],[FileName],[FileClassify],[ForeignKeyClassify],[FileBusinessType],[Iscover],[IsFocus],[IsDelete],getdate() [CreatorTime],@CreatorUserId [CreatorUserId],@CreatorAccount [CreatorAccount],getdate() [LastModifyTime],@CreatorUserId [LastModifyUserId],[DeleteTime],[DeleteUserId],[Unix]
	from [dbo].[T_File] with(nolock) where [ForeignKeyId]=@ProductId and isdelete=0

	if @@ERROR>0
	begin
		select 0 Result
	end
	else 
	begin
		select 1 Result
	end

END


GO
/****** Object:  StoredProcedure [dbo].[P_GetHomeInfoForAdmin]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[P_GetHomeInfoForAdmin]    
	-- Add the parameters for the stored procedure here 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	/*农产品待审核数量*/
	declare @CountForSupplierForProductForSpecialty int =0;
	select @CountForSupplierForProductForSpecialty=Count(Id) from [dbo].[T_ProductBasic] with(nolock) 
	where Classify=1 and IsDelete=0 and [Status]=2

	/*票务待审核数量*/
	declare @CountForSupplierForProductForTicket int =0; 
	select @CountForSupplierForProductForTicket=Count(Id) from [dbo].[T_ProductBasic] with(nolock) 
	where Classify=2 and IsDelete=0 and [Status]=2

	/*餐饮待审核数量*/
	declare @CountForSupplierForProductForCatering int =0; 
	select @CountForSupplierForProductForCatering=Count(Id) from [dbo].[T_ProductBasic] with(nolock) 
	where Classify=3 and IsDelete=0 and [Status]=2

	/*农场品退款待审核数量  退款中*/
	declare @CountForSupplierForSpecialtyOrderForRefund int =0; 
	select @CountForSupplierForSpecialtyOrderForRefund=Count(Id) from [dbo].[T_OrderBasic] with(nolock) 
	where IsDelete=0 and OrderStatus=6 and CancelForSystem=3 AND Classify=1

	/*门票退款待审核数量  退款中*/
	declare @CountForSupplierForTicketOrderForRefund int =0; 
	select @CountForSupplierForTicketOrderForRefund=Count(Id) from [dbo].[T_OrderBasic] with(nolock) 
	where IsDelete=0 and OrderStatus=6 and CancelForSystem=3 AND Classify=2

	/*餐饮退款待审核数量  退款中*/
	declare @CountForSupplierForCateringOrderForRefund int =0; 
	select @CountForSupplierForCateringOrderForRefund=Count(Id) from [dbo].[T_OrderBasic] with(nolock) 
	where IsDelete=0 and OrderStatus=6 and CancelForSystem=3 AND Classify=3

	/*提现待审核数量*/
	declare @CountForSupplierForFinaceForWithdraw int =0; 
	select @CountForSupplierForFinaceForWithdraw=Count(Id) from [dbo].[T_ApplyCash] with(nolock) 
	where IsDelete=0 and [Status]=0
   
	/*总待办事数量*/
	declare @CountForSupplierForAll int=0;
	set @CountForSupplierForAll+=@CountForSupplierForProductForSpecialty;
	set @CountForSupplierForAll+=@CountForSupplierForProductForTicket;
	set @CountForSupplierForAll+=@CountForSupplierForSpecialtyOrderForRefund;
	set @CountForSupplierForAll+=@CountForSupplierForTicketOrderForRefund;
	set @CountForSupplierForAll+=@CountForSupplierForFinaceForWithdraw; 

	select @CountForSupplierForAll as CountForSupplierForAll,
	@CountForSupplierForProductForSpecialty as CountForSupplierForProductForSpecialty,
	@CountForSupplierForProductForTicket as CountForSupplierForProductForTicket,
	@CountForSupplierForProductForCatering as CountForSupplierForProductForCatering,
	@CountForSupplierForSpecialtyOrderForRefund as CountForSupplierForSpecialtyOrderForRefund,
	@CountForSupplierForTicketOrderForRefund as CountForSupplierForTicketOrderForRefund,
	@CountForSupplierForCateringOrderForRefund as CountForSupplierForCateringOrderForRefund,
	@CountForSupplierForFinaceForWithdraw as CountForSupplierForFinaceForWithdraw 
	 
END



GO
/****** Object:  StoredProcedure [dbo].[P_GetHomeInfoForAdminByTime]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[P_GetHomeInfoForAdminByTime]    
	-- Add the parameters for the stored procedure here
 @StartTime varchar(20),
 @EndTime varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	/*新增订单数*/
	declare @CountForOrderForAdd int =0;
	select @CountForOrderForAdd=Count(Id) from [dbo].[T_OrderBasic] with(nolock) 
	where IsDelete=0 and PayStatus=2 and CreatorTime>=@StartTime and CreatorTime<@EndTime

	/*销售额*/
	declare @PriceForOrderForAddForSale decimal(18,2)=0.00; 
	select @PriceForOrderForAddForSale=isnull(sum(UpdateTotalAmount),0) from [dbo].[T_OrderBasic] with(nolock) 
	where IsDelete=0 and PayStatus=2 and CreatorTime>=@StartTime and CreatorTime<@EndTime

	/*退款订单数*/
	declare @CountForOrderForCancel int =0; 
	select @CountForOrderForCancel=Count(Id) from [dbo].[T_OrderBasic] with(nolock) 
	where IsDelete=0 and PayStatus=3 and CreatorTime>=@StartTime and CreatorTime<@EndTime

	/*退款额*/
	declare @PriceForOrderForCancel decimal(18,2)=0.00;
	select @PriceForOrderForCancel=isnull(sum(RefundAmount),0) from [dbo].[T_OrderBasic] with(nolock) 
	where IsDelete=0 and PayStatus=3 and CreatorTime>=@StartTime and CreatorTime<@EndTime
	 
	/*新增会员数*/
	declare @CountForUser int =0; 
	select @CountForUser=Count(Id) from [dbo].[T_AccountBasic] with(nolock) 
	where IsDelete=0 and [Status]=2 and Classify=2 and [LastModifyTime]>=@StartTime and [LastModifyTime]<@EndTime

	/*总会员数*/
	declare @CountForUserAll int=0;  	
	select @CountForUserAll=Count(Id) from [dbo].[T_AccountBasic] with(nolock) 
	where IsDelete=0 and [Status]=2 and Classify=2
	 
	/*新上架商品数*/
	declare @CountForProductForAdd int =0; 
	select @CountForProductForAdd=Count(Id) from [dbo].[T_ProductBasic] with(nolock) 
	where IsDelete=0 and [Status]=3 and [LastModifyTime]>=@StartTime and [LastModifyTime]<@EndTime

	/*已上架商品总数*/
	declare @CountForProductAll int =0; 
	select @CountForProductAll=Count(Id) from [dbo].[T_ProductBasic] with(nolock) 
	where IsDelete=0 and [Status]=3
	
	/*新增供应商数*/
	declare @CountForSupplierUser int =0; 
	select @CountForSupplierUser=Count(Id) from [dbo].[T_Enterprise] with(nolock) 
	where IsDelete=0 and [Status]=2 and Classify=3 and [LastModifyTime]>=@StartTime and [LastModifyTime]<@EndTime

	/*总供应商数*/
	declare @CountForSupplierUserAll int =0;  
	select @CountForSupplierUserAll=Count(Id) from [dbo].[T_Enterprise] with(nolock) 
	where IsDelete=0 and [Status]=2 and Classify=3
 

	select @CountForOrderForAdd as CountForOrderForAdd,
	@PriceForOrderForAddForSale as PriceForOrderForAddForSale,
	@CountForOrderForCancel as CountForOrderForCancel,
	@PriceForOrderForCancel as PriceForOrderForCancel,
	@CountForUser as CountForUser,
	@CountForUserAll as CountForUserAll,
	@CountForProductForAdd as CountForProductForAdd,
	@CountForProductAll as CountForProductAll,
	@CountForSupplierUser as CountForSupplierUser,
	@CountForSupplierUserAll as CountForSupplierUserAll
	 
END

GO
/****** Object:  StoredProcedure [dbo].[P_GetHomeInfoForSupplierByTime]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[P_GetHomeInfoForSupplierByTime]    
	-- Add the parameters for the stored procedure here
 @StartTime varchar(20),
 @EndTime varchar(20),
 @SupplierId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	/*新增订单数*/
	declare @CountForOrderForAdd int =0;
	select @CountForOrderForAdd=Count(Id) from [dbo].[T_OrderBasic] with(nolock) 
	where IsDelete=0 and PayStatus=2 and CreatorTime>=@StartTime and CreatorTime<@EndTime and [SupplierId]=@SupplierId

	/*销售额*/
	declare @PriceForOrderForAddForSale decimal(18,2)=0.00; 
	select @PriceForOrderForAddForSale=isnull(sum(UpdateTotalAmount),0) from [dbo].[T_OrderBasic] with(nolock) 
	where IsDelete=0 and PayStatus=2 and CreatorTime>=@StartTime and CreatorTime<@EndTime and [SupplierId]=@SupplierId

	/*退款订单数*/
	declare @CountForOrderForCancel int =0; 
	select @CountForOrderForCancel=Count(Id) from [dbo].[T_OrderBasic] with(nolock) 
	where IsDelete=0 and PayStatus=3 and CreatorTime>=@StartTime and CreatorTime<@EndTime and [SupplierId]=@SupplierId

	/*退款额*/
	declare @PriceForOrderForCancel decimal(18,2)=0.00;
	select @PriceForOrderForCancel=isnull(sum(RefundAmount),0) from [dbo].[T_OrderBasic] with(nolock) 
	where IsDelete=0 and PayStatus=3 and CreatorTime>=@StartTime and CreatorTime<@EndTime and [SupplierId]=@SupplierId
	  
	/*新上架商品数*/
	declare @CountForProductForAdd int =0; 
	select @CountForProductForAdd=Count(Id) from [dbo].[T_ProductBasic] with(nolock) 
	where IsDelete=0 and [Status]=3 and [LastModifyTime]>=@StartTime and [LastModifyTime]<@EndTime and [SupplierId]=@SupplierId

	/*已上架商品总数*/
	declare @CountForProductAll int =0; 
	select @CountForProductAll=Count(Id) from [dbo].[T_ProductBasic] with(nolock) 
	where IsDelete=0 and [Status]=3 and [SupplierId]=@SupplierId
	 

	select @CountForOrderForAdd as CountForOrderForAdd,
	@PriceForOrderForAddForSale as PriceForOrderForAddForSale,
	@CountForOrderForCancel as CountForOrderForCancel,
	@PriceForOrderForCancel as PriceForOrderForCancel, 
	@CountForProductForAdd as CountForProductForAdd,
	@CountForProductAll as CountForProductAll 
	 
END

GO
/****** Object:  StoredProcedure [dbo].[P_GetHomeInfoForSupplierId]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[P_GetHomeInfoForSupplierId]    
	-- Add the parameters for the stored procedure here
 @SupplierId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	/*农场品待退款数量*/ 
	declare @CountForSupplierForSpecialtyOrderForRefund int =0; 
	select @CountForSupplierForSpecialtyOrderForRefund=Count(Id) from [dbo].[T_OrderBasic] with(nolock) 
	where IsDelete=0 and CancelForSystem=2 and OrderStatus=6 and [SupplierId]=@SupplierId AND Classify=1
	 
	 /*门票待退款数量*/ 
	declare @CountForSupplierForTicketOrderForRefund int =0; 
	select @CountForSupplierForTicketOrderForRefund=Count(Id) from [dbo].[T_OrderBasic] with(nolock) 
	where IsDelete=0 and CancelForSystem=2 and OrderStatus=6 and [SupplierId]=@SupplierId AND Classify=2

	 /*餐饮待退款数量*/ 
	declare @CountForSupplierForCateringOrderForRefund int =0; 
	select @CountForSupplierForCateringOrderForRefund=Count(Id) from [dbo].[T_OrderBasic] with(nolock) 
	where IsDelete=0 and CancelForSystem=2 and OrderStatus=6 and [SupplierId]=@SupplierId AND Classify=3

	/*待发货*/
	declare @CountForSupplierForOrderForShip int =0; 
	select @CountForSupplierForOrderForShip=Count(Id) from [dbo].[T_OrderBasic] with(nolock) 
	where IsDelete=0 and Classify=1 and OrderStatus=2 and [SupplierId]=@SupplierId

	/*待退换货*/
	declare @CountForSupplierForOrderForExchange int =0; 
	select @CountForSupplierForOrderForExchange=Count(a.Id) from [dbo].T_RefundableRecord a with(nolock) 
	join [dbo].[T_OrderBasic] b with(nolock) on a.OrderNo=b.OrderNo
	where b.IsDelete=0 and a.[Status]=1 and b.[SupplierId]=@SupplierId
	 
	/*总待办事数量*/
	declare @CountForSupplierForAll int=0;
	set @CountForSupplierForAll+=@CountForSupplierForSpecialtyOrderForRefund; 
	set @CountForSupplierForAll+=@CountForSupplierForTicketOrderForRefund; 
	set @CountForSupplierForAll+=@CountForSupplierForCateringOrderForRefund; 
	set @CountForSupplierForAll+=@CountForSupplierForOrderForShip;
	set @CountForSupplierForAll+=@CountForSupplierForOrderForExchange; 

	select @CountForSupplierForAll as CountForSupplierForAll,
	@CountForSupplierForSpecialtyOrderForRefund as CountForSupplierForSpecialtyOrderForRefund,
	@CountForSupplierForTicketOrderForRefund as CountForSupplierForTicketOrderForRefund,
	@CountForSupplierForCateringOrderForRefund as CountForSupplierForCateringOrderForRefund,
	@CountForSupplierForOrderForShip as CountForSupplierForOrderForShip,
	@CountForSupplierForOrderForExchange as CountForSupplierForOrderForExchange
	 
END

GO
/****** Object:  StoredProcedure [dbo].[P_GetTicketByScenicOrPlayForB2C]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[P_GetTicketByScenicOrPlayForB2C]    
	-- Add the parameters for the stored procedure here
 @ScenicSoptId uniqueidentifier,    
 @PlayId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @DefaltGuid uniqueidentifier='00000000-0000-0000-0000-000000000000';

	--如果是根据景点id查询
	if @ScenicSoptId<>@DefaltGuid
	begin

		select p.Id,p.ProductName,p.MinDistributionPrice [MinMarkPrice],c.CateName,t.BookTimeLimitDay,t.BookTimeLimitHour,t.BookTimeLimitMinute,
		(select COUNT(s.Id) from T_Schedule s with(nolock) where s.ProductId=p.Id and s.IsDelete=0 and s.[Status]=1 and s.PlayDay>=CONVERT(VARCHAR(20),GETDATE(),23)) ScheduleCount
		from T_ProductBasic p with(nolock)
		join T_ProductForTicket t with(nolock) on p.Id=t.ProductId and t.IsDelete=0
		join T_ProductCategories c with(nolock) on p.ProductCategoriesId=c.Id and c.IsDelete=0
		where p.IsDelete=0 and p.[Status]=3 and p.Classify=2 and t.ScenicSpotIdStr like '%'+convert(varchar(50),@ScenicSoptId)+'%'

	end
	else
	begin
		
		select p.Id,p.ProductName,p.MinDistributionPrice [MinMarkPrice],c.CateName,t.BookTimeLimitDay,t.BookTimeLimitHour,t.BookTimeLimitMinute,
		(select COUNT(s.Id) from T_Schedule s with(nolock) where s.ProductId=p.Id and s.IsDelete=0 and s.[Status]=1 and s.PlayDay>=CONVERT(VARCHAR(20),GETDATE(),23)) ScheduleCount
		from T_ProductBasic p with(nolock)
		join T_ProductForTicket t with(nolock) on p.Id=t.ProductId and t.IsDelete=0
		join T_ProductCategories c with(nolock) on p.ProductCategoriesId=c.Id and c.IsDelete=0
		where p.IsDelete=0 and p.[Status]=3 and p.Classify=2 and t.AmusementProjectIdStr like '%'+convert(varchar(50),@PlayId)+'%'

	end

   
	 

END

GO
/****** Object:  StoredProcedure [dbo].[P_GetTicketByScenicOrPlayForH5]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[P_GetTicketByScenicOrPlayForH5]    
	-- Add the parameters for the stored procedure here
 @ScenicSoptId uniqueidentifier,    
 @PlayId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	declare @DefaltGuid uniqueidentifier='00000000-0000-0000-0000-000000000000';

	--如果是根据景点id查询
	if @ScenicSoptId<>@DefaltGuid
	begin

		select p.Id,p.ProductName,p.MinDistributionPrice [MinMarkPrice],c.CateName,
		isnull((select top 1 f.[FilePath] from [dbo].[T_File] f with(nolock) where f.[ForeignKeyId]=p.Id and f.IsDelete=0 and f.[Iscover]=1),
		(select top 1  f.[FilePath] from [dbo].[T_File] f with(nolock) where f.[ForeignKeyId]=p.Id and f.IsDelete=0)) FilePath
		from T_ProductBasic p with(nolock) 
		join [dbo].[T_ProductForTicket] t with(nolock) on p.Id=t.ProductId and t.IsDelete=0
		join T_ProductCategories c with(nolock) on p.ProductCategoriesId=c.Id and c.IsDelete=0 
		where p.IsDelete=0 and p.[Status]=3 and p.Classify=2 and t.ScenicSpotIdStr like '%'+convert(varchar(50),@ScenicSoptId)+'%'
		 

	end
	else
	begin
		
		select p.Id,p.ProductName,p.MinDistributionPrice [MinMarkPrice],c.CateName,
		isnull((select top 1 f.[FilePath] from [dbo].[T_File] f with(nolock) where f.[ForeignKeyId]=p.Id and f.IsDelete=0 and f.[Iscover]=1),
		(select top 1  f.[FilePath] from [dbo].[T_File] f with(nolock) where f.[ForeignKeyId]=p.Id and f.IsDelete=0)) FilePath
		from T_ProductBasic p with(nolock) 
		join [dbo].[T_ProductForTicket] t with(nolock) on p.Id=t.ProductId and t.IsDelete=0
		join T_ProductCategories c with(nolock) on p.ProductCategoriesId=c.Id and c.IsDelete=0 
		where p.IsDelete=0 and p.[Status]=3 and p.Classify=2 and t.AmusementProjectIdStr like '%'+convert(varchar(50),@PlayId)+'%'

		  
	end

   
	 

END

GO
/****** Object:  StoredProcedure [dbo].[P_UpdateMinPriceForProduct]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[P_UpdateMinPriceForProduct]    
	-- Add the parameters for the stored procedure here
 @ProductId uniqueidentifier
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	/*更新产品最低价格*/
	declare @MinSettlementPrice decimal(18,2)
	declare @MinDistributionPrice decimal(18,2)
	declare @MinMarkPrice decimal(18,2)
	declare @Day varchar(20)=convert(varchar(20),getdate(),23)
	declare @Classify int=0

	select @Classify=[Classify] from [dbo].[T_ProductBasic] p with(nolock) where Id=@ProductId
				
	if @Classify=1
	begin		  
	
		select @MinSettlementPrice=isnull(min(Price),0) 
		from T_Price p with(nolock)  
		where p.IsDelete=0 and p.[PriceClassify]=1 and p.ForeignKeyId=@ProductId
	
		select @MinDistributionPrice=isnull(min(Price),0) 
		from T_Price p with(nolock)  
		where p.IsDelete=0 and p.[PriceClassify]=2 and p.ForeignKeyId=@ProductId

	
		select @MinMarkPrice=isnull(min(Price),0) 
		from T_Price p with(nolock)  
		where p.IsDelete=0 and p.[PriceClassify]=3 and p.ForeignKeyId=@ProductId
		 
	end
	else if @Classify=2
	begin
		select @MinSettlementPrice=isnull(min(Price),0) 
		from T_Price p with(nolock) 
		join T_Schedule s with(nolock) on p.ForeignKeyId=s.Id and s.IsDelete=0 and s.PlayDay>@Day and s.[Status]=1
		where p.IsDelete=0 and p.[PriceClassify]=1 and s.[ProductId]=@ProductId
	
		select @MinDistributionPrice=isnull(min(Price),0) 
		from T_Price p with(nolock) 
		join T_Schedule s with(nolock) on p.ForeignKeyId=s.Id and s.IsDelete=0 and s.PlayDay>@Day and s.[Status]=1
		where p.IsDelete=0 and p.[PriceClassify]=2 and s.[ProductId]=@ProductId

	
		select @MinMarkPrice=isnull(min(Price),0) 
		from T_Price p with(nolock) 
		join T_Schedule s with(nolock) on p.ForeignKeyId=s.Id and s.IsDelete=0 and s.PlayDay>@Day and s.[Status]=1
		where p.IsDelete=0 and p.[PriceClassify]=3 and s.[ProductId]=@ProductId
	end

	update [dbo].[T_ProductBasic] set MinSettlementPrice=@MinSettlementPrice,MinDistributionPrice=@MinDistributionPrice,MinMarkPrice=@MinMarkPrice
	where Id=@ProductId

	
END


GO
/****** Object:  StoredProcedure [dbo].[SP_DBBACKUPForJob]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SP_DBBACKUPForJob]
as
DECLARE @DBfileName VARCHAR(100)
SELECT  @DBfileName=CONVERT(varchar,GETDATE(),112)
SET @DBfileName='E://DB/EnrolmentPlatform_bak/'+@DBfileName+'.bak'
BACKUP database EnrolmentPlatform to disk=@DBfileName

set nocount OFF

GO
/****** Object:  StoredProcedure [dbo].[SP_DeleteAddress]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  <Author,,houwencheng>
-- Create date: <2018年5月6日>
-- Description: <根据ID删除本身及下面所有子节点>
-- =============================================
create PROCEDURE [dbo].[SP_DeleteAddress]
 @Id  uniqueidentifier 
AS
BEGIN
 with  tab_del as (
  select Id,ParentId from [dbo].[T_Address] as T1 with(nolock)
  where T1.Id=@Id
  union all 
  select T2.Id,T2.ParentId from [dbo].[T_Address] as T2 with(nolock)
  inner join tab_del on tab_del.Id=T2.ParentId
  
 )
 update T_Address set IsDelete=1 where Id in (select Id from tab_del);
END

set nocount off

GO
/****** Object:  StoredProcedure [dbo].[SP_DeletePermissions]    Script Date: 2018/12/8 13:05:11 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:  <Author,,houwencheng>
-- Create date: <2018年4月20日>
-- Description: <根据ID删除本身及下面所有子节点>
-- =============================================
create PROCEDURE [dbo].[SP_DeletePermissions]
 @Id  uniqueidentifier 
AS
BEGIN
 with  tab_del as (
  select Id,ParentId from [dbo].[T_Permissions] as T1 with(nolock)
  where T1.Id=@Id
  union all 
  select T2.Id,T2.ParentId from [dbo].[T_Permissions] as T2 with(nolock)
  inner join tab_del on tab_del.Id=T2.ParentId
  
 )
 delete T_Permissions where Id in (select Id from tab_del);
END

set nocount off

GO
