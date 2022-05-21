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

--库存更名为指标
update T_Permissions set Name='设置招生指标' where Name='设置招生库存'
--添加学员账号菜单
IF NOT EXISTS (SELECT 1 FROM T_Permissions WHERE Id='10c2cffe-e3a0-4895-9199-0875416bd70f')
BEGIN
	INSERT [dbo].[T_Permissions] ([Id], [Name], [Level], [Area], [Controller], [Action], [Param], [Classify], [ParentId], [Sort], [Icon]) VALUES (N'10c2cffe-e3a0-4895-9199-0875416bd70f', N'学员账号', 3, N'Order', N'Manager', N'Account', NULL, 1, N'7b5cf0bb-d4f8-4d16-8c12-2f7811690a4b', 2, N'icon-iconset0291')
	INSERT [dbo].[T_Permissions] ([Id], [Name], [Level], [Area], [Controller], [Action], [Param], [Classify], [ParentId], [Sort], [Icon]) VALUES (N'b75618b6-47dc-4b83-a6f9-e14d7accdda2', N'学员账号', 3, N'Order', N'Manager', N'Account', NULL, 5, N'4fe5e89f-dda0-4b19-9c01-3d6a511d95bd', 2, N'icon-iconset0291')
END
GO