using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Systems;

namespace EnrolmentPlatform.Project.DTO.Product
{
    /// <summary>
    /// 菜名列表Dto
    /// </summary>
    public class FoodListDto
    {
        /// <summary>
        /// 菜Id
        /// </summary>
        public Guid FoodId { get; set; }

        /// <summary>
        /// 菜名称
        /// </summary> 
        public string FoodName { get; set; }

        /// <summary>
        /// 价格
        /// </summary> 
        public decimal Price { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImgUrl { get; set; }
    }

    /// <summary>
    /// 菜名查询Dto
    /// </summary>
    public class FoodSearchDto
    {
        /// <summary>
        /// 供应商Id
        /// </summary>
        public Guid SupplierId { get; set; }

        /// <summary>
        /// 菜名称
        /// </summary> 
        public string FoodName { get; set; }
    }

    /// <summary>
    /// 菜名编辑Dto
    /// </summary>
    public class FoodEditDto
    {
        /// <summary>
        /// 菜Id
        /// </summary>
        public Guid FoodId { get; set; }

        /// <summary>
        /// 菜名称
        /// </summary> 
        public string FoodName { get; set; }

        /// <summary>
        /// 价格
        /// </summary> 
        public decimal Price { get; set; }

        /// <summary>
        /// 描述
        /// </summary> 
        public string Description { get; set; }

        /// <summary>
        /// 店铺Id
        /// </summary> 
        public Guid ShopId { get; set; }

        /// <summary>
        /// 企业Id
        /// </summary> 
        public Guid EnterpriseId { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        public List<OptionParamForPictureDto> FoodImgList { get; set; }

        /// <summary>
        /// 创建者Id
        /// </summary>
        public Guid CreatorUserId { get; set; }

        /// <summary>
        /// 创建者账号
        /// </summary>
        public string CreatorAccount { get; set; }
    }
}
