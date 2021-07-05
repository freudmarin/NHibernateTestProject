using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernateTestProject.Models;

namespace NHibernateTestProject
{
    public class BookMap : ClassMapping<Book> //Class Map is used at Fluent NHibernate
    {
        public BookMap()
        {
            this.Table("book");

            this.Lazy(true);


            Id(x => x.book_id, x =>
            {
                x.Column("book_id");
                x.Generator(Generators.Identity);
                x.Type(NHibernateUtil.Int32);
            

            });

            Property(b => b.Title, x =>
            {
                x.Length(50);
                x.Type(NHibernateUtil.StringClob);
                x.NotNullable(true);
            });


            Property(b => b.Author, x =>
                {
                    x.Length(50);
                    x.Type(NHibernateUtil.StringClob);
                    x.NotNullable(true);
                   
                });
      
            ManyToOne(x => x.Category, x => {
              x.Column("category_id");
              
               // x.NotNullable(true);
                x.Lazy(LazyRelation.NoProxy);
            });
           
        }
    }
    }

    

