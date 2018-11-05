using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Systems
{
    /// <summary>
    /// 全站配置
    /// </summary>
    [Serializable]
    [DataContract]
    public class TotalStationSetDTO : BasePostOperation
    {
        /// <summary>
        /// 网站Logo
        /// </summary>
        [DataMember]
        public string WebsiteLogo { get; set; }

        /// <summary>
        /// 网站标题
        /// </summary>
        [DataMember]
        public string WebsiteTitle { get; set; }

        /// <summary>
        /// 网站关键字
        /// </summary>
        [DataMember]
        public string WebsiteKW { get; set; }
    }
}
