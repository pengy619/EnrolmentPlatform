using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.DTO.Systems;
using EnrolmentPlatform.Project.IDAL.Systems;

namespace EnrolmentPlatform.Project.DAL.Systems
{ 
    public class T_FileRepository : BaseRepository<T_File>, IT_FileRepository
    {


        /// <summary>
        /// 查询图片
        /// </summary>
        /// <returns></returns>  
        public List<OptionParamForPictureDto> GetPictureDto(Guid id, int fileClassify, int foreignKeyClassify)
        {
            List<OptionParamForPictureDto> optionParamForPictureDto = new List<OptionParamForPictureDto>();
            List<T_File> fileList = this.LoadEntities(it => it.IsDelete == false && it.ForeignKeyId == id && it.FileClassify == fileClassify && it.ForeignKeyClassify == foreignKeyClassify).ToList();
            if (fileList != null && fileList.Any())
            {
                fileList.ForEach(it =>
                {
                    optionParamForPictureDto.Add(new OptionParamForPictureDto()
                    {
                        Id = it.Id,
                        ForeignKeyId = it.ForeignKeyId,
                        FilePath = it.FilePath,
                        FileName = it.FileName,
                        FileClassify = it.FileClassify,
                        ForeignKeyClassify = it.ForeignKeyClassify,
                        Iscover = it.Iscover,
                        IsFocus = it.IsFocus
                    });
                });
            }
            return optionParamForPictureDto;
        }

        /// <summary>
        /// dto转为图片table
        /// </summary>
        /// <returns></returns> 
        public List<T_File> GetFileTable(List<OptionParamForPictureDto> optionParamForPictureDto, Guid keyId, string creatorAccount, Guid creatorUserId)
        {
            List<T_File> fileList = new List<T_File>();
            if (optionParamForPictureDto != null && optionParamForPictureDto.Any())
            {
                optionParamForPictureDto.ForEach(it =>
                {
                    fileList.Add(new T_File()
                    {
                        Id = Guid.NewGuid(),
                        ForeignKeyId = keyId,
                        FilePath = it.FilePath,
                        FileName = it.FileName,
                        FileClassify = it.FileClassify,
                        ForeignKeyClassify = it.ForeignKeyClassify,
                        Iscover = it.Iscover,
                        IsFocus = it.IsFocus,
                        CreatorAccount = creatorAccount,
                        CreatorUserId = creatorUserId
                    });
                });
            }
            return fileList;
        }
    }
}
