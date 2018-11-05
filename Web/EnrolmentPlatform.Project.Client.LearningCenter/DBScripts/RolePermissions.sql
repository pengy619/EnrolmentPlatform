------供应商端-------
--模块
insert T_Permissions values('D55D324E-2C9C-4836-A7F1-D3B652C55568','首页',1,'','Home','Index','',3,'00000000-0000-0000-0000-000000000000',1,'icon-shouye');
insert T_Permissions values('0B5B7CC1-CB39-409C-9A27-36A590D97AEA','产品',1,'Product','Specialty','Index','',3,'00000000-0000-0000-0000-000000000000',2,'icon-chanpinpeizhi');
insert T_Permissions values('7F3ADAE1-87A1-4A0F-8AC7-C6120F35832C','订单',1,'Order','Specialty','Index','',3,'00000000-0000-0000-0000-000000000000',3,'icon-dingdan');
insert T_Permissions values('8BE8E8F3-E1AB-4202-B64B-B7D0C94B716F','财务',1,'Finance','Payment','Index','',3,'00000000-0000-0000-0000-000000000000',4,'icon-baobiaotongji');
insert T_Permissions values('8BEF0073-0C7D-4670-9EF7-CF5980719396','营销',1,'','#','#','',3,'00000000-0000-0000-0000-000000000000',5,'icon-money');
insert T_Permissions values('E9E5B61D-31AC-4A5E-BB92-349C66331502','设置',1,'Setting','AccountManage','Info','',3,'00000000-0000-0000-0000-000000000000',6,'icon-shezhi');

--首页菜单
insert T_Permissions values('3FAC6E21-908F-4CD9-9E6A-4D6804669817','首页',3,'','Home','Index','',3,'D55D324E-2C9C-4836-A7F1-D3B652C55568',1,'icon-shouye');
insert T_Permissions values('0806A4C1-AE29-4CDD-90C4-DA22444E41A0','系统消息',3,'','Home','SystemInfo','',3,'D55D324E-2C9C-4836-A7F1-D3B652C55568',2,'icon-xiaoxi');
	insert T_Permissions values(newid(),'系统消息列表查询',4,'','Home','SystemMessageList','',3,'0806A4C1-AE29-4CDD-90C4-DA22444E41A0',1,'');
	insert T_Permissions values(newid(),'系统消息已读处理',4,'','Home','MessageOnRead','',3,'0806A4C1-AE29-4CDD-90C4-DA22444E41A0',2,'');
