using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Product;

namespace EnrolmentPlatform.Project.DTO.Product
{
    /// <summary>
    /// 农产品推荐
    /// </summary>
    [Serializable]
    public class RecommendProductCategoriesDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid? Id { set; get; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public Guid CateId { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string CateName { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 类型：1： H5
        /// </summary>
        public RecommendPositionEnum Classify { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { set; get; }
    }

    /// <summary>
    /// 农产品推荐保存DTO
    /// </summary>
    public class RecommendProductCategoriesSaveDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid? Id { set; get; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public Guid CateId { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        public string CateName { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 类型：1： H5
        /// </summary>
        public RecommendPositionEnum Classify { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { set; get; }
    }

    /// <summary>
    /// 推荐产品分类排序DTO
    /// </summary>
    public class RecommendProductCategoriesDeleteDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public List<Guid> Ids { set; get; }

        /// <summary>
        /// 类型
        /// </summary>
        public RecommendPositionEnum Classify { set; get; }
    }

    /// <summary>
    /// 推荐产品分类查询
    /// </summary>
    public class RecommendProductCategoriesSearchDto
    {
        public RecommendPositionEnum Type { set; get; }
    }
}
