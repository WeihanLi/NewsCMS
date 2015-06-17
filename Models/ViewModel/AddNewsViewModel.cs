using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.ViewModel
{
    public class AddNewsViewModel
    {
        public string NewsTitle { get; set; }

        public string NewsContent { get; set; }

        public int NewsTypeId { get; set; }

        public int IsTop { get; set; }

        public string NewsImagePath { get; set; }

        public string NewsAttachPath { get; set; }

        public string NewsExternalPath { get; set; }
    }
}
