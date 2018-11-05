using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using EnrolmentPlatform.Project.DTO.Accounts;
using EnrolmentPlatform.Project.IBLL.Accounts;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.WebApi.WebLibrary;

namespace EnrolmentPlatform.Project.WebApi.Areas.Accounts
{
    /// <summary>
    /// 收货地址API
    /// </summary>
    public class DeliveryAddressController : ApiBaseController
    {
        protected IT_DeliveryAddressService DeliveryAddressService;
        public DeliveryAddressController()
        {
            this.DeliveryAddressService = DIContainer.Resolve<IT_DeliveryAddressService>();
        }

        /// <summary>
        /// 获得用户的收货地址
        /// </summary>
        /// <param name="memberId">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetMemberAllDeliveryAddressList(Guid memberId)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.IsSuccess = true;
                _resultMsg.Data = this.DeliveryAddressService.GetMemberAllDeliveryAddressList(memberId);
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 获得会员收货地址详情
        /// </summary>
        /// <param name="addressId">收货地址ID</param>
        /// <param name="userId">会员ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetMemberDeliveryAddress(Guid addressId,Guid userId)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.IsSuccess = true;
                _resultMsg.Data = this.DeliveryAddressService.GetMemberDeliveryAddress(addressId, userId);
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 获得会员默认收货地址
        /// </summary>
        /// <param name="memberId">用户ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetMemberDefaultDeliveryAddress(Guid memberId)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.IsSuccess = true;
                _resultMsg.Data = this.DeliveryAddressService.GetMemberDefaultDeliveryAddress(memberId);
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 保存（新增，编辑）
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> Save(DeliveryAddressDto dto)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                Guid newId= Guid.Empty;
                var ret= this.DeliveryAddressService.Save(dto,ref newId);
                if (ret == 1)
                {
                    _resultMsg.IsSuccess = true;
                    _resultMsg.Data = newId;
                }
                else if (ret == 2)
                {
                    _resultMsg.IsSuccess = false;
                    _resultMsg.Info = "添加失败。";
                }
                else
                {
                    _resultMsg.IsSuccess = false;
                    _resultMsg.Info = "最多只能添加10条。";
                }
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 设为默认
        /// </summary>
        /// <param name="deliveryId">收货ID</param>
        /// <param name="memberId">会员ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> SetDefault(Guid memberId, Guid deliveryId)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.IsSuccess = this.DeliveryAddressService.SetDefault(memberId, deliveryId);
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="deliveryId">收货ID</param>
        /// <param name="memberId">会员ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> Delete(Guid memberId, Guid deliveryId)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.IsSuccess = this.DeliveryAddressService.Delete(memberId, deliveryId);
                return _resultMsg.ResponseMessage();
            });
        }
    }
}
