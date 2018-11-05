using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Product;

namespace EnrolmentPlatform.Project.DTO.Product
{
    /// <summary>
    /// 推荐产品 Dto
    /// </summary>
    public class RecommendProductDto
    {

        public Guid Id { get; set; }

        /// <summary>
        /// 产品Id
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductNumber { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 类型：【1:推荐到B2C首页】 RecommendPositionEnum
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


        public Guid CreatorUserId { get; set; }

        public string CreatorAccount { get; set; }

    }

    /// <summary>
    /// 删除推荐产品 Dto
    /// </summary>
    public class DeleteRecommendProductDto
    {
        /// <summary>
        /// ID集合
        /// </summary>
        public List<Guid> Ids { get; set; }

        /// <summary>
        /// 操作人ID
        /// </summary>
        public Guid OperatorId { get; set; }

        /// <summary>
        /// /操作人
        /// </summary>
        public string Operator { get; set; }
    }

    /// <summary>
    ///  
    /// </summary>
    public class SpecialtyProductPageRequestDto : GridDataRequest
    {
        /// <summary>
        ///  产品名称
        /// </summary>
        public string ProductName { get; set; }
    }


    /// <summary>
    /// 推荐产品 分页Dto
    /// </summary>
    public class SpecialtyProductPageDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        ///  产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 产品编号
        /// </summary>
        public string ProductNumber { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

    }

}
