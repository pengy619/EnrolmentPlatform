using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.EFContext;
using EnrolmentPlatform.Project.Domain.Entities.Orders;
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
            var chargeStrategy = dbContext.T_ChargeStrategy.FirstOrDefault(a => a.SchoolId == dto.SchoolId && a.LevelId==dto.LevelId
            && a.MajorId == dto.MajorId
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
                CreatorAccount = dto.UserName,
                CreatorTime = DateTime.Now,
                CreatorUserId = dto.UserId,
                DeleteTime = DateTime.MaxValue,
                DeleteUserId = Guid.Empty,
                IsDelete = false,
                LastModifyTime = DateTime.Now,
                LastModifyUserId = dto.UserId,
                Unix = DateTime.Now.ConvertDateTimeInt()
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
            dbContext.T_OrderAmount.Add(new T_OrderAmount()
            {
                Id = Guid.NewGuid(),
                OrderId = order.Id,
                TotalAmount = chargeStrategy.InstitutionCharge,
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

            //添加订单（学习中心）金额数据
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
            && a.MajorId == dto.MajorId
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
            dbContext.T_OrderAmount.Add(new T_OrderAmount()
            {
                Id = Guid.NewGuid(),
                OrderId = dto.OrderId.Value,
                TotalAmount = chargeStrategy.InstitutionCharge,
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

            //添加订单（学习中心）金额数据
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
        public List<OrderListDto> GetStudentList(OrderListReqDto req,ref int reCount)
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
                        (req.IsChannel.HasValue==false||(req.IsChannel.Value==true && a.FromChannelId.HasValue==true && a.Status!=0 && a.Status != 3) ||(req.IsChannel.Value == true && a.FromChannelId.HasValue == false))
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
                            ToLearningCenterTime = a.ToLearningCenterTime
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

            reCount = query.Count();
            if (reCount == 0)
            {
                return null;
            }

            return query.OrderByDescending(a=>a.CreateTime).Skip((req.Page - 1) * req.Limit).Take(req.Limit).ToList();
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
                        (req.ToLearningCenterId.HasValue == false || a.ToLearningCenterId == req.ToLearningCenterId.Value)
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
        /// <param name="paymentSource">支付发起方（1：招生机构，2：学习中心）</param>
        /// <param name="reCount"></param>
        /// <returns></returns>
        public List<OrderPaymentListDto> GetStudentPaymentList(OrderListReqDto req, int paymentSource, ref int reCount)
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
                        join f in dbContext.T_OrderAmount on a.Id equals f.OrderId into ftemp
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
                        (req.ToLearningCenterId.HasValue == false || a.ToLearningCenterId == req.ToLearningCenterId.Value)
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
                            TotalAmount = fftemp.TotalAmount
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

            reCount = query.Count();
            if (reCount == 0)
            {
                return null;
            }

            return query.OrderByDescending(a => a.CreateTime).Skip((req.Page - 1) * req.Limit).Take(req.Limit).ToList();
        }
    }
}
