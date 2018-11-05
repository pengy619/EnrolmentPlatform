using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.DTO.Systems
{
    /// <summary>
    /// 允许账号核销游乐项目+景点
    /// </summary>
    public class AccountVerificationDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid Id { set; get; }

        /// <summary>
        /// 资源ID
        /// </summary>
        public Guid ResourceId { set; get; }

        /// <summary>
        /// 资源类型
        /// </summary>
        public string ResourceName { set; get; }

        /// <summary>
        /// 资源地址
        /// </summary>
        public string ResourceAddress { set; get; }

        /// <summary>
        /// 资源类型
        /// </summary>
        public VerificationTypeEnum Classify { set; get; }

        /// <summary>
        /// 资源类型名称
        /// </summary>
        public string ClassifyName
        {
            get
            {
                return EnumDescriptionHelper.GetDescription(this.Classify);
            }
        }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { set; get; }
    }
}
