using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities.Finance;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Finance;

namespace EnrolmentPlatform.Project.IBLL.Finance
{
    public interface IT_OrderSettlementService:IBaseService<T_OrderSettlement>
    {
        /// <summary>
        /// 获取结算单 分页列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GridDataResponse GetPageList(OrderSettlementRequestDto request);

        /// <summary>
        /// 获取  企业 结算中心 待结算金额，已结算金额
        /// </summary>
        /// <param name="enterpriseId"></param>
        /// <returns></returns>
        SettlementCenterDto GetSettlementCenterInfo(Guid enterpriseId);

        /// <summary>
        /// 获取结算单 详情 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        SettlementsDetailsDto GetSettlementsDetails(Guid id);


        /// <summary>
        /// 生产结算单  直接把结算单的状态：已结算
        /// </summary>
        void SettlementBillService();

        /// <summary>
        /// 计算下个结算周期
        /// </summary>
        void ResetNextSettlementDate();

        /// <summary>
        /// 获取账户资产
        /// </summary>
        /// <returns></returns>
        AccountAssetsDto GetAccountAssets();


    }
}
