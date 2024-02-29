﻿using System.ComponentModel.DataAnnotations;
using Tourism.Core.Entities;

namespace TourismMVC.ViewModels
{
    public class NotificationModel
    {

        public int Id { get; set; }
        [Required]
        public string Message { get; set; } = null!;

        [Display(Name = "Read Status")]
        [Required]
        public bool ReadStatus { get; set; }
        [Required]
        public DateTime Date { get; set; } = DateTime.Now;

        public int UserId { get; set; }
        public virtual User? User { get; set; }

      public  IEnumerable<User> users { get; set; } = new List<User>();
    }
}
