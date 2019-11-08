﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace EnrolmentPlatform.Project.Domain.Entities
{
    [Serializable]
    [Table("T_Metadata")]
    [DataContract]
    public class T_Metadata : Entity
    {
        /// <summary>
        /// 名称
        /// </summary>		
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// 类型
        /// </summary>		
        [DataMember]
        public int Type { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary> 
        [DataMember]
        public bool IsEnable { get; set; }

        /// <summary>
        /// 标记
        /// </summary>
        [DataMember]
        public string Tags { get; set; }
    }
}