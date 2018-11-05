using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Product
{
    public class SpecialtyListForB2CDto
    {
        /// <summary>
        /// 产品Id
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// 产品图片
        /// </summary>
        public string ImgPath { get; set; }

        /// <summary>
        /// 销售价格
        /// </summary>
        public decimal SalesPrice { get; set; }

        /// <summary>
        /// 销售单位
        /// </summary>
        public string SalesUnit { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 品种名称
        /// </summary>
        public string VarietiesName { get; set; }

        /// <summary>
        /// 产品规格
        /// </summary>
        public string SpecsStr { get; set; }

        /// <summary>
        /// 销量
        /// </summary>
        public int SoldQuantity { get; set; }

        /// <summary>
        /// 销售模式
        /// </summary> 
        public int SalesModel { get; set; }
    }

    public class SpecialtyListForB2CSearchDto : GridDataRequest
    {
        /// <summary>
        /// 销售模式
        /// </summary>
        public int SalesModel { get; set; }

        /// <summary>
        /// 产品分类Id
        /// </summary>
        public Guid? CategoriesId { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 品种集合
        /// </summary>
        public List<Guid> VarietiesIds { get; set; }

        /// <summary>
        /// 供应商Id
        /// </summary>
        public Guid? SupplierId { get; set; }
    }
}
