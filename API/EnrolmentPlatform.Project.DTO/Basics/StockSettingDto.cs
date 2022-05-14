using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Basics
{
    /// <summary>
    /// 指标设置DTO
    /// </summary>
    public class StockSettingDto
    {
        /// <summary>
        /// 指标ID
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
        /// 批次ID
        /// </summary>
        public Guid BatchId { set; get; }

        /// <summary>
        /// 批次名称
        /// </summary>
        public string BatchName { set; get; }

        /// <summary>
        /// 策略名称
        /// </summary>		
        public string Name { get; set; }

        /// <summary>
        /// 指标
        /// </summary>		
        public int Inventory { get; set; }

        /// <summary>
        /// 已用指标
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
    /// 指标设置删除DTO
    /// </summary>
    public class StockSettingIdDto
    {
        /// <summary>
        /// 指标ID
        /// </summary>		
        public Guid StockSettingId { get; set; }
    }

    /// <summary>
    /// 指标设置查询Dto
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

    /// <summary>
    /// 指标查询Dto
    /// </summary>
    public class StockListSearchDto : GridDataRequest
    {
        /// <summary>
        /// 学校名称
        /// </summary>
        public string SchoolName { set; get; }

        /// <summary>
        /// 层次名称
        /// </summary>
        public string LevelName { set; get; }

        /// <summary>
        /// 专业名称
        /// </summary>
        public string MajorName { set; get; }

        /// <summary>
        /// 批次名称
        /// </summary>
        public string BatchName { set; get; }
    }

    /// <summary>
    /// 指标设置DTO
    /// </summary>
    public class StockListDto
    {
        /// <summary>
        /// 指标ID
        /// </summary>		
        public Guid? StockSettingId { get; set; }

        /// <summary>
        /// 学校Id
        /// </summary>		
        public Guid SchoolId { get; set; }

        /// <summary>
        /// 学校名称
        /// </summary>
        public string SchoolName { set; get; }

        /// <summary>
        /// 层次ID
        /// </summary>
        public Guid LevelId { set; get; }

        /// <summary>
        /// 层次名称
        /// </summary>
        public string LevelName { set; get; }

        /// <summary>
        /// 专业ID
        /// </summary>
        public Guid MajorId { set; get; }

        /// <summary>
        /// 专业名称
        /// </summary>
        public string MajorName { set; get; }

        /// <summary>
        /// 批次名称
        /// </summary>
        public Guid BatchId { set; get; }

        /// <summary>
        /// 批次名称
        /// </summary>
        public string BatchName { set; get; }

        /// <summary>
        /// 策略名称
        /// </summary>		
        public string Name { get; set; }

        /// <summary>
        /// 指标
        /// </summary>		
        public int Inventory { get; set; }

        /// <summary>
        /// 已用指标
        /// </summary>
        public int UsedInventory { get; set; }

        /// <summary>
        /// 剩余指标
        /// </summary>
        public int ResidueInventory
        {
            get
            {
                return this.Inventory - this.UsedInventory;
            }
        }

        /// <summary>
        /// 剩余百分比
        /// </summary>
        public string ResiduePercent
        {
            get
            {
                if (this.Inventory == 0)
                    return "0.00%";
                return ((Convert.ToDecimal(this.ResidueInventory) / Convert.ToDecimal(this.Inventory)) * 100).ToString("0.00") + "%";
            }
        }
    }
}
