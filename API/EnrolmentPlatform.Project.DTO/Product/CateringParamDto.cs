using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Product;

namespace EnrolmentPlatform.Project.DTO.Product
{
    /// <summary>
    /// 餐饮参数Dto
    /// </summary>
    public class CateringParamDto
    {
        /// <summary>
        /// 参数Id
        /// </summary>
        public Guid ParamId { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParamName { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public CateringParamClassifyEnum Classify { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 创建者Id
        /// </summary>
        public Guid CreatorUserId { get; set; }

        /// <summary>
        /// 创建者账号
        /// </summary>
        public string CreatorAccount { get; set; }
    }

    /// <summary>
    /// 餐饮参数查询Dto
    /// </summary>
    public class CateringParamSearchDto : GridDataRequest
    {
        /// <summary>
        /// 类型
        /// </summary>
        public CateringParamClassifyEnum Classify { get; set; }
    }
}
