using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.EFContext;

namespace EnrolmentPlatform.Project.IDAL
{
    public interface IDbContextFactory
    {
        E_DbClassify DbClassify { get; set; }
        EnrolmentPlatformDbContext GetCurrentThreadInstance();
    }
}
