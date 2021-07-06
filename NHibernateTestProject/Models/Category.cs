using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NHibernateTestProject.Models
{
    public class Category
    {
        public virtual int category_id { get; set; }
        public virtual string Name { get; set; }


    //    public virtual IList<Book> Books { get; set; }
    }
}
