using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.Domain.Entities.Finance;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Enums.Enterprise;
using EnrolmentPlatform.Project.DTO.Enums.Finance;
using EnrolmentPlatform.Project.DTO.Enums.Orders;
using EnrolmentPlatform.Project.DTO.Finance;
using EnrolmentPlatform.Project.IBLL.Finance;
using EnrolmentPlatform.Project.IBLL.Systems;
using EnrolmentPlatform.Project.IDAL.Accounts;
using EnrolmentPlatform.Project.IDAL.Finance;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using EnrolmentPlatform.Project.IDAL;
using EnrolmentPlatform.Project.Infrastructure.Castle;

namespace EnrolmentPlatform.Project.BLL.Finance
{
    public class T_OrderSettlementService : BaseService<T_OrderSettlement>, IT_OrderSettlementService, IInterceptorLogic
    {
        private IT_EnterpriseRepository _enterpriseRepository;
        public IT_OrderSettlementInfoRepository _orderSettlementInfoRepository;
        protected IT_SystemMessageService _systemMessageService;
        protected IDbContextFactory _dbContextFactory;
        public IT_AccountDetailInfoRepository _accountDetailInfoRepository;

        public override bool SetCurrentRepository()
        {
            this.CurrentRepository = DIContainer.Resolve<IT_OrderSettlementRepository>();
            base.AddDisposableObject(base.CurrentRepository);
            _enterpriseRepository = DIContainer.Resolve<IT_EnterpriseRepository>();
            base.AddDisposableObject(_enterpriseRepository);
            _orderSettlementInfoRepository = DIContainer.Resolve<IT_OrderSettlementInfoRepository>();
            base.AddDisposableObject(_orderSettlementInfoRepository);
            _accountDetailInfoRepository = DIContainer.Resolve<IT_AccountDetailInfoRepository>();
            base.AddDisposableObject(_accountDetailInfoRepository);
            this._systemMessageService = DIContainer.Resolve<IT_SystemMessageService>();
            base.AddDisposableObject(_systemMessageService);
            this._dbContextFactory = DIContainer.Resolve<IDbContextFactory>();
            base.AddDisposableObject(_dbContextFactory);
            return true;
        }

        /// <summary>
        /// 获取结算单 分页列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>

        public GridDataResponse GetPageList(OrderSettlementRequestDto request)
        {
            GridDataResponse response = new GridDataResponse();
            response.Data = new List<OrderSettlementDto>();
            var _whereLambda = ExtLinq.True<T_OrderSettlement>();
            if (request.EnterpriseId.HasValue)
                _whereLambda = _whereLambda.And(t => t.EnterpriseId == request.EnterpriseId);
            response.Data = CurrentRepository.LoadPageEntitiesOrderByField(
              _whereLambda,
             "Unix",
              request.Limit,
              request.Page,
              out int records,
              (request.Sort ?? "desc").ToLower().Equals("asc")
              ).Select(o => new OrderSettlementDto
              {
                  Id = o.Id,
                  OrderQuantity = o.OrderQuantity,
                  CreatorTime = o.CreatorTime,
                  Status = (SettlementStatusEnum)o.Status,
                  SettlementAmount = o.SettlementAmount,
                  SettlementNo = o.SettlementNo,
                  TotalOrderAmount = o.TotalOrderAmount
              }).ToList();
            response.Count = records;
            return response;
        }

        /// <summary>
        ///  获取  企业 结算中心 待结算金额，已结算金额
        /// </summary>
        /// <param name="enterpriseId"></param>
        /// <returns></returns>
        public SettlementCenterDto GetSettlementCenterInfo(Guid enterpriseId)
        {
            SettlementCenterDto result = new SettlementCenterDto();
            if (enterpriseId.IsEmpty())
            {
                result.SettledPrice = CurrentRepository.LoadEntities(ExtLinq.True<T_OrderSettlement>()).Select(o => o.SettlementAmount).DefaultIfEmpty(0).Sum();
            }
            else
            {
                var enterpriseModel = _enterpriseRepository.FindEntityById(enterpriseId);
                result.NextSettlementDate = enterpriseModel.NextSettlementDate.HasValue ? enterpriseModel.NextSettlementDate.Value.ToString("yyyy-MM-dd") : "";
                result.LastSettlementDate = enterpriseModel.LastSettlementDate.HasValue ? enterpriseModel.LastSettlementDate.Value.ToString("yyyy-MM-dd") : "";
                result.SettlementPeriod = EnumDescriptionHelper.GetDescription((SettlementCycleEnum)enterpriseModel.SettlementCycle);
                var query = CurrentRepository.LoadEntities(t => t.EnterpriseId == enterpriseId);
                result.SettledPrice = query.Select(o => o.SettlementAmount).DefaultIfEmpty(0).Sum();
            }
            return result;

        }

