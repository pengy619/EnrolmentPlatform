--证件类型
IF COL_LENGTH('T_Order','IDCardType') IS NULL
BEGIN
	ALTER TABLE T_Order ADD IDCardType int not null default(0)
END
GO

IF COL_LENGTH('T_OrderApproval','IDCardType') IS NULL
BEGIN
	ALTER TABLE T_OrderApproval ADD IDCardType int not null default(0)
END
GO

--学历类型
IF COL_LENGTH('T_Order','DegreeType') IS NULL
BEGIN
	ALTER TABLE T_Order ADD DegreeType nvarchar(50) null
END
GO

IF COL_LENGTH('T_OrderApproval','DegreeType') IS NULL
BEGIN
	ALTER TABLE T_OrderApproval ADD DegreeType nvarchar(50) null
END
GO

--增加"学信网学籍截图"
IF COL_LENGTH('T_OrderImage','BiYeXueJiImg') IS NULL
BEGIN
	ALTER TABLE T_OrderImage ADD BiYeXueJiImg nvarchar(max) NULL
END
GO

--增加"毕业照"
IF COL_LENGTH('T_OrderImage','BiYePhoto') IS NULL
BEGIN
	ALTER TABLE T_OrderImage ADD BiYePhoto nvarchar(max) NULL
END
GO

--增加"毕业照片是否上传"
IF COL_LENGTH('T_Order','AllBiYeImageUpload') IS NULL
BEGIN
	ALTER TABLE T_Order ADD AllBiYeImageUpload bit not null default(0)
END
GO

--添加学员账号、毕业管理菜单
IF NOT EXISTS (SELECT 1 FROM T_Permissions WHERE Id='10c2cffe-e3a0-4895-9199-0875416bd70f')
BEGIN
	INSERT [dbo].[T_Permissions] ([Id], [Name], [Level], [Area], [Controller], [Action], [Param], [Classify], [ParentId], [Sort], [Icon]) VALUES (N'10c2cffe-e3a0-4895-9199-0875416bd70f', N'学员账号', 3, N'Order', N'Manager', N'Account', NULL, 1, N'7b5cf0bb-d4f8-4d16-8c12-2f7811690a4b', 5, N'icon-account')
	INSERT [dbo].[T_Permissions] ([Id], [Name], [Level], [Area], [Controller], [Action], [Param], [Classify], [ParentId], [Sort], [Icon]) VALUES (N'b75618b6-47dc-4b83-a6f9-e14d7accdda2', N'学员账号', 3, N'Order', N'Manager', N'Account', NULL, 5, N'4fe5e89f-dda0-4b19-9c01-3d6a511d95bd', 5, N'icon-account')
	INSERT [dbo].[T_Permissions] ([Id], [Name], [Level], [Area], [Controller], [Action], [Param], [Classify], [ParentId], [Sort], [Icon]) VALUES (N'710f0431-742a-474b-bc17-603749570a9c', N'毕业管理', 3, N'Order', N'Graduation', N'Index', NULL, 1, N'7b5cf0bb-d4f8-4d16-8c12-2f7811690a4b', 6, N'icon-yunyingguanli')
	INSERT [dbo].[T_Permissions] ([Id], [Name], [Level], [Area], [Controller], [Action], [Param], [Classify], [ParentId], [Sort], [Icon]) VALUES (N'0e00c6d6-7e00-4c6f-a45d-6bc09188378f', N'上传毕业照', 4, N'Order', N'Graduation', N'Option', NULL, 1, N'710f0431-742a-474b-bc17-603749570a9c', 1, NULL)
	INSERT [dbo].[T_Permissions] ([Id], [Name], [Level], [Area], [Controller], [Action], [Param], [Classify], [ParentId], [Sort], [Icon]) VALUES (N'33139891-e64f-46dc-a68d-315abd7808e4', N'毕业管理', 3, N'Order', N'Graduation', N'Index', NULL, 5, N'4fe5e89f-dda0-4b19-9c01-3d6a511d95bd', 6, N'icon-yunyingguanli')
	INSERT [dbo].[T_Permissions] ([Id], [Name], [Level], [Area], [Controller], [Action], [Param], [Classify], [ParentId], [Sort], [Icon]) VALUES (N'768149ef-3d0d-4a64-a16a-2be255be3a53', N'上传毕业照', 4, N'Order', N'Graduation', N'Option', NULL, 5, N'33139891-e64f-46dc-a68d-315abd7808e4', 1, NULL)
END
GO

--库存更名为指标
update T_Permissions set Name='设置招生指标' where Name='设置招生库存'

--学院中心增加毕业管理菜单
IF NOT EXISTS (SELECT 1 FROM T_Permissions WHERE Id='8cd58c07-80d1-4c36-9b72-766257b5130a')
BEGIN
	INSERT [dbo].[T_Permissions] ([Id], [Name], [Level], [Area], [Controller], [Action], [Param], [Classify], [ParentId], [Sort], [Icon]) VALUES (N'8cd58c07-80d1-4c36-9b72-766257b5130a', N'毕业管理', 3, N'Order', N'Graduation', N'Index', NULL, 3, N'7f3adae1-87a1-4a0f-8ac7-c6120f35832c', 4, N'icon-yunyingguanli')
	INSERT [dbo].[T_Permissions] ([Id], [Name], [Level], [Area], [Controller], [Action], [Param], [Classify], [ParentId], [Sort], [Icon]) VALUES (N'fc4d1123-2b87-4997-9f01-e49b010e2e50', N'查看毕业照', 4, N'Order', N'Graduation', N'Option', NULL, 3, N'8cd58c07-80d1-4c36-9b72-766257b5130a', 1, NULL)
END
GO

--招生机构学校配置表
IF OBJECT_ID(N'T_SchoolSetting',N'U') IS NULL
BEGIN
	CREATE TABLE [dbo].[T_SchoolSetting](
		[Id] [uniqueidentifier] NOT NULL,
		[EnterpriseId] [uniqueidentifier] NOT NULL,
		[SchoolId] [uniqueidentifier] NOT NULL,
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
	 CONSTRAINT [PK_dbo.T_SchoolSetting] PRIMARY KEY CLUSTERED 
	(
		[Id] ASC
	)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
	) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

END
GO

INSERT [dbo].[T_Permissions] ([Id], [Name], [Level], [Area], [Controller], [Action], [Param], [Classify], [ParentId], [Sort], [Icon]) VALUES (N'dbafddd7-63b8-4a8e-9fc7-2d3adf9f322b', N'配置报考学校', 4, N'Account', N'Supplier', N'SchoolConfig', NULL, 1, N'02bd4623-122a-42fa-8950-ad3d2e29f0a2', 5, NULL)
GO