using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities.Orders;
using EnrolmentPlatform.Project.DTO.Enums.Orders;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.IBLL.Orders;
using EnrolmentPlatform.Project.IDAL;
using EnrolmentPlatform.Project.IDAL.Basics;
using EnrolmentPlatform.Project.IDAL.Orders;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.BLL.Orders
{
    public class T_OrderService : IT_OrderService
    {
        private IT_OrderRepository orderRepository;
        private IT_OrderImageRepository orderImageRepository;
        private IT_OrderAmountRepository orderAmountRepository;
        private IT_StockSettingRepository stockSettingRepository;
        private IT_ChargeStrategyRepository chargeStrategyRepository;
        protected IDbContextFactory _dbContextFactory;

        public T_OrderService()
        {
            this.orderRepository = DIContainer.Resolve<IT_OrderRepository>();
            this.orderImageRepository = DIContainer.Resolve<IT_OrderImageRepository>();
            this.orderAmountRepository = DIContainer.Resolve<IT_OrderAmountRepository>();
            this.stockSettingRepository = DIContainer.Resolve<IT_StockSettingRepository>();
            this.chargeStrategyRepository = DIContainer.Resolve<IT_ChargeStrategyRepository>();
            this._dbContextFactory = DIContainer.Resolve<IDbContextFactory>();
        }

        /// <summary>
        /// 新增订单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>1：成功，2：找不到当前时间段的价格策略，3：失败，4：同一批次重复录入</returns>
        public int AddOrder(OrderDto dto)
        {
            var exisit = this.orderRepository.Count(a => a.IsDelete == false && a.BatchId == dto.BatchId && a.SchoolId == dto.SchoolId && a.IDCardNo == dto.IDCardNo
            && a.Status != (int)OrderStatusEnum.LeaveSchool && a.FromChannelId == dto.FromChannelId) > 0;
            if (exisit == true)
            {
                //同一批次重复录入
                return 4;
            }

            try
            {
                return this.orderRepository.AddOrder(dto);
            }
            catch (Exception ex)
            {
                return 3;
            }
        }

        /// <summary>
        /// 修改报名单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>1：成功，2：找不到当前时间段的价格策略，3：失败，4：同一批次重复录入</returns>
        public int UpdateOrder(OrderDto dto)
        {
            var exisit = this.orderRepository.Count(a => a.IsDelete == false && a.Id != dto.OrderId.Value && a.BatchId == dto.BatchId && a.SchoolId == dto.SchoolId && a.IDCardNo == dto.IDCardNo
            && a.Status != (int)OrderStatusEnum.LeaveSchool && a.FromChannelId == dto.FromChannelId) > 0;
            if (exisit == true)
            {
                //同一批次重复录入
                return 4;
            }

            try
            {
                return this.orderRepository.UpdateOrder(dto);
            }
            catch
            {
                return 3;
            }
        }

        /// <summary>
        /// 获得报名单图片
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderImageDto FindOrderImage(Guid orderId)
        {
            var entity = this.orderImageRepository.LoadEntities(a => a.OrderId == orderId).FirstOrDefault();
            if (entity == null)
            {
                return null;
            }

            return new OrderImageDto()
            {
                BiYeZhengImg = entity.BiYeZhengImg,
                Id = entity.Id,
                IDCard1 = entity.IDCard1,
                IDCard2 = entity.IDCard2,
                LiangCunLanDiImg = entity.LiangCunLanDiImg,
                MianKaoJiSuanJiImg = entity.MianKaoJiSuanJiImg,
                MianKaoYingYuImg = entity.MianKaoYingYuImg,
                OrderId = entity.OrderId,
                QiTa = entity.QiTa,
                TouXiang = entity.TouXiang,
                XueXinWangImg = entity.XueXinWangImg
            };
        }

        /// <summary>
        /// 获得报名单图片
        /// </summary>
        /// <param name="orderIds"></param>
        /// <returns></returns>
        public List<OrderImageDto> FindOrderImage(List<Guid> orderIds)
        {
            return this.orderImageRepository.LoadEntities(a => orderIds.Contains(a.OrderId))
                .Select(entity => new OrderImageDto()
                {
                    BiYeZhengImg = entity.BiYeZhengImg,
                    Id = entity.Id,
                    IDCard1 = entity.IDCard1,
                    IDCard2 = entity.IDCard2,
                    LiangCunLanDiImg = entity.LiangCunLanDiImg,
                    MianKaoJiSuanJiImg = entity.MianKaoJiSuanJiImg,
                    MianKaoYingYuImg = entity.MianKaoYingYuImg,
                    OrderId = entity.OrderId,
                    QiTa = entity.QiTa,
                    TouXiang = entity.TouXiang,
                    XueXinWangImg = entity.XueXinWangImg
                }).ToList();
        }

        /// <summary>
        /// 修改报名单图片
        /// </summary>
        /// <param name="dto">dto</param>
        /// <returns></returns>
        public bool UpdateImage(OrderImageDto dto)
        {
            using (DbConnection conn = ((IObjectContextAdapter)_dbContextFactory.GetCurrentThreadInstance()).ObjectContext.Connection)
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    //获得图片
                    var entity = this.orderImageRepository.LoadEntities(a => a.OrderId == dto.OrderId).FirstOrDefault();
                    entity.BiYeZhengImg = dto.BiYeZhengImg;
                    entity.IDCard1 = dto.IDCard1;
                    entity.IDCard2 = dto.IDCard2;
                    entity.LiangCunLanDiImg = dto.LiangCunLanDiImg;
                    entity.MianKaoJiSuanJiImg = dto.MianKaoJiSuanJiImg;
                    entity.MianKaoYingYuImg = dto.MianKaoYingYuImg;
                    entity.QiTa = dto.QiTa;
                    entity.TouXiang = dto.TouXiang;
                    entity.XueXinWangImg = dto.XueXinWangImg;
                    bool ret = this.orderImageRepository.UpdateEntity(entity, Domain.EFContext.E_DbClassify.Write, "修改报名单照片", true, entity.Id.ToString())
                        > 0 ? true : false;
                    if (ret)
                    {
                        //如果所有图片都上传了
                        if (!string.IsNullOrWhiteSpace(dto.BiYeZhengImg) && !string.IsNullOrWhiteSpace(dto.LiangCunLanDiImg)
                            && !string.IsNullOrWhiteSpace(dto.IDCard1) && !string.IsNullOrWhiteSpace(dto.IDCard2)
                            && !string.IsNullOrWhiteSpace(dto.MianKaoJiSuanJiImg) && !string.IsNullOrWhiteSpace(dto.MianKaoYingYuImg)
                            && !string.IsNullOrWhiteSpace(dto.QiTa) && !string.IsNullOrWhiteSpace(dto.TouXiang)
                            && !string.IsNullOrWhiteSpace(dto.XueXinWangImg))
                        {
                            //修改所有图片上传状态
                            var order = this.orderRepository.FindEntityById(dto.OrderId);
                            order.AllOrderImageUpload = true;
                            ret = this.orderRepository.UpdateEntity(order, Domain.EFContext.E_DbClassify.Write, "", false, "") > 0;
                        }
                    }
                    if (ret == true)
                    {
                        tran.Commit();
                        return ret;
                    }

                    return ret;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 获得订单
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        public OrderDto GetOrder(Guid id)
        {
            var entity = this.orderRepository.FindEntityById(id);
            if (entity == null)
            {
                return null;
            }

            return new OrderDto()
            {
                Address = entity.Address,
                AllOrderImageUpload = entity.AllOrderImageUpload,
                BatchId = entity.BatchId,
                Email = entity.Email,
                EnrollAddress = entity.EnrollAddress,
                ExamDate = entity.ExamDate,
                ExamSubject = entity.ExamSubject,
                FromChannelId = entity.FromChannelId,
                FromTypeName = entity.FromTypeName,
                GraduateSchool = entity.GraduateSchool,
                HighesDegree = entity.HighesDegree,
                IDCardNo = entity.IDCardNo,
                LevelId = entity.LevelId,
                MajorId = entity.MajorId,
                Native = entity.Native,
                OrderId = entity.Id,
                Phone = entity.Phone,
                Remark = entity.Remark,
                SchoolId = entity.SchoolId,
                Status = entity.Status,
                StudentName = entity.StudentName,
                TencentNo = entity.TencentNo,
                ToLearningCenterId = entity.ToLearningCenterId,
                UserId = entity.CreatorUserId,
                UserName = entity.CreatorAccount,
                WorkUnit = entity.WorkUnit,
                BiYeZhengBianHao = entity.BiYeZhengBianHao,
                GongZuoDanWei = entity.GongZuoDanWei,
                GraduationTime = entity.GraduationTime,
                IsTvUniversity = entity.IsTvUniversity,
                SuoDuZhuanYe = entity.SuoDuZhuanYe,
                JiGuan = entity.JiGuan,
                MinZu = entity.MinZu,
                Sex = entity.Sex,
                CreateTime = entity.CreatorTime,
                CreateUserName = entity.UserName,
                AssistStatus = entity.AssistStatus,
                CustomerField = entity.CustomerField
            };
        }

        /// <summary>
        /// 获得订单
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        public List<OrderDto> GetOrder(List<Guid> ids)
        {
            return this.orderRepository.LoadEntities(a => ids.Contains(a.Id))
                .Select(entity => new OrderDto()
                {
                    Address = entity.Address,
                    AllOrderImageUpload = entity.AllOrderImageUpload,
                    BatchId = entity.BatchId,
                    Email = entity.Email,
                    EnrollAddress = entity.EnrollAddress,
                    ExamDate = entity.ExamDate,
                    ExamSubject = entity.ExamSubject,
                    FromChannelId = entity.FromChannelId,
                    FromTypeName = entity.FromTypeName,
                    GraduateSchool = entity.GraduateSchool,
                    HighesDegree = entity.HighesDegree,
                    IDCardNo = entity.IDCardNo,
                    LevelId = entity.LevelId,
                    MajorId = entity.MajorId,
                    Native = entity.Native,
                    OrderId = entity.Id,
                    Phone = entity.Phone,
                    Remark = entity.Remark,
                    SchoolId = entity.SchoolId,
                    Status = entity.Status,
                    StudentName = entity.StudentName,
                    TencentNo = entity.TencentNo,
                    ToLearningCenterId = entity.ToLearningCenterId,
                    UserId = entity.CreatorUserId,
                    UserName = entity.CreatorAccount,
                    WorkUnit = entity.WorkUnit,
                    BiYeZhengBianHao = entity.BiYeZhengBianHao,
                    GongZuoDanWei = entity.GongZuoDanWei,
                    JiGuan = entity.JiGuan,
                    MinZu = entity.MinZu,
                    Sex = entity.Sex,
                    CustomerField = entity.CustomerField
                })
                .ToList();
        }

        /// <summary>
        /// 报名提交（直接为已报名）
        /// </summary>
        /// <param name="dto">dto</param>
        /// <returns></returns>
        public ResultMsg SubmitOrder(SubmitOrderDto dto)
        {
            using (DbConnection conn = ((IObjectContextAdapter)_dbContextFactory.GetCurrentThreadInstance()).ObjectContext.Connection)
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    foreach (var item in dto.IDs)
                    {
                        var entity = this.orderRepository.FindEntityById(item);
                        if (entity == null ||
                            (entity.Status != (int)OrderStatusEnum.Init && entity.Status != (int)OrderStatusEnum.Reject))
                        {
                            throw new Exception("提交的订单数据错误。");
                        }

                        //校验同一批次重复录入
                        var exisit = this.orderRepository.Count(a => a.IsDelete == false && a.Id != entity.Id && a.BatchId == entity.BatchId && a.SchoolId == entity.SchoolId && a.IDCardNo == entity.IDCardNo
                        && a.Status != (int)OrderStatusEnum.LeaveSchool && a.Status != (int)OrderStatusEnum.Init && a.Status != (int)OrderStatusEnum.Reject) > 0;
                        if (exisit == true)
                        {
                            //同一批次重复录入
                            throw new Exception(entity.StudentName + "的订单批次录入重复。");
                        }

                        //校验是否有库存
                        var stock = this.stockSettingRepository.LoadEntities(a => a.SchoolId == entity.SchoolId && a.LevelId == entity.LevelId
                          && a.MajorId == entity.MajorId && a.BatchId == entity.BatchId).FirstOrDefault();
                        if (stock == null || stock.UsedInventory >= stock.Inventory)
                        {
                            throw new Exception(entity.StudentName + "的订单没有库存。");
                        }

                        //查找当前时间段的通用或机构收费策略
                        var chargeStrategys = this.chargeStrategyRepository.LoadEntities(t => t.SchoolId == entity.SchoolId && t.LevelId == entity.LevelId
                        && t.MajorId == entity.MajorId && ((t.LearningCenterId == Guid.Empty && t.InstitutionId == Guid.Empty) || t.InstitutionId == entity.FromChannelId)
                        && DateTime.Today >= t.StartDate && DateTime.Today <= t.EndDate).ToList();
                        if (chargeStrategys != null && chargeStrategys.Any())
                        {
                            //如果收费存在则删除
                            if (this.orderAmountRepository.Count(t => t.OrderId == entity.Id && t.PaymentSource == 1) > 0)
                            {
                                this.orderAmountRepository.PhysicsDeleteBy(t => t.OrderId == entity.Id && t.PaymentSource == 1);
                            }
                            //添加订单（招生机构）金额数据
                            var commonCharge = chargeStrategys.FirstOrDefault(t => t.InstitutionId == Guid.Empty);
                            var institutionCharge = chargeStrategys.FirstOrDefault(t => t.InstitutionId == entity.FromChannelId);
                            this.orderAmountRepository.AddEntity(new T_OrderAmount
                            {
                                Id = Guid.NewGuid(),
                                OrderId = entity.Id,
                                TotalAmount = institutionCharge != null ? institutionCharge.InstitutionCharge : commonCharge.InstitutionCharge,
                                ApprovalAmount = 0,
                                PayedAmount = 0,
                                PaymentSource = 1,
                                CreatorTime = DateTime.Now,
                                CreatorUserId = dto.UserId
                            });
                        }
                        else
                        {
                            return new ResultMsg() { IsSuccess = false, Info = entity.StudentName + "的订单匹配不到收费策略。" };
                        }

                        //1.修改库存
                        stock.UsedInventory = stock.UsedInventory + 1;
                        stock.LastModifyTime = DateTime.Now;
                        this.stockSettingRepository.UpdateEntity(stock);

                        //2.修改报名单状态
                        entity.Status = (int)OrderStatusEnum.Enroll;
                        entity.EnrollTime = DateTime.Now;
                        entity.LastModifyTime = DateTime.Now;
                        entity.LastModifyUserId = dto.UserId;
                        this.orderRepository.UpdateEntity(entity, Domain.EFContext.E_DbClassify.Write, "报名提交", true, entity.Id.ToString());
                    }

                    tran.Commit();
                    return new ResultMsg() { IsSuccess = true };
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return new ResultMsg() { IsSuccess = false, Info = ex.Message };
                }
            }
        }

        /// <summary>
        /// 拒绝
        /// </summary>
        /// <param name="orderIdList">orderIdList</param>
        /// <param name="userId">修改人</param>
        /// <param name="comment">拒绝理由</param>
        /// <returns></returns>
        public bool Reject(List<Guid> orderIdList, Guid userId, string comment)
        {
            using (DbConnection conn = ((IObjectContextAdapter)_dbContextFactory.GetCurrentThreadInstance()).ObjectContext.Connection)
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    foreach (var item in orderIdList)
                    {
                        var entity = this.orderRepository.FindEntityById(item);
                        if (entity == null ||
                            (entity.Status != (int)OrderStatusEnum.Enroll && entity.Status != (int)OrderStatusEnum.ToLearningCenter && entity.Status != (int)OrderStatusEnum.Audited))
                        {
                            break;
                        }

                        //库存处理
                        var stock = this.stockSettingRepository.LoadEntities(a => a.SchoolId == entity.SchoolId && a.LevelId == entity.LevelId
                          && a.MajorId == entity.MajorId && a.BatchId == entity.BatchId).FirstOrDefault();

                        //如果有库存
                        if (stock != null)
                        {
                            //需要修改已用库存
                            stock.UsedInventory = stock.UsedInventory - 1;
                            stock.LastModifyTime = DateTime.Now;
                            this.stockSettingRepository.UpdateEntity(stock);
                        }

                        //修改订单状态
                        entity.Status = (int)OrderStatusEnum.Reject;
                        entity.ToLearningCenterId = null;
                        entity.LastModifyTime = DateTime.Now;
                        entity.LastModifyUserId = userId;
                        this.orderRepository.UpdateEntity(entity, Domain.EFContext.E_DbClassify.Write, "已被拒绝，拒绝理由【" + comment + "】", true, entity.Id.ToString());
                    }

                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 报送中心
        /// </summary>
        /// <param name="orderIdList">orderIdList</param>
        /// <param name="toLearningCenterId">报送的学院中心</param>
        /// <param name="userId">修改人</param>
        /// <returns></returns>
        public ResultMsg ToLearningCenter(List<Guid> orderIdList, Guid toLearningCenterId, Guid userId)
        {
            using (DbConnection conn = ((IObjectContextAdapter)_dbContextFactory.GetCurrentThreadInstance()).ObjectContext.Connection)
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    foreach (var item in orderIdList)
                    {
                        var entity = this.orderRepository.FindEntityById(item);
                        if (entity == null || entity.Status != (int)OrderStatusEnum.Enroll)
                        {
                            return new ResultMsg() { IsSuccess = false, Info = "存在不能进行报送的订单。" };
                        }

                        //查找当前时间段的通用或学院中心收费策略
                        var chargeStrategys = this.chargeStrategyRepository.LoadEntities(t => t.SchoolId == entity.SchoolId && t.LevelId == entity.LevelId
                        && t.MajorId == entity.MajorId && ((t.LearningCenterId == Guid.Empty && t.InstitutionId == Guid.Empty) || t.LearningCenterId == toLearningCenterId)
                        && DateTime.Today >= t.StartDate && DateTime.Today <= t.EndDate).ToList();
                        if (chargeStrategys != null && chargeStrategys.Any())
                        {
                            //如果收费存在则删除
                            if (this.orderAmountRepository.Count(t => t.OrderId == entity.Id && t.PaymentSource == 2) > 0)
                            {
                                this.orderAmountRepository.PhysicsDeleteBy(t => t.OrderId == entity.Id && t.PaymentSource == 2);
                            }
                            //添加订单（学院中心）金额数据
                            var commonCharge = chargeStrategys.FirstOrDefault(t => t.LearningCenterId == Guid.Empty);
                            var centerCharge = chargeStrategys.FirstOrDefault(t => t.LearningCenterId == toLearningCenterId);
                            this.orderAmountRepository.AddEntity(new T_OrderAmount
                            {
                                Id = Guid.NewGuid(),
                                OrderId = entity.Id,
                                TotalAmount = centerCharge != null ? centerCharge.CenterCharge : commonCharge.CenterCharge,
                                ApprovalAmount = 0,
                                PayedAmount = 0,
                                PaymentSource = 2,
                                CreatorTime = DateTime.Now,
                                CreatorUserId = userId
                            });
                        }
                        else
                        {
                            return new ResultMsg() { IsSuccess = false, Info = entity.StudentName + "的订单匹配不到收费策略。" };
                        }

                        //已报送中心
                        entity.Status = (int)OrderStatusEnum.ToLearningCenter;
                        entity.ToLearningCenterId = toLearningCenterId;
                        entity.ToLearningCenterTime = DateTime.Now;
                        entity.LastModifyTime = DateTime.Now;
                        entity.LastModifyUserId = userId;
                        this.orderRepository.UpdateEntity(entity, Domain.EFContext.E_DbClassify.Write, "已报送学院中心", true, entity.Id.ToString());
                    }

                    tran.Commit();
                    return new ResultMsg() { IsSuccess = true };
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return new ResultMsg() { IsSuccess = false, Info = ex.Message };
                }
            }
        }

        /// <summary>
        /// 录取
        /// </summary>
        /// <param name="orderId">orderId</param>
        /// <param name="xuehao">xuehao</param>
        /// <param name="zhanghao">zhanghao</param>
        /// <param name="mima">mima</param>
        /// <param name="mima">userId</param>
        /// <returns></returns>
        public bool Luqu(Guid orderId, string xuehao, string zhanghao, string mima, Guid userId)
        {
            var entity = this.orderRepository.FindEntityById(orderId);
            if (entity == null || (entity.Status != (int)OrderStatusEnum.ToLearningCenter && entity.Status != (int)OrderStatusEnum.Audited))
            {
                return false;
            }

            entity.Status = (int)OrderStatusEnum.Join;
            entity.JoinTime = DateTime.Now;
            entity.LastModifyTime = DateTime.Now;
            entity.StudentNo = xuehao;
            entity.UserName = zhanghao;
            entity.Password = mima;
            entity.LastModifyUserId = userId;
            return this.orderRepository.UpdateEntity(entity, Domain.EFContext.E_DbClassify.Write, "录取成功", true, entity.Id.ToString()) > 0;
        }

        /// <summary>
        /// 毕业
        /// </summary>
        /// <param name="orderIdList">orderIdList</param>
        /// <param name="userId">修改人</param>
        /// <returns></returns>
        public bool Graduated(List<Guid> orderIdList, Guid userId)
        {
            using (DbConnection conn = ((IObjectContextAdapter)_dbContextFactory.GetCurrentThreadInstance()).ObjectContext.Connection)
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    foreach (var item in orderIdList)
                    {
                        var entity = this.orderRepository.FindEntityById(item);
                        if (entity == null ||
                            (entity.Status != (int)OrderStatusEnum.Join))
                        {
                            break;
                        }

                        entity.Status = (int)OrderStatusEnum.Graduated;
                        entity.LastModifyTime = DateTime.Now;
                        entity.LastModifyUserId = userId;
                        this.orderRepository.UpdateEntity(entity, Domain.EFContext.E_DbClassify.Write, "已经毕业", true, entity.Id.ToString());
                    }

                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 修改渠道
        /// </summary>
        /// <param name="ids">ids</param>
        /// <param name="trainingInstitutionsId">trainingInstitutionsId</param>
        /// <param name="userId">userId</param>
        /// <returns></returns>
        public bool UpdateTrainingInstitutions(Guid[] ids, Guid trainingInstitutionsId, Guid userId)
        {
            using (DbConnection conn = ((IObjectContextAdapter)_dbContextFactory.GetCurrentThreadInstance()).ObjectContext.Connection)
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    foreach (var item in ids)
                    {
                        var entity = this.orderRepository.FindEntityById(item);
                        entity.FromChannelId = trainingInstitutionsId;
                        entity.FromTypeName = "机构";
                        entity.LastModifyTime = DateTime.Now;
                        entity.LastModifyUserId = userId;
                        this.orderRepository.UpdateEntity(entity, Domain.EFContext.E_DbClassify.Write, "修改渠道", true, entity.Id.ToString());
                    }

                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }

        ///// <summary>
        ///// 录取
        ///// </summary>
        ///// <param name="orderIdList">orderIdList</param>
        ///// <param name="userId">修改人</param>
        ///// <returns></returns>
        //public bool Luqu(List<Guid> orderIdList, Guid userId)
        //{
        //    using (DbConnection conn = ((IObjectContextAdapter)_dbContextFactory.GetCurrentThreadInstance()).ObjectContext.Connection)
        //    {
        //        conn.Open();
        //        var tran = conn.BeginTransaction();
        //        try
        //        {
        //            foreach (var item in orderIdList)
        //            {
        //                var entity = this.orderRepository.FindEntityById(item);
        //                if (entity == null || entity.Status != (int)OrderStatusEnum.ToLearningCenter)
        //                {
        //                    break;
        //                }

        //                entity.Status = (int)OrderStatusEnum.Join;
        //                entity.JoinTime = DateTime.Now;
        //                entity.LastModifyTime = DateTime.Now;
        //                entity.LastModifyUserId = userId;
        //                this.orderRepository.UpdateEntity(entity, Domain.EFContext.E_DbClassify.Write, "录取成功", true, entity.Id.ToString());
        //            }

        //            tran.Commit();
        //            return true;
        //        }
        //        catch (Exception ex)
        //        {
        //            tran.Rollback();
        //            return false;
        //        }
        //    }
        //}

        /// <summary>
        /// 退学
        /// </summary>
        /// <param name="orderIdList">orderIdList</param>
        /// <param name="userId">修改人</param>
        /// <returns></returns>
        public bool Leave(List<Guid> orderIdList, Guid userId)
        {
            using (DbConnection conn = ((IObjectContextAdapter)_dbContextFactory.GetCurrentThreadInstance()).ObjectContext.Connection)
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    foreach (var item in orderIdList)
                    {
                        var entity = this.orderRepository.FindEntityById(item);
                        if (entity == null || entity.Status != (int)OrderStatusEnum.Join)
                        {
                            return false;
                        }
                        entity.Status = (int)OrderStatusEnum.LeaveSchool;
                        entity.LeaveTime = DateTime.Now;
                        entity.LastModifyTime = DateTime.Now;
                        entity.LastModifyUserId = userId;
                        this.orderRepository.UpdateEntity(entity, Domain.EFContext.E_DbClassify.Write, "退学", true, entity.Id.ToString());
                    }

                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 渠道操作退学
        /// </summary>
        /// <param name="orderIdList">orderIdList</param>
        /// <param name="userId">修改人</param>
        /// <returns></returns>
        public bool ChannelLeave(List<Guid> orderIdList, Guid userId)
        {
            using (DbConnection conn = ((IObjectContextAdapter)_dbContextFactory.GetCurrentThreadInstance()).ObjectContext.Connection)
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    foreach (var item in orderIdList)
                    {
                        var entity = this.orderRepository.FindEntityById(item);
                        if (entity == null || entity.Status == (int)OrderStatusEnum.Graduated || entity.Status == (int)OrderStatusEnum.LeaveSchool)
                        {
                            return false;
                        }
                        //录取状态退学直接更改状态为退学，其他状态点击退学直接回到草稿状态
                        if (entity.Status == (int)OrderStatusEnum.Join)
                        {
                            entity.Status = (int)OrderStatusEnum.LeaveSchool;
                            entity.LeaveTime = DateTime.Now;
                        }
                        else
                        {
                            entity.Status = (int)OrderStatusEnum.Init;
                        }
                        entity.LastModifyTime = DateTime.Now;
                        entity.LastModifyUserId = userId;
                        this.orderRepository.UpdateEntity(entity, Domain.EFContext.E_DbClassify.Write, "渠道退学", true, entity.Id.ToString());
                    }

                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 初审
        /// </summary>
        /// <param name="orderIdList">orderIdList</param>
        /// <param name="userId">修改人</param>
        /// <returns></returns>
        public bool Audit(List<Guid> orderIdList, Guid userId)
        {
            using (DbConnection conn = ((IObjectContextAdapter)_dbContextFactory.GetCurrentThreadInstance()).ObjectContext.Connection)
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    foreach (var item in orderIdList)
                    {
                        var entity = this.orderRepository.FindEntityById(item);
                        if (entity == null || entity.Status != (int)OrderStatusEnum.ToLearningCenter)
                        {
                            return false;
                        }
                        entity.Status = (int)OrderStatusEnum.Audited;
                        entity.LastModifyTime = DateTime.Now;
                        entity.LastModifyUserId = userId;
                        this.orderRepository.UpdateEntity(entity, Domain.EFContext.E_DbClassify.Write, "初审", true, entity.Id.ToString());
                    }

                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="orderIds">orderIds</param>
        /// <returns></returns>
        public bool Delete(Guid[] orderIds)
        {
            return this.orderRepository.LogicDeleteBy(a => orderIds.Contains(a.Id)) > 0;
        }

        /// <summary>
        /// 获得报名列表
        /// </summary>
        /// <param name="req"></param>
        /// <param name="reCount"></param>
        /// <returns></returns>
        public List<OrderListDto> GetStudentList(OrderListReqDto req, ref int reCount)
        {
            return this.orderRepository.GetStudentList(req, ref reCount);
        }

        /// <summary>
        /// 获得报名照片列表
        /// </summary>
        /// <param name="req"></param>
        /// <param name="reCount"></param>
        /// <returns></returns>
        public List<OrderImageListDto> GetStudentImageList(OrderListReqDto req, ref int reCount)
        {
            return this.orderRepository.GetStudentImageList(req, ref reCount);
        }

        /// <summary>
        /// 获得报名缴费列表
        /// </summary>
        /// <param name="req"></param>
        /// <param name="reCount"></param>
        /// <returns></returns>
        public List<OrderPaymentListDto> GetStudentPaymentList(OrderListReqDto req, ref int reCount)
        {
            return this.orderRepository.GetStudentPaymentList(req, ref reCount);
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
            return this.orderRepository.UpdateQDAmount(orderId, amount, amountType);
        }

        /// <summary>
        /// 获得订单尾款
        /// </summary>
        /// <param name="orderIds">订单集合</param>
        /// <param name="paymentSource">1：招生机构，2：渠道中心</param>
        /// <returns></returns>
        public decimal GetOrderAmountUnPayedTotal(List<Guid> orderIds, int paymentSource)
        {
            var orderList = this.orderAmountRepository.LoadEntities(a => orderIds.Contains(a.OrderId) && a.PaymentSource == paymentSource)
                .ToList();
            return orderList.Sum(a => (a.TotalAmount - a.PayedAmount - a.ApprovalAmount));
        }

        /// <summary>
        /// 上传报名单
        /// </summary>
        /// <param name="list">报名单列表</param>
        /// <returns></returns>
        public string Upload(List<OrderUploadDto> list)
        {
            return orderRepository.Upload(list);
        }

        /// <summary>
        /// 录取上传
        /// </summary>
        /// <param name="list">报名单列表</param>
        /// <returns></returns>
        public string LuQuUpload(List<OrderLuQuUploadDto> list)
        {
            return orderRepository.LuQuUpload(list);
        }

        /// <summary>
        /// 获得订单统计
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public OrderStatisticsDto GetOrderStatistics(OrderListReqDto req)
        {
            return orderRepository.GetOrderStatistics(req);
        }

        /// <summary>
        /// 协助处理提交
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        public bool AssistSubmit(Guid[] ids)
        {
            using (DbConnection conn = ((IObjectContextAdapter)_dbContextFactory.GetCurrentThreadInstance()).ObjectContext.Connection)
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    foreach (var item in ids)
                    {
                        var entity = this.orderRepository.FindEntityById(item);
                        if (entity == null || entity.AssistStatus.HasValue == true)
                        {
                            break;
                        }

                        entity.AssistStatus = 1;
                        entity.LastModifyTime = DateTime.Now;
                        this.orderRepository.UpdateEntity(entity, Domain.EFContext.E_DbClassify.Write, "提交协助处理！", true, entity.Id.ToString());
                    }

                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 协助处理完成
        /// </summary>
        /// <param name="ids">ids</param>
        /// <returns></returns>
        public bool AssistDispose(Guid[] ids)
        {
            using (DbConnection conn = ((IObjectContextAdapter)_dbContextFactory.GetCurrentThreadInstance()).ObjectContext.Connection)
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    foreach (var item in ids)
                    {
                        var entity = this.orderRepository.FindEntityById(item);
                        if (entity == null || entity.AssistStatus != 1)
                        {
                            break;
                        }

                        entity.AssistStatus = 2;
                        entity.LastModifyTime = DateTime.Now;
                        this.orderRepository.UpdateEntity(entity, Domain.EFContext.E_DbClassify.Write, "协助处理完成！", true, entity.Id.ToString());
                    }

                    tran.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 初审上传
        /// </summary>
        /// <param name="list">报名单列表</param>
        /// <returns></returns>
        public string AuditUpload(List<OrderAuditUploadDto> list)
        {
            return orderRepository.AuditUpload(list);
        }

        /// <summary>
        /// 机构上传报名单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public string JiGouUpload(JiGouOrderUploadDto dto)
        {
            return orderRepository.JiGouUpload(dto);
        }
    }
}
