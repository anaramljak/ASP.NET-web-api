using System;
using System.Collections.Generic;
using System.Text;

namespace ReviewerDomain.Models
{
    public class Base
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime Created { get; set; }

        public string UserId { get; set; }

        

    }
}
