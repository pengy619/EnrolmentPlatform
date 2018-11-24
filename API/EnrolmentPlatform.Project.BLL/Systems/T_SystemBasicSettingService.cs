using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Enums.Systems;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.IBLL.Systems;
using EnrolmentPlatform.Project.IDAL;
using EnrolmentPlatform.Project.IDAL.Systems;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.Castle;

namespace EnrolmentPlatform.Project.BLL.Systems
{
    public class T_SystemBasicSettingService : BaseService<T_SystemBasicSetting>, IT_SystemBasicSettingService, IInterceptorLogic
    {
        protected IDbContextFactory _dbContextFactory;
        public override bool SetCurrentRepository()
        {
            this.CurrentRepository = DIContainer.Resolve<IT_SystemBasicSettingRepository>();
            base.AddDisposableObject(base.CurrentRepository);
            this._dbContextFactory = DIContainer.Resolve<IDbContextFactory>();
            base.AddDisposableObject(_dbContextFactory);
            return true;
        }

        #region 系统设置
        /// <summary>
        /// 获取系统设置
        /// </summary>
        /// <returns></returns>
        public SystemParameterDTO GetSystemParameter()
        {
            SystemParameterDTO dto = new SystemParameterDTO();
            int b2CSpecialtyCancelTime = (int)SystemBasicSettingEnum.B2CSpecialtyCancelTime;
            int b2CTicketCancelTime = (int)SystemBasicSettingEnum.B2CTicketCancelTime;
            int touristsCenterTicketCancelTime = (int)SystemBasicSettingEnum.TouristsCenterTicketCancelTime;
            int specialtyAutoReceiptTime = (int)SystemBasicSettingEnum.SpecialtyAutoReceiptTime;
            int isNeedAdultBySelfSupplier = (int)SystemBasicSettingEnum.IsNeedAdultBySelfSupplier;
            int tikcetAutoComplete = (int)SystemBasicSettingEnum.TikcetAutoComplete;
            int payRetainageAutoCancel = (int)SystemBasicSettingEnum.PayRetainageAutoCancel;
            int cateringOrderAutoCancel = (int)SystemBasicSettingEnum.CateringOrderAutoCancel;
            List<int> list = new List<int>();
            list.Add(b2CSpecialtyCancelTime);
            list.Add(b2CTicketCancelTime);
            list.Add(touristsCenterTicketCancelTime);
            list.Add(specialtyAutoReceiptTime);
            list.Add(isNeedAdultBySelfSupplier);
            list.Add(tikcetAutoComplete);
            list.Add(payRetainageAutoCancel);
            list.Add(cateringOrderAutoCancel);
            List<T_SystemBasicSetting> listSet = this.LoadEntities(t => list.Contains(t.Key)).ToList();
            dto.B2CSpecialtyCancelTime = listSet.Where(t => t.Key == b2CSpecialtyCancelTime).FirstOrDefault() == null ? "" : listSet.Where(t => t.Key == b2CSpecialtyCancelTime).FirstOrDefault().Value;
            dto.B2CTicketCancelTime = listSet.Where(t => t.Key == b2CTicketCancelTime).FirstOrDefault() == null ? "" : listSet.Where(t => t.Key == b2CTicketCancelTime).FirstOrDefault().Value;
            dto.TouristsCenterTicketCancelTime = listSet.Where(t => t.Key == touristsCenterTicketCancelTime).FirstOrDefault() == null ? "" : listSet.Where(t => t.Key == touristsCenterTicketCancelTime).FirstOrDefault().Value;
            dto.SpecialtyAutoReceiptTime = listSet.Where(t => t.Key == specialtyAutoReceiptTime).FirstOrDefault() == null ? "" : listSet.Where(t => t.Key == specialtyAutoReceiptTime).FirstOrDefault().Value;
            dto.IsNeedAdultBySelfSupplier = listSet.Where(t => t.Key == isNeedAdultBySelfSupplier).FirstOrDefault() == null ? "" : listSet.Where(t => t.Key == isNeedAdultBySelfSupplier).FirstOrDefault().Value;
            dto.TikcetAutoComplete = listSet.Where(t => t.Key == tikcetAutoComplete).FirstOrDefault() == null ? "" : listSet.Where(t => t.Key == tikcetAutoComplete).FirstOrDefault().Value;
            dto.PayRetainageAutoCancel = listSet.Where(t => t.Key == payRetainageAutoCancel).FirstOrDefault() == null ? "" : listSet.Where(t => t.Key == payRetainageAutoCancel).FirstOrDefault().Value;
            dto.CateringOrderAutoCancel = listSet.Where(t => t.Key == cateringOrderAutoCancel).FirstOrDefault() == null ? "" : listSet.Where(t => t.Key == cateringOrderAutoCancel).FirstOrDefault().Value;
            return dto;
        }
        /// <summary>
        /// 系统设置操作
        /// </summary>
        /// <returns></returns>
        public ResultMsg SystemParameterSet(SystemParameterDTO dto)
        {
            ResultMsg msg = new ResultMsg();
            #region 数据查询

            int b2CSpecialtyCancelTime = (int)SystemBasicSettingEnum.B2CSpecialtyCancelTime;
            int b2CTicketCancelTime = (int)SystemBasicSettingEnum.B2CTicketCancelTime;
            int touristsCenterTicketCancelTime = (int)SystemBasicSettingEnum.TouristsCenterTicketCancelTime;
            int specialtyAutoReceiptTime = (int)SystemBasicSettingEnum.SpecialtyAutoReceiptTime;
            int isNeedAdultBySelfSupplier = (int)SystemBasicSettingEnum.IsNeedAdultBySelfSupplier;
            int tikcetAutoComplete = (int)SystemBasicSettingEnum.TikcetAutoComplete;
            int payRetainageAutoCancel = (int)SystemBasicSettingEnum.PayRetainageAutoCancel;
            int cateringOrderAutoCancel = (int)SystemBasicSettingEnum.CateringOrderAutoCancel;
            List<int> list = new List<int>();
            list.Add(b2CSpecialtyCancelTime);
            list.Add(b2CTicketCancelTime);
            list.Add(touristsCenterTicketCancelTime);
            list.Add(specialtyAutoReceiptTime);
            list.Add(isNeedAdultBySelfSupplier);
            list.Add(tikcetAutoComplete);
            list.Add(payRetainageAutoCancel);
            list.Add(cateringOrderAutoCancel);
            List<T_SystemBasicSetting> listSet = this.LoadEntities(t => list.Contains(t.Key)).ToList();


            #endregion

            #region 初始化数据
            //b2c农产品取消时间
            T_SystemBasicSetting b2CSpecialtyCancelTime_entity = listSet.Where(t => t.Key == b2CSpecialtyCancelTime).FirstOrDefault();
            if (b2CSpecialtyCancelTime_entity == null)
            {
                b2CSpecialtyCancelTime_entity = new T_SystemBasicSetting()
                {
                    Id = Guid.NewGuid(),
                    Key = b2CSpecialtyCancelTime,
                    Value = dto.B2CSpecialtyCancelTime,
                    CreatorAccount = dto.UpdateUserName,
                    CreatorUserId = dto.UpdateUserId
                };
            }
            else
            {
                b2CSpecialtyCancelTime_entity.Value = dto.B2CSpecialtyCancelTime;
                b2CSpecialtyCancelTime_entity.LastModifyUserId = dto.UpdateUserId;
            }
            //b2c票务取消时间
            T_SystemBasicSetting b2CTicketCancelTime_entity = listSet.Where(t => t.Key == b2CTicketCancelTime).FirstOrDefault();
            if (b2CTicketCancelTime_entity == null)
            {
                b2CTicketCancelTime_entity = new T_SystemBasicSetting()
                {
                    Id = Guid.NewGuid(),
                    Key = b2CTicketCancelTime,
                    Value = dto.B2CTicketCancelTime,
                    CreatorAccount = dto.UpdateUserName,
                    CreatorUserId = dto.UpdateUserId
                };
            }
            else
            {
                b2CTicketCancelTime_entity.Value = dto.B2CTicketCancelTime;
                b2CTicketCancelTime_entity.LastModifyUserId = dto.UpdateUserId;
            }
            //游客中心票务取消时间
            T_SystemBasicSetting touristsCenterTicketCancelTime_entity = listSet.Where(t => t.Key == touristsCenterTicketCancelTime).FirstOrDefault();
            if (touristsCenterTicketCancelTime_entity == null)
            {
                touristsCenterTicketCancelTime_entity = new T_SystemBasicSetting()
                {
                    Id = Guid.NewGuid(),
                    Key = touristsCenterTicketCancelTime,
                    Value = dto.TouristsCenterTicketCancelTime,
                    CreatorAccount = dto.UpdateUserName,
                    CreatorUserId = dto.UpdateUserId
                };
            }
            else
            {
                touristsCenterTicketCancelTime_entity.Value = dto.TouristsCenterTicketCancelTime;
                touristsCenterTicketCancelTime_entity.LastModifyUserId = dto.UpdateUserId;
            }
            //农产品自动收货时间
            T_SystemBasicSetting specialtyAutoReceiptTime_entity = listSet.Where(t => t.Key == specialtyAutoReceiptTime).FirstOrDefault();
            if (specialtyAutoReceiptTime_entity == null)
            {
                specialtyAutoReceiptTime_entity = new T_SystemBasicSetting()
                {
                    Id = Guid.NewGuid(),
                    Key = specialtyAutoReceiptTime,
                    Value = dto.SpecialtyAutoReceiptTime,
                    CreatorAccount = dto.UpdateUserName,
                    CreatorUserId = dto.UpdateUserId
                };
            }
            else
            {
                specialtyAutoReceiptTime_entity.Value = dto.SpecialtyAutoReceiptTime;
                specialtyAutoReceiptTime_entity.LastModifyUserId = dto.UpdateUserId;
            }
            //自营供应商发布产品是否需要审核
            T_SystemBasicSetting isNeedAdultBySelfSupplier_entity = listSet.Where(t => t.Key == isNeedAdultBySelfSupplier).FirstOrDefault();
            if (isNeedAdultBySelfSupplier_entity == null)
            {
                isNeedAdultBySelfSupplier_entity = new T_SystemBasicSetting()
                {
                    Id = Guid.NewGuid(),
                    Key = isNeedAdultBySelfSupplier,
                    Value = dto.IsNeedAdultBySelfSupplier,
                    CreatorAccount = dto.UpdateUserName,
                    CreatorUserId = dto.UpdateUserId
                };
            }
            else
            {
                isNeedAdultBySelfSupplier_entity.Value = dto.IsNeedAdultBySelfSupplier;
                isNeedAdultBySelfSupplier_entity.LastModifyUserId = dto.UpdateUserId;
            }
            //门票在游玩日期后多少天完成
            T_SystemBasicSetting tikcetAutoComplete_entity = listSet.Where(t => t.Key == tikcetAutoComplete).FirstOrDefault();
            if (tikcetAutoComplete_entity == null)
            {
                tikcetAutoComplete_entity = new T_SystemBasicSetting()
                {
                    Id = Guid.NewGuid(),
                    Key = tikcetAutoComplete,
                    Value = dto.TikcetAutoComplete,
                    CreatorAccount = dto.UpdateUserName,
                    CreatorUserId = dto.UpdateUserId
                };
            }
            else
            {
                tikcetAutoComplete_entity.Value = dto.TikcetAutoComplete;
                tikcetAutoComplete_entity.LastModifyUserId = dto.UpdateUserId;
            }
            //预售订单尾款支付
            T_SystemBasicSetting payRetainageAutoCancel_entity = listSet.Where(t => t.Key == payRetainageAutoCancel).FirstOrDefault();
            if (payRetainageAutoCancel_entity == null)
            {
                payRetainageAutoCancel_entity = new T_SystemBasicSetting()
                {
                    Id = Guid.NewGuid(),
                    Key = payRetainageAutoCancel,
                    Value = dto.PayRetainageAutoCancel,
                    CreatorAccount = dto.UpdateUserName,
                    CreatorUserId = dto.UpdateUserId
                };
            }
            else
            {
                payRetainageAutoCancel_entity.Value = dto.PayRetainageAutoCancel;
                payRetainageAutoCancel_entity.LastModifyUserId = dto.UpdateUserId;
            }
            //餐饮订单自动取消时间
            T_SystemBasicSetting cateringOrderAutoCancel_entity = listSet.Where(t => t.Key == cateringOrderAutoCancel).FirstOrDefault();
            if (cateringOrderAutoCancel_entity == null)
            {
                cateringOrderAutoCancel_entity = new T_SystemBasicSetting()
                {
                    Id = Guid.NewGuid(),
                    Key = cateringOrderAutoCancel,
                    Value = dto.CateringOrderAutoCancel,
                    CreatorAccount = dto.UpdateUserName,
                    CreatorUserId = dto.UpdateUserId
                };
            }
            else
            {
                cateringOrderAutoCancel_entity.Value = dto.CateringOrderAutoCancel;
                cateringOrderAutoCancel_entity.LastModifyUserId = dto.UpdateUserId;
            }
            List<T_SystemBasicSetting> sysSetList = new List<T_SystemBasicSetting>()
            {
                b2CSpecialtyCancelTime_entity,
                b2CTicketCancelTime_entity,
                touristsCenterTicketCancelTime_entity,
                specialtyAutoReceiptTime_entity,
                isNeedAdultBySelfSupplier_entity,
                tikcetAutoComplete_entity,
                payRetainageAutoCancel_entity,
                cateringOrderAutoCancel_entity
            };
            #endregion

            #region  tran
            DbConnection conn = ((IObjectContextAdapter)_dbContextFactory.GetCurrentThreadInstance()).ObjectContext.Connection;
            conn.Open();
            using (var tran = conn.BeginTransaction())
            {
                try
                {
                    this.PhysicsDeleteBy(t => list.Contains(t.Key));
                    msg.IsSuccess = this.AddEntities(sysSetList) > 0;
                    if (msg.IsSuccess)
                    {
                        tran.Commit();
                    }
                    else
                    {
                        tran.Rollback();
                    }
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    msg.IsSuccess = false;
                    msg.Info = ex.Message;
                }

            }
            #endregion
            return msg;
        }
        #endregion

