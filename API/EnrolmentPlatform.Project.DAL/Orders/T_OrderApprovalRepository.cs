using EnrolmentPlatform.Project.Domain.EFContext;
using EnrolmentPlatform.Project.Domain.Entities.Orders;
using EnrolmentPlatform.Project.DTO.Enums.Orders;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.IDAL.Orders;
using EnrolmentPlatform.Project.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DAL.Orders
{
    public class T_OrderApprovalRepository : BaseRepository<T_OrderApproval>, IT_OrderApprovalRepository
    {
        /// <summary>
        /// 审批
        /// </summary>
        /// <param name="approvalId">approvalId</param>
        /// <param name="approved">approved</param>
        /// <param name="comment">comment</param>
        /// <returns></returns>
        public ResultMsg Approval(Guid approvalId, bool approved, string comment)
        {
            EnrolmentPlatformDbContext dbContext = this.GetDbContext();
            var approval = dbContext.T_OrderApproval.FirstOrDefault(a => a.Id == approvalId);
            if (approval == null)
            {
                return new ResultMsg() { IsSuccess = false, Info = "找不到审批信息" };
            }

            //审核通过
            if (approved == true)
            {
                var order = dbContext.T_Order.FirstOrDefault(a => a.Id == approval.OrderId);
                if (order == null)
                {
                    return new ResultMsg() { IsSuccess = false, Info = "找不到订单信息" };
                }

                var orderImage = dbContext.T_OrderImage.FirstOrDefault(a => a.OrderId == approval.OrderId);
                if (orderImage == null)
                {
                    return new ResultMsg() { IsSuccess = false, Info = "找不到订单图片信息" };
                }

                //1.修改审核信息
                approval.ApprovalStatus = (int)OrderApprovalStatusEnum.Approved;
                approval.ApprovalComment = comment;
                dbContext.Entry(approval).State = EntityState.Modified;

                //2.修改订单基础信息
                order.Address = approval.Address;
                order.AllOrderImageUpload = approval.AllOrderImageUpload;
                order.BiYeZhengBianHao = approval.BiYeZhengBianHao;
                order.CreatorAccount = approval.ZhaoShengLaoShi;
                order.Email = approval.Email;
                order.GongZuoDanWei = approval.GongZuoDanWei;
                order.GraduateSchool = approval.GraduateSchool;
                order.HighesDegree = approval.HighesDegree;
                order.IDCardNo = approval.IDCardNo;
                order.JiGuan = approval.JiGuan;
                order.MinZu = approval.MinZu;
                order.Phone = approval.Phone;
                order.Remark = approval.Remark;
                order.Sex = approval.Sex;
                order.StudentName = approval.StudentName;
                order.TencentNo = approval.TencentNo;
                dbContext.Entry(order).State = EntityState.Modified;

                //3.修改订单照片信息
                var orderImageUpdateInfo = dbContext.T_OrderImageApproval.FirstOrDefault(a => a.OrderApprovalId == approvalId);
                if (orderImageUpdateInfo != null)
                {
                    orderImage.BiYeZhengImg = orderImageUpdateInfo.BiYeZhengImg;
                    orderImage.IDCard1 = orderImageUpdateInfo.IDCard1;
                    orderImage.IDCard2 = orderImageUpdateInfo.IDCard2;
                    orderImage.LiangCunLanDiImg = orderImageUpdateInfo.LiangCunLanDiImg;
                    orderImage.MianKaoJiSuanJiImg = orderImageUpdateInfo.MianKaoJiSuanJiImg;
                    orderImage.MianKaoYingYuImg = orderImageUpdateInfo.MianKaoYingYuImg;
                    orderImage.QiTa = orderImageUpdateInfo.QiTa;
                    orderImage.TouXiang = orderImageUpdateInfo.TouXiang;
                    orderImage.XueXinWangImg = orderImageUpdateInfo.XueXinWangImg;
                    dbContext.Entry(orderImage).State = EntityState.Modified;
                }

                //4.先删除原有的订单附件
                dbContext.T_File.RemoveRange(dbContext.T_File.Where(a => a.ForeignKeyId == approval.OrderId));

                //5.添加附件至附件表
                var approvalFiles = dbContext.T_File.Where(a => a.ForeignKeyId == approval.Id).ToList();
                foreach (var item in approvalFiles)
                {
                    dbContext.T_File.Add(new Domain.Entities.T_File()
                    {
                        ForeignKeyId = approval.OrderId,
                        FilePath = item.FilePath,
                        FileName = item.FileName,
                        CreatorUserId = approval.CreatorUserId,
                        CreatorAccount = approval.CreatorAccount
                    });
                }
            }
            else
            {
                //修改审核信息
                approval.ApprovalStatus = (int)OrderApprovalStatusEnum.Faild;
                approval.ApprovalComment = comment;
                dbContext.Entry(approval).State = EntityState.Modified;
            }

            dbContext.ModuleKey = approvalId.ToString();
            dbContext.LogChangesDuringSave = true;
            dbContext.BusinessName = "订单修改审核";
            dbContext.SaveChanges();
            return new ResultMsg() { IsSuccess = true };
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="approvalIdList">approvalIdList</param>
        /// <returns></returns>
        public ResultMsg Delete(List<Guid> approvalIdList)
        {
            EnrolmentPlatformDbContext dbContext = this.GetDbContext();
            var approvals = dbContext.T_OrderApproval.Where(a => approvalIdList.Contains(a.Id));
            foreach (var item in approvals)
            {
                if (item.ApprovalStatus != (int)OrderApprovalStatusEnum.Init && item.ApprovalStatus != (int)OrderApprovalStatusEnum.Faild)
                {
                    return new ResultMsg() { IsSuccess=false, Info="当前状态不允许删除。" };
                }
            }

            //1.删除图片
            var images = dbContext.T_OrderImageApproval.Where(a => approvalIdList.Contains(a.OrderApprovalId));
            dbContext.T_OrderImageApproval.RemoveRange(images);

            //2.删除审批
            dbContext.T_OrderApproval.RemoveRange(approvals);

            //3.删除附件
            var files = dbContext.T_File.Where(a => approvalIdList.Contains(a.ForeignKeyId));
            dbContext.T_File.RemoveRange(files);

            dbContext.LogChangesDuringSave = false;
            dbContext.SaveChanges();
            return new ResultMsg() { IsSuccess = true };
        }

        /// <summary>
        /// 提交
        /// </summary>
        /// <param name="approvalIdList">approvalIdList</param>
        /// <returns></returns>
        public ResultMsg Submit(List<Guid> approvalIdList)
        {
            EnrolmentPlatformDbContext dbContext = this.GetDbContext();
            var approvals = dbContext.T_OrderApproval.Where(a => approvalIdList.Contains(a.Id));
            foreach (var item in approvals)
            {
                if (item.ApprovalStatus != (int)OrderApprovalStatusEnum.Init)
                {
                    return new ResultMsg() { IsSuccess = false, Info = "当前状态不允许提交。" };
                }
            }

            foreach (var item in approvals)
            {
                item.ApprovalStatus = (int)OrderApprovalStatusEnum.Approval;
                dbContext.Entry(item).State = EntityState.Modified;
            }
            dbContext.SaveChanges();
            return new ResultMsg() { IsSuccess = true };
        }

        /// <summary>
        /// 根据订单ID获得待审核的订单修改申请
        /// </summary>
        /// <param name="req">req</param>
        /// <param name="reCount">reCount</param>
        /// <returns></returns>
        public List<OrderUpdateApprovalListDto> GetOrderUpdateApprovalList(OrderUpdateApprovalReq req,out int reCount)
        {
            StringBuilder sql = new StringBuilder(@"SELECT 
                        a.Id AS OrderId, 
                        a.CreatorTime AS CreateTime, 
	                    a.[CreatorAccount] AS CreateUserName,
                        a.ApprovalStatus,
                        a.StudentName,
                        a.IDCardNo,
                        m1.Name as BatchName,
	                    m2.Name as SchoolName,
	                    m3.Name as LevelName,
	                    m4.Name as MajorName
                        from T_OrderApproval AS a 
                        LEFT JOIN T_Order AS o ON A.OrderId=o.Id
                        LEFT JOIN T_Metadata AS m1 ON o.BatchId = m1.Id
                        LEFT JOIN T_Metadata AS m2 ON o.SchoolId = m2.Id
                        LEFT JOIN T_Metadata AS m3 ON o.LevelId = m3.Id
                        LEFT JOIN T_Metadata AS m4 ON o.MajorId = m4.Id
                        LEFT JOIN T_OrderImage as im ON o.Id=im.OrderId
                        where o.IsDelete=0");

            List<SqlParameter> parameters = new List<SqlParameter>();

            if (!string.IsNullOrWhiteSpace(req.IDCard))
            {
                sql.Append(" and a.IDCard like @IDCard");
                parameters.Add(new SqlParameter("@IDCard", "%" + req.IDCard + "%"));
            }

            if (!string.IsNullOrWhiteSpace(req.Phone))
            {
                sql.Append(" and a.Phone like @Phone");
                parameters.Add(new SqlParameter("@Phone", "%" + req.Phone + "%"));
            }

            if (!string.IsNullOrWhiteSpace(req.SchoolName))
            {
                sql.Append(" and m2.Name like @SchoolName");
                parameters.Add(new SqlParameter("@SchoolName", "%" + req.SchoolName + "%"));
            }

            if (!string.IsNullOrWhiteSpace(req.StudentName))
            {
                sql.Append(" and a.StudentName like @StudentName");
                parameters.Add(new SqlParameter("@StudentName", "%" + req.StudentName + "%"));
            }

            if (req.FromChannelId.HasValue)
            {
                sql.Append(" and o.FromChannelId=@FromChannelId");
                parameters.Add(new SqlParameter("@FromChannelId", req.FromChannelId.Value));
            }

            if (req.Status.HasValue)
            {
                sql.Append(" and a.ApprovalStatus=@ApprovalStatus");
                parameters.Add(new SqlParameter("@ApprovalStatus", req.Status.Value));
            }

            EnrolmentPlatformDbContext dbContext = this.GetDbContext();
            reCount = dbContext.Database.SqlQuery<int>("select count(1) from (" + sql.ToString() + ") as t1", parameters.Select(x => ((ICloneable)x).Clone()).ToArray()).FirstOrDefault();
            if (reCount == 0)
            {
                return null;
            }

            var list = dbContext.Database.SqlQuery<OrderUpdateApprovalListDto>(sql.ToString(), (SqlParameter[])parameters.ToArray().Clone())
               .OrderByDescending(a => a.CreateTime)
               .Skip((req.Page - 1) * req.Limit)
               .Take(req.Limit).ToList();

            return list;
        }
    }
}
