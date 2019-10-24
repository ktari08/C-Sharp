using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace wedding_planner.Models
{
    public class Wedding
    {
        [Key]
        public int WeddingId {get;set;}

        [Required(ErrorMessage="Please enter a wedder")]
        public string groom {get;set;}
        
        [Required(ErrorMessage="Please enter a wedder")]
        public string bride {get;set;}

        [Required(ErrorMessage="Please enter a date")]
        public DateTime WeddingDate {get;set;}

        [Required(ErrorMessage="Please enter an address for the wedding")]
        public string address {get;set;}

        public int UserId { get; set; }
        
        public User User { get; set; }

        public List<Rsvp> Attending {get;set;}

        public Wedding()
        {
            Attending = new List<Rsvp>();
        }
    }
}