using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Product
{
    /// <summary>
    /// 票务销售图文模式DTO
    /// </summary>
    public class ImageTikectListDto
    {
        /// <summary>
        /// 景点ID
        /// </summary>
        public Guid ScenicSportId { set; get; }

        /// <summary>
        /// 景点名称
        /// </summary>
        public string ScenicSport { set; get; }

        /// <summary>
        /// 景点图片
        /// </summary>
        public string Image { set; get; }

        /// <summary>
        /// 景点等级名称
        /// </summary>
        public string LevelName { set; get; }

        /// <summary>
        /// 景点分类名称
        /// </summary>
        public string ScenicClassifyName { set; get; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { set; get; }

        /// <summary>
        /// 开放时间
        /// </summary>
        public string OpenTime { set; get; }

        /// <summary>
        /// 票列表
        /// </summary>
        public List<ProductForTicketSalesDto> TicketList { set; get; }
    }
}
