
--首页模块
insert T_Permissions values('C073E363-0784-43B1-BB47-F46EF4C21683','首页','1','','','','',1,'00000000-0000-0000-0000-000000000000',1,'icon-shouye');
insert T_Permissions values('5E8A0889-3CC5-472D-8AF7-0EEBCE809FDC','首页','3','','home','index','',1,'C073E363-0784-43B1-BB47-F46EF4C21683',1,'icon-shouye');
	insert T_Permissions values(NEWID(),'获取数据','4','','home','GetDataInfo','',1,'5E8A0889-3CC5-472D-8AF7-0EEBCE809FDC',1,'');
	insert T_Permissions values(NEWID(),'获取数据','4','','home','GetDataInfo','',1,'5E8A0889-3CC5-472D-8AF7-0EEBCE809FDC',1,'');
insert T_Permissions values('36049446-9AFC-4CBF-8805-D78C80C9FABA','系统消息','3','','home','SystemInfo','',1,'C073E363-0784-43B1-BB47-F46EF4C21683',2,'icon-xiaoxi');
	insert T_Permissions values(NEWID(),'已读','4','','home','MessageOnRead','',1,'36049446-9AFC-4CBF-8805-D78C80C9FABA',1,'');
	insert T_Permissions values(NEWID(),'列表查询','4','','home','SystemMessageList','',1,'36049446-9AFC-4CBF-8805-D78C80C9FABA',2,'');

--产品模块
insert T_Permissions values('22043DC8-9F73-40E4-AE8D-754B9539C242','产品','1','','','','',1,'00000000-0000-0000-0000-000000000000',2,'icon-chanpinpeizhi');
insert T_Permissions values('C89E8ED4-B0B1-4E84-A7F3-6885B227D4CD','农产品','2','','','','',1,'22043DC8-9F73-40E4-AE8D-754B9539C242',1,'icon-nongchanpin');
insert T_Permissions values('811B781E-64BA-4BB5-8B0D-AC35C633BA6B','农产品管理','3','Product','Specialty','index','',1,'C89E8ED4-B0B1-4E84-A7F3-6885B227D4CD',1,'');
	insert T_Permissions values(NEWID(),'审核','4','Product','Specialty','ProductOnSale','',1,'811B781E-64BA-4BB5-8B0D-AC35C633BA6B',1,'icon-chanpinshenhe');
	insert T_Permissions values(NEWID(),'下架','4','Product','Specialty','ProductOffSale','',1,'811B781E-64BA-4BB5-8B0D-AC35C633BA6B',2,'icon-chanpinxiajia');
	insert T_Permissions values(NEWID(),'删除','4','Product','Specialty','DeleteProduct','',1,'811B781E-64BA-4BB5-8B0D-AC35C633BA6B',3,'icon-chanpinxiajia');
	insert T_Permissions values(NEWID(),'查看详情','4','Product','Specialty','GetProductInfo','',1,'811B781E-64BA-4BB5-8B0D-AC35C633BA6B',4,'icon-chanpinxiajia');
