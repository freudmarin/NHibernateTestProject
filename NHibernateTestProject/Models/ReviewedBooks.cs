using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NHibernateTestProject.Models
{
    public class ReviewedBooks
    {
        public virtual int reviewedbook_id { get; set; }
        public virtual int user_id { get; set; }

        public virtual int book_id { get; set; }

        public virtual int review_id { get; set; }

        public virtual int score { get; set; }

        public virtual Book Book { get; set; }

        public virtual Review  Review { get; set; }

        public virtual User User { get; set; }
    }
}
