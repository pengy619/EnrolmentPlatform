using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Systems
{

    public enum E_AddressClassify
    {

        [Description("大洲")]
        Continent = 1,
        [Description("国家")]
        Country = 2,
        //[EnumDisplayName("区域")]
        //Region = 3,
        [Description("省或直辖市")]
        Province = 3,
        [Description("地级市")]
        City = 4,
        [Description("区县")]
        District = 5,
        [Description("乡镇")]
        Town = 6,
        [Description("街道")]
        Street = 7,
    }

}
