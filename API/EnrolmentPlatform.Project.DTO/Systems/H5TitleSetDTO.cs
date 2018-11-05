using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Systems
{
    /// <summary>
    /// H5标题设置
    /// </summary>
    [Serializable]
    [DataContract]
    public class H5TitleSetDTO : BasePostOperation
    {
        /// <summary>
        /// 标题
        /// </summary>
        [DataMember]
        public string H5Title { get; set; }
    }
}
