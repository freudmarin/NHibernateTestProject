using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;

namespace NHibernateTestProject.NHibernate.Extensions
{
    public static class NHibernateExtensions
    {
        public static IServiceCollection AddNHibernate(this IServiceCollection services, string connectionString)
        {
            var mapper = new ModelMapper();
       //   mapper.AddMappings(typeof(NHibernateExtensions).Assembly.ExportedTypes);
            HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();

            var configuration = new Configuration();
            configuration.DataBaseIntegration(c =>
            {
                c.Dialect<MsSql2012Dialect>();
                c.ConnectionString = connectionString;
                c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                c.SchemaAction = SchemaAutoAction.Update; //very important
                c.LogFormattedSql = true;
                c.LogSqlInConsole = true;
            });
         
            configuration.AddMapping(domainMapping);
           configuration.AddXmlFile("C:\\Users\\marin.dulja\\source\\repos\\NHibernateTestProject\\NHibernateTestProject\\Models\\Book.hbm.xml");
            configuration.AddXmlFile("C:\\Users\\marin.dulja\\source\\repos\\NHibernateTestProject\\NHibernateTestProject\\Models\\Review.hbm.xml");
            configuration.AddXmlFile("C:\\Users\\marin.dulja\\source\\repos\\NHibernateTestProject\\NHibernateTestProject\\Models\\Category.hbm.xml");
            configuration.AddXmlFile("C:\\Users\\marin.dulja\\source\\repos\\NHibernateTestProject\\NHibernateTestProject\\Models\\User.hbm.xml");
            configuration.AddXmlFile("C:\\Users\\marin.dulja\\source\\repos\\NHibernateTestProject\\NHibernateTestProject\\Models\\ReviewedBooks.hbm.xml");
            configuration.AddXmlFile("C:\\Users\\marin.dulja\\source\\repos\\NHibernateTestProject\\NHibernateTestProject\\Models\\BuyedBooks.hbm.xml");

            var sessionFactory = configuration.BuildSessionFactory();
            //Same for every request
            services.AddSingleton(sessionFactory);
            //open session,create database connection
            //same within a request,different across different requests
            services.AddScoped(factory => sessionFactory.OpenSession());
            services.AddScoped<IMapperSession, NHibernateMapperSession>();
            ISession session = sessionFactory.OpenSession();
         









            return services;
        }
   
  
    }
}
