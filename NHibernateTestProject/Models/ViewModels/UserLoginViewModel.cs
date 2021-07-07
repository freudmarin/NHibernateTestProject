using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NHibernateTestProject.Models.ViewModels
{
    public class UserLoginViewModel
    {
        [Display(Name = "UserName")]
        [Required(ErrorMessage = "Username is required")]
        public string username { get; set; }
      
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
