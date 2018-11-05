using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Systems;

namespace EnrolmentPlatform.Project.IDAL.Systems
{ 
    public interface IT_FileRepository : IBaseRepository<T_File>
    {

        /// <summary>
        /// 查询图片
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fileClassify"></param>
        /// <param name="foreignKeyClassify"></param>
        /// <returns></returns>
        List<OptionParamForPictureDto> GetPictureDto(Guid id, int fileClassify, int foreignKeyClassify);
        /// <summary>
        /// dto转为图片table
        /// </summary>
        /// <returns></returns> 
        List<T_File> GetFileTable(List<OptionParamForPictureDto> optionParamForPictureDto, Guid keyId, string creatorAccount, Guid creatorUserId);
    }
}
