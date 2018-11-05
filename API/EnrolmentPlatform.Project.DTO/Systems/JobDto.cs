using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Systems
{
    /// <summary>
    /// 职位DTO
    /// </summary>
    public class JobDto
    {
        /// <summary>
        /// 岗位ID
        /// </summary>
        public Guid? JobId { set; get; }

        /// <summary>
        /// 职位名称
        /// </summary>
        public string JobName { set; get; }

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
    /// 职位查询DTO
    /// </summary>
    public class JobSearchDto : GridDataRequest
    {
    }

    /// <summary>
    /// 删除DTO
    /// </summary>
    public class JobDeleteDto
    {
        /// <summary>
        /// 需要删除的岗位ID
        /// </summary>
        public Guid[] JobIds { set; get; }

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
