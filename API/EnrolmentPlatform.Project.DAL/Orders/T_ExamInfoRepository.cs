﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.IDAL.Orders;

namespace EnrolmentPlatform.Project.DAL.Orders
{
    public class T_ExamInfoRepository : BaseRepository<T_ExamInfo>, IT_ExamInfoRepository
    {
    }
}
