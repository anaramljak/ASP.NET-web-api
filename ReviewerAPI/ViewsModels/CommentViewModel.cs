using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ReviewerAPI.ViewsModels
{
    public class CommentViewModel
    {
        [Required]
        public int PostId { get; set; }
        [Required]

        public int ReviewId { get; set; }

        public string Message { get; set; }

        public string UserId { get; set; }
    }
}
