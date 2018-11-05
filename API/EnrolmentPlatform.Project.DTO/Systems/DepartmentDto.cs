using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Systems
{
    /// <summary>
    /// 部门DTO
    /// </summary>
    public class DepartmentDto
    {
        /// <summary>
        /// 部门ID
        /// </summary>
        public Guid? DepartmentId { set; get; }

        /// <summary>
        /// 部门名称
        /// </summary>
        public string DepartmentName { set; get; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        public Guid CreateUserId { set; get; }

        /// <summary>
        /// 创建人账号
        /// </summary>
        public string CreatorAccount { get; set; }
    }

    /// <summary>
    /// 部门查询DTO
    /// </summary>
    public class DepartmentSearchDto : GridDataRequest
    {
    }

    /// <summary>
    /// 删除DTO
    /// </summary>
    public class DepartmentDeleteDto
    {
        /// <summary>
        /// 需要删除的部门ID
        /// </summary>
        public Guid[] DepartmentIds { set; get; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        public Guid CreateUserId { set; get; }

        /// <summary>
        /// 创建人账号
        /// </summary>
        public string CreatorAccount { get; set; }
    }
}
