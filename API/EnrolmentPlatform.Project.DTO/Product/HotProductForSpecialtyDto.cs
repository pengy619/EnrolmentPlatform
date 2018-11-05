using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Product
{
    /// <summary>
    /// 热门农产品
    /// </summary>
    public class HotProductForSpecialtyDto
    {
        /// <summary>
        /// 产品Id
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// 图片链接
        /// </summary>
        public string ImgUrl { get; set; }

        /// <summary>
        /// 已售数量
        /// </summary>
        public int SoldQuantity { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}
