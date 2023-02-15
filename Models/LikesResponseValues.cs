using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKAPITask.Models
{
    public class LikesResponseValues
    {
        public string count { get; set; }
        public List<User> users { get; set; }
    }
}