﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThroughEveryAge.Data
{
    public class Contact
    {
        public int ContactId { get; set; }

        public string FirstName { get; set; }
     
        public string LastName { get; set; }
  
        public string Email { get; set; }
       
        public string Subject { get; set; }
     
        public string Message { get; set; }
    }
}
