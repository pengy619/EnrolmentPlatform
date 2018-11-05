using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Product;

namespace EnrolmentPlatform.Project.DTO.Product
{
    /// <summary>
    /// 游乐项目推荐DTO
    /// </summary>
    [Serializable]
    public class RecommendAmusementProjectDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid? Id { set; get; }

        /// <summary>
        /// 项目Id
        /// </summary>
        public Guid AmusementProjectId { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string AmusementProjectName { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 类型：【1:推荐到首页】 RecommendAmusementProjectClassifyEunm
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
    /// 游乐项目推荐保存DTO
    /// </summary>
    public class RecommendAmusementProjectSaveDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid? Id { set; get; }

        /// <summary>
        /// 项目Id
        /// </summary>
        public Guid AmusementProjectId { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string AmusementProjectName { get; set; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 类型：【1:推荐到首页】 RecommendAmusementProjectClassifyEunm
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
    /// 推荐项目排序DTO
    /// </summary>
    public class RecommendAmusementProjectDeleteDto
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
    /// 推荐项目查询
    /// </summary>
    public class RecommendAmusementProjectSearchDto
    {
        public RecommendPositionEnum Type { set; get; }
    }
}
