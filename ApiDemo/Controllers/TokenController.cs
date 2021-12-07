using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiDemo.Interfaces;
using ApiDemo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IJwtTokenManager _tokenManager;

        public TokenController(IJwtTokenManager jwtTokenManager)
        {
            _tokenManager = jwtTokenManager;
        
        }
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public ActionResult<Response> Authenticate(UserCredentials credential)
        {
            //[FromBody]

            //
            //var user = users.Select(user=>user.EmailId== credential.UserName&&user.Password==credential.Password).SingleOrDefault();
            Response response = new Response();
            response = _tokenManager.Authenticate(credential.UserName, credential.Password);
            if (null==response)
                return Unauthorized();
            
           
            return Ok(response);
        }

        
      
    }
}
