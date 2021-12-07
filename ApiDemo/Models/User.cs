using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ApiDemo.Model
{

    public class User
    {
        [Key]
        public Guid Id { get; set; }

       
        public String ImageName { get; set; }

        [NotMapped]
        public IFormFile ProfilePicture { get; set; }

        [NotMapped]
        public string ImageSrc { get; set; }

        [Required]
        public String Name { get; set; }

        [Required]
        public String Mobile { get; set; }

        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }

        [Required]
        public String Location { get; set; }

        [Required]
        public String Username { get; set; }


        [Required]
        public String EmailId { get; set; }

        [Required]
        [MinLength(8,ErrorMessage ="Min length should be 8")]
        public String Password { get; set; }

       

    


      


    }
}
