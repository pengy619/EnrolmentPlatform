using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.DTO.Systems
{
    /// <summary>
    /// 角色DTO
    /// </summary>
    public class RoleDto
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid? RoleId { set; get; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { set; get; }

        /// <summary>
        /// 系统类型
        /// </summary>
        public SystemTypeEnum SystemType { set; get; }

        /// <summary>
        /// 所属企业ID
        /// </summary>
        public Guid EnterpriseId { set; get; }

        /// <summary>
        /// 状态Int值
        /// </summary>
        public int IntStatus { set; get; }

        /// <summary>
        /// 状态
        /// </summary>
        public StatusBaseEnum Status
        {
            get
            {
                return (StatusBaseEnum)this.IntStatus;
            }
        }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName
        {
            get
            {
                return EnumDescriptionHelper.GetDescription(this.Status);
            }
        }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid CurUserId { set; get; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string CurUserAccount { set; get; }

        /// <summary>
        /// 权限集合
        /// </summary>
        public List<Guid> PermissionList { set; get; }
    }

    /// <summary>
    /// 模块DTO
    /// </summary>
    public class ModuleDto
    {
        /// <summary>
        /// 模块ID
        /// </summary>
        public Guid ModuleId { set; get; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { set; get; }

        /// <summary>
        /// 菜单列表
        /// </summary>
        public List<MenuDto> MenuList { set; get; }
    }

    /// <summary>
    /// 权限DTO
    /// </summary>
    public class MenuDto
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public Guid MenuId { set; get; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string MenuName { set; get; }
    }

    /// <summary>
    /// 角色DTO
    /// </summary>
    public class RoleSearchDto : GridDataRequest
    {
        /// <summary>
        /// 系统类型
        /// </summary>
        public SystemTypeEnum SystemType { set; get; }

        /// <summary>
        /// 角色状态
        /// </summary>
        public StatusBaseEnum? Status { set; get; }

        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { set; get; }

        /// <summary>
        /// 所属企业ID
        /// </summary>
        public Guid EnterpriseId { set; get; }
    }

    /// <summary>
    /// 角色权限DTO
    /// </summary>
    [Serializable]
    public class RolePermissionDto
    {
        /// <summary>
        /// 菜单ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 菜单等级
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// 操作
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 图标
        /// </summary>

        public string Icon { get; set; }
    }
}
