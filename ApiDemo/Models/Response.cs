using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDemo.Models
{
    public class Response
    {
        public Guid Id { get; set; }
        public string Token { get; set; }
        public long ExpTime { get; set; }
    }
}
