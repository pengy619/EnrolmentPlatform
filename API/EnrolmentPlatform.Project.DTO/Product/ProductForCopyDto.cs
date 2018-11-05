using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Product
{
    /// <summary>
    /// 产品复制参数
    /// </summary>
    public class ProductForCopyDto
    {
        /// <summary>
        /// 产品Id
        /// </summary>
        public Guid ProductId { get; set; }
        /// <summary>
        /// 新产品Id
        /// </summary>
        public Guid NewProductId { get; set; }
        /// <summary>
        /// 供应商Id
        /// </summary>
        public Guid SupplierId { get; set; }
        /// <summary>
        /// 产品类型
        /// </summary>
        public int Classify { get; set; }
        /// <summary>
        /// 创建人Id
        /// </summary>
        public Guid CreatorUserId { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatorAccount { get; set; }
    }
}
