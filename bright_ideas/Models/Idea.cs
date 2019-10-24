using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace bright_ideas.Models
{
    public class Idea
    {
        [Key]
        public int IdeaId{get;set;}

        [Required(ErrorMessage="Please enter a bright idea")]
        [MinLength(5, ErrorMessage = "Idea must be at least 5 characters long")]
        public string TheIdea {get;set;}

        public int UserId { get; set; }

        public User User { get; set; }

        public List<Like> likeThis {get;set;}
        
        public Idea()
        {
            likeThis = new List<Like>();
        }
    }
}