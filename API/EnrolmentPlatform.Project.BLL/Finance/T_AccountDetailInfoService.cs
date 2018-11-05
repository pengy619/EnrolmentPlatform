using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Enums.Orders;
using EnrolmentPlatform.Project.DTO.Finance;
using EnrolmentPlatform.Project.IBLL.Finance;
using EnrolmentPlatform.Project.IDAL.Finance;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.Castle;

namespace EnrolmentPlatform.Project.BLL.Finance
{
    public class T_AccountDetailInfoService : BaseService<T_AccountDetailInfo>, IT_AccountDetailInfoService, IInterceptorLogic
    {
        public override bool SetCurrentRepository()
        {
            this.CurrentRepository = DIContainer.Resolve<IT_AccountDetailInfoRepository>();
            base.AddDisposableObject(base.CurrentRepository);
            return true;

        }

        /// <summary>
        ///  获取  账户资金流水  分页列表
        /// </summary>
        /// <param name="request"></param>
        /// <param name="records"></param>
        /// <returns></returns>
       
        public GridDataResponse GetPageList(AccountDetailInfoListDto request)
        {
            GridDataResponse response = new GridDataResponse();
            response.Data = new List<AccountDetailInfoDto>();
            var _whereLambda = ExtLinq.True<T_AccountDetailInfo>();
            if (request.EnterpriseId.HasValue)//供应商ID为空时，查找景区的 账户资金流水
            {
                _whereLambda = _whereLambda.And(t => t.EnterpriseId == request.EnterpriseId);
            }
            if (request.TransactionType.HasValue)
            {
                _whereLambda = _whereLambda.And(o => o.TransactionClassify == (int)request.TransactionType.Value);
            }
            if (request.StartTime.HasValue && request.EndTime.HasValue)
            {
                _whereLambda = _whereLambda.And(o => o.CreatorTime >= request.StartTime.Value && o.CreatorTime <= request.EndTime.Value);
            }
            response.Data = CurrentRepository.LoadPageEntitiesOrderByField(
               _whereLambda,
              "Unix",
               request.Limit,
               request.Page,
               out int records,
               (request.Sort ?? "desc").ToLower().Equals("asc")
               ).Select(o => new AccountDetailInfoDto
               {
                   Amount = o.Amount,
                   CreatorTime = o.CreatorTime,
                   TransactionType = (DTO.Enums.Finance.TransactionTypeEnum)o.TransactionClassify,
                   TranscationAfterAmount = o.TranscationAfterAmount,
                   TranscationBeforeAmount = o.TranscationBeforeAmount,
                   TranscationNo = o.TranscationNo
               }).ToList();
            response.Count = records;
            return response;
        }
    }
}
