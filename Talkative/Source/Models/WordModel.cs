using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talkative.Source.Models
{
    public class WordModel
    {
        public string Id { get; set; }
        public string Word { get; set; }
        public string WordGroupId { get; set; }
        public string wordIMG { get; set; }

        public ImageSource ImageSource { get; set; }
    }
}
