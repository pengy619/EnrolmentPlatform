using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.IBLL.Systems
{
    /// <summary>
    /// 角色服务接口
    /// </summary>
    public interface IT_RoleService : IBaseService<T_Role>
    {
        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="dto">角色DTO</param>
        /// <returns>1：成功，2：已经存在，3：失败</returns>
        int AddRole(RoleDto dto);

        /// <summary>
        /// 获得角色信息
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        RoleDto GetRole(Guid roleId);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleIds">角色ID集合</param>
        /// <param name="msg">失败的时候消息</param>
        /// <returns></returns>
        bool DeleteRole(Guid[] roleIds, out string msg);

        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="dto">角色DTO</param>
        /// <returns>1：成功，2：已经存在，3：失败</returns>
        int UpdateRole(RoleDto dto);

        /// <summary>
        /// 获得角色列表
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <param name="reCount">总记录</param>
        /// <returns></returns>
        List<RoleDto> GetRoleList(RoleSearchDto param, out int reCount);

        /// <summary>
        /// 获得当前系统所有正常的角色
        /// </summary>
        /// <param name="param">查询条件</param>
        /// <param name="reCount">总记录</param>
        /// <returns></returns>
        List<RoleDto> GetRoleList(RoleSearchDto param);

        /// <summary>
        /// 获得系统权限
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        List<ModuleDto> GetRolePermissions(SystemTypeEnum type);

        /// <summary>
        /// 获得角色权限
        /// </summary>
        /// <param name="roleId">权限ID</param>
        /// <returns></returns>
        List<RolePermissionDto> GetRolePermissionList(Guid roleId, SystemTypeEnum systemTypeEnum);

        /// <summary>
        /// 根据路经得到当前权限
        /// </summary>
        /// <param name="area"></param>
        /// <param name="controller"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        List<RolePermissionDto> GetPermissionsByLocation(string area, string controller, Guid roleId, SystemTypeEnum systemTypeEnum, string action = "index");

        /// <summary>
        /// 获得系统所有完整权限
        /// </summary>
        /// <param name="systemTypeEnum">系统类型</param>
        /// <returns></returns>
        List<RolePermissionDto> GetAllPermissionList(SystemTypeEnum systemTypeEnum);
        /// <summary>
        /// 清除角色权限
        /// </summary>
        /// <param name="roleId">角色Id</param>
        bool ClearRolePremission(Guid roleId);
        /// <summary>
        /// 清除角色权限
        /// </summary>
        /// <param name="roleId">角色Id</param>
        bool ClearRoleAndSystemPremission(Guid roleId, SystemTypeEnum systemTypeEnum);


        /// <summary>
        /// 根据ID获取权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T_Permissions GetPermissionById(Guid id);

        /// <summary>
        /// 修改 增加权限
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ResultMsg UpdatePermission(T_Permissions entity);

        /// <summary>
        /// 删除权限及其下面子节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeletePermissionById(Guid id);
    }
}
