using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NHibernateTestProject.Models
{
    public class Review
    {
        public virtual int review_id { get; set; }

        public virtual string  review_text { get; set; }

      
    }
}