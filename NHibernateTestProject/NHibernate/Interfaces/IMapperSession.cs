using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernateTestProject.Models;

namespace NHibernateTestProject.NHibernate
{
    public interface IMapperSession
    {
        void BeginTransaction();
        Task Commit();
        Task Rollback();
        void CloseTransaction();
        Task SaveBook(Book entity);
        Task Delete(Book entity);
        Book GetBookById(int id);



        Task SaveCategory(Category entity);
        Task Delete(Category entity);
        Category GetCategoryById(int id);

        IQueryable<Book> Books { get; }
            IQueryable<Category> Categories { get; }

        public async Task<T> RunInTransaction<T>(Func<Task<T>> func)
        {
            try
            {
                BeginTransaction();

                var retval = await func();

                await Commit();

                return retval;
            }
            catch
            {
                await Rollback();

                throw;
            }
            finally
            {
                CloseTransaction();
            }
        }
    }
    }

