using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Orders
{
    /// <summary>
    /// 订单图像
    /// </summary>
    public class OrderImageDto
    {
        /// <summary>
        /// 图像ID
        /// </summary>
        public Guid Id { set; get; }

        /// <summary>
        /// 订单ID
        /// </summary>
        public Guid OrderId { set; get; }

        /// <summary>
        /// 身份证正面
        /// </summary>
        public string IDCard1 { set; get; }

        /// <summary>
        /// 身份证反面
        /// </summary>
        public string IDCard2 { set; get; }

        /// <summary>
        /// 两寸蓝底
        /// </summary>
        public string LiangCunLanDiImg { set; get; }

        /// <summary>
        /// 毕业证
        /// </summary>
        public string BiYeZhengImg { set; get; }

        /// <summary>
        /// 社保/居住证正
        /// </summary>
        public string MianKaoYingYuImg { set; get; }

        /// <summary>
        /// 社保/居住证反
        /// </summary>
        public string MianKaoJiSuanJiImg { set; get; }

        /// <summary>
        /// 教育部学历证书电子备案表
        /// </summary>
        public string XueXinWangImg { set; get; }

        /// <summary>
        /// 其他
        /// </summary>
        public string QiTa { set; get; }

        /// <summary>
        /// 录取通知书
        /// </summary>
        public string TouXiang { set; get; }
    }

    public class PicIndex
    {
        public Guid OrderId { set; get; }
        public string ImageType { set; get; }
        public int PictureIndex { set; get; }
    }
}
