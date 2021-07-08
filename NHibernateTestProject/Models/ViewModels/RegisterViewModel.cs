using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NHibernateTestProject.Models.ViewModels
{
    public class RegisterViewModel
    {
        public virtual int user_id { get; set; }
        [Required(ErrorMessage = "UserName is required")]
        public virtual string username { get; set; }
        [Required(ErrorMessage = "FullName is required")]
        public virtual string fullname { get; set; }
        [Required(ErrorMessage = "Email is required")]

        public virtual string email { get; set; }
        [Required(ErrorMessage = "Bank Number is required")]
        public virtual string bankCardNumber { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Password is required")]
        public virtual string password { get; set; }

  
    }
}
