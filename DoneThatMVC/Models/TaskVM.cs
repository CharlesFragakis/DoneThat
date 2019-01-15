using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DoneThatMVC.Models
{
    public class TaskVM
    {
        public int? Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        [Required]
        public TimeSpan Start { get; set; }
        [Required]
        public TimeSpan Finish { get; set; }
        [Required]
        public DateTime Date { get; set; }
        public int Priority { get; set; }
    }
}
