using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Systems;

namespace EnrolmentPlatform.Project.DTO.Systems
{
    /// <summary>
    /// 权限DTO
    /// </summary>
    public class PermissionDto
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid? Id { set; get; }

        /// <summary>
        /// 父级ID
        /// </summary>
        public Guid ParentId { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 系统类型
        /// </summary>
        public SystemTypeEnum SystemType { set; get; }

        /// <summary>
        /// 区域名称
        /// </summary>
        public string AreaName { set; get; }

        /// <summary>
        /// 控制器名称
        /// </summary>
        public string ControllerName { set; get; }

        /// <summary>
        /// Action名称
        /// </summary>
        public string ActionName { set; get; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { set; get; }

        /// <summary>
        /// 等级
        /// </summary>
        public int Level { set; get; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { set; get; }
    }
}
