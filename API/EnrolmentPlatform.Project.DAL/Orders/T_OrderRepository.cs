﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities.Orders;
using EnrolmentPlatform.Project.IDAL.Orders;

namespace EnrolmentPlatform.Project.DAL.Orders
{
    public class T_OrderRepository : BaseRepository<T_Order>, IT_OrderRepository
    {
    }
}
