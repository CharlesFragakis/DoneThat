using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoneThatMVC.Models
{
    public class Friendship
    {
        public bool IsFriend { get; set; }

        public int MakeRequestId { get; set; }
        public int AcceptRequestId { get; set; }

        public User MakeRequestUser { get; set; }
        public User AcceptRequestUser { get; set; }
    }
}
