using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NHibernateTestProject.Models
{
    public class BuyedBooks
    {
        public virtual int buyedbook_id { get; set; }
        public virtual int user_id { get; set; }

        public virtual int book_id { get; set; }

        public virtual int quantity { get; set; }


        public virtual User User { get; set; }

        public virtual Book Book { get; set; }
    }
}
