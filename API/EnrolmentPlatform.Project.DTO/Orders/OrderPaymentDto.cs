using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Orders
{
    public class OrderPaymentDto
    {
        /// <summary>
        /// 订单号
        /// </summary>
        public List<string> OrderNos { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public int PayClassify { get; set; }

        /// <summary>
        /// 用户Id
        /// </summary>
        public Guid UserId { get; set; }
    }

    public class PaymentNotifyDto
    {
        /// <summary>
        /// 合拼单据编号
        /// </summary>
        public string BillNo { get; set; }
        /// <summary>
        /// 支付流水
        /// </summary>
        public string PayTransactionNo { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public int PayClassify { get; set; }
    }
}
