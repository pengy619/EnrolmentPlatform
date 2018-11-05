---销售
insert T_Permissions values('ECDDA2A5-25E3-4F36-81A2-BF9E8713F513','销售','1','','','','',5,'00000000-0000-0000-0000-000000000000',2,'icon-shouye');
insert T_Permissions values('1E4EE5B7-CE31-4201-9000-7D1281058D2E','票务销售','3','Sale','ProductForTicket','Index','',5,'ECDDA2A5-25E3-4F36-81A2-BF9E8713F513',1,'icon-shouye');
insert T_Permissions values(NEWID(),'订单支付','4','Sale','ProductForTicket','OrderPay','',5,'1E4EE5B7-CE31-4201-9000-7D1281058D2E',1,'icon-shouye');
---订单
insert T_Permissions values('4FE5E89F-DDA0-4B19-9C01-3D6A511D95BD','订单','1','','','','',5,'00000000-0000-0000-0000-000000000000',3,'icon-shouye');
insert T_Permissions values('6FC4B3FE-469F-4BAE-B2EF-1B441F6628A0','票务订单','3','Order','Ticket','Index','',5,'4FE5E89F-DDA0-4B19-9C01-3D6A511D95BD',1,'icon-shouye');
insert T_Permissions values('3E56E302-BB48-411F-ACDB-778DCBDD814D','线上订单取票','3','Order','Ticket','OnlineOrder','',5,'4FE5E89F-DDA0-4B19-9C01-3D6A511D95BD',2,'icon-shouye');
insert T_Permissions values('E2C08E85-9B49-4E27-B876-0F53C6992CE7','票务核销','3','Order','VerificationTicket','Index','',5,'4FE5E89F-DDA0-4B19-9C01-3D6A511D95BD',3,'icon-shouye');

---财务
insert T_Permissions values('9C674F40-CDE7-4A29-89FF-FA61CD1563AC','财务','1','','','','',5,'00000000-0000-0000-0000-000000000000',4,'icon-shouye');
insert T_Permissions values('02922657-5DE0-4CFF-954D-E4880AA72BF4','账户明细','3','Finance','Payment','Index','',5,'9C674F40-CDE7-4A29-89FF-FA61CD1563AC',1,'icon-shouye');

---设置
insert T_Permissions values('1F2F4646-9414-4994-883F-9195A8706EE9','设置','1','','','','',5,'00000000-0000-0000-0000-000000000000',5,'icon-shouye');
insert T_Permissions values('2BF5F993-B952-4DD8-BDCF-8F69449F666A','系统管理','2','','','','',5,'1F2F4646-9414-4994-883F-9195A8706EE9',1,'icon-shouye');
insert T_Permissions values('E369D208-6DF2-4DA8-BBE1-485C4A99AB44','角色管理','3','Setting','Roles','List','',5,'2BF5F993-B952-4DD8-BDCF-8F69449F666A',1,'icon-shouye');
	insert T_Permissions values(NEWID(),'角色详情','4','Setting','Roles','Option','',5,'E369D208-6DF2-4DA8-BBE1-485C4A99AB44',1,'');
	insert T_Permissions values(NEWID(),'角色列表查询','4','Setting','Roles','Search','',5,'E369D208-6DF2-4DA8-BBE1-485C4A99AB44',2,'');
	insert T_Permissions values(NEWID(),'角色保存','4','Setting','Roles','SaveRole','',5,'E369D208-6DF2-4DA8-BBE1-485C4A99AB44',3,'');
	insert T_Permissions values(NEWID(),'删除角色','4','Setting','Roles','DeleteRole','',5,'E369D208-6DF2-4DA8-BBE1-485C4A99AB44',4,'');
insert T_Permissions values('F9073A17-8652-45CD-86AC-1E8729B3D44A','子账户管理','3','Setting','AccountManage','List','',5,'2BF5F993-B952-4DD8-BDCF-8F69449F666A',2,'icon-shouye');
	insert T_Permissions values(NEWID(),'子账户列表查询','4','Setting','AccountManage','Search','',5,'F9073A17-8652-45CD-86AC-1E8729B3D44A',1,'');
	insert T_Permissions values(NEWID(),'子账户详情','4','Setting','AccountManage','Option','',5,'F9073A17-8652-45CD-86AC-1E8729B3D44A',2,'');
	insert T_Permissions values(NEWID(),'保存用户信息','4','Setting','AccountManage','SaveUser','',5,'F9073A17-8652-45CD-86AC-1E8729B3D44A',3,'');
	insert T_Permissions values(NEWID(),'删除用户','4','Setting','AccountManage','DeleteUser','',5,'F9073A17-8652-45CD-86AC-1E8729B3D44A',4,'');
insert T_Permissions values('11186A2D-D3C7-48B9-B446-0F5114E15CB6','登陆密码修改','3','Setting','AccountManage','ModifyForPwd','',5,'2BF5F993-B952-4DD8-BDCF-8F69449F666A',3,'icon-shouye');
	insert T_Permissions values(NEWID(),'发送验证码','4','Setting','AccountManage','SendVeriyCode','',5,'11186A2D-D3C7-48B9-B446-0F5114E15CB6',1,'');
	insert T_Permissions values(NEWID(),'修改密码','4','Setting','AccountManage','ModifyForPwdOp','',5,'11186A2D-D3C7-48B9-B446-0F5114E15CB6',2,'');

--日志
insert T_Permissions values('C6509DDD-02E7-47D7-B8F9-1BBE2D37FADF','日志/消息','2','','','','',5,'1F2F4646-9414-4994-883F-9195A8706EE9',2,'icon-shouye');
insert T_Permissions values('CB384189-EF28-4BC8-BDB7-65473F43D636','登陆日志管理','3','Setting','Log','Login','',5,'C6509DDD-02E7-47D7-B8F9-1BBE2D37FADF',1,'icon-shouye');
insert T_Permissions values('C05FCBB7-4071-481D-8511-7C846627327F','操作日志管理','3','Setting','Log','Option','',5,'C6509DDD-02E7-47D7-B8F9-1BBE2D37FADF',2,'icon-shouye');