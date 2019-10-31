using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.Domain.Entities.Basics
{
    /// <summary>
    /// 自定义字段
    /// </summary>
    [Serializable]
    [Table("T_CustomerField")]
    [DataContract]
    public class T_CustomerField : Entity
    {
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
    }
}
