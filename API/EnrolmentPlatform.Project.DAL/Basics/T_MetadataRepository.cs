﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.IDAL.Basics;

namespace EnrolmentPlatform.Project.DAL.Basics
{
    public class T_MetadataRepository : BaseRepository<T_Metadata>, IT_MetadataRepository
    {
    }
}
