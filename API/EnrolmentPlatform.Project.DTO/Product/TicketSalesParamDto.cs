using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Product
{
    public class TicketSalesParamDto
    {
        /// <summary>
        ///  票种列表
        /// </summary>
        public List<OptionParamForKeyValueDto> TicketTypeList { get; set; }

        /// <summary>
        ///  主题 列表
        /// </summary>
        public List<OptionParamForKeyValueDto> ThemeList { get; set; }

        /// <summary>
        /// 景点列表
        /// </summary>
        public List<OptionParamForKeyValueDto> AttractionsList { get; set; }

        /// <summary>
        /// 游乐项目 列表
        /// </summary>
        public List<OptionParamForKeyValueDto> AmusementItemsList { get; set; }

    }
}
