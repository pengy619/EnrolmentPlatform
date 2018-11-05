using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Systems
{
    public class AddressDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 中文名称
        /// </summary>
        public string ChinaName { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int Classify { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        public Guid ParentId { get; set; }

        public List<AddressDto> ProvinceLst { get; set; }
        public List<AddressDto> CityLst { get; set; }
        public List<AddressDto> DistrictLst { get; set; } 
        public List<AddressDto> ParentAddressLst { get; set; }


    }
}