insert T_Permissions values('5D9F7DC3-EF3B-4984-9186-3FE58333713A','品种管理','3','Product','Specialty','Varieties','',1,'C89E8ED4-B0B1-4E84-A7F3-6885B227D4CD',2,'');
	insert T_Permissions values(NEWID(),'添加品种','4','Product','Specialty','OptionBasicInfo','',1,'5D9F7DC3-EF3B-4984-9186-3FE58333713A',1,'icon-chanpinshenhe');
	insert T_Permissions values(NEWID(),'启用','4','Product','Specialty','VarietiesOnEnabled','',1,'5D9F7DC3-EF3B-4984-9186-3FE58333713A',2,'icon-chanpinshenhe');
	insert T_Permissions values(NEWID(),'禁用','4','Product','Specialty','VarietiesOnDisabled','',1,'5D9F7DC3-EF3B-4984-9186-3FE58333713A',3,'icon-chanpinshenhe');
	insert T_Permissions values(NEWID(),'编辑','4','Product','Specialty','OptionBasicInfo','',1,'5D9F7DC3-EF3B-4984-9186-3FE58333713A',4,'icon-chanpinshenhe');
	insert T_Permissions values(NEWID(),'删除','4','Product','Specialty','DelSpecialtyVarieties','',1,'5D9F7DC3-EF3B-4984-9186-3FE58333713A',5,'icon-chanpinshenhe');
	/*票务*/
	insert T_Permissions values('18918ED4-B0B1-4E84-A113-6885B227D411','票务','2','','','','',1,'22043DC8-9F73-40E4-AE8D-754B9539C242',2,'icon-fapiaoguanli');
	insert T_Permissions values('28918ED4-B0B1-4E84-A113-6885B227D412','票务管理','3','Product','Tickets','Index','',1,'18918ED4-B0B1-4E84-A113-6885B227D411',1,'');
	insert T_Permissions values('38918ED4-B0B1-4E84-A113-6885B227D413','景点管理','3','Product','LandMng','Index','',1,'18918ED4-B0B1-4E84-A113-6885B227D411',2,'');
	insert T_Permissions values('48918ED4-B0B1-4E84-A113-6885B227D414','游乐项目管理','3','Product','PlayMng','Index','',1,'18918ED4-B0B1-4E84-A113-6885B227D411',3,'');
	insert T_Permissions values(NEWID(),'景点编辑','4','Product','PlayMng','Option','',1,'38918ED4-B0B1-4E84-A113-6885B227D413',1,'');
	insert T_Permissions values(NEWID(),'游乐项目详情','4','Product','PlayMng','Detail','',1,'48918ED4-B0B1-4E84-A113-6885B227D414',1,'');
	 

--订单管理
insert T_Permissions values('7B5CF0BB-D4F8-4D16-8C12-2F7811690A4B','订单','1','','','','',1,'00000000-0000-0000-0000-000000000000',3,'icon-dingdan');
insert T_Permissions values('0A5F934F-76D3-44BF-9684-8FF9FCF9B29D','农产品订单','2','','','','',1,'7B5CF0BB-D4F8-4D16-8C12-2F7811690A4B',1,'icon-nongchanpin');
insert T_Permissions values('135FD225-493A-42A0-8535-C7A4D947E018','订单管理','3','Order','Specialty','index','',1,'0A5F934F-76D3-44BF-9684-8FF9FCF9B29D',1,'');
	INSERT T_Permissions values(NEWID(),'查看详情','4','Order','Specialty','Detail','',1,'135FD225-493A-42A0-8535-C7A4D947E018',1,'');
	INSERT T_Permissions values(NEWID(),'退款处理','4','Order','Specialty','ScenicHandleRefund','',1,'135FD225-493A-42A0-8535-C7A4D947E018',2,'');
insert T_Permissions values('FCE65156-3320-4AE9-91BF-E895D947FA3E','退换货订单','3','Order','Specialty','RefundChange','',1,'0A5F934F-76D3-44BF-9684-8FF9FCF9B29D',2,'');
	insert T_Permissions values(NEWID(),'查看详情','4','Order','Specialty','RefundChangeDetail','',1,'FCE65156-3320-4AE9-91BF-E895D947FA3E',1,'');
--财务管理
insert T_Permissions values('1CE30BF2-F22A-4EF7-B722-5552BB07BD45','财务','1','','','','',1,'00000000-0000-0000-0000-000000000000',4,'icon-baobiaotongji');
insert T_Permissions values('0463128A-A871-43CB-BC7F-F5AFCD6E7205','账户资产','3','Finance','Payment','index','',1,'1CE30BF2-F22A-4EF7-B722-5552BB07BD45',1,'icon-zhanghuzichan');
insert T_Permissions values('BEA6F912-D679-4875-8D1F-1583719CCDAB','结算管理','3','Finance','','Payment','SettlementCenter',1,'1CE30BF2-F22A-4EF7-B722-5552BB07BD45',2,'icon-icon-');
	insert T_Permissions values(NEWID(),'查看详情','4','Finance','Payment','SettlementCenterInfo','',1,'BEA6F912-D679-4875-8D1F-1583719CCDAB',1,'');
