--证件类型
IF COL_LENGTH('T_Order','IDCardType') IS NULL
BEGIN
	ALTER TABLE T_Order ADD IDCardType int not null default(0)
END
GO

--学历类型
IF COL_LENGTH('T_Order','DegreeType') IS NULL
BEGIN
	ALTER TABLE T_Order ADD DegreeType nvarchar(50) not null default('')
END
GO

--库存更名为指标
update T_Permissions set Name='设置招生指标' where Name='设置招生库存'