using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKAPITask.Models
{
    public class SaveWallPhotoResponseValues
    {
        public int album_id { get; set; }
        public int date { get; set; }
        public int id { get; set; }
        public int owner_id { get; set; }
        public string access_key { get; set; }
        public List<PhotoSize> sizes { get; set; }
        public string text { get; set; }
        public bool has_tags { get; set; }
    }
}