INSERT T_Permissions values('89F2A462-91A1-4BD6-941B-FBE49B70E548','提现管理','2','','','','',1,'1CE30BF2-F22A-4EF7-B722-5552BB07BD45',3,'icon-29');
insert T_Permissions values('CC049A29-6380-408C-BCFE-2E5BE8A6D893','提现审核','3','Finance','Withdraw','index','',1,'89F2A462-91A1-4BD6-941B-FBE49B70E548',1,'');
	INSERT T_Permissions values(NEWID(),'查看详情','4','Finance','Withdraw','Detail','',1,'CC049A29-6380-408C-BCFE-2E5BE8A6D893',1,'icon-29');
	INSERT T_Permissions values(NEWID(),'批量审核','4','Finance','Withdraw','AuditApplyCash','',1,'CC049A29-6380-408C-BCFE-2E5BE8A6D893',2,'icon-29');
insert T_Permissions values('0D4AB57C-CE5C-4EA1-ACBF-5A92E586E0DB','银行卡管理','3','Finance','Withdraw','BankCard','',1,'89F2A462-91A1-4BD6-941B-FBE49B70E548',2,'');
	INSERT T_Permissions values(NEWID(),'查看详情','4','Finance','Withdraw','BankDetail','',1,'0D4AB57C-CE5C-4EA1-ACBF-5A92E586E0DB',1,'icon-29');
insert T_Permissions values('BEA6F912-D679-4875-8D1F-1583719CCDAA','财务基础设置','3','Finance','','BasicSetting','index',1,'1CE30BF2-F22A-4EF7-B722-5552BB07BD45',4,'icon-iconset0291');
	INSERT T_Permissions values(NEWID(),'保存','4','Finance','BasicSetting','SetWithdrawRange','',1,'BEA6F912-D679-4875-8D1F-1583719CCDAA',1,'icon-29');

--用户管理
insert T_Permissions values('5D93C6B0-CFD6-4A2A-B1FE-ED14EFE80EF3','用户','1','','','','',1,'00000000-0000-0000-0000-000000000000',5,'icon-account');
insert T_Permissions values('02BD4623-122A-42FA-8950-AD3D2E29F0A2','供应商管理','3','Account','Supplier','index','',1,'5D93C6B0-CFD6-4A2A-B1FE-ED14EFE80EF3',1,'icon-zhanghuzichan');
	insert T_Permissions values(newid(),'添加/编辑供应商',4,'Account','Supplier','Option','',1,'02BD4623-122A-42FA-8950-AD3D2E29F0A2',1,'');
	insert T_Permissions values(newid(),'启用/禁用供应商',4,'Account','Supplier','UpdateSupplierStatus','',1,'02BD4623-122A-42FA-8950-AD3D2E29F0A2',2,'');
	insert T_Permissions values(newid(),'删除供应商',4,'Account','Supplier','DeleteSuppliers','',1,'02BD4623-122A-42FA-8950-AD3D2E29F0A2',3,'');
	insert T_Permissions values(newid(),'重置密码',4,'Account','Supplier','ResetPassword','',1,'02BD4623-122A-42FA-8950-AD3D2E29F0A2',4,'');

INSERT T_Permissions values('2FCFE581-196E-42A7-9CAD-C64C99A01AF8','会员管理','3','Account','Users','index','',1,'5D93C6B0-CFD6-4A2A-B1FE-ED14EFE80EF3',2,'icon-iconset0291');
	insert T_Permissions values(NEWID(),'会员管理查询','4','Account','Users','Search','',1,'2FCFE581-196E-42A7-9CAD-C64C99A01AF8',1,'');
	insert T_Permissions values(NEWID(),'启用会员','4','Account','Users','ActiveUser','',1,'2FCFE581-196E-42A7-9CAD-C64C99A01AF8',2,'');
	insert T_Permissions values(NEWID(),'禁用会员','4','Account','Users','InActiveUser','',1,'2FCFE581-196E-42A7-9CAD-C64C99A01AF8',3,'');
	insert T_Permissions values(NEWID(),'会员信息详情','4','Account','Users','Info','',1,'2FCFE581-196E-42A7-9CAD-C64C99A01AF8',4,'');

