using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiDemo.Models;

namespace ApiDemo.Interfaces
{
   public interface IJwtTokenManager
    {
        Response Authenticate(string userName, string password);
    }
}
