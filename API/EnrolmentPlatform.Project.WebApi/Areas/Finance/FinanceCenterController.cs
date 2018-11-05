using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using EnrolmentPlatform.Project.DTO.Finance;
using EnrolmentPlatform.Project.DTO.Orders;
using EnrolmentPlatform.Project.IBLL.Finance;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.WebApi.WebLibrary;
using static EnrolmentPlatform.Project.DTO.Orders.TicketOrderInfo;

namespace EnrolmentPlatform.Project.WebApi.Areas.Finance
{
    /// <summary>
    /// 财务结算中心
    /// </summary>
    public class FinanceCenterController : ApiBaseController
    {
        protected IT_AccountDetailInfoService _accountDetailInfoService;
        protected IT_OrderSettlementService _orderSettlementService;


        public FinanceCenterController()
        {
            _accountDetailInfoService = DIContainer.Resolve<IT_AccountDetailInfoService>();
            _orderSettlementService = DIContainer.Resolve<IT_OrderSettlementService>();
        }

        /// <summary>
        ///  获取结算单列表
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> GetOrderSettlementList(OrderSettlementRequestDto request)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.Data = _orderSettlementService.GetPageList(request);
                return _resultMsg.ResponseMessage();
            });
        }


        /// <summary>
        /// 获取 企业资金交易流水 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> GetAccountDetailInfoList(AccountDetailInfoListDto request)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.Data = _accountDetailInfoService.GetPageList(request);
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        ///  获取  企业 结算中心 待结算金额，已结算金额
        /// </summary>
        /// <param name="enterpriseId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetSettlementCenterInfo(Guid enterpriseId)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.Data = _orderSettlementService.GetSettlementCenterInfo(enterpriseId);
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 获取结算单 详情 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetSettlementsDetails(Guid id)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.Data = _orderSettlementService.GetSettlementsDetails(id);
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 计算下个结算周期
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> ResetNextSettlementDate()
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _orderSettlementService.ResetNextSettlementDate();
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 生产结算单
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> SettlementBillService()
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _orderSettlementService.SettlementBillService();
                return _resultMsg.ResponseMessage();
            });
        }


        /// <summary>
        ///  景区  获取账户资产
        /// </summary>
        /// <returns></returns>
        public async Task<HttpResponseMessage> GetAccountAssets()
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg
                {
                    Data = _orderSettlementService.GetAccountAssets()
                };
                return _resultMsg.ResponseMessage();
            });
        }
    }
}
