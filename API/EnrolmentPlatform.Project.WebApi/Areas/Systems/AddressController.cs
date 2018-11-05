using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using EnrolmentPlatform.Project.Domain.Entities;
using EnrolmentPlatform.Project.IBLL.Systems;
using EnrolmentPlatform.Project.Infrastructure;
using EnrolmentPlatform.Project.WebApi.WebLibrary;

namespace EnrolmentPlatform.Project.WebApi.Areas.Systems
{
    public class AddressController : ApiBaseController
    {
        protected IT_AddressService AddressService;
        public AddressController()
        {
            this.AddressService = DIContainer.Resolve<IT_AddressService>();
        }
        /// <summary>
        /// 查父类
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> FindParentAddressId(Guid parentId)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.Data = AddressService.FindParentAddress(parentId);
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 查子类
        /// </summary>
        /// <param name="parentId"
        [HttpGet]
        public async Task<HttpResponseMessage> FindSubAddressId(Guid Id)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.Data = AddressService.FindSubAddressId(Id);
                return _resultMsg.ResponseMessage();
            });
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetAllProvinceByCountryName(string countryName)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.Data = AddressService.GetAllProvinceByCountryName(countryName);
                return _resultMsg.ResponseMessage();
            });
        }
        /// <summary>
        /// 根据类型查找
        /// </summary>
        /// <param name="classify"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> FindAddressByClassify(int classify)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.Data = AddressService.FindAddressByClassify(classify);
                return _resultMsg.ResponseMessage();
            });
        }
        /// <summary>
        /// 根据类型和父类Id查找
        /// </summary>
        /// <param name="classify"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> FindAddressByClassifyAndParentId(int classify, Guid parentId)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.Data = AddressService.FindAddressByClassifyAndParentId(classify, parentId);
                return _resultMsg.ResponseMessage();
            });
        }
        /// <summary>
        /// 根据Id查找
        /// </summary>
        /// <param name="classify"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> FindAddressById(Guid Id)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = new ResultMsg();
                _resultMsg.Data = AddressService.FindEntityById(Id);
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetList()
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = AddressService.GetList();
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> Add(T_Address entity)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = AddressService.Add(entity);
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 根据ID获取地址
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> GetEntityById(Guid id)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = AddressService.GetEntityById(id);
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> Update(T_Address entity)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = AddressService.Update(entity);
                return _resultMsg.ResponseMessage();
            });
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<HttpResponseMessage> DeleteById(Guid id)
        {
            return await Task.Run(() =>
            {
                ResultMsg _resultMsg = AddressService.DeleteById(id);
                return _resultMsg.ResponseMessage();
            });
        }
    }
}
