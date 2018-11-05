using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.IDAL.Systems;

namespace EnrolmentPlatform.Project.DAL.Systems
{
    public class T_AddressRepository : BaseRepository<T_Address>, IT_AddressRepository
    {
    }
}
