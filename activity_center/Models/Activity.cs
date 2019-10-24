using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace activity_center.Models
{
    public class TheActivity
    {
        [Key]
        public int TheActivityId {get;set;}

        [Required(ErrorMessage="Please enter a title")]
        public string title {get;set;}

        [Required(ErrorMessage="Please enter a date")]
        public DateTime ActivityDate {get;set;}

        [Required(ErrorMessage="Please enter a description")]
        public string description {get;set;}

        [Required(ErrorMessage="Please enter a duration")]
        [RegularExpression(@"^[0-9*#+:]+$", ErrorMessage = "Duration must only contain numbers")]
        public int duration {get;set;}

        [Required]
        public string duration_unit {get;set;}

        [Required(ErrorMessage="Please enter a time")]
        [RegularExpression(@"^[0-9*#+:pmam]+$", ErrorMessage = "Time must only contain numbers and special characters")]
        public string time {get;set;}

        public int UserId { get; set; }

        public User User { get; set; }

        public List<Rsvp> Attending {get;set;}

        public TheActivity()
        {
            Attending = new List<Rsvp>();
        }
    }
}