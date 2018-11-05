using System.Runtime.Remoting.Messaging;
using System.Web;
using EnrolmentPlatform.Project.Domain.EFContext;
using EnrolmentPlatform.Project.IDAL;

namespace EnrolmentPlatform.Project.DAL
{
    public class DbContextFactory : IDbContextFactory
    {
        public E_DbClassify DbClassify { get; set; }
        public EnrolmentPlatformDbContext GetCurrentThreadInstance()
        {
            string connectionName = "EnrolmentPlatformDbContextWrite";
            if (DbClassify.Equals(E_DbClassify.Read))
            {
                connectionName = "EnrolmentPlatformDbContextRead";
            }
            //ECardPassDbContext dbContext = new ECardPassDbContext(connectionName); ;

            EnrolmentPlatformDbContext dbContext = CallContext.GetData(connectionName) as EnrolmentPlatformDbContext;

            if (dbContext == null)  //线程在内存中没有此上下文  
            {
                //如果不存在上下文 创建一个(自定义)EF上下文  并且放在数据内存中去  
                dbContext = new EnrolmentPlatformDbContext(connectionName);
                CallContext.SetData(connectionName, dbContext);
            }
            return dbContext;  
        }

    }
}
