using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Orders
{
    /// <summary>
    /// 退换货详情
    /// </summary>
    [Serializable]
    [DataContract]
    public class RefundableRecordDetail
    {
        /// <summary>
        /// 退换货实体
        /// </summary>
        [DataMember]
        public Refundable_RecordDetail RefundableRecord { get; set; }
        /// <summary>
        /// 农产品订单项集合
        /// </summary>
        [DataMember]
        public List<OrderItem_RecordDetail> OrderItemList { get; set; }
        /// <summary>
        /// 日志集合
        /// </summary>
        [DataMember]
        public List<LogSetting_RecordDetail> LogList { get; set; }
        /// <summary>
        /// 文件集合
        /// </summary>
        [DataMember]
        public List<File_RecordDetail> FileList { get; set; }
    }
    public class Refundable_RecordDetail
    {
        /// <summary>
        /// 订单编号
        /// </summary> 
        [DataMember]
        public string OrderNo { get; set; }
        /// <summary>
        /// 退换货编号
        /// </summary> 
        [DataMember]
        public string RefundableNo { get; set; }
        /// <summary>
        /// 理由
        /// </summary> 
        [DataMember]
        public string Reason { get; set; }
        /// <summary>
        /// 详细理由
        /// </summary> 
        [DataMember]
        public string ReasonDetail { get; set; }
        [DataMember]
        public string ProductName { get; set; }
        /// <summary>
        /// 商品数量
        /// </summary> 
        [DataMember]
        public int ProductQuantity { get; set; }
        /// <summary>
        /// 操作类型
        /// </summary> 
        [DataMember]
        public int OptionClassify { get; set; }
        /// <summary>
        /// 状态
        /// </summary> 
        [DataMember]
        public int Status { get; set; }
        [DataMember]
        // 当前领域实体的全局唯一标识
        public Guid Id { get; set; }
        [DataMember]
        public bool IsDelete { get; set; }
        [DataMember]
        public DateTime CreatorTime { get; set; }
        [DataMember]
        public Guid CreatorUserId { get; set; }
        [DataMember]
        public string CreatorAccount { get; set; }
        [DataMember]
        public DateTime LastModifyTime { get; set; }
        [DataMember]
        public Guid LastModifyUserId { get; set; }
        [DataMember]
        public DateTime DeleteTime { get; set; }
        [DataMember]
        public Guid DeleteUserId { get; set; }
        [DataMember]
        public long Unix { get; set; }

        /// <summary>
        /// 退换货订单详细ID
        /// </summary>
        [DataMember]
        public Guid OrderItemId { get; set; }

        /// <summary>
        /// 图片
        /// </summary>
        [DataMember]
        public string Photo { get; set; }
    }
    public class OrderItem_RecordDetail
    {
        /// <summary>
        /// 订单ID
        /// </summary> 
        [DataMember]
        public Guid OrderId { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary> 
        [DataMember]
        public string OrderNo { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary> 
        [DataMember]
        public string ProductName { get; set; }
        /// <summary>
        /// 产品ID
        /// </summary> 
        [DataMember]
        public Guid ProductID { get; set; }
        /// <summary>
        /// 单价
        /// </summary> 
        [DataMember]
        public decimal Price { get; set; }
        /// <summary>
        /// 数量
        /// </summary> 
        [DataMember]
        public int Quantity { get; set; }
        /// <summary>
        /// 小计
        /// </summary> 
        [DataMember]
        public decimal SubTotalAmount { get; set; }
        /// <summary>
        /// 定金比例
        /// </summary> 
        [DataMember]
        public decimal DepositRatio { get; set; }
        /// <summary>
        /// 定金
        /// </summary> 
        [DataMember]
        public decimal DepositPrice { get; set; }
        /// <summary>
        /// 尾款
        /// </summary> 
        [DataMember]
        public decimal RetainagePrice { get; set; }
        /// <summary>
        /// 运费
        /// </summary> 
        [DataMember]
        public decimal ExpressFee { get; set; }
        /// <summary>
        /// 品种
        /// </summary> 
        [DataMember]
        public string Varieties { get; set; }
        /// <summary>
        /// 规格
        /// </summary> 
        [DataMember]
        public string SpecsStr { get; set; }
        /// <summary>
        /// 单位
        /// </summary> 
        [DataMember]
        public string SalesUnit { get; set; }
        /// <summary>
        /// 销售模式
        /// </summary> 
        [DataMember]
        public int SalesModel { get; set; }
        /// <summary>
        /// 产品编号
        /// </summary> 
        [DataMember]
        public string ProductNo { get; set; }
        /// <summary>
        /// 产品分类名称
        /// </summary> 
        [DataMember]
        public string CategoryName { get; set; }
        /// <summary>
        /// 代付尾款时间
        /// </summary> 
        [DataMember]
        public DateTime? PayRetainageTime { get; set; }
        [DataMember]
        // 当前领域实体的全局唯一标识
        public Guid Id { get; set; }
        [DataMember]
        public bool IsDelete { get; set; }
        [DataMember]
        public DateTime CreatorTime { get; set; }
        [DataMember]
        public Guid CreatorUserId { get; set; }
        [DataMember]
        public string CreatorAccount { get; set; }
        [DataMember]
        public DateTime LastModifyTime { get; set; }
        [DataMember]
        public Guid LastModifyUserId { get; set; }
        [DataMember]
        public DateTime DeleteTime { get; set; }
        [DataMember]
        public Guid DeleteUserId { get; set; }
        [DataMember]
        public long Unix { get; set; }
    }
    public class LogSetting_RecordDetail
    {
        [DataMember]
        public string TableName { get; set; }
        [DataMember]
        public string BusinessName { get; set; }
        [DataMember]
        public Guid PrimaryKey { get; set; }
        [DataMember]
        public Guid ModuleKey { get; set; }
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public string IP { get; set; }
        [DataMember]
        public string OperationType { get; set; }
        [DataMember]
        public string OldContent { get; set; }
        [DataMember]
        public string NewContent { get; set; }
        [DataMember]
        // 当前领域实体的全局唯一标识
        public Guid Id { get; set; }
        [DataMember]
        public bool IsDelete { get; set; }
        [DataMember]
        public DateTime CreatorTime { get; set; }
        [DataMember]
        public Guid CreatorUserId { get; set; }
        [DataMember]
        public string CreatorAccount { get; set; }
        [DataMember]
        public DateTime LastModifyTime { get; set; }
        [DataMember]
        public Guid LastModifyUserId { get; set; }
        [DataMember]
        public DateTime DeleteTime { get; set; }
        [DataMember]
        public Guid DeleteUserId { get; set; }
        [DataMember]
        public long Unix { get; set; }

    }
    public class File_RecordDetail
    {
        /// <summary>
        /// 外键ID
        /// </summary> 
        [DataMember]
        public Guid ForeignKeyId { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary> 
        [DataMember]
        public string FilePath { get; set; }
        /// <summary>
        /// 文件名称
        /// </summary> 
        [DataMember]
        public string FileName { get; set; }
        /// <summary>
        /// 文件类型
        /// </summary> 
        [DataMember]
        public int FileClassify { get; set; }
        /// <summary>
        /// 外键类型
        /// </summary> 
        [DataMember]
        public int ForeignKeyClassify { get; set; }

        /// <summary>
        /// 文件外键业务类型
        /// </summary>
        public int FileBusinessType { set; get; }

        /// <summary>
        /// 是否封面图
        /// </summary> 
        [DataMember]
        public bool Iscover { get; set; }
        /// <summary>
        /// 是否轮播
        /// </summary> 
        [DataMember]
        public bool IsFocus { get; set; }
        [DataMember]
        // 当前领域实体的全局唯一标识
        public Guid Id { get; set; }
        [DataMember]
        public bool IsDelete { get; set; }
        [DataMember]
        public DateTime CreatorTime { get; set; }
        [DataMember]
        public Guid CreatorUserId { get; set; }
        [DataMember]
        public string CreatorAccount { get; set; }
        [DataMember]
        public DateTime LastModifyTime { get; set; }
        [DataMember]
        public Guid LastModifyUserId { get; set; }
        [DataMember]
        public DateTime DeleteTime { get; set; }
        [DataMember]
        public Guid DeleteUserId { get; set; }
        [DataMember]
        public long Unix { get; set; }

    }
}
