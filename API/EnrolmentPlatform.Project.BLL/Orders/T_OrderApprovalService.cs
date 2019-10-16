using EnrolmentPlatform.Project.DTO.Enums.Orders;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.IBLL.Orders;
using EnrolmentPlatform.Project.IDAL.Orders;
using EnrolmentPlatform.Project.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.BLL.Orders
{
    public class T_OrderApprovalService : IT_OrderApprovalService
    {
        private IT_OrderApprovalRepository orderApprovalRepository = DIContainer.Resolve<IT_OrderApprovalRepository>();
        private IT_OrderImageApprovalRepository orderImageApprovalRepository = DIContainer.Resolve<IT_OrderImageApprovalRepository>();

        /// <summary>
        /// 根据订单ID获得待审核的订单修改申请
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderApprovalDto GetOrderApplyApprovalInfo(Guid orderId)
        {
            var entity = this.orderApprovalRepository.LoadEntities(a => a.IsDelete == false && a.OrderId == orderId && a.ApprovalStatus == (int)OrderApprovalStatusEnum.Init)
                .FirstOrDefault();
            if (entity == null)
            {
                return null;
            }
            return new OrderApprovalDto()
            {
                ApprovalId = entity.Id,
                Address = entity.Address,
                ApprovalComment = entity.ApprovalComment,
                ApprovalStatus = entity.ApprovalStatus,
                BiYeZhengBianHao = entity.BiYeZhengBianHao,
                Email = entity.Email,
                GongZuoDanWei = entity.GongZuoDanWei,
                GraduateSchool = entity.GraduateSchool,
                HighesDegree = entity.HighesDegree,
                IDCardNo = entity.IDCardNo,
                JiGuan = entity.JiGuan,
                MinZu = entity.MinZu,
                OrderId = entity.OrderId,
                Phone = entity.Phone,
                Remark = entity.Remark,
                Sex = entity.Sex,
                StudentName = entity.StudentName,
                TencentNo = entity.TencentNo,
                ZhaoShengLaoShi = entity.ZhaoShengLaoShi
            };
        }

        /// <summary>
        /// 根据订单ID获得待审核的订单图片修改申请
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderApprovalImgDto GetOrderImageApplyApprovalInfo(Guid orderId)
        {
            var entity = this.orderApprovalRepository.LoadEntities(a => a.IsDelete == false && a.OrderId == orderId && a.ApprovalStatus == (int)OrderApprovalStatusEnum.Init)
                .FirstOrDefault();
            if (entity == null)
            {
                return null;
            }

            var imageEntity = this.orderImageApprovalRepository.FindEntityById(entity.Id);
            if (imageEntity == null)
            {
                return null;
            }

            return new OrderApprovalImgDto()
            {
                ApprovalId = imageEntity.OrderApprovalId,
                BiYeZhengImg = imageEntity.BiYeZhengImg,
                IDCard1 = imageEntity.IDCard1,
                IDCard2 = imageEntity.IDCard2,
                LiangCunLanDiImg = imageEntity.LiangCunLanDiImg,
                MianKaoJiSuanJiImg = imageEntity.MianKaoJiSuanJiImg,
                MianKaoYingYuImg = imageEntity.MianKaoYingYuImg,
                OrderId = imageEntity.OrderId,
                QiTa = imageEntity.QiTa,
                TouXiang = imageEntity.TouXiang,
                XueXinWangImg = imageEntity.XueXinWangImg
            };
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="approvalIdList">approvalIdList</param>
        /// <returns></returns>
        public ResultMsg Delete(List<Guid> approvalIdList)
        {
            return this.orderApprovalRepository.Delete(approvalIdList);
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="approvalIdList">approvalIdList</param>
        /// <returns></returns>
        public ResultMsg Submit(List<Guid> approvalIdList)
        {
            return this.orderApprovalRepository.Submit(approvalIdList);
        }

        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="approvalId">approvalId</param>
        /// <param name="approved">approved</param>
        /// <param name="comment">comment</param>
        /// <returns></returns>
        public ResultMsg Approval(Guid approvalId,bool approved,string comment)
        {
            return this.orderApprovalRepository.Approval(approvalId, approved, comment);
        }

        /// <summary>
        /// 根据订单ID获得待审核的订单修改申请
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public List<OrderUpdateApprovalListDto> GetOrderUpdateApprovalList(OrderUpdateApprovalReq req, out int reCount)
        {
            return this.orderApprovalRepository.GetOrderUpdateApprovalList(req, out reCount);
        }
    }
}
