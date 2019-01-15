using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoneThatMVC.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsActive { get; set; }
        public string Email { get; set; }

        public List<Task> tasks { get; set; }
        public List<Friendship> requests { get; set; }
        public List<Friendship> acceptances { get; set; }
        public List<Message> messagesReceived { get; set; }
        public List<Message> messagesSent { get; set; }
    }
}
