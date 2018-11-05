using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Systems
{
   public class PermissionsDTO
    {
        [DataMember]
        // 当前领域实体的全局唯一标识
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Level { get; set; }
        [DataMember]
        public string Area { get; set; }
        [DataMember]
        public string Controller { get; set; }
        [DataMember]
        public string Action { get; set; }
        [DataMember]
        public string Param { get; set; }
        [DataMember]
        public int Classify { get; set; }
        [DataMember]
        public Guid ParentId { get; set; }
        [DataMember]
        public int Sort { get; set; }
        [DataMember]
        public string Icon { get; set; }
    }
}
