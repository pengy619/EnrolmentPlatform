using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Accounts;

namespace EnrolmentPlatform.Project.IBLL.Accounts
{
    /// <summary>
    /// 收货地址服务接口
    /// </summary>
    public interface IT_DeliveryAddressService
    {
        /// <summary>
        /// 获得会员收货地址
        /// </summary>
        /// <param name="userId">会员ID</param>
        /// <returns></returns>
        List<DeliveryAddressDto> GetMemberAllDeliveryAddressList(Guid memberId);

        /// <summary>
        /// 获得会员收货地址详情
        /// </summary>
        /// <param name="addressId">收货地址ID</param>
        /// <param name="userId">会员ID</param>
        /// <returns></returns>
        DeliveryAddressDto GetMemberDeliveryAddress(Guid addressId,Guid userId);

        /// <summary>
        /// 获得会员默认收货地址
        /// </summary>
        /// <param name="userId">会员ID</param>
        /// <returns></returns>
        DeliveryAddressDto GetMemberDefaultDeliveryAddress(Guid memberId);

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="addressId">新增成功后的地址ID</param>
        /// <returns>1：成功，2：失败，3：最多只能添加10条</returns>
        int Save(DeliveryAddressDto dto, ref Guid addressId);

        /// <summary>
        /// 设置为默认收货地址
        /// </summary>
        /// <param name="memberId">会员ID</param>
        /// <param name="deliveryId">收货地址ID</param>
        /// <returns></returns>
        bool SetDefault(Guid memberId, Guid deliveryId);

        /// <summary>
        /// 删除收货地址
        /// </summary>
        /// <param name="deleiveryId">收货地址ID</param>
        /// <returns></returns>
        bool Delete(Guid memberId, Guid deleiveryId);
    }
}
