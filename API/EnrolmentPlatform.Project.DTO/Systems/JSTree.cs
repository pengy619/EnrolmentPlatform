using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnrolmentPlatform.Project.DTO.Systems
{
  public  class JSTree
    {
        public Guid id { get; set; }
        public string text { get; set; }
        public List<JSTree> children { get; set; }
        public string icon { get; set; }

        public LiAttr li_attr { get; set; }
        public State state { get; set; }
        public int Sort { get; set; }
    }
    public class State
    {
        public bool opened { get; set; }
        public bool selected { get; set; }
        public bool disabled { get; set; }


    }
    public class LiAttr
    {
        public Guid parentId { get; set; }
        public int level { get; set; }
        public int classify { get; set; }
        public string ShortPinyin { get; set; }
        public Guid itemId { get; set; }
    }
}
