using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Infrastructure;

namespace EnrolmentPlatform.Project.DTO.Systems
{
    public class LogSettingDTO : GridDataRequest
    {
        /// <summary>
        /// Key
        /// </summary>
        public Guid Key { get; set; }
        /// <summary>
        /// 是否是模块
        /// </summary>
        public bool IsModule { get; set; }

        public Guid EnterpriseId { get; set; }

        public string IP { get; set; }

        public string BusinessName { get; set; }

        public DateTime CreatorTime { get; set; }
        public string CreatorAccount { get; set; }

        public string KeyWrod { get; set; }
        public DateTime StartDate
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(DateStr))
                {
                    return (DateStr.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries)[0]).ToDate();
                }
                else
                {
                    return DateTime.MinValue;
                }
            }
        }
        public DateTime EndDate
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(DateStr))
                {
                    return Convert.ToDateTime(DateStr.Split(new string[] { " - " }, StringSplitOptions.RemoveEmptyEntries)[1] + " 23:59:59");
                }
                else
                {
                    return DateTime.MaxValue;
                }
            }
        }
        public string DateStr { get; set; }
    }


}
