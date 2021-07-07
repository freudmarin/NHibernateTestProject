using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NHibernateTestProject.Models
{
    public class Book
    {
        [Key]
     public virtual int book_id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public virtual string Title { get; set; }
        [Required(ErrorMessage = "Author is required")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Use letters only please")]
        public virtual string Author { get; set; }
        [Required(ErrorMessage = "Plaese Choose category")]
        [DisplayName("Category Name")]
        public virtual  int category_id { get; set; }
        [Required(ErrorMessage = "Price is required")]
        public virtual int price { get; set; }
        [Required(ErrorMessage = "Stock is required")]
        public virtual int stock { get; set; }
        public virtual ISet<Review> Reviews { get; set; }
        public virtual Category Category { get; set; }
    }
}