        /// <summary>
        /// 获取结算单 详情 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public SettlementsDetailsDto GetSettlementsDetails(Guid id)
        {
            var orderSettlement = CurrentRepository.FindEntityById(id);
            if (orderSettlement!=null)
            {
                var enterprise = _enterpriseRepository.FindEntityById(orderSettlement.EnterpriseId,Domain.EFContext.E_DbClassify.Write,true);
                if (enterprise!=null)
                {
                    SettlementsDetailsDto result = new SettlementsDetailsDto()
                    {
                        CreatorAccount = enterprise.EnterpriseName,
                        OrderQuantity = orderSettlement.OrderQuantity,
                        CreatorTime = orderSettlement.CreatorTime,
                        Status = (OrderSettlementStatusEnum)orderSettlement.Status,
                        SettlementAmount = orderSettlement.SettlementAmount,
                        SettlementNo = orderSettlement.SettlementNo,
                        TotalOrderAmount = orderSettlement.TotalOrderAmount
                    };
                    var list = _orderSettlementInfoRepository.LoadEntities(o => o.SettlementNo == orderSettlement.SettlementNo).ToList();
                    result.SettlementCycle = list.FirstOrDefault()?.SettlementCycle;
                    result.SettlementsOrderInfoDtos = list.Select(t => new SettlementsOrderInfoDto
                    {
                        Amount = t.OrderAmount,
                        Classify = (OrderClassifyEnum)t.OrderClassify,
                        CreatorTime = t.CreatorTime,
                        OrderNo = t.OrderNo,
                        OrderStatus = t.OrderStatus,
                        ProductName = t.OrderName,
                        SettlementAmount = t.SettlementAmount
                    }).ToList();
                    return result;

                }
                else
                {
                    throw new Exception("id为【" + orderSettlement.EnterpriseId.ToString() + "】企业不存在");
                }
   
            }
            else
            {
                throw new Exception("id为【" + id.ToString() + "】结算单编号不存在");
            }


        }


        /// <summary>
        /// 生产结算单  直接把结算单的状态：已结算
        /// </summary>
        public void SettlementBillService()
        {
            
        }

        /// <summary>
        ///计算下个结算周期 
        /// </summary>
        public void ResetNextSettlementDate()
        {
            var currentdate = DateTime.Now.Date;
            var list = _enterpriseRepository.LoadEntities(o => !o.NextSettlementDate.HasValue || o.NextSettlementDate <= currentdate).ToList();
            foreach (var m in list)
            {
                var lastest = m.NextSettlementDate ?? currentdate;
                m.LastSettlementDate = lastest;
                if (m.SettlementCycle == 1)//及时
                {
                    m.NextSettlementDate = lastest.AddDays(1);
                }
                else if (m.SettlementCycle == 2)//周结
                {
                    m.NextSettlementDate = lastest.AddDays(7);
                }
                else if (m.SettlementCycle == 3)//月结
                {
                    m.NextSettlementDate = lastest.AddMonths(1);
                }
            }
            if (list.Count > 0)
            {
                _enterpriseRepository.UpdateEntities(list);
            }
        }


        /// <summary>
        ///  获取账户资产
        /// </summary>
        public AccountAssetsDto GetAccountAssets()
        {
            AccountAssetsDto accountAssetsDto = new AccountAssetsDto();
            accountAssetsDto.Assets = _enterpriseRepository.LoadEntities(ExtLinq.True<T_Enterprise>()).Select(o => o.Balance).DefaultIfEmpty().Sum();
            return accountAssetsDto;
        }




    }
}
