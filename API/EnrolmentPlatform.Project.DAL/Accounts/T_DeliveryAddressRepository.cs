using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Accounts;
using EnrolmentPlatform.Project.IDAL.Accounts;

namespace EnrolmentPlatform.Project.DAL.Accounts
{
    /// <summary>
    /// 收货地址数据处理
    /// </summary>
    public class T_DeliveryAddressRepository : BaseRepository<T_DeliveryAddress>, IT_DeliveryAddressRepository
    {

    }
}
