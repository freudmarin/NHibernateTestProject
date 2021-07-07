using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NHibernateTestProject.Models
{
    public class Category
    {
        [Key]
        public virtual int category_id { get; set; }
        [Required(ErrorMessage = "Category name is required")]
        public virtual string Name { get; set; }


    //    public virtual IList<Book> Books { get; set; }
    }
}
