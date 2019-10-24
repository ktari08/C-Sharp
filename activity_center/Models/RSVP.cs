using System;
using System.ComponentModel.DataAnnotations;

namespace activity_center.Models
{
    public class Rsvp
    {
        public int RsvpId {get;set;}
        public int UserId {get;set;}

        public int TheActivityId {get;set;}

        public User User {get;set;}
        public TheActivity TheActivity {get;set;}
    }
}