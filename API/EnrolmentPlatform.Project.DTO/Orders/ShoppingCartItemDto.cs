using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Orders
{
    /// <summary>
    /// 购物车项目Dto
    /// </summary>
    public class ShoppingCartItemDto
    {
        /// <summary>
        /// 项目Id
        /// </summary>
        public Guid ItemId { get; set; }

        /// <summary>
        /// 供应商ID
        /// </summary>
        public Guid SupplierId { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// 产品规格
        /// </summary>
        public string SpecsStr { get; set; }

        /// <summary>
        /// 产品图片
        /// </summary>
        public string ProductImg { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// 小计
        /// </summary>
        public decimal SubAmount { get; set; }

        /// <summary>
        /// 销售单位
        /// </summary>
        public string SaleslUnit { get; set; }

        /// <summary>
        /// 起售数量
        /// </summary>
        public int MinBuyQuantity { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        public Guid CreatorUserId { get; set; }

        /// <summary>
        /// 创建人账号
        /// </summary>
        public string CreatorAccount { get; set; }
    }

    /// <summary>
    /// 更新购物车项目Dto
    /// </summary>
    public class UpdateShoppingCartItemDto
    {
        /// <summary>
        /// 项目Id
        /// </summary>
        public Guid ItemId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Quantity { get; set; }
    }
}
