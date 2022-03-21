using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace ReviewerDomain.Models
{
    public class Review : Base
    {
        public List<Comment> Comments { get; set; }
        [Range(0, 5, ErrorMessage = "Please select number between 1 and 5")]
        public int Rating { get; set; }

        public Post post { get; set; }

    }
}
