using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums
{
    public enum StatusBaseEnum
    {

        [Description("已禁用")]
        Disabled = 1,

        [Description("已启用")]
        Enabled = 2
    }

    public enum MessageStatusEnum
    { 
        [Description("未读")]
        UnRead = 1, 
        [Description("已读")]
        Read = 2
    }
}
