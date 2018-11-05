using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.DTO.Enums.Enterprise;
using EnrolmentPlatform.Project.DTO.Enums.Product;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.Infrastructure.EnumHelper;

namespace EnrolmentPlatform.Project.DTO.Product
{  
    /// <summary>
    /// 游玩项目列表信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class ListForTicketForPlayMngDto
    {
        /// <summary>
        /// 编号
        /// </summary> 
        [DataMember]
        public Guid Id { get; set; }
        /// <summary>
        /// 编号
        /// </summary> 
        [DataMember]
        public int ProjectNumber { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [DataMember]
        public string ProjectName { get; set; }
        /// <summary>
        /// 所在地
        /// </summary>
        [DataMember]
        public string Address { get; set; }
        /// <summary>
        /// 景点
        /// </summary>
        [DataMember]
        public string ScenicSpotNameStr { get; set; } 
        /// <summary>
        /// 景点范围 【1：景点内】【2：景点外】
        /// </summary>
        public int ScenicRange { get; set; }
        /// <summary>
        /// 供应商类型
        /// </summary>
        [DataMember]
        public int SupplierType { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [DataMember]
        public string SupplierName { get; set; }
        /// <summary>
        /// 时间
        /// </summary>
        [DataMember]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Status 【1：禁用】【2：启用】【3：草稿】
        /// </summary>
        [DataMember]
        public int Status { get; set; }
        /// <summary>
        ///状态中文
        /// </summary>
        [DataMember]
        public string StatusStr
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((StatusForPlayProjectEnum)Status);
            }
        }
        /// <summary>
        /// 景点范围中文
        /// </summary>
        [DataMember]
        public string ScenicRangeStr
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((StatusForScenicRangeEnum)ScenicRange);
            }
        }
        /// <summary>
        /// 供应商类型中文
        /// </summary>
        [DataMember]
        public string SupplierTypeStr
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((SupplierTypeEnum)SupplierType);
            }
        }
    }


    /// <summary>
    /// 游玩项目信息
    /// </summary>
    [Serializable]
    [DataContract]
    public class OptionParamForPlayMngDataDto
    {
        /// <summary>
        /// 编号
        /// </summary> 
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public Guid SupplierId { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        [DataMember]
        public string ProjectName { get; set; }
        /// <summary>
        /// 编号
        /// </summary> 
        [DataMember]
        public int ProjectNumber { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        [DataMember]
        public string Address { get; set; }
        /// <summary>
        /// 经纬度
        /// </summary>
        [DataMember]
        public string Coordinate { get; set; }
        /// <summary>
        /// 景点Id以逗号分隔
        /// </summary>
        [DataMember]
        public string ScenicSpotIdStr { get; set; }
        /// <summary>
        /// 景点Id以逗号分隔
        /// </summary>
        [DataMember]
        public string ScenicSpotNameStr { get; set; }
        /// <summary>
        /// 景点范围 【1：景点内】【2：景点外】
        /// </summary>
        [DataMember]
        public int ScenicRange { get; set; }
        /// <summary>
        /// 预订须知
        /// </summary>
        [DataMember]
        public string OrderNotes { get; set; }
        /// <summary>
        /// 详细介绍
        /// </summary>
        [DataMember]
        public string Description { get; set; }
        /// <summary>
        /// 交通指南
        /// </summary>
        [DataMember]
        public string TrafficGuide { get; set; }
        /// <summary>
        /// 状态：【1：待审核】【2：已审核】【3：审核拒绝】
        /// </summary>
        [DataMember]
        public int Status { get; set; }
        /// <summary>
        /// 审核结果
        /// </summary>
        [DataMember]
        public string AuditResult { get; set; }
        /// <summary>
        /// 审核时间
        /// </summary>
        [DataMember]
        public DateTime AuditTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [DataMember]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [DataMember]
        public string CreateName { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        [DataMember]
        public Guid CreatorUserId { get; set; }
        /// <summary>
        /// SupplierName
        /// </summary>
        [DataMember]
        public string SupplierName { get; set; }

        /// <summary>
        /// 开放时间
        /// </summary>
        [DataMember]
        public string OpenTime { get; set; }
        /// <summary>
        /// 关闭时间
        /// </summary>
        [DataMember]
        public string CloseTime { get; set; }
        [DataMember]
        public List<OptionParamForKeyValueDto> OptionParamForKeyValueDto { get; set; } = new List<OptionParamForKeyValueDto>();
        [DataMember]
        public List<OptionParamForPictureDto> OptionParamForPictureDto { get; set; } = new List<OptionParamForPictureDto>(); 

        [DataMember]
        public string StatusStr
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((StatusForPlayProjectEnum)Status);
            }
        }
        [DataMember]
        public string ScenicRangeStr
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((StatusForScenicRangeEnum)Status);
            }
        }
        [DataMember]
        public string SupplierTypeStr
        {
            get
            {
                return EnumDescriptionHelper.GetDescription((SupplierTypeEnum)Status);
            }
        }
    }
}
