using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Basics
{
    /// <summary>
    /// 自定义字段DTO
    /// </summary>
    public class CustomerFieldDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid? Id { set; get; }

        /// <summary>
        /// 学校ID
        /// </summary>
        public Guid SchoolId { set; get; }

        /// <summary>
        /// 自定义字段名称
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 自定义字段类型CustomerFieldTypeEnum
        /// </summary>
        public int CustomerFieldType { set; get; }

        /// <summary>
        /// 字段选项选项用|分割
        /// </summary>
        public string SelectItems { set; get; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { set; get; }
        
        /// <summary>
        /// 用户姓名
        /// </summary>
        public string UserName { set; get; }
    }

    /// <summary>
    /// 查询Dto
    /// </summary>
    public class GetAllListSearchDto
    {
        /// <summary>
        /// 学校Id
        /// </summary>
        public Guid SchoolId { set; get; }
    }
}
