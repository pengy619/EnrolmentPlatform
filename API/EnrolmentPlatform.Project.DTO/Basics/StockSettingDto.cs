using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Basics
{
    /// <summary>
    /// 库存设置DTO
    /// </summary>
    public class StockSettingDto
    {
        /// <summary>
        /// 库存ID
        /// </summary>		
        public Guid? StockSettingId { get; set; }

        /// <summary>
        /// 学校Id
        /// </summary>		
        public Guid SchoolId { get; set; }

        /// <summary>
        /// 层次ID
        /// </summary>
        public Guid LevelId { set; get; }

        /// <summary>
        /// 专业ID
        /// </summary>
        public Guid MajorId { set; get; }

        /// <summary>
        /// 策略名称
        /// </summary>		
        public string Name { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>		
        public DateTime StartDate { get; set; }

        /// <summary>
        /// 截止时间
        /// </summary>		
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 开始时间Str
        /// </summary>		
        public string StartDateStr
        {
            get
            {
                return this.StartDate.ToString("yyyy-MM-dd");
            }
        }

        /// <summary>
        /// 截止时间Str
        /// </summary>		
        public string EndDateStr
        {
            get
            {
                return this.EndDate.ToString("yyyy-MM-dd");
            }
        }

        /// <summary>
        /// 库存
        /// </summary>		
        public int Inventory { get; set; }

        /// <summary>
        /// 已用库存
        /// </summary>		
        public int UsedInventory { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { set; get; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { set; get; }
    }

    /// <summary>
    /// 库存设置删除DTO
    /// </summary>
    public class StockSettingIdDto
    {
        /// <summary>
        /// 库存ID
        /// </summary>		
        public Guid StockSettingId { get; set; }
    }

    /// <summary>
    /// 库存设置查询Dto
    /// </summary>
    public class StockSettingSearchDto : GridDataRequest
    {
        /// <summary>
        /// 学校Id
        /// </summary>		
        public Guid SchoolId { get; set; }

        /// <summary>
        /// 层次ID
        /// </summary>
        public Guid LevelId { set; get; }

        /// <summary>
        /// 专业ID
        /// </summary>
        public Guid MajorId { set; get; }
    }
}
