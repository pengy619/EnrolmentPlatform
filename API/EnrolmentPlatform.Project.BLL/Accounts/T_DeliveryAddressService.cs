using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Accounts;
using EnrolmentPlatform.Project.IBLL.Accounts;
using EnrolmentPlatform.Project.IDAL;
using EnrolmentPlatform.Project.IDAL.Accounts;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.Infrastructure.Castle;

namespace EnrolmentPlatform.Project.BLL.Accounts
{
    /// <summary>
    /// 收货地址服务
    /// </summary>
    public class T_DeliveryAddressService : IT_DeliveryAddressService, IInterceptorLogic
    {
        private IT_DeliveryAddressRepository deliveryAddressRepository = null;
        protected IDbContextFactory _dbContextFactory= DIContainer.Resolve<IDbContextFactory>();

        public T_DeliveryAddressService()
        {
            this.deliveryAddressRepository = DIContainer.Resolve<IT_DeliveryAddressRepository>();
        }

        /// <summary>
        /// 获得会员收货地址
        /// </summary>
        /// <param name="userId">会员ID</param>
        /// <returns></returns>
        public List<DeliveryAddressDto> GetMemberAllDeliveryAddressList(Guid memberId)
        {
            return this.deliveryAddressRepository.LoadEntities(it => it.IsDelete == false && it.CreatorUserId == memberId)
                .OrderByDescending(a=>a.IsDefault).ThenByDescending(a=>a.CreatorTime).ToList()
                .Select(o => new DeliveryAddressDto
                {
                    AccountId = o.AccountId,
                    Address = o.Address,
                    AddressId = o.AddressId,
                    Consignee = o.Consignee,
                    ContactPhone = o.ContactPhone,
                    FullAddressName = o.FullAddressName,
                    Id = o.Id,
                    IsDefault = o.IsDefault,
                    UserId = o.CreatorUserId
                }).ToList();
        }

        /// <summary>
        /// 获得会员收货地址详情
        /// </summary>
        /// <param name="addressId">收货地址ID</param>
        /// <param name="userId">会员ID</param>
        /// <returns></returns>
        public DeliveryAddressDto GetMemberDeliveryAddress(Guid addressId,Guid userId)
        {
            var o= this.deliveryAddressRepository.FindEntityById(addressId);
            if (o == null || o.CreatorUserId != userId) return null;
            return new DeliveryAddressDto()
            {
                AccountId = o.AccountId,
                Address = o.Address,
                AddressId = o.AddressId,
                Consignee = o.Consignee,
                ContactPhone = o.ContactPhone,
                FullAddressName = o.FullAddressName,
                Id = o.Id,
                IsDefault = o.IsDefault,
                UserId = o.CreatorUserId
            };
        }

