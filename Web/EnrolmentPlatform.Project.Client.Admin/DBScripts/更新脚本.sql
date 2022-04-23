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