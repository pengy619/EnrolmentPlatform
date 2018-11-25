using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Orders;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.IBLL.Orders;
using EnrolmentPlatform.Project.IDAL;
using EnrolmentPlatform.Project.IDAL.Orders;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.BLL.Orders
{
    public class T_OrderService: IT_OrderService
    {
        private IT_OrderRepository orderRepository;
        private IT_OrderImageRepository orderImageRepository;
        protected IDbContextFactory _dbContextFactory;

        public T_OrderService()
        {
            this.orderRepository = DIContainer.Resolve<IT_OrderRepository>();
            this.orderImageRepository = DIContainer.Resolve<IT_OrderImageRepository>();
            this._dbContextFactory = DIContainer.Resolve<IDbContextFactory>();
        }

        /// <summary>
        /// 新增订单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>1：成功，2：找不到当前时间段的价格策略，3：失败，4：同一批次重复录入</returns>
        public int AddOrder(OrderDto dto)
        {
            var exisit = this.orderRepository.Count(a => a.BatchId == dto.BatchId && a.IDCardNo == dto.IDCardNo) > 0;
            if (exisit == true)
            {
                //同一批次重复录入
                return 4;
            }
            return this.orderRepository.AddOrder(dto);
        }

        /// <summary>
        /// 修改报名单
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>1：成功，2：找不到当前时间段的价格策略，3：失败，4：同一批次重复录入</returns>
        public int UpdateOrder(OrderDto dto)
        {
            var exisit = this.orderRepository.Count(a => a.Id != dto.OrderId.Value && a.BatchId == dto.BatchId && a.IDCardNo == dto.IDCardNo) > 0;
            if (exisit == true)
            {
                //同一批次重复录入
                return 4;
            }

            //实体
            var entity = this.orderRepository.FindEntityById(dto.OrderId.Value);
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
            return this.orderRepository.UpdateEntity(entity, Domain.EFContext.E_DbClassify.Write, "修改报名单", true, entity.Id.ToString())
                > 0 ? 1 : 3;
        }

        /// <summary>
        /// 获得报名单图片
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderImageDto FindOrderImage(Guid orderId)
        {
            var entity = this.orderImageRepository.FindEntityById(orderId);
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
                    bool ret= this.orderImageRepository.UpdateEntity(entity, Domain.EFContext.E_DbClassify.Write, "修改报名单照片", true, entity.Id.ToString())
                        > 0 ? true : false;
                    if (ret)
                    {
                        //如果所有图片都上传了
                        if (!string.IsNullOrWhiteSpace(dto.BiYeZhengImg) || !string.IsNullOrWhiteSpace(dto.LiangCunLanDiImg)
                            || !string.IsNullOrWhiteSpace(dto.IDCard1) || !string.IsNullOrWhiteSpace(dto.IDCard2)
                            || !string.IsNullOrWhiteSpace(dto.MianKaoJiSuanJiImg) || !string.IsNullOrWhiteSpace(dto.MianKaoYingYuImg)
                            || !string.IsNullOrWhiteSpace(dto.QiTa) || !string.IsNullOrWhiteSpace(dto.TouXiang)
                            || !string.IsNullOrWhiteSpace(dto.XueXinWangImg))
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
                WorkUnit = entity.WorkUnit

            };
        }

        /// <summary>
        /// 报名提交（直接为已报名）
        /// </summary>
        /// <param name="orderId">orderId</param>
        /// <param name="userId">修改人</param>
        /// <returns>1：成功，2：失败，3：请将照片完善</returns>
        public int SubmitOrder(Guid orderId, Guid userId)
        {
            var entity = this.orderRepository.FindEntityById(orderId);
            if (entity == null || entity.Status != (int)OrderStatusEnum.Init)
            {
                return 2;
            }

            //检查照片是否完善
            if (entity.AllOrderImageUpload == false)
            {
                return 3;
            }

            entity.Status = (int)OrderStatusEnum.Enroll;
            entity.EnrollTime = DateTime.Now;
            entity.LastModifyTime = DateTime.Now;
            entity.LastModifyUserId = userId;
            return this.orderRepository.UpdateEntity(entity, Domain.EFContext.E_DbClassify.Write, "报名提交", true, entity.Id.ToString())
               > 0 ? 1 : 2;
        }

        /// <summary>
        /// 报送中心（直接是录取）
        /// </summary>
        /// <param name="orderId">orderId</param>
        /// <param name="toLearningCenterId">报送的学习中心</param>
        /// <param name="userId">修改人</param>
        /// <returns></returns>
        public bool JoinSubmit(Guid orderId,Guid toLearningCenterId,Guid userId)
        {
            var entity = this.orderRepository.FindEntityById(orderId);
            if (entity == null || entity.Status != (int)OrderStatusEnum.Enroll)
            {
                return false;
            }
            entity.Status = (int)OrderStatusEnum.Join;
            entity.ToLearningCenterId = toLearningCenterId;
            entity.JoinTime = DateTime.Now;
            entity.LastModifyTime = DateTime.Now;
            entity.LastModifyUserId = userId;
            return this.orderRepository.UpdateEntity(entity, Domain.EFContext.E_DbClassify.Write, "已报送学习中心", true, entity.Id.ToString())
               > 0;
        }

        /// <summary>
        /// 退学
        /// </summary>
        /// <param name="orderId">orderId</param>
        /// <param name="userId">修改人</param>
        /// <returns></returns>
        public bool Leave(Guid orderId, Guid userId)
        {
            var entity = this.orderRepository.FindEntityById(orderId);
            if (entity == null || entity.Status != (int)OrderStatusEnum.Enroll)
            {
                return false;
            }
            entity.Status = (int)OrderStatusEnum.LeaveSchool;
            entity.LeaveTime = DateTime.Now;
            entity.LastModifyTime = DateTime.Now;
            entity.LastModifyUserId = userId;
            return this.orderRepository.UpdateEntity(entity, Domain.EFContext.E_DbClassify.Write, "退学", true, entity.Id.ToString()) > 0;
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="orderId">orderId</param>
        /// <returns></returns>
        public bool Delete(Guid orderId)
        {
            return this.orderRepository.LogicDeleteBy(a => a.Id == orderId) > 0;
        }
    }
}