        /// <summary>
        /// 获得会员默认收货地址
        /// </summary>
        /// <param name="userId">会员ID</param>
        /// <returns></returns>
        public DeliveryAddressDto GetMemberDefaultDeliveryAddress(Guid memberId)
        {
            var defaultAddress = this.deliveryAddressRepository.LoadEntities(it => it.IsDelete == false && it.CreatorUserId == memberId
                         && it.IsDefault == true).FirstOrDefault();
            if (defaultAddress == null)
            {
                return null;
            }

            return new DeliveryAddressDto()
            {
                AccountId = defaultAddress.AccountId,
                Address = defaultAddress.Address,
                AddressId = defaultAddress.AddressId,
                Consignee = defaultAddress.Consignee,
                ContactPhone = defaultAddress.ContactPhone,
                FullAddressName = defaultAddress.FullAddressName,
                Id = defaultAddress.Id,
                IsDefault = defaultAddress.IsDefault,
                UserId = defaultAddress.CreatorUserId
            };
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="addressId">新增成功后的地址ID</param>
        /// <returns>1：成功，2：失败，3：最多只能添加10条</returns>
        public int Save(DeliveryAddressDto dto,ref Guid addressId)
        {
            //id有值为修改
            if (dto.Id.HasValue == true)
            {
                var temp = this.deliveryAddressRepository.FindEntityById(dto.Id.Value);
                temp.Consignee = dto.Consignee;
                temp.ContactPhone = dto.ContactPhone;
                temp.FullAddressName = dto.FullAddressName;
                temp.Address = dto.Address;
                temp.AddressId = dto.AddressId;
                var ret = this.deliveryAddressRepository.UpdateEntity(temp, Domain.EFContext.E_DbClassify.Write, "", false);
                return ret > 0 ? 1 : 2;
            }
            else
            {
                //校验收货地址是否大于10条
                var recount = this.deliveryAddressRepository.LoadEntities(a => a.IsDelete == false && a.CreatorUserId == dto.UserId).Count();
                if (recount >= 10)
                {
                    //大于等于10条的时候不允许添加
                    return 3;
                }

                //如果不存在收货地址
                if (recount == 0)
                {
                    //那么第一条收货地址为默认收货地址
                    dto.IsDefault = true;
                }

                //否则为新增
                var data = new T_DeliveryAddress()
                {
                    AccountId = dto.UserId,
                    Address = dto.Address,
                    AddressId = dto.AddressId,
                    Consignee = dto.Consignee,
                    ContactPhone = dto.ContactPhone,
                    CreatorUserId = dto.UserId,
                    Id = Guid.NewGuid(),
                    FullAddressName = dto.FullAddressName,
                    IsDefault = dto.IsDefault
                };

                var ret = this.deliveryAddressRepository.AddEntity(data, Domain.EFContext.E_DbClassify.Write, "", false);
                addressId = data.Id;
                return ret > 0 ? 1 : 2;
            }
        }

        /// <summary>
        /// 设置为默认收货地址
        /// </summary>
        /// <param name="memberId">会员ID</param>
        /// <param name="deliveryId">收货地址ID</param>
        /// <returns></returns>
        public bool SetDefault(Guid memberId, Guid deliveryId)
        {
            using (DbConnection conn = ((IObjectContextAdapter)_dbContextFactory.GetCurrentThreadInstance()).ObjectContext.Connection)
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    //1.去除之前的默认
                    var defaultDelivery = this.deliveryAddressRepository
                        .LoadEntities(a => a.CreatorUserId == memberId && a.IsDefault == true).FirstOrDefault();
                    if (defaultDelivery != null)
                    {
                        defaultDelivery.IsDefault = false;
                    }
                    this.deliveryAddressRepository.UpdateEntity(defaultDelivery, Domain.EFContext.E_DbClassify.Write, "", false);

                    //2.设置当前为默认
                    var curDelivery = this.deliveryAddressRepository.FindEntityById(deliveryId);
                    if (curDelivery != null && curDelivery.CreatorUserId == memberId)
                    {
                        curDelivery.IsDefault = true;
                        this.deliveryAddressRepository.UpdateEntity(curDelivery, Domain.EFContext.E_DbClassify.Write, "", false);
                        tran.Commit();
                        return true;
                    }

                    return false;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }

        /// <summary>
        /// 删除收货地址
        /// </summary>
        /// <param name="deleiveryId">收货地址ID</param>
        /// <returns></returns>
        public bool Delete(Guid memberId, Guid deleiveryId)
        {
            using (DbConnection conn = ((IObjectContextAdapter)_dbContextFactory.GetCurrentThreadInstance()).ObjectContext.Connection)
            {
                conn.Open();
                var tran = conn.BeginTransaction();
                try
                {
                    var entity = this.deliveryAddressRepository.FindEntityById(deleiveryId);
                    if (entity == null || entity.CreatorUserId != memberId)
                    {
                        return false;
                    }

                    //物理删除
                    bool ret= this.deliveryAddressRepository.PhysicsDeleteEntity(entity, Domain.EFContext.E_DbClassify.Write, "", false) > 0;

                    //如果默认的收货地址删除了
                    if (entity.IsDefault==true && ret == true)
                    {
                        //最后添加的一条为默认收货地址
                        var last = this.deliveryAddressRepository.LoadEntities(it => it.IsDelete == false && it.CreatorUserId == memberId)
                          .OrderByDescending(a => a.CreatorTime).FirstOrDefault();
                        if (last != null)
                        {
                            last.IsDefault = true;
                            ret = this.deliveryAddressRepository.UpdateEntity(last, Domain.EFContext.E_DbClassify.Write, "", false) > 0;
                        }
                    }

                    //处理成功
                    if (ret == true)
                    {
                        tran.Commit();
                    }
                    return ret;
                }
                catch (Exception)
                {
                    tran.Rollback();
                    return false;
                }
            }
        }
    }
}
