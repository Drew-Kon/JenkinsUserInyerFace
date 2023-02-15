using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKAPITask.Models
{
    public class PhotoSize
    {
        public int height { get; set; }
        public string url { get; set; }
        public string type { get; set; }
        public int width
        {
            get; set;
        }
    }
}