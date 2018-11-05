using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Enums.Systems
{

    public enum CredentialsTypeEnum
    {
        /// <summary>
        /// 身份证
        /// </summary>
        [Description("身份证")]
        ID_Card = 1,
        /// <summary>
        /// 护照
        /// </summary>
        [Description("护照")]
        HUZhao = 2,
        /// <summary>
        /// 台胞证
        /// </summary>
        [Description("台胞证")]
        TaiBao = 3,
        /// <summary>
        /// 港澳通行证
        /// </summary>
        [Description("港澳通行证")]
        Description = 4,
        /// <summary>
        /// 其它
        /// </summary>
        [Description("其它")]
        Other = 5
    }

    public enum ContactsClassifyEnum
    {
        /// <summary>
        /// 成人
        /// </summary>
        [Description("成人")]
        Adult = 1,
        /// <summary>
        /// 儿童
        /// </summary>
        [Description("儿童")]
        Child = 2,
        /// <summary>
        /// 老人
        /// </summary>
        [Description("老人")]
        Old = 3
    }


}
