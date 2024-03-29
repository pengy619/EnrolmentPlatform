﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.Domain.Entities
{
    [Serializable]
    [Table("T_StockSetting")]
    [DataContract]
    public class T_StockSetting : Entity
    {
        /// <summary>
        /// 学校Id
        /// </summary>		
        [DataMember]
        public Guid SchoolId { get; set; }

        /// <summary>
        /// 层次ID
        /// </summary>
        [DataMember]
        public Guid LevelId { set; get; }

        /// <summary>
        /// 专业ID
        /// </summary>
        [DataMember]
        public Guid MajorId { set; get; }

        /// <summary>
        /// 批次ID
        /// </summary>
        [DataMember]
        public Guid BatchId { set; get; }

        /// <summary>
        /// 策略名称
        /// </summary>		
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 指标
        /// </summary>		
        [DataMember]
        public int Inventory { get; set; }

        /// <summary>
        /// 已用指标
        /// </summary>		
        [DataMember]
        public int UsedInventory { get; set; }
    }
}
