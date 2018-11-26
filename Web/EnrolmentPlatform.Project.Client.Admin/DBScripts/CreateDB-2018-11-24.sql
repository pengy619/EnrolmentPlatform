USE [EnrolmentPlatform]
GO
/****** Object:  Table [dbo].[T_AccountBasic]    Script Date: 2018/11/24 20:04:30 ******/
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
/****** Object:  Table [dbo].[T_Address]    Script Date: 2018/11/24 20:04:30 ******/
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
/****** Object:  Table [dbo].[T_Department]    Script Date: 2018/11/24 20:04:30 ******/
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
/****** Object:  Table [dbo].[T_Enterprise]    Script Date: 2018/11/24 20:04:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[T_Enterprise](
	[Id] [uniqueidentifier] NOT NULL,
	[EnterpriseName] [nvarchar](200) NULL,
	[Contact] [nvarchar](max) NULL,
	[Phone] [nvarchar](max) NULL,
	[Classify] [int] NOT NULL,
	[Remark] [nvarchar](max) NULL,
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
 CONSTRAINT [PK_dbo.T_Enterprise] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_File]    Script Date: 2018/11/24 20:04:30 ******/
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
/****** Object:  Table [dbo].[T_Job]    Script Date: 2018/11/24 20:04:30 ******/
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
/****** Object:  Table [dbo].[T_LogSetting]    Script Date: 2018/11/24 20:04:30 ******/
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
/****** Object:  Table [dbo].[T_LogSettingDetail]    Script Date: 2018/11/24 20:04:30 ******/
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
/****** Object:  Table [dbo].[T_Order]    Script Date: 2018/11/24 20:04:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
	[Phone] [nvarchar](100) NULL,
	[TencentNo] [nvarchar](10) NULL,
	[Email] [varchar](100) NOT NULL,
	[Address] [nvarchar](10) NULL,
	[Remark] [varchar](100) NOT NULL,
	[Status] [int] NOT NULL,
	[FromChannelId] [uniqueidentifier] NULL,
	[FromTypeName] [varchar](100) NULL,
	[ToLearningCenterId] [uniqueidentifier] NULL,
	[GraduateSchool] [nvarchar](max) NULL,
	[HighesDegree] [nvarchar](max) NULL,
	[WorkUnit] [nvarchar](max) NULL,
	[EnrollAddress] [nvarchar](max) NULL,
	[ExamSubject] [nvarchar](max) NULL,
	[ExamDate] [datetime] NULL,
	[AllOrderImageUpload] [bit] NOT NULL,
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
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[T_OrderAmount]    Script Date: 2018/11/24 20:04:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_OrderAmount](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderId] [uniqueidentifier] NOT NULL,
	[PaymentSource] [int] NOT NULL,
	[TotalAmount] [decimal](12, 2) NOT NULL,
	[PayedAmount] [decimal](12, 2) NOT NULL,
	[UnPayedAmount] [decimal](12, 2) NOT NULL,
	[ApprovalAmount] [decimal](12, 2) NOT NULL,
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
/****** Object:  Table [dbo].[T_OrderImage]    Script Date: 2018/11/24 20:04:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_OrderImage](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderId] [uniqueidentifier] NOT NULL,
	[IDCard1] [nvarchar](max) NOT NULL,
	[IDCard2] [nvarchar](max) NOT NULL,
	[LiangCunLanDiImg] [nvarchar](max) NOT NULL,
	[BiYeZhengImg] [nvarchar](max) NOT NULL,
	[MianKaoYingYuImg] [nvarchar](max) NOT NULL,
	[MianKaoJiSuanJiImg] [nvarchar](max) NOT NULL,
	[XueXinWangImg] [nvarchar](max) NOT NULL,
	[QiTa] [nvarchar](max) NOT NULL,
	[TouXiang] [nvarchar](max) NOT NULL,
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
/****** Object:  Table [dbo].[T_PaymentInfo]    Script Date: 2018/11/24 20:04:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_PaymentInfo](
	[Id] [uniqueidentifier] NOT NULL,
	[PaymentRecordId] [uniqueidentifier] NOT NULL,
	[Amount] [decimal](12, 2) NOT NULL,
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
	[OrderId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.T_PaymentInfo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[T_PaymentRecord]    Script Date: 2018/11/24 20:04:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[T_PaymentRecord](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Type] [int] NOT NULL,
	[TotalAmount] [decimal](12, 2) NOT NULL,
	[FilePath] [nvarchar](max) NULL,
	[PaymentSource] [int] NULL,
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
/****** Object:  Table [dbo].[T_Permissions]    Script Date: 2018/11/24 20:04:30 ******/
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
/****** Object:  Table [dbo].[T_Role]    Script Date: 2018/11/24 20:04:30 ******/
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
/****** Object:  Table [dbo].[T_RolePermissionsRelation]    Script Date: 2018/11/24 20:04:30 ******/
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
/****** Object:  Table [dbo].[T_SystemBasicSetting]    Script Date: 2018/11/24 20:04:30 ******/
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
/****** Object:  Table [dbo].[T_SystemLoginLog]    Script Date: 2018/11/24 20:04:30 ******/
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
/****** Object:  Table [dbo].[T_SystemMessage]    Script Date: 2018/11/24 20:04:30 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_DBBACKUPForJob]    Script Date: 2018/11/24 20:04:30 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_DeleteAddress]    Script Date: 2018/11/24 20:04:30 ******/
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
/****** Object:  StoredProcedure [dbo].[SP_DeletePermissions]    Script Date: 2018/11/24 20:04:30 ******/
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


/****** Object:  Table [dbo].[T_Metadata]    Script Date: 2018-11-24 22:54:08 ******/
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
/****** Object:  Table [dbo].[T_SchoolLevelMajor]    Script Date: 2018-11-24 22:54:08 ******/
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
/****** Object:  Table [dbo].[T_ChargeStrategy]    Script Date: 2018-11-24 22:54:08 ******/
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
/****** Object:  Table [dbo].[T_Exam]    Script Date: 2018-11-24 22:54:08 ******/
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
/****** Object:  Table [dbo].[T_ExamInfo]    Script Date: 2018-11-24 22:54:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
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
