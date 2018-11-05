using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using EnrolmentPlatform.Project.IBLL.Systems;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.WebApi.WebLibrary;

namespace EnrolmentPlatform.Project.WebApi.Areas.Systems
{ 
    public class FileController : ApiBaseController
    {
        protected IT_FileService FileService;
        public FileController()
        {
            this.FileService = DIContainer.Resolve<IT_FileService>();
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="Id">文件Id</param> 
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> DeleteFileById(Guid Id)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.IsSuccess = FileService.DeleteFileById(Id);
                return _resultMsg.ResponseMessage();
            });
        } 
    }
}
