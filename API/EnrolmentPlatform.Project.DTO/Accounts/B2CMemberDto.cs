using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Accounts
{
    /// <summary>
    /// B2C会员账号信息
    /// </summary>
    public class B2CMemberDto
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        public Guid Id { set; get; }

        /// <summary>
        /// 账号
        /// </summary>
        public string AccountNo { set; get; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { set; get; }

        /// <summary>
        /// 图像
        /// </summary> 
        public string Picture { get; set; }

        /// <summary>
        /// 昵称
        /// </summary> 
        public string Nickname { get; set; }

        /// <summary>
        /// 备注
        /// </summary> 
        public string Remark { get; set; }

        /// <summary>
        /// 性别
        /// </summary> 
        public string Sex { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { set; get; }

        /// <summary>
        /// 带*的手机号
        /// </summary>
        public string SercretPhone
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.Phone))
                {
                    if (this.Phone.Length == 11)
                    {
                        return this.Phone.Substring(0, 3) + "*****" + this.Phone.Substring(8);
                    }
                    return this.Phone;
                }
                return "";
            }
        }

        /// <summary>
        /// 地址Id
        /// </summary> 
        public Guid? AddressId { get; set; }
    }
}
