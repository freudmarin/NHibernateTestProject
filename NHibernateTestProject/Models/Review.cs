using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NHibernateTestProject.Models
{
    public class Review
    {
        public virtual int review_id { get; set; }
        [Required(ErrorMessage = "Cateegory name is required")]
        [StringLength(200,MinimumLength=2)]
        public virtual string  review_text { get; set; }

        public virtual User User { get; set; }
    }
}