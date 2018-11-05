using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.IDAL.Accounts;

namespace EnrolmentPlatform.Project.DAL.Accounts
{
    public class T_AccountVerificationRepository : BaseRepository<T_AccountVerification>, IT_AccountVerificationRepository
    {
    }
}
