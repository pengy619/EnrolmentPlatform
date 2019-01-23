using System;
using System.Collections.Generic;
using System.Data.Entity;
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

            //价格策略检查
            var nowDate = DateTime.Now.Date;
            var chargeStrategy = dbContext.T_ChargeStrategy.FirstOrDefault(a => a.SchoolId == dto.SchoolId && a.LevelId == dto.LevelId
            && a.MajorId == dto.MajorId && a.InstitutionId == Guid.Empty
            && nowDate >= a.StartDate && nowDate <= a.EndDate);
            if (chargeStrategy == null)
            {
                //找不到当前时间段的价格策略
                return 2;
            }

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
                IDCardNo = dto.IDCardNo,
                Phone = dto.Phone,
                TencentNo = dto.TencentNo,
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
                Sex = dto.Sex
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

            //添加订单（招生机构）金额数据
            var insChargeStrategy = dbContext.T_ChargeStrategy.FirstOrDefault(a => a.SchoolId == dto.SchoolId && a.LevelId == dto.LevelId
            && a.MajorId == dto.MajorId && a.InstitutionId == dto.FromChannelId
            && nowDate >= a.StartDate && nowDate <= a.EndDate);
            dbContext.T_OrderAmount.Add(new T_OrderAmount()
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                TotalAmount = insChargeStrategy == null ? chargeStrategy.InstitutionCharge : insChargeStrategy.InstitutionCharge, //如果机构未设置费用策略则取通用的
                ApprovalAmount = 0,
                PayedAmount = 0,
                PaymentSource = 1,
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

            //添加订单（学院中心）金额数据
            dbContext.T_OrderAmount.Add(new T_OrderAmount()
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                TotalAmount = chargeStrategy.CenterCharge,
                ApprovalAmount = 0,
                PayedAmount = 0,
                PaymentSource = 2,
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
        /// <returns>1：成功，2：找不到当前时间段的价格策略，3：失败，4：同一批次重复录入</returns>
        public int UpdateOrder(OrderDto dto)
        {
            EnrolmentPlatformDbContext dbContext = this.GetDbContext();
            //价格策略检查
            var nowDate = DateTime.Now.Date;
            var chargeStrategy = dbContext.T_ChargeStrategy.FirstOrDefault(a => a.SchoolId == dto.SchoolId && a.LevelId == dto.LevelId
            && a.MajorId == dto.MajorId && a.InstitutionId == Guid.Empty
            && nowDate >= a.StartDate && nowDate <= a.EndDate);
            if (chargeStrategy == null)
            {
                //找不到当前时间段的价格策略
                return 2;
            }

            //修改订单
            var entity = dbContext.T_Order.FirstOrDefault(a => a.Id == dto.OrderId.Value);
            entity.StudentName = dto.StudentName;
            entity.IDCardNo = dto.IDCardNo;
            entity.Phone = dto.Phone;
            entity.TencentNo = dto.TencentNo;
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
            if (!string.IsNullOrWhiteSpace(dto.CreateUserName))
            {
                entity.CreatorAccount = dto.CreateUserName;
            }
            dbContext.Entry(entity).State = EntityState.Modified;

            //删除价格
            var priceList = dbContext.T_OrderAmount.Where(a => a.OrderId == dto.OrderId.Value).ToList();
            if (priceList != null && priceList.Count > 0)
            {
                foreach (var item in priceList)
                {
                    dbContext.T_OrderAmount.Remove(item);
                }
            }

            //添加订单（招生机构）金额数据
            var insChargeStrategy = dbContext.T_ChargeStrategy.FirstOrDefault(a => a.SchoolId == dto.SchoolId && a.LevelId == dto.LevelId
            && a.MajorId == dto.MajorId && a.InstitutionId == entity.FromChannelId
            && nowDate >= a.StartDate && nowDate <= a.EndDate);
            dbContext.T_OrderAmount.Add(new T_OrderAmount()
            {
                Id = Guid.NewGuid(),
                OrderId = dto.OrderId.Value,
                TotalAmount = insChargeStrategy == null ? chargeStrategy.InstitutionCharge : insChargeStrategy.InstitutionCharge,
                ApprovalAmount = 0,
                PayedAmount = 0,
                PaymentSource = 1,
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

            //添加订单（学院中心）金额数据
            dbContext.T_OrderAmount.Add(new T_OrderAmount()
            {
                Id = Guid.NewGuid(),
                OrderId = dto.OrderId.Value,
                TotalAmount = chargeStrategy.CenterCharge,
                ApprovalAmount = 0,
                PayedAmount = 0,
                PaymentSource = 2,
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
                        where a.IsDelete == false && (noStudentName || a.StudentName.Contains(req.StudentName)) &&
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
                        (req.IsChannelAdd.HasValue == false || (a.FromChannelId.HasValue != req.IsChannelAdd.Value)) &&
                        (req.IsChannel.HasValue == false || (req.IsChannel.Value == true && a.FromChannelId.HasValue == true && a.Status != 0 && a.Status != 3) || (req.IsChannel.Value == true && a.FromChannelId.HasValue == false))
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
                            XueHao = a.StudentNo,
                            FromChannelId = a.FromChannelId,
                            FromChannelName = fftemp.EnterpriseName,
                            ToLearningCenterName = ggtemp.EnterpriseName,
                            Address = a.Address,
                            BiYeZhengBianHao = a.BiYeZhengBianHao,
                            GongZuoDanWei = a.GongZuoDanWei,
                            GraduateSchool = a.GraduateSchool,
                            HighesDegree = a.HighesDegree,
                            IDCardNo = a.IDCardNo,
                            JiGuan = a.JiGuan,
                            MinZu = a.MinZu,
                            Sex = a.Sex
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

            //批次
            if (!string.IsNullOrWhiteSpace(req.BatchName))
            {
                query = query.Where(a => a.BatchName.Contains(req.BatchName));
            }

            reCount = query.Count();
            if (reCount == 0)
            {
                return null;
            }

            return query.OrderByDescending(a => a.CreateTime).Skip((req.Page - 1) * req.Limit).Take(req.Limit).ToList();
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
                        join f in dbContext.T_OrderImage on a.Id equals f.OrderId into ftemp
                        from fftemp in ftemp.DefaultIfEmpty()
                        where a.IsDelete == false && (noStudentName || a.StudentName.Contains(req.StudentName)) &&
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
                        (req.IsChannelAdd.HasValue == false || (a.FromChannelId.HasValue != req.IsChannelAdd.Value)) &&
                        (req.IsChannel.HasValue == false || (req.IsChannel.Value == true && a.FromChannelId.HasValue == true && a.Status != 0 && a.Status != 3) || (req.IsChannel.Value == true && a.FromChannelId.HasValue == false))
                        select new OrderImageListDto()
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
                            BiYeZhengImg = fftemp.BiYeZhengImg,
                            IDCard1 = fftemp.IDCard1,
                            IDCard2 = fftemp.IDCard2,
                            LiangCunLanDiImg = fftemp.LiangCunLanDiImg,
                            MianKaoJiSuanJiImg = fftemp.MianKaoJiSuanJiImg,
                            MianKaoYingYuImg = fftemp.MianKaoYingYuImg,
                            QiTa = fftemp.QiTa,
                            TouXiang = fftemp.TouXiang,
                            XueXinWangImg = fftemp.XueXinWangImg
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

            //查找订单id集合
            if (req.OrderIds != null && req.OrderIds.Count > 0)
            {
                query = query.Where(a => req.OrderIds.Contains(a.OrderId));
            }

            reCount = query.Count();
            if (reCount == 0)
            {
                return null;
            }

            return query.OrderByDescending(a => a.CreateTime).Skip((req.Page - 1) * req.Limit).Take(req.Limit).ToList();
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
                        join f in dbContext.T_OrderAmount on new { OrderId = a.Id, PaymentSource = 1 } equals new { OrderId = f.OrderId, PaymentSource = f.PaymentSource } into ftemp
                        from fftemp in ftemp.DefaultIfEmpty()
                        join g in dbContext.T_OrderAmount on new { OrderId = a.Id, PaymentSource = 2 } equals new { OrderId = g.OrderId, PaymentSource = g.PaymentSource } into gtemp
                        from ggtemp in gtemp.DefaultIfEmpty()
                        join h in dbContext.T_Enterprise on a.ToLearningCenterId.Value equals h.Id into htemp
                        from hhtemp in htemp.DefaultIfEmpty()
                        where a.IsDelete == false && (noStudentName || a.StudentName.Contains(req.StudentName)) &&
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
                        (req.IsChannelAdd.HasValue == false || (a.FromChannelId.HasValue != req.IsChannelAdd.Value)) &&
                        (req.IsChannel.HasValue == false || (req.IsChannel.Value == true && a.FromChannelId.HasValue == true && a.Status != 0 && a.Status != 3) || (req.IsChannel.Value == true && a.FromChannelId.HasValue == false))
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
                            ApprovalAmount = fftemp.ApprovalAmount,
                            PayedAmount = fftemp.PayedAmount,
                            TotalAmount = fftemp.TotalAmount,
                            QDApprovalAmount = ggtemp.ApprovalAmount,
                            QDPayedAmount = ggtemp.PayedAmount,
                            QDTotalAmount = ggtemp.TotalAmount,
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

            return query.OrderByDescending(a => a.CreateTime).Skip((req.Page - 1) * req.Limit).Take(req.Limit).ToList();
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
            var mdata= dbContext.T_Metadata.ToList();
            //所有批次
            var batchList = mdata.Where(a => a.Type == (int)MetadataTypeEnum.Batch).ToList();
            //所有学校
            var schoolList = mdata.Where(a => a.Type == (int)MetadataTypeEnum.School).ToList();
            //所有层次
            var levelList = mdata.Where(a => a.Type == (int)MetadataTypeEnum.Level).ToList();
            //所有专业
            var majorList = mdata.Where(a => a.Type == (int)MetadataTypeEnum.Major).ToList();

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
            && a.Status != (int)OrderStatusEnum.LeaveSchool) > 0;
                if (exisit == true)
                {
                    //同一批次重复录入
                    return "第" + (i + 1).ToString() + "行的数据重复录入！";
                }

                var exisit2 = orderList.Count(a => a.BatchId == order.BatchId && a.SchoolId == order.SchoolId && a.IDCardNo == order.IDCardNo
            && a.Status != (int)OrderStatusEnum.LeaveSchool) > 0;
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
    }
}
