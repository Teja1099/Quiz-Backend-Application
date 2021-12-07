using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ApiDemo.Interfaces;
using ApiDemo.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiDemo.Controllers
{
   
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _user;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(IUser user, IWebHostEnvironment webHostEnvironment)
        {
            _user = user;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize]
        [HttpGet]
        [Route("get")]
        public IActionResult GetUsers()
        {
            var users = _user.GetUsers();
            users = users.Select(x => new User()
            {
                Id = x.Id,
                Name = x.Name,
                EmailId = x.EmailId,
                Mobile=x.Mobile,
                Username=x.Username,
                Location=x.Location,
                DOB=x.DOB.Date,
                Password = x.Password,
                ImageName = x.ImageName,
                ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, x.ImageName)
            }).ToList();

            return Ok(users);
        }

        [Authorize]
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetUserById(Guid id)
        {
            var user = _user.GetUserById(id);
            if (null!=user)
            {
                user.DOB = user.DOB.Date;
                user.ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, user.ImageName);
                return Ok(user);
            }
            return NotFound("This id is not found");
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("post")]
        public async Task<ActionResult<User>> AddUser([FromForm]User user)
        {
            user.ImageName = await SaveImage(user.ProfilePicture);
            //user.ImageName = "123";
            var temp = _user.AddUser(user);

            return StatusCode(210);
        }

        [Authorize]
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteUser(Guid id)
        {
            User user = _user.GetUserById(id);
            if(null!=user)
            {
                DeleteImage(user.ImageName);
                _user.DeleteUser(user);
                return Ok("Successful");
            }
            return NotFound("User doesn't exsit");
        }
        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new string(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", imageName);
            using(var fileStream = new FileStream(imagePath,FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }
    }
}
