using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;
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

        Task SaveUser(User entity);
        Task Delete(User entity);
        User GetUserById(int id);
      
        Task SaveCategory(Category entity);
        Task Delete(Category entity);
        Category GetCategoryById(int id);

        IQueryable<Book> Books { get; }
            IQueryable<Category> Categories { get; }
        IQueryable<User> Users { get; }
        Task SavePurchase(BuyedBooks entity);
        bool LoginUser(string username, string password);
        bool isAdmin(string username, string password);
        int GetUserID(string username, string password);
        Task SaveReview(Review entity);
        Task SaveReviewUser(ReviewedBooks entity);
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

