﻿using System.Data.Entity;
using EnrolmentPlatform.Project.Domain.Entities;
using System.Linq;
using System.Data.Entity.Core.Objects;
using System.Collections.Generic;
using EnrolmentPlatform.Project.Domain.Entities.Orders;

namespace EnrolmentPlatform.Project.Domain.EFContext
{
    public partial class EnrolmentPlatformDbContext : DbContext
    {
        //账号
        public DbSet<T_AccountBasic> T_AccountBasic { get; set; }
        public DbSet<T_Enterprise> T_Enterprise { get; set; }
        public DbSet<T_Permissions> T_Permissions { get; set; }
        public DbSet<T_Role> T_Role { get; set; }
        public DbSet<T_RolePermissionsRelation> T_RolePermissionsRelation { get; set; }

        //文件
        public DbSet<T_File> T_File { get; set; }

        //系统
        public DbSet<T_Address> T_Address { get; set; }
        public DbSet<T_Department> T_Department { get; set; }
        public DbSet<T_Job> T_Job { get; set; }
        public DbSet<T_LogSetting> T_LogSetting { get; set; }
        public DbSet<T_LogSettingDetail> T_LogSettingDetail { get; set; }
        public DbSet<T_SystemBasicSetting> T_SystemBasicSetting { get; set; }
        public DbSet<T_SystemLoginLog> T_SystemLoginLog { get; set; }
        public DbSet<T_SystemMessage> T_SystemMessage { get; set; }

        //配置
        public DbSet<T_ChargeStrategy> T_ChargeStrategy { set; get; }

        //订单
        public DbSet<T_Order> T_Order { get; set; }
        public DbSet<T_OrderAmount> T_OrderAmount { get; set; }
        public DbSet<T_OrderImage> T_OrderImage { get; set; }
        public DbSet<T_PaymentInfo> T_PaymentInfo { get; set; }
        public DbSet<T_PaymentRecord> T_PaymentRecord { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

    }
}
