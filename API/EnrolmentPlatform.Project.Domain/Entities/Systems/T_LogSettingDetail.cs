using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EnrolmentPlatform.Project.Domain.Entities
{
    [Table("T_LogSettingDetail")]
    [Serializable]
    public class T_LogSettingDetail : Entity
    {
        public Guid LogId { get; set; }
        public string ColumnName { get; set; }
        public string OldColumnValue { get; set; }
        public string NewColumnValue { get; set; }
    }
}
