using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.IBLL.Systems;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.WebApi.WebLibrary;

namespace EnrolmentPlatform.Project.WebApi.Areas.Systems
{
    /// <summary>
    /// 角色API
    /// </summary>
    public class RoleController : ApiBaseController
    {
        protected IT_RoleService RoleService;
        public RoleController()
        {
            this.RoleService = DIContainer.Resolve<IT_RoleService>();
        }

        /// <summary>
        /// 保存角色信息
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> SaveRole(RoleDto dto)
        {
            return await Task.Run(() =>
            {
                dto.PermissionList = dto.PermissionList.Distinct().ToList();
                ResultMsg _resultMsg = new ResultMsg();
                int ret = 0;
                if (dto.RoleId.HasValue)
                {
                    ret = this.RoleService.UpdateRole(dto);
                }
                else
                {
                    ret = this.RoleService.AddRole(dto);
                }

                if (ret == 1)
                {
                    _resultMsg.IsSuccess = true;
                }
                else if (ret == 2)
                {
                    _resultMsg.IsSuccess = false;
                    _resultMsg.Info = "存在重复角色名称。";
                }
                else
                {
                    _resultMsg.IsSuccess = false;
                    _resultMsg.Info = "保存失败。";
                }
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 获得角色信息
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetRole(Guid roleId)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                RoleDto ret = this.RoleService.GetRole(roleId);
                _resultMsg.IsSuccess = true;
                _resultMsg.Data = ret;
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> DeleteRole(Guid[] roleIds)
        {
            return await Task.Run(() =>
            {
                string msg = null;
                ResultMsg _resultMsg = new ResultMsg();
                bool ret = this.RoleService.DeleteRole(roleIds, out msg);
                _resultMsg.IsSuccess = ret;
                _resultMsg.Info = msg;
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 获得角色列表
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> GetRoleList(RoleSearchDto param)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                int reCount = 0;
                var lst = RoleService.GetRoleList(param, out reCount);
                GridDataResponse grid = new GridDataResponse
                {
                    Count = reCount,
                    Data = lst
                };
                _resultMsg.Data = grid;
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 获得当前系统可使用角色列表
        /// </summary>
        /// <param name="type">系统类型</param>
        /// <param name="enterpriseId">企业ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetActiveRoleList(SystemTypeEnum type, Guid enterpriseId)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                var lst = RoleService.GetRoleList(new RoleSearchDto() { SystemType = type, EnterpriseId = enterpriseId });
                _resultMsg.IsSuccess = true;
                _resultMsg.Data = lst.ToJson();
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 获得系统权限
        /// </summary>
        /// <param name="type">系统类型</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetRolePermissions(SystemTypeEnum type)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                var lst = RoleService.GetRolePermissions(type);
                _resultMsg.Data = lst;
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 获得角色权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetRolePermissionList(Guid roleId, int systemTypeEnum)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                var lst = RoleService.GetRolePermissionList(roleId, (SystemTypeEnum)systemTypeEnum);
                _resultMsg.Data = lst;
                _resultMsg.IsSuccess = true;
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 根据路经得到当前权限
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetPermissionsByLocation(string area, string controller, Guid roleId, int systemTypeEnum, string action = "index")
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                var lst = RoleService.GetPermissionsByLocation(area, controller, roleId, (SystemTypeEnum)systemTypeEnum, action);
                _resultMsg.Data = lst;
                _resultMsg.IsSuccess = true;
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 获得角色权限
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetAllPermissionList(int systemTypeEnum)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                var lst = RoleService.GetAllPermissionList((SystemTypeEnum)systemTypeEnum);
                _resultMsg.Data = lst;
                _resultMsg.IsSuccess = true;
                return _resultMsg.ResponseMessage();
            });
        }
        /// <summary>
        /// 清除角色权限
        /// </summary>
        /// <param name="roleId">角色Id</param>]
        [HttpGet]
        public async Task<HttpResponseMessage> ClearRolePremission(Guid roleId)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.IsSuccess = RoleService.ClearRolePremission(roleId);
                return _resultMsg.ResponseMessage();
            });
        }
        /// <summary>
        /// 清除角色权限
        /// </summary>
        /// <param name="roleId">角色Id</param>]
        [HttpGet]
        public async Task<HttpResponseMessage> ClearRoleAndSystemPremission(Guid roleId, int systemCLassify)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.IsSuccess = RoleService.ClearRoleAndSystemPremission(roleId, (SystemTypeEnum)systemCLassify);
                return _resultMsg.ResponseMessage();
            });
        }
        

        /// <summary>
        /// 根据ID获取权限
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage>  GetPermissionById(Guid id)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.Data = this.RoleService.GetPermissionById(id);
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 修改 新增权限
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> UpdatePermission(T_Permissions entity)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = this.RoleService.UpdatePermission(entity);
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 删除权限及其下面所有子节点
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> DeletePermissionById(Guid id)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.IsSuccess=this.RoleService.DeletePermissionById(id);
                return _resultMsg.ResponseMessage();
            });
        }
         
    }
}
