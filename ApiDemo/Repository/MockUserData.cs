using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiDemo.Repository
{
    public class MockUserData
    {
        public static Dictionary<string, string> Users = new Dictionary<string, string>
        {
            {"sampleuser","password" },
            {"Demouser","password"}
        };
    }
}