        #region 全站配置
        /// <summary>
        /// 获取全站配置
        /// </summary>
        /// <returns></returns>
        public TotalStationSetDTO GetTotalStationSet()
        {
            TotalStationSetDTO dto = new TotalStationSetDTO();
            int websiteLogo = (int)SystemBasicSettingEnum.WebsiteLogo;
            int websiteTitle = (int)SystemBasicSettingEnum.WebsiteTitle;
            int websiteKW = (int)SystemBasicSettingEnum.WebsiteKW;
            List<int> list = new List<int>();
            list.Add(websiteLogo);
            list.Add(websiteTitle);
            list.Add(websiteKW);
            List<T_SystemBasicSetting> listSet = this.LoadEntities(t => list.Contains(t.Key)).ToList();
            dto.WebsiteLogo = listSet.Where(t => t.Key == websiteLogo).FirstOrDefault() == null ? "" : listSet.Where(t => t.Key == websiteLogo).FirstOrDefault().Value;
            dto.WebsiteTitle = listSet.Where(t => t.Key == websiteTitle).FirstOrDefault() == null ? "" : listSet.Where(t => t.Key == websiteTitle).FirstOrDefault().Value;
            dto.WebsiteKW = listSet.Where(t => t.Key == websiteKW).FirstOrDefault() == null ? "" : listSet.Where(t => t.Key == websiteKW).FirstOrDefault().Value;
            return dto;
        }
        /// <summary>
        /// 全站配置操作
        /// </summary>
        /// <returns></returns>
        public ResultMsg TotalStationSet(TotalStationSetDTO dto)
        {
            ResultMsg msg = new ResultMsg();
            #region 数据查询

            int websiteLogo = (int)SystemBasicSettingEnum.WebsiteLogo;
            int websiteTitle = (int)SystemBasicSettingEnum.WebsiteTitle;
            int websiteKW = (int)SystemBasicSettingEnum.WebsiteKW;
            List<int> list = new List<int>();
            list.Add(websiteLogo);
            list.Add(websiteTitle);
            list.Add(websiteKW);
            List<T_SystemBasicSetting> listSet = this.LoadEntities(t => list.Contains(t.Key)).ToList();
            T_SystemBasicSetting websiteLogo_entity = listSet.Where(t => t.Key == websiteLogo).FirstOrDefault();

            #endregion

            #region 初始化数据
            if (websiteLogo_entity == null)
            {
                websiteLogo_entity = new T_SystemBasicSetting()
                {
                    Id = Guid.NewGuid(),
                    Key = websiteLogo,
                    Value = dto.WebsiteLogo,
                    CreatorAccount = dto.UpdateUserName,
                    CreatorUserId = dto.UpdateUserId
                };
            }
            else
            {
                websiteLogo_entity.Value = dto.WebsiteLogo;
                websiteLogo_entity.LastModifyUserId = dto.UpdateUserId;
            }
            T_SystemBasicSetting websiteTitle_entity = listSet.Where(t => t.Key == websiteTitle).FirstOrDefault();
            if (websiteTitle_entity == null)
            {
                websiteTitle_entity = new T_SystemBasicSetting()
                {
                    Id = Guid.NewGuid(),
                    Key = websiteTitle,
                    Value = dto.WebsiteTitle,
                    CreatorAccount = dto.UpdateUserName,
                    CreatorUserId = dto.UpdateUserId
                };
            }
            else
            {
                websiteTitle_entity.Value = dto.WebsiteTitle;
                websiteTitle_entity.LastModifyUserId = dto.UpdateUserId;
            }
            T_SystemBasicSetting websiteKW_entity = listSet.Where(t => t.Key == websiteKW).FirstOrDefault();
            if (websiteKW_entity == null)
            {
                websiteKW_entity = new T_SystemBasicSetting()
                {
                    Id = Guid.NewGuid(),
                    Key = websiteKW,
                    Value = dto.WebsiteKW,
                    CreatorAccount = dto.UpdateUserName,
                    CreatorUserId = dto.UpdateUserId
                };
            }
            else
            {
                websiteKW_entity.Value = dto.WebsiteKW;
                websiteKW_entity.LastModifyUserId = dto.UpdateUserId;
            }

            List<T_SystemBasicSetting> sysSetList = new List<T_SystemBasicSetting>()
            {
                websiteLogo_entity,
                websiteTitle_entity,
                websiteKW_entity
            };
            #endregion

            #region  tran
            DbConnection conn = ((IObjectContextAdapter)_dbContextFactory.GetCurrentThreadInstance()).ObjectContext.Connection;
            conn.Open();
            using (var tran = conn.BeginTransaction())
            {
                try
                {
                    this.PhysicsDeleteBy(t => list.Contains(t.Key));
                    msg.IsSuccess = this.AddEntities(sysSetList) > 0;
                    if (msg.IsSuccess)
                    {
                        tran.Commit();
                    }
                    else
                    {
                        tran.Rollback();
                    }
                    RedisHelper.Remove("GetTotalStationSet");//清楚缓存
                }
                catch (Exception ex)
                {
                    tran.Rollback();
                    msg.IsSuccess = false;
                    msg.Info = ex.Message;
                }

            }
            #endregion
            return msg;
        }
        #endregion
    }
}