--产品菜单
	insert T_Permissions values('E517C674-2406-4A92-AB86-42453C8AF32B','农产品管理',3,'Product','Specialty','Index','',3,'0B5B7CC1-CB39-409C-9A27-36A590D97AEA',1,'icon-nongchanpin');
	insert T_Permissions values(newid(),'添加农产品/编辑',4,'Product','Specialty','Option','',3,'E517C674-2406-4A92-AB86-42453C8AF32B',1,'');
	insert T_Permissions values(newid(),'申请上架',4,'Product','Specialty','ProductOnSale','',3,'E517C674-2406-4A92-AB86-42453C8AF32B',2,'');
	insert T_Permissions values(newid(),'下架',4,'Product','Specialty','ProductOffSale','',3,'E517C674-2406-4A92-AB86-42453C8AF32B',3,'');
	insert T_Permissions values(newid(),'删除',4,'Product','Specialty','DeleteProduct','',3,'E517C674-2406-4A92-AB86-42453C8AF32B',4,'');
	insert T_Permissions values(newid(),'查看',4,'Product','Specialty','Detail','',3,'E517C674-2406-4A92-AB86-42453C8AF32B',5,'');
	insert T_Permissions values(newid(),'复制',4,'Product','Specialty','CopyProduct','',3,'E517C674-2406-4A92-AB86-42453C8AF32B',6,'');
	insert T_Permissions values(newid(),'上市',4,'Product','Specialty','ProductToMarket','',3,'E517C674-2406-4A92-AB86-42453C8AF32B',7,'');
	insert T_Permissions values(newid(),'列表查询',4,'Product','Specialty','ProductList','',3,'E517C674-2406-4A92-AB86-42453C8AF32B',8,'');
	--游乐项目
	insert T_Permissions values('9D82E6AA-9C37-4A77-A5B9-3980BE37D98D','游乐项目管理',3,'Product','PlayMng','Index','',3,'0B5B7CC1-CB39-409C-9A27-36A590D97AEA',2,'icon-youleyuan');
	insert T_Permissions values(newid(),'添加/编辑游乐项目',4,'Product','PlayMng','Option','',3,'3482E6A5-9C37-4A77-A5B9-3980BE37D934',1,'');
	insert T_Permissions values(newid(),'删除',4,'Product','PlayMng','DelPlayMng','',3,'3482E6A5-9C37-4A77-A5B9-3980BE37D934',2,'');
	insert T_Permissions values(newid(),'查看',4,'Product','PlayMng','Detail','',3,'3482E6A5-9C37-4A77-A5B9-3980BE37D934',3,'');
	insert T_Permissions values(newid(),'游乐项目列表查询',4,'Product','PlayMng','PlayList','',3,'3482E6A5-9C37-4A77-A5B9-3980BE37D934',4,'');
	--票务管理
	insert T_Permissions values('3482E6A5-9C37-4A77-A5B9-3980BE37D934','票务管理',3,'Product','Ticket','Index','',3,'0B5B7CC1-CB39-409C-9A27-36A590D97AEA',3,'icon-youleyuan');
	insert T_Permissions values(newid(),'添加/编辑票务',4,'Product','Ticket','Option','',3,'3482E6A5-9C37-4A77-A5B9-3980BE37D934',1,'');
	insert T_Permissions values(newid(),'上架',4,'Product','Ticket','TicketOnSale','',3,'3482E6A5-9C37-4A77-A5B9-3980BE37D934',2,'');
	insert T_Permissions values(newid(),'下架',4,'Product','Ticket','TicketOffSale','',3,'3482E6A5-9C37-4A77-A5B9-3980BE37D934',3,'');
	insert T_Permissions values(newid(),'删除',4,'Product','Ticket','DeleteTicket','',3,'3482E6A5-9C37-4A77-A5B9-3980BE37D934',4,'');
	insert T_Permissions values(newid(),'查看',4,'Product','Ticket','Detail','',3,'3482E6A5-9C37-4A77-A5B9-3980BE37D934',5,'');
	insert T_Permissions values(newid(),'复制',4,'Product','Ticket','CopyTicket','',3,'3482E6A5-9C37-4A77-A5B9-3980BE37D934',6,''); 
	insert T_Permissions values(newid(),'班期日历',4,'Product','Inventory','Ticket','',3,'3482E6A5-9C37-4A77-A5B9-3980BE37D934',7,''); 


--订单菜单
insert T_Permissions values('989808E0-4FB9-4FAD-9EB6-A8BF92132B43','农产品订单',3,'Order','Specialty','Index','',3,'7F3ADAE1-87A1-4A0F-8AC7-C6120F35832C',1,'icon-nongchanpin');
	insert T_Permissions values(newid(),'查看',4,'Order','Specialty','Detail','',3,'989808E0-4FB9-4FAD-9EB6-A8BF92132B43',1,'');
insert T_Permissions values('989808E0-4FB9-4FAD-9EB6-A8BF92132B44','退换货订单',3,'Order','Specialty','RefundChange','',3,'7F3ADAE1-87A1-4A0F-8AC7-C6120F35832C',2,'icon-nongchanpin');
	insert T_Permissions values(newid(),'查看',4,'Order','Specialty','RefundChangeDetail','',3,'989808E0-4FB9-4FAD-9EB6-A8BF92132B44',1,'');
