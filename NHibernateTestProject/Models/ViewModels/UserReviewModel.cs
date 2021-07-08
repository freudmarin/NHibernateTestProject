using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NHibernateTestProject.Models.ViewModels
{
    public class UserReviewModel
    {

        public virtual int review_id { get; set; }
        [Required(ErrorMessage = "Review Text should not be empty ")]
        public virtual string review_text { get; set; }
        [Required(ErrorMessage = "Score is required")]
        public virtual int  score { get; set; }
  
    }
}
