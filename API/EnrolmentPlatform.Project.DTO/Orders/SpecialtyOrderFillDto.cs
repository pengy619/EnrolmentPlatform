using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Orders
{
    public class SpecialtyOrderFillDto
    {
        /// <summary>
        /// 供应商Id
        /// </summary>
        public Guid SupplierId { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// 产品Id
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// 产品图片
        /// </summary>
        public string ProductImg { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 销售模式【1：现售】【2：预售】
        /// </summary> 
        public int SalesModel { get; set; }

        /// <summary>
        /// 供货时间
        /// </summary>
        public DateTime? SupplierTime { get; set; }

        /// <summary>
        /// 定金比例
        /// </summary>
        public decimal DepositRatio { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 购买数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 最小购买数量
        /// </summary>
        public int MinBuyQuantity { get; set; }

        /// <summary>
        /// 销售单位
        /// </summary>
        public string SaleslUnit { get; set; }
    }

    public class CheckInventoryDto
    {
        /// <summary>
        /// 产品Id
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }
    }
}