--运营
insert T_Permissions values('C5541BFA-7215-452C-BF92-97CF8E28F79E','运营','1','','','','',1,'00000000-0000-0000-0000-000000000000',6,'icon-yunyingguanli');
insert T_Permissions values('160371F6-7848-4593-BB31-B99DDE86AFE6','B2C配置','2','','','','',1,'C5541BFA-7215-452C-BF92-97CF8E28F79E',1,'icon-msnui-website');
insert T_Permissions values('7DE49C41-63DE-4059-BC3A-A84F36D3BD3A','全站配置','3','Operate','SettingB2C','Website','',1,'160371F6-7848-4593-BB31-B99DDE86AFE6',1,'');
   INSERT T_Permissions values(NEWID(),'保存','4','Operate','SettingB2C','WebsiteSet','',1,'7DE49C41-63DE-4059-BC3A-A84F36D3BD3A',1,'');
insert T_Permissions values('FAD21FE5-C2C7-4CF6-8937-09A870785CC9','首页配置','3','Operate','SettingB2C','index','',1,'160371F6-7848-4593-BB31-B99DDE86AFE6',2,'');
   INSERT T_Permissions values(NEWID(),'添加','4','Operate','SettingB2C','SaveBanner','',1,'FAD21FE5-C2C7-4CF6-8937-09A870785CC9',1,'');
   INSERT T_Permissions values(NEWID(),'删除','4','Operate','SettingB2C','DeleteBanners','',1,'FAD21FE5-C2C7-4CF6-8937-09A870785CC9',2,'');
   INSERT T_Permissions values(NEWID(),'编辑','4','Operate','SettingB2C','SaveBanner','',1,'FAD21FE5-C2C7-4CF6-8937-09A870785CC9',3,'');
insert T_Permissions values('53125D5D-AADA-44AF-8323-5B1021A137A5','广告配置','3','Operate','SettingB2C','FarmMall','',1,'160371F6-7848-4593-BB31-B99DDE86AFE6',3,'');
   INSERT T_Permissions values(NEWID(),'添加','4','Operate','SettingB2C','SaveBanner','',1,'53125D5D-AADA-44AF-8323-5B1021A137A5',1,'');
   INSERT T_Permissions values(NEWID(),'删除','4','Operate','SettingB2C','DeleteBanners','',1,'53125D5D-AADA-44AF-8323-5B1021A137A5',2,'');
   INSERT T_Permissions values(NEWID(),'编辑','4','Operate','SettingB2C','SaveBanner','',1,'53125D5D-AADA-44AF-8323-5B1021A137A5',3,'');

--内容
insert T_Permissions values('761482F5-50DA-4E87-856B-928AD676F957','内容','1','','','','',1,'00000000-0000-0000-0000-000000000000',7,'icon-wenzhang');
insert T_Permissions values('93B88E94-A7F1-4B49-AC5A-D9DC44051EA1','内容管理','2','','','','',1,'761482F5-50DA-4E87-856B-928AD676F957',1,'icon-msnui-website');
insert T_Permissions values('CFD23C40-9201-43B4-B5F7-4B5D407C9096','内容管理','3','Content','Article','Index','',1,'93B88E94-A7F1-4B49-AC5A-D9DC44051EA1',1,'');
   insert T_Permissions values(NEWID(),'发布','4','Content','Article','AddOrEditArticle','',1,'CFD23C40-9201-43B4-B5F7-4B5D407C9096',1,'');
   insert T_Permissions values(NEWID(),'删除','4','Content','Article','DeleteArticles','',1,'CFD23C40-9201-43B4-B5F7-4B5D407C9096',2,'');
   insert T_Permissions values(NEWID(),'编辑','4','Content','Article','AddOrEditArticle','',1,'CFD23C40-9201-43B4-B5F7-4B5D407C9096',3,'');
   insert T_Permissions values(NEWID(),'详情','4','Content','Article','Detail','',1,'CFD23C40-9201-43B4-B5F7-4B5D407C9096',4,'');
