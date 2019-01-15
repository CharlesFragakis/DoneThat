﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoneThatMVC.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime TimeStamp { get; set; }


        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

        public User Sender { get; set; }
        public User Receiver { get; set; }
    }
}