--财务菜单
insert T_Permissions values('1E2D6017-72E3-4D39-A477-AA66342E5212','账户资产',3,'Finance','Payment','Index','',3,'8BE8E8F3-E1AB-4202-B64B-B7D0C94B716F',1,'icon-zhanghuzichan');
	insert T_Permissions values(newid(),'提现',4,'Finance','WithDraw','Option','',3,'1E2D6017-72E3-4D39-A477-AA66342E5212',1,'');
	insert T_Permissions values(newid(),'收支明细列表',4,'Finance','Payment','GetAccountDetailInfoList','',3,'1E2D6017-72E3-4D39-A477-AA66342E5212',2,'');
insert T_Permissions values('3CC7FAAF-D71A-4403-ADF4-FF6A790D2E64','结算中心',3,'Finance','Payment','SettlementCenter','',3,'8BE8E8F3-E1AB-4202-B64B-B7D0C94B716F',2,'icon-29');
	insert T_Permissions values(newid(),'查看',4,'Finance','Payment','SettlementsDetails','',3,'3CC7FAAF-D71A-4403-ADF4-FF6A790D2E64',1,'');
	insert T_Permissions values(newid(),'获取待结算单',4,'Finance','Payment','GetPendingOrderInfoList','',3,'3CC7FAAF-D71A-4403-ADF4-FF6A790D2E64',2,'');
	insert T_Permissions values(newid(),'获取结算单',4,'Finance','Payment','GetOrderSettlement','',3,'3CC7FAAF-D71A-4403-ADF4-FF6A790D2E64',3,'');
insert T_Permissions values('FB46A04A-A360-4950-BB69-780EFE539414','提现管理',3,'Finance','Withdraw','Index','',3,'8BE8E8F3-E1AB-4202-B64B-B7D0C94B716F',3,'icon-29');
	insert T_Permissions values(newid(),'提现',4,'Finance','WithDraw','Option','',3,'FB46A04A-A360-4950-BB69-780EFE539414',1,'');
	insert T_Permissions values(newid(),'查看',4,'Finance','WithDraw','Detail','',3,'FB46A04A-A360-4950-BB69-780EFE539414',2,'');
	insert T_Permissions values(newid(),'获取提现列表',4,'Finance','WithDraw','GetApplyCashList','',3,'FB46A04A-A360-4950-BB69-780EFE539414',3,'');
insert T_Permissions values('4631F6A6-7E0B-42DE-A8AD-00E3F0783299','银行卡管理',3,'Finance','BankCard','Index','',3,'8BE8E8F3-E1AB-4202-B64B-B7D0C94B716F',4,'icon-iconset0291');
	insert T_Permissions values(newid(),'添加新银行卡',4,'Finance','BankCard','Option','',3,'4631F6A6-7E0B-42DE-A8AD-00E3F0783299',1,'');
	insert T_Permissions values(newid(),'解绑',4,'Finance','BankCard','UnbindingBankCard','',3,'4631F6A6-7E0B-42DE-A8AD-00E3F0783299',2,'');
insert T_Permissions values('2A2D169D-E55F-45EC-BF63-0DB8ECCE3A24','提现密码设置',3,'Finance','Withdraw','Password','',3,'8BE8E8F3-E1AB-4202-B64B-B7D0C94B716F',5,'icon-mima');
	insert T_Permissions values(newid(),'确认设置',4,'Finance','Withdraw','Password','',3,'2A2D169D-E55F-45EC-BF63-0DB8ECCE3A24',1,'');
