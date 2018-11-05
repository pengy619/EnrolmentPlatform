using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.EFContext;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.IDAL.Systems;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.DAL.Systems
{
    public class T_SystemMessageRepository : BaseRepository<T_SystemMessage>, IT_SystemMessageRepository
    {
        /// <summary>
        /// 供应商根据条件分页查询系统消息列表
        /// </summary>
        /// <param name="paramForSystemMessageDto"></param>
        /// <returns></returns>
        public GridDataResponse GetSystemMessageForSupplierForList(ParamForSystemMessageDto paramForSystemMessageDto)
        {
            GridDataResponse gridDataResponse = new GridDataResponse();
            var db = base.GetDbContext();

            var resultData = from sm in db.T_SystemMessage
                             where sm.IsDelete == false && (paramForSystemMessageDto.Status > 0 ? sm.Status.Equals(paramForSystemMessageDto.Status) : true)
                             && (paramForSystemMessageDto.EnterpriseId.Equals(Guid.Empty) ? true : sm.EnterpriseId.Equals(paramForSystemMessageDto.EnterpriseId))
                             select new SystemMessageDto
                             {
                                 Id = sm.Id,
                                 BusinessName = sm.BusinessName,
                                 Content = sm.Content,
                                 Title = sm.Title,
                                 Status = sm.Status,
                                 CreatorTime = sm.CreatorTime,
                                 CreatorAccount = sm.CreatorAccount
                             };
            gridDataResponse.Count = resultData.Count();
            if (gridDataResponse.Count > 0)
            {
                resultData = ExtLinq.ApplyOrder(resultData, paramForSystemMessageDto.Field ?? "CreatorTime", (paramForSystemMessageDto.Sort ?? "desc").ToLower().Equals("asc"));
                resultData = resultData.Skip((paramForSystemMessageDto.Page - 1) * paramForSystemMessageDto.Limit).Take(paramForSystemMessageDto.Limit);
                gridDataResponse.Data = resultData.ToList();
            }
            return gridDataResponse;
        }


        /// <summary>
        /// 供应商查询首页信息
        /// </summary>
        /// <param name="supplierId"></param>
        /// <returns></returns>
        public HomeInfoForSupplierDto GetHomeInfoForSupplierId(Guid supplierId)
        {
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@SupplierId",supplierId)
            };
            List<HomeInfoForSupplierDto> list = this.SqlQuery<HomeInfoForSupplierDto>("exec [dbo].[P_GetHomeInfoForSupplierId] @SupplierId", E_DbClassify.Write, paras);
            HomeInfoForSupplierDto homeInfoForSupplierDto = list.FirstOrDefault();
            return homeInfoForSupplierDto;
        }

        /// <summary>
        /// ADMIN查询首页信息
        /// </summary> 
        /// <returns></returns>
        public HomeInfoForSupplierDto GetHomeInfoForAdmin()
        { 
            List<HomeInfoForSupplierDto> list = this.SqlQuery<HomeInfoForSupplierDto>("exec [dbo].[P_GetHomeInfoForAdmin]", E_DbClassify.Write);
            HomeInfoForSupplierDto homeInfoForSupplierDto = list.FirstOrDefault(); 
            return homeInfoForSupplierDto;
        }


        /// <summary>
        /// ADMIN查询首页信息
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public HomeInfoForAdminDto GetHomeInfoForAdminDtoByTime(string startTime, string endTime)
        {
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@StartTime",startTime),
                new SqlParameter("@EndTime",endTime)
            };
            List<HomeInfoForAdminDto> list = this.SqlQuery<HomeInfoForAdminDto>("exec [dbo].[P_GetHomeInfoForAdminByTime] @StartTime,@EndTime", E_DbClassify.Write, paras);
            HomeInfoForAdminDto homeInfoForAdminDto = list.FirstOrDefault();
            return homeInfoForAdminDto;
        }

        /// <summary>
        /// 供应商查询首页信息
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        public HomeInfoForAdminDto GetHomeInfoForSupplierByTime(string startTime, string endTime, Guid supplierId)
        {
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@StartTime",startTime),
                new SqlParameter("@EndTime",endTime),
                new SqlParameter("@SupplierId",supplierId)
            };
            List<HomeInfoForAdminDto> list = this.SqlQuery<HomeInfoForAdminDto>("exec [dbo].[P_GetHomeInfoForSupplierByTime] @StartTime,@EndTime,@SupplierId", E_DbClassify.Write, paras);
            HomeInfoForAdminDto homeInfoForAdminDto = list.FirstOrDefault();
            return homeInfoForAdminDto;
        }
    }
}
