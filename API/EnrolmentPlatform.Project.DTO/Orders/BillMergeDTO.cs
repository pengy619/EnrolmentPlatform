using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Orders
{
    /// <summary>
    /// 单据合并表
    /// </summary>
    public class BillMergeDTO
    {
        /// <summary>
        /// 合并名称
        /// </summary>
        public string BillMergeName { get; set; }
        /// <summary>
        /// 合并编号
        /// </summary>
        public string BillMergeNo { get; set; }
        /// <summary>
        /// 单据编号
        /// </summary>
        public string BillNoStr { get; set; }
        /// <summary>
        /// 支付流水号
        /// </summary>
        public string PayTransactionNo { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public int PayClassify { get; set; }
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 支付状态
        /// </summary>
        public int PayStatus { get; set; }
    }
    /// <summary>
    /// 单据表
    /// </summary>
    public class BillDto
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }
        /// <summary>
        /// 单据编号
        /// </summary>
        public string BillNo { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// 订单实际金额
        /// </summary>
        public decimal OrderAmount { get; set; }
        /// <summary>
        /// 交易类型
        /// </summary>
        public int TransactionClassify { get; set; }
        /// <summary>
        /// 交易流水号
        /// </summary>
        public string TranscationNo { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public int PayClassify { get; set; }
        /// <summary>
        /// 支付状态
        /// </summary>
        public int PayStatus { get; set; }
        /// <summary>
        /// 单据类型
        /// </summary>
        public int BillClassify { get; set; }
    }
}
