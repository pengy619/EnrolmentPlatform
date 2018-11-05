using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Systems;

namespace EnrolmentPlatform.Project.DTO.Accounts
{
    /// <summary>
    /// 常用联系人
    /// </summary>
    public class ContactsDto
    {
        public Guid? Id { set; get; }

        /// <summary>
        /// 游客姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 带星号的手机号
        /// </summary>
        public string SercretPhone
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.Phone))
                {
                    if (this.Phone.Length == 11)
                    {
                        return this.Phone.Substring(0, 3) + "****" + this.Phone.Substring(7, 4);
                    }
                }

                return this.Phone;
            }
        }

        /// <summary>
        /// 证件类型：【1身份证】【2护照】【3台胞证】【4港澳通行证】【5其他】
        /// </summary>
        public CredentialsTypeEnum CredentialsType { get; set; }

        /// <summary>
        /// 证件号
        /// </summary>
        public string Credentials { get; set; }

        /// <summary>
        /// 带星号的证件号
        /// </summary>
        public string SercretCredentials
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.Credentials))
                {
                    if (this.Credentials.Length == 18)
                    {
                        return this.Credentials.Substring(0, 5) + "*********" + this.Credentials.Substring(14, 4);
                    }
                }

                return this.Credentials;
            }
        }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 类型【1成人】【2儿童】【3；老人】
        /// </summary>
        public ContactsClassifyEnum Classify { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { set; get; }
    }
}
