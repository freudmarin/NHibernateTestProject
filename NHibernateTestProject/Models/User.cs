using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NHibernateTestProject.Models
{
    public class User
    {
        public virtual int user_id { get; set; }
        public virtual string username { get; set; }
        public virtual string fullname { get; set; }

        public virtual string email { get; set; }
        public virtual string bankCardNumber { get; set; }
        public virtual string password { get; set; }
        public virtual ISet<Review> Reviews { get; set; }

        public virtual ISet<Book> Books { get; set; }

    }
}
