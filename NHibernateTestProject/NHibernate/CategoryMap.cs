using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernateTestProject.Models;

namespace NHibernateTestProject.NHibernate
{
    public class CategoryMap : ClassMapping<Category>
    {
        public CategoryMap()
        {
            Table("category");
            Lazy(true);
            Id(x => x.category_id, x =>
            {
                x.Column("category_id");
                x.Generator(Generators.Identity);
                x.Type(NHibernateUtil.Int32);


            });

            Property(b => b.Name, x =>
            {
                x.Length(50);
                x.Type(NHibernateUtil.StringClob);
                x.NotNullable(true);
            });
            //This was not needed
            //Bag(x => x.Books, bag => {
            //    bag.Inverse(true); // Is collection inverse?
            //    bag.Cascade(Cascade.DeleteOrphans); //set cascade strategy
            //    bag.Key(k => k.Column(col => col.Name("CategroyId"))); //foreign key in Category table
            //}, a => a.OneToMany());
            //    this.List(x => x.Books, x =>

            //    {

            //        x.Key(y =>

            //        {

            //            y.Column("category_id");

            //            y.NotNullable(true);

            //        });



            //        x.Lazy(CollectionLazy.Lazy);

            //        x.Cascade(Cascade.All | Cascade.DeleteOrphans);

            //        x.Inverse(true);

            //    }, x =>

            //    {

            //        x.OneToMany();

            //    });

            //}
        }
    }
}
