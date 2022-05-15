using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.EFContext;
using EnrolmentPlatform.Project.Domain.Entities.Orders;
using EnrolmentPlatform.Project.DTO.Enums.Basics;
using EnrolmentPlatform.Project.DTO.Enums.Orders;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.IDAL.Orders;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.DAL.Orders
{
    public class T_OrderRepository : BaseRepository<T_Order>, IT_OrderRepository
    {
        /// <summary>
        /// 新增订单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>1：成功，2：找不到当前时间段的价格策略，3：失败</returns>
        public int AddOrder(OrderDto dto)
        {
            EnrolmentPlatformDbContext dbContext = this.GetDbContext();

            #region 新增处理

            T_Order order = new T_Order()
            {
                Id = Guid.NewGuid(),
                BatchId = dto.BatchId,
                AllOrderImageUpload = false,
                AllQuDaoAmountPayed = false,
                AllZSZhongXinAmountPayed = false,
                Email = dto.Email,
                StudentName = dto.StudentName,
                IDCardType = dto.IDCardType,
                IDCardNo = dto.IDCardNo,
                Phone = dto.Phone,
                TencentNo = dto.TencentNo,
                DegreeType = dto.DegreeType,
                SchoolId = dto.SchoolId,
                LevelId = dto.LevelId,
                MajorId = dto.MajorId,
                Remark = dto.Remark,
                Status = (int)OrderStatusEnum.Init,
                FromTypeName = dto.FromTypeName,
                FromChannelId = dto.FromChannelId,
                CreatorAccount = dto.CreateUserName,
                CreatorTime = DateTime.Now,
                CreatorUserId = dto.UserId,
                DeleteTime = DateTime.MaxValue,
                DeleteUserId = Guid.Empty,
                IsDelete = false,
                LastModifyTime = DateTime.Now,
                LastModifyUserId = dto.UserId,
                Unix = DateTime.Now.ConvertDateTimeInt(),
                Address = dto.Address,
                BiYeZhengBianHao = dto.BiYeZhengBianHao,
                GongZuoDanWei = dto.GongZuoDanWei,
                GraduateSchool = dto.GraduateSchool,
                HighesDegree = dto.HighesDegree,
                JiGuan = dto.JiGuan,
                MinZu = dto.MinZu,
                Sex = dto.Sex,
                SuoDuZhuanYe = dto.SuoDuZhuanYe,
                IsTvUniversity = dto.IsTvUniversity,
                GraduationTime = dto.GraduationTime,
                CustomerField = dto.CustomerField
            };

            //添加订单
            dbContext.T_Order.Add(order);

            //添加订单图片数据
            dbContext.T_OrderImage.Add(new T_OrderImage()
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                CreatorAccount = dto.UserName,
                CreatorTime = DateTime.Now,
                CreatorUserId = dto.UserId,
                DeleteTime = DateTime.MaxValue,
                DeleteUserId = Guid.Empty,
                IsDelete = false,
                LastModifyTime = DateTime.Now,
                LastModifyUserId = dto.UserId,
                Unix = DateTime.Now.ConvertDateTimeInt()
            });

            dbContext.ModuleKey = order.Id.ToString();
            dbContext.LogChangesDuringSave = true;
            dbContext.BusinessName = "新增报名";
            return dbContext.SaveChanges() > 0 ? 1 : 3;

            #endregion
        }

        /// <summary>
        /// 修改报名单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>1：成功，2：找不到当前时间段的价格策略，3：失败</returns>
        public int UpdateOrder(OrderDto dto)
        {
            EnrolmentPlatformDbContext dbContext = this.GetDbContext();

            var entity = dbContext.T_Order.FirstOrDefault(a => a.Id == dto.OrderId.Value);
            if (entity == null)
            {
                return 3;
            }

            //if (dto.Status != (int)OrderStatusEnum.Init && dto.Status != (int)OrderStatusEnum.Reject)
            //{
            //    return 3;
            //}

            #region 更新订单信息
            entity.StudentName = dto.StudentName;
            entity.IDCardType = dto.IDCardType;
            entity.IDCardNo = dto.IDCardNo;
            entity.Phone = dto.Phone;
            entity.TencentNo = dto.TencentNo;
            entity.Email = dto.Email;
            entity.DegreeType = dto.DegreeType;
            entity.SchoolId = dto.SchoolId;
            entity.LevelId = dto.LevelId;
            entity.MajorId = dto.MajorId;
            entity.BatchId = dto.BatchId;
            entity.Remark = dto.Remark;
            entity.LastModifyUserId = dto.UserId;
            entity.LastModifyTime = DateTime.Now;
            entity.Address = dto.Address;
            entity.BiYeZhengBianHao = dto.BiYeZhengBianHao;
            entity.GongZuoDanWei = dto.GongZuoDanWei;
            entity.GraduateSchool = dto.GraduateSchool;
            entity.HighesDegree = dto.HighesDegree;
            entity.JiGuan = dto.JiGuan;
            entity.MinZu = dto.MinZu;
            entity.Sex = dto.Sex;
            entity.SuoDuZhuanYe = dto.SuoDuZhuanYe;
            entity.IsTvUniversity = dto.IsTvUniversity;
            entity.GraduationTime = dto.GraduationTime;
            entity.CustomerField = dto.CustomerField;
            if (!string.IsNullOrWhiteSpace(dto.CreateUserName))
            {
                entity.CreatorAccount = dto.CreateUserName;
            }
            dbContext.Entry(entity).State = EntityState.Modified;
            #endregion

            dbContext.ModuleKey = dto.OrderId.Value.ToString();
            dbContext.LogChangesDuringSave = true;
            dbContext.BusinessName = "修改报名单";
            return dbContext.SaveChanges() > 0 ? 1 : 3;
        }

        /// <summary>
        /// 获得报名列表
        /// </summary>
        /// <param name="req"></param>
        /// <param name="reCount"></param>
        /// <returns></returns>
        public List<OrderListDto> GetStudentList(OrderListReqDto req, ref int reCount)
        {
            if (req.DateTo.HasValue)
            {
                req.DateTo = req.DateTo.Value.AddDays(1);
            }

            var noStudentName = string.IsNullOrWhiteSpace(req.StudentName);
            var noPhone = string.IsNullOrWhiteSpace(req.Phone);
            var noIdCard = string.IsNullOrWhiteSpace(req.IDCard);
            var noCreateName = string.IsNullOrWhiteSpace(req.CreateUserName);
            EnrolmentPlatformDbContext dbContext = this.GetDbContext();
            var query = from a in dbContext.T_Order
                        join b in dbContext.T_Metadata on a.BatchId equals b.Id into btemp
                        from bbtemp in btemp.DefaultIfEmpty()
                        join c in dbContext.T_Metadata on a.SchoolId equals c.Id into ctemp
                        from cctemp in ctemp.DefaultIfEmpty()
                        join d in dbContext.T_Metadata on a.LevelId equals d.Id into dtemp
                        from ddtemp in dtemp.DefaultIfEmpty()
                        join e in dbContext.T_Metadata on a.MajorId equals e.Id into etemp
                        from eetemp in etemp.DefaultIfEmpty()
                        join f in dbContext.T_Enterprise on a.FromChannelId.Value equals f.Id into ftemp
                        from fftemp in ftemp.DefaultIfEmpty()
                        join g in dbContext.T_Enterprise on a.ToLearningCenterId.Value equals g.Id into gtemp
                        from ggtemp in gtemp.DefaultIfEmpty()
                        where a.IsDelete == false && (req.UserId.HasValue ? a.CreatorUserId == req.UserId.Value : true) &&
                        (noStudentName || a.StudentName.Contains(req.StudentName)) &&
                        (noPhone || a.Phone.Contains(req.Phone)) &&
                        (noIdCard || a.IDCardNo.Contains(req.IDCard)) &&
                        (noCreateName || a.CreatorAccount.Contains(req.CreateUserName)) &&
                        (req.DateFrom.HasValue == false || a.CreatorTime >= req.DateFrom.Value) &&
                        (req.DateTo.HasValue == false || a.CreatorTime < req.DateTo.Value) &&
                        (req.QuDaoXueFei.HasValue == false || a.AllQuDaoAmountPayed == req.QuDaoXueFei.Value) &&
                        (req.ZhaoShengXueFei.HasValue == false || a.AllZSZhongXinAmountPayed == req.ZhaoShengXueFei.Value) &&
                        (req.Status.HasValue == false || a.Status == (int)req.Status.Value) &&
                        (req.FromChannelId.HasValue == false || a.FromChannelId == req.FromChannelId.Value) &&
                        (req.AllOrderImageUpload.HasValue == false || a.AllOrderImageUpload == req.AllOrderImageUpload.Value) &&
                        (req.ToLearningCenterId.HasValue == false || a.ToLearningCenterId == req.ToLearningCenterId.Value) &&
                        (req.IsChannelAdd.HasValue == false || (a.FromChannelId.HasValue != req.IsChannelAdd.Value))
                        //(req.IsChannel.HasValue == false || (req.IsChannel.Value == true && a.FromChannelId.HasValue == true && a.Status != 0) || (req.IsChannel.Value == true && a.FromChannelId.HasValue == false))
                        select new OrderListDto()
                        {
                            AllOrderImageUpload = a.AllOrderImageUpload,
                            BatchName = bbtemp.Name,
                            CreateTime = a.CreatorTime,
                            CreateUserName = a.CreatorAccount,
                            EnrollTime = a.EnrollTime,
                            JoinTime = a.JoinTime,
                            LeaveTime = a.LeaveTime,
                            LevelName = ddtemp.Name,
                            MajorName = eetemp.Name,
                            OrderId = a.Id,
                            SchoolName = cctemp.Name,
                            Status = a.Status,
                            StudentName = a.StudentName,
                            ToLearningCenterTime = a.ToLearningCenterTime,
                            XueHao = a.StudentNo.ToString(),
                            FromChannelId = a.FromChannelId,
                            FromChannelName = fftemp.EnterpriseName.ToString(),
                            ToLearningCenterName = ggtemp.EnterpriseName.ToString(),
                            Address = a.Address,
                            BiYeZhengBianHao = a.BiYeZhengBianHao,
                            GongZuoDanWei = a.GongZuoDanWei,
                            GraduateSchool = a.GraduateSchool,
                            HighesDegree = a.HighesDegree,
                            IDCardNo = a.IDCardNo,
                            JiGuan = a.JiGuan,
                            MinZu = a.MinZu,
                            Sex = a.Sex,
                            Phone = a.Phone,
                            TencentNo = a.TencentNo,
                            Email = a.Email,
                            UserName = a.UserName.ToString(),
                            Password = a.Password,
                            AssistStatus = a.AssistStatus,
                            CustomerField = a.CustomerField,
                            GraduationTime = a.GraduationTime
                        };

            //学校
            if (!string.IsNullOrWhiteSpace(req.SchoolName))
            {
                query = query.Where(a => a.SchoolName.Contains(req.SchoolName));
            }

            //层级
            if (!string.IsNullOrWhiteSpace(req.LevelName))
            {
                query = query.Where(a => a.LevelName.Contains(req.LevelName));
            }

            //专业
            if (!string.IsNullOrWhiteSpace(req.MajorName))
            {
                query = query.Where(a => a.MajorName.Contains(req.MajorName));
            }

            //批次
            if (!string.IsNullOrWhiteSpace(req.BatchName))
            {
                query = query.Where(a => a.BatchName.Contains(req.BatchName));
            }

            if (req.AssistStatus.HasValue)
            {
                if (req.AssistStatus.Value != 0)
                {
                    query = query.Where(a => a.AssistStatus.Value == (int)req.AssistStatus.Value);
                }
                else
                {
                    query = query.Where(a => a.AssistStatus.HasValue == false);
                }
            }

            reCount = query.Count();
            if (reCount == 0)
            {
                return null;
            }

            query = ExtLinq.ApplyOrder(query, req.Field ?? "CreateTime", false);
            var orderList = query.Skip((req.Page - 1) * req.Limit).Take(req.Limit).ToList();
            var orderIds = orderList.Select(t => t.OrderId).ToList();
            var imageList = dbContext.T_OrderImage.Where(t => orderIds.Contains(t.OrderId)).ToList();
            orderList.ForEach(o =>
            {
                o.StudentImg = imageList.FirstOrDefault(t => t.OrderId == o.OrderId)?.LiangCunLanDiImg;
            });
            return orderList;
        }

        /// <summary>
        /// 获得报名照片列表
        /// </summary>
        /// <param name="req"></param>
        /// <param name="reCount"></param>
        /// <returns></returns>
        public List<OrderImageListDto> GetStudentImageList(OrderListReqDto req, ref int reCount)
        {
            if (req.DateTo.HasValue)
            {
                req.DateTo = req.DateTo.Value.AddDays(1);
            }

            StringBuilder sql = new StringBuilder(@"SELECT 
                        o.CreatorTime AS CreateTime, 
	                    o.[CreatorAccount] AS CreateUserName,
                        o.AssistStatus,
                        o.Id AS OrderId, 
                        o.Status AS[Status],
                        o.StudentName AS StudentName, 
                        m1.Name as BatchName,
	                    m2.Name as SchoolName,
	                    m3.Name as LevelName,
	                    m4.Name as MajorName,
                        im.BiYeZhengImg,im.IDCard1,im.IDCard2,
                        im.LiangCunLanDiImg,im.MianKaoJiSuanJiImg,im.MianKaoYingYuImg,
                        im.QiTa,im.TouXiang,im.XueXinWangImg
                        from T_Order AS o
                        LEFT JOIN T_Metadata AS m1 ON o.BatchId = m1.Id
                        LEFT JOIN T_Metadata AS m2 ON o.SchoolId = m2.Id
                        LEFT JOIN T_Metadata AS m3 ON o.LevelId = m3.Id
                        LEFT JOIN T_Metadata AS m4 ON o.MajorId = m4.Id
                        LEFT JOIN T_OrderImage as im ON o.Id=im.OrderId
                        where o.IsDelete=0");

            List<SqlParameter> parameters = new List<SqlParameter>();

            #region 查询条件

            if (req.UserId.HasValue)
            {
                sql.Append(" and o.CreatorUserId=@UserId");
                parameters.Add(new SqlParameter("@UserId", req.UserId.Value));
            }

            if (!string.IsNullOrWhiteSpace(req.StudentName))
            {
                sql.Append(" and o.StudentName like @StudentName");
                parameters.Add(new SqlParameter("@StudentName", "%" + req.StudentName + "%"));
            }

            if (!string.IsNullOrWhiteSpace(req.Phone))
            {
                sql.Append(" and o.Phone like @Phone");
                parameters.Add(new SqlParameter("@Phone", "%" + req.Phone + "%"));
            }

            if (!string.IsNullOrWhiteSpace(req.IDCard))
            {
                sql.Append(" and o.IDCardNo like @IDCard");
                parameters.Add(new SqlParameter("@IDCard", "%" + req.IDCard + "%"));
            }

            if (!string.IsNullOrWhiteSpace(req.CreateUserName))
            {
                sql.Append(" and o.CreatorAccount like @CreatorAccount");
                parameters.Add(new SqlParameter("@CreatorAccount", "%" + req.CreateUserName + "%"));
            }

            if (req.DateFrom.HasValue)
            {
                sql.Append(" and o.CreatorTime>=@DateFrom");
                parameters.Add(new SqlParameter("@DateFrom", req.DateFrom.Value));
            }

            if (req.DateTo.HasValue)
            {
                req.DateTo = req.DateTo.Value.AddDays(1);
                sql.Append(" and o.CreatorTime<@DateTo");
                parameters.Add(new SqlParameter("@DateTo", req.DateTo.Value));
            }

            if (req.QuDaoXueFei.HasValue)
            {
                sql.Append(" and o.AllQuDaoAmountPayed=@AllQuDaoAmountPayed");
                parameters.Add(new SqlParameter("@AllQuDaoAmountPayed", req.QuDaoXueFei.Value));
            }

            if (req.ZhaoShengXueFei.HasValue)
            {
                sql.Append(" and o.AllZSZhongXinAmountPayed=@AllZSZhongXinAmountPayed");
                parameters.Add(new SqlParameter("@AllZSZhongXinAmountPayed", req.ZhaoShengXueFei.Value));
            }

            if (req.AllOrderImageUpload.HasValue)
            {
                sql.Append(" and o.AllOrderImageUpload=@AllOrderImageUpload");
                parameters.Add(new SqlParameter("@AllOrderImageUpload", req.AllOrderImageUpload.Value));
            }

            if (req.Status.HasValue)
            {
                sql.Append(" and o.Status=@Status");
                parameters.Add(new SqlParameter("@Status", req.Status.Value));
            }

            if (req.AssistStatus.HasValue)
            {
                if (req.AssistStatus.Value != 0)
                {
                    sql.Append(" and o.AssistStatus=@AssistStatus");
                    parameters.Add(new SqlParameter("@AssistStatus", req.AssistStatus.Value));
                }
                else
                {
                    sql.Append(" and o.AssistStatus is null");
                }
            }

            if (req.FromChannelId.HasValue)
            {
                sql.Append(" and o.FromChannelId=@FromChannelId");
                parameters.Add(new SqlParameter("@FromChannelId", req.FromChannelId.Value));
            }

            if (req.ToLearningCenterId.HasValue)
            {
                sql.Append(" and o.ToLearningCenterId=@ToLearningCenterId");
                parameters.Add(new SqlParameter("@ToLearningCenterId", req.ToLearningCenterId.Value));
            }

            if (req.IsChannelAdd.HasValue)
            {
                sql.Append(" and o.FromChannelId is not null");
            }

            //if (req.IsChannel.HasValue)
            //{
            //    sql.Append(" and ((o.FromChannelId is not null and o.Status!=0) or o.FromChannelId is null)");
            //}

            //学校
            if (!string.IsNullOrWhiteSpace(req.SchoolName))
            {
                sql.Append(" and m2.Name like @SchoolName");
                parameters.Add(new SqlParameter("@SchoolName", "%" + req.SchoolName + "%"));
            }

            //层级
            if (!string.IsNullOrWhiteSpace(req.LevelName))
            {
                sql.Append(" and m3.Name like @LevelName");
                parameters.Add(new SqlParameter("@LevelName", "%" + req.LevelName + "%"));
            }

            //批次
            if (!string.IsNullOrWhiteSpace(req.BatchName))
            {
                sql.Append(" and m1.Name like @BatchName");
                parameters.Add(new SqlParameter("@BatchName", "%" + req.BatchName + "%"));
            }

            //查找订单id集合
            if (req.OrderIds != null && req.OrderIds.Count > 0)
            {
                StringBuilder sb = new StringBuilder("(");
                for (var i = 0; i < req.OrderIds.Count; i++)
                {
                    var curOrderId = req.OrderIds[i];
                    sb.Append("'" + curOrderId.ToString() + "'");
                    if (i != req.OrderIds.Count - 1)
                    {
                        sb.Append(",");
                    }
                }
                sb.Append(")");

                sql.Append(" and o.Id in " + sb.ToString());
            }

            //资料状态
            if (req.OrderImageStatus.HasValue == true)
            {
                //缺毕业证
                if (req.OrderImageStatus.Value == OrderImageStatusEnum.DefBiYeZheng)
                {
                    sql.Append(" and im.BiYeZhengImg is null");
                }

                //缺电子备案
                if (req.OrderImageStatus.Value == OrderImageStatusEnum.DefDianZiBeiAn)
                {
                    sql.Append(" and im.XueXinWangImg is null");
                }

                //缺蓝底
                if (req.OrderImageStatus.Value == OrderImageStatusEnum.DefLanDi)
                {
                    sql.Append(" and im.LiangCunLanDiImg is null");
                }

                //缺异地证明
                if (req.OrderImageStatus.Value == OrderImageStatusEnum.DefYiDiZhengMing)
                {
                    sql.Append(" and (im.MianKaoYingYuImg is null and im.MianKaoJiSuanJiImg is null)");
                }
            }

            #endregion

            EnrolmentPlatformDbContext dbContext = this.GetDbContext();
            reCount = dbContext.Database.SqlQuery<int>("select count(1) from (" + sql.ToString() + ") as t1", parameters.Select(x => ((ICloneable)x).Clone()).ToArray()).FirstOrDefault();
            if (reCount == 0)
            {
                return null;
            }

            var list = dbContext.Database.SqlQuery<OrderImageListDto>(sql.ToString(), (SqlParameter[])parameters.ToArray().Clone())
               .OrderByDescending(a => a.CreateTime)
               .Skip((req.Page - 1) * req.Limit)
               .Take(req.Limit).ToList();

            return list;
        }

        /// <summary>
        /// 获得报名缴费列表
        /// </summary>
        /// <param name="req"></param>
        /// <param name="paymentSource">支付发起方（1：招生机构，2：渠道中心）</param>
        /// <param name="reCount"></param>
        /// <returns></returns>
        public List<OrderPaymentListDto> GetStudentPaymentList(OrderListReqDto req, ref int reCount)
        {
            if (req.DateTo.HasValue)
            {
                req.DateTo = req.DateTo.Value.AddDays(1);
            }

            var noStudentName = string.IsNullOrWhiteSpace(req.StudentName);
            var noPhone = string.IsNullOrWhiteSpace(req.Phone);
            var noIdCard = string.IsNullOrWhiteSpace(req.IDCard);
            var noCreateName = string.IsNullOrWhiteSpace(req.CreateUserName);
            var noBatchName = string.IsNullOrWhiteSpace(req.BatchName);
            EnrolmentPlatformDbContext dbContext = this.GetDbContext();
            var query = from a in dbContext.T_Order
                        join b in dbContext.T_Metadata on a.BatchId equals b.Id into btemp
                        from bbtemp in btemp.DefaultIfEmpty()
                        join c in dbContext.T_Metadata on a.SchoolId equals c.Id into ctemp
                        from cctemp in ctemp.DefaultIfEmpty()
                        join d in dbContext.T_Metadata on a.LevelId equals d.Id into dtemp
                        from ddtemp in dtemp.DefaultIfEmpty()
                        join e in dbContext.T_Metadata on a.MajorId equals e.Id into etemp
                        from eetemp in etemp.DefaultIfEmpty()
                        join h in dbContext.T_Enterprise on a.ToLearningCenterId.Value equals h.Id into htemp
                        from hhtemp in htemp.DefaultIfEmpty()
                        where a.IsDelete == false && (req.UserId.HasValue ? a.CreatorUserId == req.UserId.Value : true) &&
                        (noStudentName || a.StudentName.Contains(req.StudentName)) &&
                        (noPhone || a.Phone.Contains(req.Phone)) &&
                        (noIdCard || a.IDCardNo.Contains(req.IDCard)) &&
                        (noBatchName || bbtemp.Name.Contains(req.BatchName)) &&
                        (noCreateName || a.CreatorAccount.Contains(req.CreateUserName)) &&
                        (req.DateFrom.HasValue == false || a.CreatorTime >= req.DateFrom.Value) &&
                        (req.DateTo.HasValue == false || a.CreatorTime < req.DateTo.Value) &&
                        (req.QuDaoXueFei.HasValue == false || a.AllQuDaoAmountPayed == req.QuDaoXueFei.Value) &&
                        (req.ZhaoShengXueFei.HasValue == false || a.AllZSZhongXinAmountPayed == req.ZhaoShengXueFei.Value) &&
                        //缴费单只查询已录取(req.Status.HasValue == false || a.Status == (int)req.Status.Value) &&
                        a.JoinTime.HasValue == true &&
                        (req.FromChannelId.HasValue == false || a.FromChannelId == req.FromChannelId.Value) &&
                        (req.AllOrderImageUpload.HasValue == false || a.AllOrderImageUpload == req.AllOrderImageUpload.Value) &&
                        (req.ToLearningCenterId.HasValue == false || a.ToLearningCenterId == req.ToLearningCenterId.Value) &&
                        (req.IsChannelAdd.HasValue == false || (a.FromChannelId.HasValue != req.IsChannelAdd.Value))
                        //(req.IsChannel.HasValue == false || (req.IsChannel.Value == true && a.FromChannelId.HasValue == true && a.Status != 0 && a.Status != 3) || (req.IsChannel.Value == true && a.FromChannelId.HasValue == false))
                        select new OrderPaymentListDto()
                        {
                            BatchName = bbtemp.Name,
                            CreateTime = a.CreatorTime,
                            CreateUserName = a.CreatorAccount,
                            LevelName = ddtemp.Name,
                            MajorName = eetemp.Name,
                            OrderId = a.Id,
                            SchoolName = cctemp.Name,
                            Status = a.Status,
                            StudentName = a.StudentName,
                            ToLearningCenterId = a.ToLearningCenterId.Value,
                            ToLearningCenterName = hhtemp.EnterpriseName
                        };

            //学校
            if (!string.IsNullOrWhiteSpace(req.SchoolName))
            {
                query = query.Where(a => a.SchoolName.Contains(req.SchoolName));
            }

            //层级
            if (!string.IsNullOrWhiteSpace(req.LevelName))
            {
                query = query.Where(a => a.LevelName.Contains(req.LevelName));
            }

            //层级
            if (!string.IsNullOrWhiteSpace(req.ToLearningCenterName))
            {
                query = query.Where(a => a.ToLearningCenterName.Contains(req.ToLearningCenterName));
            }

            reCount = query.Count();
            if (reCount == 0)
            {
                return null;
            }

            var list = query.OrderByDescending(a => a.CreateTime).Skip((req.Page - 1) * req.Limit).Take(req.Limit).ToList();
            var orderIdList = list.Select(t => t.OrderId).ToList();
            var orderAmountList = dbContext.T_OrderAmount.Where(t => orderIdList.Contains(t.OrderId)).ToList();
            list.ForEach(t =>
            {
                var jgAmount = orderAmountList.FirstOrDefault(a => a.OrderId == t.OrderId && a.PaymentSource == 1);
                if (jgAmount != null)
                {
                    t.ApprovalAmount = jgAmount.ApprovalAmount;
                    t.PayedAmount = jgAmount.PayedAmount;
                    t.TotalAmount = jgAmount.TotalAmount;
                }
                var qdAmount = orderAmountList.FirstOrDefault(a => a.OrderId == t.OrderId && a.PaymentSource == 2);
                if (qdAmount != null)
                {
                    t.QDApprovalAmount = qdAmount.ApprovalAmount;
                    t.QDPayedAmount = qdAmount.PayedAmount;
                    t.QDTotalAmount = qdAmount.TotalAmount;
                }
            });
            return list;
        }

        /// <summary>
        /// 修改订单金额
        /// </summary>
        /// <param name="orderId">订单ID</param>
        /// <param name="amount">金额</param>
        /// <param name="amountType">金额类型</param>
        /// <returns>1：成功，2：找不到，3：金额不能小于已经申请的金额，4失败</returns>
        public int UpdateQDAmount(Guid orderId, decimal amount, int amountType)
        {
            EnrolmentPlatformDbContext dbContext = this.GetDbContext();
            var amountEntity = dbContext.T_OrderAmount.FirstOrDefault(a => a.OrderId == orderId && a.PaymentSource == amountType);
            if (amountEntity == null)
            {
                return 2;
            }

            //如果已审核金额 + 已支付金额 > 修改的金额
            if ((amountEntity.ApprovalAmount + amountEntity.PayedAmount) > amount)
            {
                return 3;
            }

            amountEntity.TotalAmount = amount;
            dbContext.Entry(amountEntity).State = EntityState.Modified;
            dbContext.LogChangesDuringSave = false;
            dbContext.BusinessName = "修改订单金额";
            return dbContext.SaveChanges() > 0 ? 1 : 3;
        }

        /// <summary>
        /// 上传报名单
        /// </summary>
        /// <param name="list">报名单列表</param>
        /// <returns></returns>
        public string Upload(List<OrderUploadDto> list)
        {
            EnrolmentPlatformDbContext dbContext = this.GetDbContext();
            var mdata = dbContext.T_Metadata.ToList();
            //所有批次
            var batchList = mdata.Where(a => a.Type == (int)MetadataTypeEnum.Batch).ToList();
            //所有学校
            var schoolList = mdata.Where(a => a.Type == (int)MetadataTypeEnum.School).ToList();
            //所有层次
            var levelList = mdata.Where(a => a.Type == (int)MetadataTypeEnum.Level).ToList();
            //所有专业
            var majorList = mdata.Where(a => a.Type == (int)MetadataTypeEnum.Major).ToList();
            //学校配置
            var schoolConfigList = dbContext.T_SchoolLevelMajor.ToList();

            var supplierList = dbContext.T_Enterprise.ToList();
            //所有机构
            var jgList = supplierList.Where(a => a.Classify == 5).ToList();
            //所有中心
            var zxList = supplierList.Where(a => a.Classify == 3).ToList();

            //数据新增
            List<T_Order> orderList = new List<T_Order>();
            List<T_OrderAmount> orderAmountList = new List<T_OrderAmount>();
            for (int i = 0; i < list.Count; i++)
            {
                var dto = list[i];
                var batch = batchList.FirstOrDefault(a => a.Name == dto.BatchName);
                if (batch == null) { return "第" + (i + 1).ToString() + "行的批次在系统不存在！"; }

                var school = schoolList.FirstOrDefault(a => a.Name == dto.SchoolName);
                if (school == null) { return "第" + (i + 1).ToString() + "行的学校在系统不存在！"; }

                var majar = majorList.FirstOrDefault(a => a.Name == dto.MajorName);
                if (majar == null) { return "第" + (i + 1).ToString() + "行的专业在系统不存在！"; }

                var level = levelList.FirstOrDefault(a => a.Name == dto.LevelName);
                if (level == null) { return "第" + (i + 1).ToString() + "行的层次在系统不存在！"; }

                var schoolLevel = schoolConfigList.FirstOrDefault(a => a.ParentId == school.Id && a.ItemId == level.Id);
                if (schoolLevel == null) { return "第" + (i + 1).ToString() + "行的层次与学校不匹配！"; }

                var schoolMajar = schoolConfigList.FirstOrDefault(a => a.ParentId == schoolLevel.Id && a.ItemId == majar.Id);
                if (schoolMajar == null) { return "第" + (i + 1).ToString() + "行的专业与学校不匹配！"; }

                var jg = jgList.FirstOrDefault(a => a.EnterpriseName == dto.FromChannelName);
                if (jg == null) { return "第" + (i + 1).ToString() + "行的招生机构在系统不存在！"; }

                var zx = zxList.FirstOrDefault(a => a.EnterpriseName == dto.ToLearningCenterName);
                if (zx == null) { return "第" + (i + 1).ToString() + "行的学院中心在系统不存在！"; }

                #region 新增数据处理

                T_Order order = new T_Order()
                {
                    Id = Guid.NewGuid(),
                    BatchId = batch.Id,
                    AllOrderImageUpload = false,
                    AllQuDaoAmountPayed = false,
                    AllZSZhongXinAmountPayed = false,
                    Email = dto.Email,
                    StudentName = dto.StudentName,
                    IDCardNo = dto.IDCardNo,
                    Phone = dto.Phone,
                    TencentNo = dto.TencentNo,
                    SchoolId = school.Id,
                    LevelId = level.Id,
                    MajorId = majar.Id,
                    Remark = dto.Remark,
                    Status = (int)OrderStatusEnum.Join,
                    FromTypeName = dto.FromChannelName,
                    FromChannelId = jg.Id,
                    CreatorAccount = dto.CreateUserName,
                    CreatorTime = dto.CreateDate.Value,
                    CreatorUserId = Guid.Empty,
                    DeleteTime = DateTime.MaxValue,
                    DeleteUserId = Guid.Empty,
                    IsDelete = false,
                    LastModifyTime = DateTime.Now,
                    LastModifyUserId = Guid.Empty,
                    Unix = DateTime.Now.ConvertDateTimeInt(),
                    Address = dto.Address,
                    BiYeZhengBianHao = dto.BiYeZhengBianHao,
                    GongZuoDanWei = dto.GongZuoDanWei,
                    GraduateSchool = dto.GraduateSchool,
                    HighesDegree = dto.HighesDegree,
                    JiGuan = dto.JiGuan,
                    MinZu = dto.MinZu,
                    Sex = dto.Sex,
                    JoinTime = dto.LuquDate,
                    Native = dto.JiGuan,
                    Password = dto.Password,
                    StudentNo = dto.StudentNo,
                    ToLearningCenterId = zx.Id,
                    UserName = dto.UserName,
                    WorkUnit = dto.GongZuoDanWei,
                    EnrollAddress = dto.Address
                };

                var exisit = dbContext.T_Order.Count(a => a.IsDelete == false && a.BatchId == order.BatchId && a.SchoolId == order.SchoolId && a.IDCardNo == order.IDCardNo
                    && a.Status != (int)OrderStatusEnum.LeaveSchool && a.FromChannelId == jg.Id) > 0;
                if (exisit == true)
                {
                    //同一批次重复录入
                    return "第" + (i + 1).ToString() + "行的数据重复录入！";
                }

                var exisit2 = orderList.Count(a => a.BatchId == order.BatchId && a.SchoolId == order.SchoolId && a.IDCardNo == order.IDCardNo) > 0;
                if (exisit2 == true)
                {
                    //同一批次重复录入
                    return "第" + (i + 1).ToString() + "行的数据重复录入！";
                }

                orderList.Add(order);

                //添加订单（招生机构）金额数据
                T_OrderAmount jgAmount = new T_OrderAmount()
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    TotalAmount = dto.JiGouAmount,
                    PayedAmount = dto.JiGouPayedAmount,
                    ApprovalAmount = 0,
                    PaymentSource = 1,
                    CreatorAccount = dto.UserName,
                    CreatorTime = DateTime.Now,
                    CreatorUserId = Guid.Empty,
                    DeleteTime = DateTime.MaxValue,
                    DeleteUserId = Guid.Empty,
                    IsDelete = false,
                    LastModifyTime = DateTime.Now,
                    LastModifyUserId = Guid.Empty,
                    Unix = DateTime.Now.ConvertDateTimeInt()
                };

                //添加订单（学院中心）金额数据
                T_OrderAmount zxAmount = new T_OrderAmount()
                {
                    Id = Guid.NewGuid(),
                    OrderId = order.Id,
                    TotalAmount = dto.ZhongXinAmount,
                    PayedAmount = dto.ZhongXinPayedAmount,
                    ApprovalAmount = 0,
                    PaymentSource = 2,
                    CreatorAccount = dto.UserName,
                    CreatorTime = DateTime.Now,
                    CreatorUserId = Guid.Empty,
                    DeleteTime = DateTime.MaxValue,
                    DeleteUserId = Guid.Empty,
                    IsDelete = false,
                    LastModifyTime = DateTime.Now,
                    LastModifyUserId = Guid.Empty,
                    Unix = DateTime.Now.ConvertDateTimeInt()
                };

                orderAmountList.Add(jgAmount);
                orderAmountList.Add(zxAmount);

                #endregion
            }

            //新增订单
            foreach (var item in orderList)
            {
                dbContext.T_Order.Add(item);

                //添加订单图片数据
                dbContext.T_OrderImage.Add(new T_OrderImage()
                {
                    Id = Guid.NewGuid(),
                    OrderId = item.Id,
                    CreatorAccount = item.UserName,
                    CreatorTime = DateTime.Now,
                    CreatorUserId = item.CreatorUserId,
                    DeleteTime = DateTime.MaxValue,
                    DeleteUserId = Guid.Empty,
                    IsDelete = false,
                    LastModifyTime = DateTime.Now,
                    LastModifyUserId = item.CreatorUserId,
                    Unix = DateTime.Now.ConvertDateTimeInt()
                });
            }

            //新增订单金额
            foreach (var item in orderAmountList)
            {
                dbContext.T_OrderAmount.Add(item);
            }

            dbContext.LogChangesDuringSave = false;
            dbContext.SaveChanges();
            return "";
        }

        /// <summary>
        /// 录取上传
        /// </summary>
        /// <param name="list">报名单列表</param>
        /// <returns></returns>
        public string LuQuUpload(List<OrderLuQuUploadDto> list)
        {
            EnrolmentPlatformDbContext dbContext = this.GetDbContext();
            var mdata = dbContext.T_Metadata.ToList();
            //所有批次
            var batchList = mdata.Where(a => a.Type == (int)MetadataTypeEnum.Batch).ToList();
            //所有学校
            var schoolList = mdata.Where(a => a.Type == (int)MetadataTypeEnum.School).ToList();
            //所有层次
            var levelList = mdata.Where(a => a.Type == (int)MetadataTypeEnum.Level).ToList();
            //所有专业
            var majorList = mdata.Where(a => a.Type == (int)MetadataTypeEnum.Major).ToList();

            //数据校验
            for (int i = 0; i < list.Count; i++)
            {
                var dto = list[i];
                var batch = batchList.FirstOrDefault(a => a.Name == dto.BatchName);
                if (batch == null) { return "第" + (i + 2).ToString() + "行的批次在系统不存在！"; }

                var school = schoolList.FirstOrDefault(a => a.Name == dto.SchoolName);
                if (school == null) { return "第" + (i + 2).ToString() + "行的学校在系统不存在！"; }

                var level = levelList.FirstOrDefault(a => a.Name == dto.LevelName);
                if (level == null) { return "第" + (i + 2).ToString() + "行的层次在系统不存在！"; }

                var majar = majorList.FirstOrDefault(a => a.Name == dto.MajorName);
                if (majar == null) { return "第" + (i + 2).ToString() + "行的专业在系统不存在！"; }

                var order = dbContext.T_Order.FirstOrDefault(a => a.StudentName == dto.StudentName && a.IDCardNo == dto.IDCardNo && a.BatchId == batch.Id
                  && a.SchoolId == school.Id && a.LevelId == level.Id && a.MajorId == majar.Id && a.IsDelete == false);
                if (order == null)
                {
                    return "第" + (i + 2).ToString() + "行的订单在系统不存在！";
                }
                if (order.Status != (int)OrderStatusEnum.ToLearningCenter && order.Status != (int)OrderStatusEnum.Audited)
                {
                    return "第" + (i + 2).ToString() + "行的订单不是已报送或已初审状态！";
                }

                //查找录取时间段的收费策略
                var chargeStrategys = dbContext.T_ChargeStrategy.Where(t => t.SchoolId == order.SchoolId && t.LevelId == order.LevelId
                && t.MajorId == order.MajorId && ((t.InstitutionId == Guid.Empty && t.LearningCenterId == Guid.Empty) || (t.InstitutionId == order.FromChannelId && t.LearningCenterId == order.ToLearningCenterId))
                && dto.LuquDate.Value >= t.StartDate && dto.LuquDate.Value <= t.EndDate).ToList();
                if (chargeStrategys != null && chargeStrategys.Any())
                {
                    //如果收费存在则删除
                    var priceList = dbContext.T_OrderAmount.Where(a => a.OrderId == order.Id).ToList();
                    if (priceList != null && priceList.Count > 0)
                    {
                        dbContext.T_OrderAmount.RemoveRange(priceList);
                    }
                    //添加订单（招生机构）金额数据
                    var commonCharge = chargeStrategys.FirstOrDefault(t => t.InstitutionId == Guid.Empty && t.LearningCenterId == Guid.Empty);
                    var institutionCharge = chargeStrategys.FirstOrDefault(t => t.InstitutionId == order.FromChannelId);
                    var centerCharge = chargeStrategys.FirstOrDefault(t => t.LearningCenterId == order.ToLearningCenterId);

                    if (commonCharge == null && (institutionCharge == null || centerCharge == null))
                    {
                        return $"找不到通用费用策略，或渠道，中心收费策略为空[第{(i + 2)}行数据]。";
                    }

                    dbContext.T_OrderAmount.Add(new T_OrderAmount
                    {
                        Id = Guid.NewGuid(),
                        OrderId = order.Id,
                        TotalAmount = institutionCharge != null ? institutionCharge.InstitutionCharge : commonCharge.InstitutionCharge,
                        ApprovalAmount = 0,
                        PayedAmount = 0,
                        PaymentSource = 1,
                        CreatorAccount = dto.UserName,
                        CreatorTime = DateTime.Now,
                        CreatorUserId = Guid.Empty,
                        DeleteTime = DateTime.MaxValue,
                        DeleteUserId = Guid.Empty,
                        IsDelete = false,
                        LastModifyTime = DateTime.Now,
                        LastModifyUserId = Guid.Empty,
                        Unix = DateTime.Now.ConvertDateTimeInt()
                    });
                    //添加订单（学院中心）金额数据
                    dbContext.T_OrderAmount.Add(new T_OrderAmount
                    {
                        Id = Guid.NewGuid(),
                        OrderId = order.Id,
                        TotalAmount = centerCharge != null ? centerCharge.CenterCharge : commonCharge.CenterCharge,
                        ApprovalAmount = 0,
                        PayedAmount = 0,
                        PaymentSource = 2,
                        CreatorAccount = dto.UserName,
                        CreatorTime = DateTime.Now,
                        CreatorUserId = Guid.Empty,
                        DeleteTime = DateTime.MaxValue,
                        DeleteUserId = Guid.Empty,
                        IsDelete = false,
                        LastModifyTime = DateTime.Now,
                        LastModifyUserId = Guid.Empty,
                        Unix = DateTime.Now.ConvertDateTimeInt()
                    });
                }
                else
                {
                    return "第" + (i + 2).ToString() + "行的订单匹配不到收费策略！";
                }

                order.StudentNo = dto.StudentNo;
                order.UserName = dto.UserName;
                order.Password = dto.Password;
                order.JoinTime = dto.LuquDate.Value;
                order.Status = (int)OrderStatusEnum.Join;
                dbContext.Entry(order).State = EntityState.Modified;
            }
            dbContext.SaveChanges();
            return "";
        }

        /// <summary>
        /// 获得订单统计
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public OrderStatisticsDto GetOrderStatistics(OrderListReqDto req)
        {
            if (req.DateTo.HasValue)
            {
                req.DateTo = req.DateTo.Value.AddDays(1);
            }

            StringBuilder sql = new StringBuilder(@"SELECT 
                        count(1) countAll,
                        isnull(sum(case when status=0 then 1 else 0 end),0) count0,
                        isnull(sum(case when status=2 then 1 else 0 end),0) count2,
                        isnull(sum(case when status=3 then 1 else 0 end),0) count3,
                        isnull(sum(case when status=4 then 1 else 0 end),0) count4,
                        isnull(sum(case when status=5 then 1 else 0 end),0) count5,
                        isnull(sum(case when status=6 then 1 else 0 end),0) count6,
                        isnull(sum(case when status=7 then 1 else 0 end),0) count7,
                        isnull(sum(case when status=8 then 1 else 0 end),0) count8
                        from T_Order AS o
                        LEFT JOIN T_Metadata AS m1 ON o.BatchId = m1.Id
                        LEFT JOIN T_Metadata AS m2 ON o.SchoolId = m2.Id
                        LEFT JOIN T_Metadata AS m3 ON o.LevelId = m3.Id
                        where o.IsDelete=0");

            List<SqlParameter> parameters = new List<SqlParameter>();

            #region 查询条件

            if (req.UserId.HasValue)
            {
                sql.Append(" and o.CreatorUserId=@UserId");
                parameters.Add(new SqlParameter("@UserId", req.UserId.Value));
            }

            if (!string.IsNullOrWhiteSpace(req.StudentName))
            {
                sql.Append(" and o.StudentName like @StudentName");
                parameters.Add(new SqlParameter("@StudentName", "%" + req.StudentName + "%"));
            }

            if (!string.IsNullOrWhiteSpace(req.Phone))
            {
                sql.Append(" and o.Phone like @Phone");
                parameters.Add(new SqlParameter("@Phone", "%" + req.Phone + "%"));
            }

            if (!string.IsNullOrWhiteSpace(req.IDCard))
            {
                sql.Append(" and o.IDCardNo like @IDCard");
                parameters.Add(new SqlParameter("@IDCard", "%" + req.IDCard + "%"));
            }

            if (!string.IsNullOrWhiteSpace(req.CreateUserName))
            {
                sql.Append(" and o.CreatorAccount like @CreatorAccount");
                parameters.Add(new SqlParameter("@CreatorAccount", "%" + req.CreateUserName + "%"));
            }

            if (req.DateFrom.HasValue)
            {
                sql.Append(" and o.CreatorTime>=@DateFrom");
                parameters.Add(new SqlParameter("@DateFrom", req.DateFrom.Value));
            }

            if (req.DateTo.HasValue)
            {
                req.DateTo = req.DateTo.Value.AddDays(1);
                sql.Append(" and o.CreatorTime<@DateTo");
                parameters.Add(new SqlParameter("@DateTo", req.DateTo.Value));
            }

            if (req.QuDaoXueFei.HasValue)
            {
                sql.Append(" and o.AllQuDaoAmountPayed=@AllQuDaoAmountPayed");
                parameters.Add(new SqlParameter("@AllQuDaoAmountPayed", req.QuDaoXueFei.Value));
            }

            if (req.ZhaoShengXueFei.HasValue)
            {
                sql.Append(" and o.AllZSZhongXinAmountPayed=@AllZSZhongXinAmountPayed");
                parameters.Add(new SqlParameter("@AllZSZhongXinAmountPayed", req.ZhaoShengXueFei.Value));
            }

            if (req.AllOrderImageUpload.HasValue)
            {
                sql.Append(" and o.AllOrderImageUpload=@AllOrderImageUpload");
                parameters.Add(new SqlParameter("@AllOrderImageUpload", req.AllOrderImageUpload.Value));
            }

            if (req.FromChannelId.HasValue)
            {
                sql.Append(" and o.FromChannelId=@FromChannelId");
                parameters.Add(new SqlParameter("@FromChannelId", req.FromChannelId.Value));
            }

            if (req.ToLearningCenterId.HasValue)
            {
                sql.Append(" and o.ToLearningCenterId=@ToLearningCenterId");
                parameters.Add(new SqlParameter("@ToLearningCenterId", req.ToLearningCenterId.Value));
            }

            //if (req.IsChannel.HasValue)
            //{
            //    sql.Append(" and ((o.FromChannelId is not null and o.Status!=0) or o.FromChannelId is null)");
            //}

            //学校
            if (!string.IsNullOrWhiteSpace(req.SchoolName))
            {
                sql.Append(" and m2.Name like @SchoolName");
                parameters.Add(new SqlParameter("@SchoolName", "%" + req.SchoolName + "%"));
            }

            //层级
            if (!string.IsNullOrWhiteSpace(req.LevelName))
            {
                sql.Append(" and m3.Name like @LevelName");
                parameters.Add(new SqlParameter("@LevelName", "%" + req.LevelName + "%"));
            }

            //批次
            if (!string.IsNullOrWhiteSpace(req.BatchName))
            {
                sql.Append(" and m1.Name like @BatchName");
                parameters.Add(new SqlParameter("@BatchName", "%" + req.BatchName + "%"));
            }

            #endregion

            EnrolmentPlatformDbContext dbContext = this.GetDbContext();
            var dto = dbContext.Database.SqlQuery<OrderStatisticsDto>(sql.ToString(), parameters.ToArray()).FirstOrDefault();
            return dto;
        }

        /// <summary>
        /// 初审上传
        /// </summary>
        /// <param name="list">报名单列表</param>
        /// <returns></returns>
        public string AuditUpload(List<OrderAuditUploadDto> list)
        {
            EnrolmentPlatformDbContext dbContext = this.GetDbContext();
            var mdata = dbContext.T_Metadata.ToList();
            //所有批次
            var batchList = mdata.Where(a => a.Type == (int)MetadataTypeEnum.Batch).ToList();
            //所有学校
            var schoolList = mdata.Where(a => a.Type == (int)MetadataTypeEnum.School).ToList();
            //所有层次
            var levelList = mdata.Where(a => a.Type == (int)MetadataTypeEnum.Level).ToList();
            //所有专业
            var majorList = mdata.Where(a => a.Type == (int)MetadataTypeEnum.Major).ToList();

            //数据校验
            for (int i = 0; i < list.Count; i++)
            {
                var dto = list[i];
                var batch = batchList.FirstOrDefault(a => a.Name == dto.BatchName);
                if (batch == null) { return "第" + (i + 2).ToString() + "行的批次在系统不存在！"; }

                var school = schoolList.FirstOrDefault(a => a.Name == dto.SchoolName);
                if (school == null) { return "第" + (i + 2).ToString() + "行的学校在系统不存在！"; }

                var level = levelList.FirstOrDefault(a => a.Name == dto.LevelName);
                if (level == null) { return "第" + (i + 2).ToString() + "行的层次在系统不存在！"; }

                var majar = majorList.FirstOrDefault(a => a.Name == dto.MajorName);
                if (majar == null) { return "第" + (i + 2).ToString() + "行的专业在系统不存在！"; }

                var order = dbContext.T_Order.FirstOrDefault(a => a.StudentName == dto.StudentName && a.IDCardNo == dto.IDCardNo && a.BatchId == batch.Id
                  && a.SchoolId == school.Id && a.LevelId == level.Id && a.MajorId == majar.Id && a.IsDelete == false);
                if (order == null)
                {
                    return "第" + (i + 2).ToString() + "行的数据在系统不存在！";
                }
                if (order.Status != (int)OrderStatusEnum.ToLearningCenter)
                    continue;

                order.Status = (int)OrderStatusEnum.Audited;
                dbContext.Entry(order).State = EntityState.Modified;
            }
            dbContext.SaveChanges();
            return "";
        }

        /// <summary>
        /// 机构上传报名单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public string JiGouUpload(JiGouOrderUploadDto dto)
        {
            EnrolmentPlatformDbContext dbContext = this.GetDbContext();
            var mdata = dbContext.T_Metadata.ToList();
            //所有批次
            var batchList = mdata.Where(a => a.Type == (int)MetadataTypeEnum.Batch).ToList();
            //所有学校
            var schoolList = mdata.Where(a => a.Type == (int)MetadataTypeEnum.School).ToList();
            //所有层次
            var levelList = mdata.Where(a => a.Type == (int)MetadataTypeEnum.Level).ToList();
            //所有专业
            var majorList = mdata.Where(a => a.Type == (int)MetadataTypeEnum.Major).ToList();
            //学校配置
            var schoolConfigList = dbContext.T_SchoolLevelMajor.ToList();

            //数据新增
            List<T_Order> orderList = new List<T_Order>();
            for (int i = 0; i < dto.OrderUploadList.Count; i++)
            {
                var item = dto.OrderUploadList[i];
                var batch = batchList.FirstOrDefault(a => a.Name == item.BatchName);
                if (batch == null) { return "第" + (i + 1).ToString() + "行的批次在系统不存在！"; }

                var school = schoolList.FirstOrDefault(a => a.Name == item.SchoolName);
                if (school == null) { return "第" + (i + 1).ToString() + "行的学校在系统不存在！"; }

                var majar = majorList.FirstOrDefault(a => a.Name == item.MajorName);
                if (majar == null) { return "第" + (i + 1).ToString() + "行的专业在系统不存在！"; }

                var level = levelList.FirstOrDefault(a => a.Name == item.LevelName);
                if (level == null) { return "第" + (i + 1).ToString() + "行的层次在系统不存在！"; }

                var schoolLevel = schoolConfigList.FirstOrDefault(a => a.ParentId == school.Id && a.ItemId == level.Id);
                if (schoolLevel == null) { return "第" + (i + 1).ToString() + "行的层次与学校不匹配！"; }

                var schoolMajar = schoolConfigList.FirstOrDefault(a => a.ParentId == schoolLevel.Id && a.ItemId == majar.Id);
                if (schoolMajar == null) { return "第" + (i + 1).ToString() + "行的专业与学校不匹配！"; }

                #region 新增数据处理

                T_Order order = new T_Order()
                {
                    Id = Guid.NewGuid(),
                    BatchId = batch.Id,
                    AllOrderImageUpload = false,
                    AllQuDaoAmountPayed = false,
                    AllZSZhongXinAmountPayed = false,
                    Email = item.Email,
                    StudentName = item.StudentName,
                    IDCardNo = item.IDCardNo,
                    Phone = item.Phone,
                    TencentNo = item.TencentNo,
                    SchoolId = school.Id,
                    LevelId = level.Id,
                    MajorId = majar.Id,
                    Remark = item.Remark,
                    Status = (int)OrderStatusEnum.Init,
                    FromTypeName = "机构",
                    FromChannelId = dto.FromChannelId,
                    CreatorAccount = item.CreateUserName,
                    CreatorTime = item.CreateDate ?? DateTime.Now,
                    CreatorUserId = dto.CreatorUserId,
                    DeleteTime = DateTime.MaxValue,
                    DeleteUserId = Guid.Empty,
                    IsDelete = false,
                    LastModifyTime = DateTime.Now,
                    LastModifyUserId = Guid.Empty,
                    Unix = DateTime.Now.ConvertDateTimeInt(),
                    Address = item.Address,
                    BiYeZhengBianHao = item.BiYeZhengBianHao,
                    GongZuoDanWei = item.GongZuoDanWei,
                    GraduateSchool = item.GraduateSchool,
                    HighesDegree = item.HighesDegree,
                    JiGuan = item.JiGuan,
                    MinZu = item.MinZu,
                    Sex = item.Sex,
                    Native = item.JiGuan,
                    WorkUnit = item.GongZuoDanWei,
                    EnrollAddress = item.Address,
                    SuoDuZhuanYe = item.SuoDuZhuanYe,
                    IsTvUniversity = item.IsTvUniversity == "是" ? true : false,
                    GraduationTime = item.GraduationTime
                };

                var exisit = dbContext.T_Order.Count(a => a.IsDelete == false && a.BatchId == order.BatchId && a.SchoolId == order.SchoolId && a.IDCardNo == order.IDCardNo
                    && a.Status != (int)OrderStatusEnum.LeaveSchool && a.FromChannelId == dto.FromChannelId) > 0;
                if (exisit == true)
                {
                    //同一批次重复录入
                    return "第" + (i + 1).ToString() + "行的数据重复录入！";
                }

                var exisit2 = orderList.Count(a => a.BatchId == order.BatchId && a.SchoolId == order.SchoolId && a.IDCardNo == order.IDCardNo) > 0;
                if (exisit2 == true)
                {
                    //同一批次重复录入
                    return "第" + (i + 1).ToString() + "行的数据重复录入！";
                }

                orderList.Add(order);

                #endregion
            }

            //新增订单
            foreach (var item in orderList)
            {
                dbContext.T_Order.Add(item);

                //添加订单图片数据
                dbContext.T_OrderImage.Add(new T_OrderImage()
                {
                    Id = Guid.NewGuid(),
                    OrderId = item.Id,
                    CreatorAccount = item.UserName,
                    CreatorTime = DateTime.Now,
                    CreatorUserId = item.CreatorUserId,
                    DeleteTime = DateTime.MaxValue,
                    DeleteUserId = Guid.Empty,
                    IsDelete = false,
                    LastModifyTime = DateTime.Now,
                    LastModifyUserId = item.CreatorUserId,
                    Unix = DateTime.Now.ConvertDateTimeInt()
                });
            }

            dbContext.LogChangesDuringSave = false;
            dbContext.SaveChanges();
            return "";
        }

        /// <summary>
        /// 获得报名列表
        /// </summary>
        /// <param name="orderId">orderId</param>
        /// <returns></returns>
        public OrderDto GetOrder(Guid orderId)
        {
            EnrolmentPlatformDbContext dbContext = this.GetDbContext();
            var query = from a in dbContext.T_Order
                        join b in dbContext.T_Metadata on a.BatchId equals b.Id into btemp
                        from bbtemp in btemp.DefaultIfEmpty()
                        join c in dbContext.T_Metadata on a.SchoolId equals c.Id into ctemp
                        from cctemp in ctemp.DefaultIfEmpty()
                        join d in dbContext.T_Metadata on a.LevelId equals d.Id into dtemp
                        from ddtemp in dtemp.DefaultIfEmpty()
                        join e in dbContext.T_Metadata on a.MajorId equals e.Id into etemp
                        from eetemp in etemp.DefaultIfEmpty()
                        select new OrderDto()
                        {
                            AllOrderImageUpload = a.AllOrderImageUpload,
                            BatchName = bbtemp.Name,
                            CreateTime = a.CreatorTime,
                            CreateUserName = a.CreatorAccount,
                            MajorName = eetemp.Name,
                            OrderId = a.Id,
                            SchoolName = cctemp.Name,
                            Status = a.Status,
                            StudentName = a.StudentName,
                            FromChannelId = a.FromChannelId,
                            GraduationTime = a.GraduationTime,
                            LevelId = a.LevelId,
                            BatchId = a.BatchId,
                            MajorId = a.MajorId,
                            SchoolId = a.SchoolId,
                            LevelName = ddtemp.Name,
                            SuoDuZhuanYe = a.SuoDuZhuanYe,
                            WorkUnit = a.WorkUnit,
                            EnrollAddress = a.EnrollAddress,
                            IsTvUniversity = a.IsTvUniversity,
                            Native = a.Native,
                            Remark = a.Remark,
                            Address = a.Address,
                            BiYeZhengBianHao = a.BiYeZhengBianHao,
                            GongZuoDanWei = a.GongZuoDanWei,
                            GraduateSchool = a.GraduateSchool,
                            HighesDegree = a.HighesDegree,
                            IDCardNo = a.IDCardNo,
                            JiGuan = a.JiGuan,
                            MinZu = a.MinZu,
                            Sex = a.Sex,
                            Phone = a.Phone,
                            TencentNo = a.TencentNo,
                            Email = a.Email,
                            UserName = a.CreatorAccount,
                            AssistStatus = a.AssistStatus,
                            CustomerField = a.CustomerField,
                            IDCardType = a.IDCardType,
                            DegreeType = a.DegreeType,
                            Account = a.UserName.ToString(),
                            Password = a.Password
                        };
            return query.FirstOrDefault(t => t.OrderId == orderId);
        }
    }
}
