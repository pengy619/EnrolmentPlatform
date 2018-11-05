using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Product
{
    /// <summary>
    /// 存过返回值
    /// </summary>
    [DataContract]
    public class DBOptionForResultDto
    {
        /// <summary>
        /// 存过返回值
        /// </summary>
        [DataMember]
        public int Result { get; set; }
    }
}