insert T_Permissions values('091B346D-6728-44FE-AA50-36A61441A891','栏目管理','3','Content','ArticleCategory','Index','',1,'93B88E94-A7F1-4B49-AC5A-D9DC44051EA1',2,'');
   insert T_Permissions values(NEWID(),'添加','4','Content','ArticleCategory','AddArticleCategory','',1,'091B346D-6728-44FE-AA50-36A61441A891',1,'');
   insert T_Permissions values(NEWID(),'删除','4','Content','ArticleCategory','DeleteArticleCategory','',1,'091B346D-6728-44FE-AA50-36A61441A891',2,'');
   insert T_Permissions values(NEWID(),'编辑','4','Content','ArticleCategory','UpdateArticleCategory','',1,'091B346D-6728-44FE-AA50-36A61441A891',3,'');

--设置
insert T_Permissions values('D1C8D096-706F-44FD-B24D-057754B99FE9','设置','1','','','','',1,'00000000-0000-0000-0000-000000000000',8,'icon-shezhi');
insert T_Permissions values('6785BCB2-D124-4969-8D5D-C6F028545D22','参数设置','2','','','','',1,'D1C8D096-706F-44FD-B24D-057754B99FE9',1,'icon-shezhi');
insert T_Permissions values('D25DA8E6-EC9D-4139-9B7A-184C7E852C7F','农产品参数','3','Setting','ParamForSpecialty','index','',1,'6785BCB2-D124-4969-8D5D-C6F028545D22',1,'');
	insert T_Permissions values(NEWID(),'农产品分类添加','4','Setting','ParamForSpecialty','AddProductCategories','',1,'D25DA8E6-EC9D-4139-9B7A-184C7E852C7F',1,'');
	insert T_Permissions values(NEWID(),'农产品分类编辑','4','Setting','ParamForSpecialty','ModifyProductCategories','',1,'D25DA8E6-EC9D-4139-9B7A-184C7E852C7F',2,'');
	insert T_Permissions values(NEWID(),'农产品分类删除','4','Setting','ParamForSpecialty','DelProductCategories','',1,'D25DA8E6-EC9D-4139-9B7A-184C7E852C7F',3,'');
	insert T_Permissions values(NEWID(),'农产品名称添加','4','Setting','ParamForSpecialty','AddSpecialtyName','',1,'D25DA8E6-EC9D-4139-9B7A-184C7E852C7F',4,'');
	insert T_Permissions values(NEWID(),'农产品名称编辑','4','Setting','ParamForSpecialty','ModifySpecialtyName','',1,'D25DA8E6-EC9D-4139-9B7A-184C7E852C7F',5,'');
	insert T_Permissions values(NEWID(),'农产品名称删除','4','Setting','ParamForSpecialty','DelSpecialtyName','',1,'D25DA8E6-EC9D-4139-9B7A-184C7E852C7F',6,'');
	   
	INSERT T_Permissions values(NEWID(),'单位设置添加','4','Setting','ParamForSpecialty','AddProductUnit','',1,'D25DA8E6-EC9D-4139-9B7A-184C7E852C7F',4,'');
	insert T_Permissions values(NEWID(),'单位设置编辑','4','Setting','ParamForSpecialty','ModifyProductUnit','',1,'D25DA8E6-EC9D-4139-9B7A-184C7E852C7F',5,'');
	insert T_Permissions values(NEWID(),'单位设置删除','4','Setting','ParamForSpecialty','DelProductUnit','',1,'D25DA8E6-EC9D-4139-9B7A-184C7E852C7F',6,'');
insert T_Permissions values('DB6BD2CB-5038-4180-BC7C-9887E3FA21AF','系统参数','3','Setting','ParamForSystem','index','',1,'6785BCB2-D124-4969-8D5D-C6F028545D22',2,'');

insert T_Permissions values('E156DA66-9532-492B-9DF0-AA3615FFEED9','组织架构设置','2','','','','',1,'D1C8D096-706F-44FD-B24D-057754B99FE9',2,'icon-zuzhijigou');
insert T_Permissions values('8D9FC7BA-2001-4B63-AA14-8F04CB3F8B77','部门管理','3','Setting','Department','index','',1,'E156DA66-9532-492B-9DF0-AA3615FFEED9',1,'');
	insert T_Permissions values(NEWID(),'部门管理查询','4','Setting','Department','Search','',1,'8D9FC7BA-2001-4B63-AA14-8F04CB3F8B77',1,'');
	insert T_Permissions values(NEWID(),'保存部门信息','4','Setting','Department','SaveDepartment','',1,'8D9FC7BA-2001-4B63-AA14-8F04CB3F8B77',2,'');
	insert T_Permissions values(NEWID(),'删除部门信息','4','Setting','Department','DeleteDepartment','',1,'8D9FC7BA-2001-4B63-AA14-8F04CB3F8B77',3,'');
