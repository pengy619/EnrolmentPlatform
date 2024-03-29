﻿using EnrolmentPlatform.Project.DTO.Enums.Orders;
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
        /// 保存
        /// </summary>
        /// <param name="dto">dto</param>
        /// <returns></returns>
        public ResultMsg Save(OrderApprovalDto dto)
        {
            return this.orderApprovalRepository.Save(dto);
        }

        /// <summary>
        /// 保存图片
        /// </summary>
        /// <param name="dto">dto</param>
        /// <returns></returns>
        public ResultMsg SaveImage(OrderApprovalImgDto dto)
        {
            return this.orderApprovalRepository.SaveImage(dto);
        }

        /// <summary>
        /// 根据订单ID获得待审核的订单修改申请
        /// </summary>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public OrderApprovalDto GetOrderApplyApprovalInfoByOrderId(Guid orderId)
        {
            var entity = this.orderApprovalRepository.LoadEntities(a => a.IsDelete == false && a.OrderId == orderId
                && (a.ApprovalStatus == (int)OrderApprovalStatusEnum.Init || a.ApprovalStatus == (int)OrderApprovalStatusEnum.Approval))
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
                ZhaoShengLaoShi = entity.ZhaoShengLaoShi,
                SuoDuZhuanYe = entity.SuoDuZhuanYe,
                IsTvUniversity = entity.IsTvUniversity,
                GraduationTime = entity.GraduationTime,
                BatchId = entity.BatchId,
                LevelId = entity.LevelId,
                MajorId = entity.MajorId,
                SchoolId = entity.SchoolId,
                UserId = entity.CreatorUserId,
                CustomerField = entity.CustomerField,
                IDCardType = entity.IDCardType,
                DegreeType = entity.DegreeType
            };
        }

        /// <summary>
        /// 根据ID获得待审核的订单修改申请
        /// </summary>
        /// <param name="approvalId"></param>
        /// <returns></returns>
        public OrderApprovalDto GetOrderApplyApprovalInfo(Guid approvalId)
        {
            var entity = this.orderApprovalRepository.LoadEntities(a => a.Id == approvalId).FirstOrDefault();
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
                ZhaoShengLaoShi = entity.ZhaoShengLaoShi,
                SuoDuZhuanYe = entity.SuoDuZhuanYe,
                IsTvUniversity = entity.IsTvUniversity,
                GraduationTime = entity.GraduationTime,
                CustomerField = entity.CustomerField,
                BatchId = entity.BatchId,
                LevelId = entity.LevelId,
                MajorId = entity.MajorId,
                SchoolId = entity.SchoolId,
                IDCardType = entity.IDCardType,
                DegreeType = entity.DegreeType
            };
        }

        /// <summary>
        /// 根据ID获得待审核的订单图片修改申请
        /// </summary>
        /// <param name="approvalId"></param>
        /// <returns></returns>
        public OrderApprovalImgDto GetOrderImageApplyApprovalInfo(Guid approvalId)
        {
            var imageEntity = this.orderImageApprovalRepository.LoadEntities(a => a.OrderApprovalId == approvalId).FirstOrDefault();
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
        /// <param name="approvalIdList">approvalIdList</param>
        /// <param name="approved">approved</param>
        /// <param name="comment">comment</param>
        /// <returns></returns>
        public ResultMsg Approval(List<Guid> approvalIdList, bool approved, string comment)
        {
            return this.orderApprovalRepository.Approval(approvalIdList, approved, comment);
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
