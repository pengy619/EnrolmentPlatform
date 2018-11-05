using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.DTO.Systems
{
    /// <summary>
    /// 广告Dto
    /// </summary>
    public class BannerDto
    {
        /// <summary>
        /// 广告Id
        /// </summary>
        public Guid BannerId { get; set; }

        /// <summary>
        /// 图片路径
        /// </summary>
        public string ImgPath { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        public int Position { get; set; }

        public string PositionStr
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((BannerPositionEnum)Position);
            }
        }

        /// <summary>
        /// banner类型   
        /// </summary>
        public int Classify { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 创建者Id
        /// </summary>
        public Guid CreatorUserId { get; set; }

        /// <summary>
        /// 创建者账号
        /// </summary>
        public string CreatorAccount { get; set; }

        /// <summary>
        /// 发布对象
        /// </summary>
        public int PublicObject { get; set; }
    }

    public class BannerSearchDto : GridDataRequest
    {
        /// <summary>
        /// banner类型   
        /// </summary>
        public int Classify { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        public int? Position { get; set; }

        /// <summary>
        /// 发布对象
        /// </summary>
        public int PublicObject { get; set; }
    }

    /// <summary>
    /// 更新广告排序Dto
    /// </summary>
    public class UpdateBannerSortDto
    {
        /// <summary>
        /// 广告Id
        /// </summary>
        public Guid BannerId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }

    /// <summary>
    /// 初始化广告设置Dto
    /// </summary>
    public class InitBannerSettingsDto
    {
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
