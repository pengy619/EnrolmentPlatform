using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Accounts
{
    /// <summary>
    /// 收货地址DTO
    /// </summary>
    public class DeliveryAddressDto
    {
        /// <summary>
        /// 收货地址ID
        /// </summary>
        public Guid? Id { set; get; }

        /// <summary>
        /// 用户ID
        /// </summary> 
        public Guid AccountId { get; set; }

        /// <summary>
        /// 收货人
        /// </summary> 
        public string Consignee { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary> 
        public string ContactPhone { get; set; }

        /// <summary>
        /// 具体地址
        /// </summary> 
        public string Address { get; set; }

        /// <summary>
        /// 完整地址名称
        /// </summary> 
        public string FullAddressName { get; set; }

        /// <summary>
        /// 地址ID
        /// </summary> 
        public Guid AddressId { get; set; }

        /// <summary>
        /// 默认地址
        /// </summary> 
        public bool IsDefault { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { set; get; }

        /// <summary>
        /// 带星号的手机号
        /// </summary>
        public string SercretPhone
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.ContactPhone))
                {
                    if (this.ContactPhone.Length == 11)
                    {
                        return this.ContactPhone.Substring(0, 3) + "****" + this.ContactPhone.Substring(7, 4);
                    }
                }

                return this.ContactPhone;
            }
        }
    }
}
