using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ReviewerAPI.ViewsModels
{
    public class ReviewViewModel
    {
    
            [Required]
            public int PostId { get; set; }
            [Required]
           
             public int ReviewId { get; set; }

            public string Message { get; set; }

            [Range(0, 5, ErrorMessage = "Izaberite broj između 1 i 5")]
            public int Rating { get; set; }

            public string UserId { get; set; }




    }
}