insert T_Permissions values('8753B5E8-747C-44B0-A297-D370AC3386C4','职位管理','3','Setting','Position','index','',1,'E156DA66-9532-492B-9DF0-AA3615FFEED9',2,'');
	insert T_Permissions values(NEWID(),'职位管理查询','4','Setting','Position','Search','',1,'8753B5E8-747C-44B0-A297-D370AC3386C4',1,'');
	insert T_Permissions values(NEWID(),'保存职位管理','4','Setting','Position','SavePosition','',1,'8753B5E8-747C-44B0-A297-D370AC3386C4',2,'');
	insert T_Permissions values(NEWID(),'删除职位管理','4','Setting','Position','DeletePosition','',1,'8753B5E8-747C-44B0-A297-D370AC3386C4',3,'');
insert T_Permissions values('EF4F2018-33B7-4C19-8170-7226260799E7','角色设置','3','Setting','Roles','index','',1,'E156DA66-9532-492B-9DF0-AA3615FFEED9',3,'');
	insert T_Permissions values(NEWID(),'角色管理查询','4','Setting','Roles','Search','',1,'EF4F2018-33B7-4C19-8170-7226260799E7',1,'');
	insert T_Permissions values(NEWID(),'保存角色信息','4','Setting','Roles','SaveRole','',1,'EF4F2018-33B7-4C19-8170-7226260799E7',2,'');
	insert T_Permissions values(NEWID(),'删除角色管理','4','Setting','Roles','DeleteRole','',1,'EF4F2018-33B7-4C19-8170-7226260799E7',3,'');
	insert T_Permissions values(NEWID(),'角色信息详情','4','Setting','Roles','Option','',1,'EF4F2018-33B7-4C19-8170-7226260799E7',4,'');
insert T_Permissions values('7AF9EE4B-6FAF-4625-9A10-7D8D233561A2','员工管理','3','Setting','Staff','index','',1,'E156DA66-9532-492B-9DF0-AA3615FFEED9',4,'');
	insert T_Permissions values(NEWID(),'员工管理查询','4','Setting','Staff','Search','',1,'7AF9EE4B-6FAF-4625-9A10-7D8D233561A2',1,'');
	insert T_Permissions values(NEWID(),'保存员工信息','4','Setting','Staff','SaveUser','',1,'7AF9EE4B-6FAF-4625-9A10-7D8D233561A2',2,'');
	insert T_Permissions values(NEWID(),'删除员工信息','4','Setting','Staff','DeleteUser','',1,'7AF9EE4B-6FAF-4625-9A10-7D8D233561A2',3,'');
	insert T_Permissions values(NEWID(),'启用员工信息','4','Setting','Staff','ActiveUser','',1,'7AF9EE4B-6FAF-4625-9A10-7D8D233561A2',4,'');
	insert T_Permissions values(NEWID(),'禁用员工信息','4','Setting','Staff','InActiveUser','',1,'7AF9EE4B-6FAF-4625-9A10-7D8D233561A2',5,'');
	insert T_Permissions values(NEWID(),'员工信息详情','4','Setting','Staff','Option','',1,'7AF9EE4B-6FAF-4625-9A10-7D8D233561A2',6,'');
insert T_Permissions values('2AC3A344-B691-4F83-B0B7-9415882CA238','系统日志','2','','','','',1,'D1C8D096-706F-44FD-B24D-057754B99FE9',1,'icon-rizhi');
insert T_Permissions values('87A9FF47-360B-4371-909C-A648EB0ED82A','登陆日志管理','3','Setting','Log','Login','',1,'2AC3A344-B691-4F83-B0B7-9415882CA238',1,'');
insert T_Permissions values('445DEF6E-19A2-4D86-8836-6DEBDF5C773C','操作日志管理','3','Setting','Log','Option','',1,'2AC3A344-B691-4F83-B0B7-9415882CA238',2,'');