insert T_Permissions values('76D6C13B-F615-4096-BD1D-B71A36BB71BF','发票管理',3,'Finance','Invoice','Index','',3,'8BE8E8F3-E1AB-4202-B64B-B7D0C94B716F',6,'icon-fapiaoguanli');
--营销菜单
--设置菜单
insert T_Permissions values('DD4D6696-3010-4446-9D05-A7D311AC7918','账户管理',2,'','','','',3,'E9E5B61D-31AC-4A5E-BB92-349C66331502',1,'icon-account');
insert T_Permissions values('778A2045-9701-4A60-9ADC-7577D2377C42','账户信息',3,'Setting','AccountManage','Info','',3,'DD4D6696-3010-4446-9D05-A7D311AC7918',1,'icon-account');
insert T_Permissions values('0E638319-6A63-45E4-8592-3E9704C8BB78','角色设置',3,'Setting','Roles','List','',3,'DD4D6696-3010-4446-9D05-A7D311AC7918',2,'icon-account');
	insert T_Permissions values(newid(),'角色详情',4,'Setting','Roles','Option','',3,'0E638319-6A63-45E4-8592-3E9704C8BB78',1,'');
	insert T_Permissions values(newid(),'列表查询',4,'Setting','Roles','Search','',3,'0E638319-6A63-45E4-8592-3E9704C8BB78',2,'');
	insert T_Permissions values(newid(),'角色保存',4,'Setting','Roles','SaveRole','',3,'0E638319-6A63-45E4-8592-3E9704C8BB78',3,'');
	insert T_Permissions values(newid(),'删除角色',4,'Setting','Roles','DeleteRole','',3,'0E638319-6A63-45E4-8592-3E9704C8BB78',4,'');
insert T_Permissions values('562E78D9-7FD1-4169-9594-23B8902412BC','子账户管理',3,'Setting','ChildAccount','List','',3,'DD4D6696-3010-4446-9D05-A7D311AC7918',3,'icon-account');
	insert T_Permissions values(newid(),'子账号详情',4,'Setting','ChildAccount','Option','',3,'562E78D9-7FD1-4169-9594-23B8902412BC',1,'');
	insert T_Permissions values(newid(),'保存子账号信息',4,'Setting','ChildAccount','SaveUser','',3,'562E78D9-7FD1-4169-9594-23B8902412BC',2,'');
	insert T_Permissions values(newid(),'删除用户',4,'Setting','ChildAccount','DeleteUser','',3,'562E78D9-7FD1-4169-9594-23B8902412BC',3,'');
	insert T_Permissions values(newid(),'列表查询',4,'Setting','ChildAccount','Search','',3,'562E78D9-7FD1-4169-9594-23B8902412BC',4,'');
insert T_Permissions values('14A92D3B-F425-4E6B-BD91-F4AB5777EED9','登录密码修改',3,'Setting','AccountManage','ModifyForPwd','',3,'DD4D6696-3010-4446-9D05-A7D311AC7918',4,'icon-account');
	insert T_Permissions values(newid(),'发送验证码',4,'Setting','AccountManage','SendVeriyCode','',3,'14A92D3B-F425-4E6B-BD91-F4AB5777EED9',1,'');
	insert T_Permissions values(newid(),'修改密码',4,'Setting','AccountManage','ModifyForPwdOp','',3,'14A92D3B-F425-4E6B-BD91-F4AB5777EED9',2,'');
insert T_Permissions values('4FADBB5C-863E-42CF-8A38-B622464483FD','系统日志',2,'','','','',3,'E9E5B61D-31AC-4A5E-BB92-349C66331502',1,'icon-rizhi');
insert T_Permissions values('E0255F0A-4FD7-4D34-8E52-662C2B236922','登录日志管理',3,'Setting','Log','Login','',3,'4FADBB5C-863E-42CF-8A38-B622464483FD',1,'icon-rizhi');
	insert T_Permissions values(newid(),'登录日志管理查询',4,'Setting','Log','SearchLoginLog','',3,'E0255F0A-4FD7-4D34-8E52-662C2B236922',1,'');
insert T_Permissions values('BCE6C8E1-EB85-4B48-AAC8-A29A3063679B','操作日志管理',3,'Setting','Log','Option','',3,'4FADBB5C-863E-42CF-8A38-B622464483FD',2,'icon-rizhi');
	insert T_Permissions values(newid(),'操作日志管理查询',4,'Setting','Log','SearchOptionLog','',3,'BCE6C8E1-EB85-4B48-AAC8-A29A3063679B',1,'');
