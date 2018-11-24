﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.Domain.Entities.Orders
{
    [Serializable]
    [Table("T_OrderImage")]
    [DataContract]
    public class T_OrderImage : Entity
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [DataMember]
        public Guid OrderId { set; get; }

        /// <summary>
        /// 身份证正面
        /// </summary>
        [DataMember]
        public string IDCard1 { set; get; }

        /// <summary>
        /// 身份证反面
        /// </summary>
        [DataMember]
        public string IDCard2 { set; get; }

        /// <summary>
        /// 两寸蓝底
        /// </summary>
        [DataMember]
        public string LiangCunLanDiImg { set; get; }

        /// <summary>
        /// 毕业证
        /// </summary>
        [DataMember]
        public string BiYeZhengImg { set; get; }

        /// <summary>
        /// 免考英语
        /// </summary>
        [DataMember]
        public string MianKaoYingYuImg { set; get; }

        /// <summary>
        /// 免考计算机
        /// </summary>
        [DataMember]
        public string MianKaoJiSuanJiImg { set; get; }

        /// <summary>
        /// 学信网截图
        /// </summary>
        [DataMember]
        public string XueXinWangImg { set; get; }

        /// <summary>
        /// 其他
        /// </summary>
        [DataMember]
        public string QiTa { set; get; }

        /// <summary>
        /// 头像
        /// </summary>
        [DataMember]
        public string TouXiang { set; get; }
    }
}
