using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NHibernateTestProject.Models
{
    public class Book
    {
     public virtual int book_id { get; set; }
    public virtual string Title { get; set; }
        public virtual string Author { get; set; }

        public virtual  int category_id { get; set; }

        public virtual int price { get; set; }
        public virtual int stock { get; set; }
        public virtual ISet<Review> Reviews { get; set; }
        public virtual Category Category { get; set; }
    }
}
