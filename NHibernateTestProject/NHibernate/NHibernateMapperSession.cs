using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;
using NHibernateTestProject.Models;

namespace NHibernateTestProject.NHibernate
{
    public class NHibernateMapperSession : IMapperSession
    {
        private readonly ISession _session;
        private ITransaction _transaction;

        public NHibernateMapperSession(ISession session)
        {
            _session = session;


        }

        public IQueryable<Book> Books => _session.Query<Book>();
        public IQueryable<Category> Categories => _session.Query<Category>();

        public IQueryable<User> Users => _session.Query<User>();

        public void BeginTransaction()
        {
            _transaction = _session.BeginTransaction();
        }

        public async Task Commit()
        {
            await _transaction.CommitAsync();
        }

        public async Task Rollback()
        {
            await _transaction.RollbackAsync();
        }

        public void CloseTransaction()
        {
            if (_transaction != null)
            {
                _transaction.Dispose();
                _transaction = null;
            }
        }

        public async Task SaveBook(Book entity)
        {
            await _session.SaveOrUpdateAsync(entity);
        }

        public async Task Delete(Book entity)
        {
            await _session.DeleteAsync(entity);
        }

        public Book GetBookById(int id)
        {
            return _session.Query<Book>().Where(x => x.book_id == id).FirstOrDefault();
        }
        public async Task SaveCategory(Category entity)
        {
            await _session.SaveOrUpdateAsync(entity);
        }

        public async Task Delete(Category entity)
        {
            await _session.DeleteAsync(entity);
        }

        public Category GetCategoryById(int id)
        {
            return _session.Query<Category>().Where(x => x.category_id == id).FirstOrDefault();
        }
        public async Task SaveUser(User entity)
        {

            await _session.SaveOrUpdateAsync(entity);
        }
        public async Task SaveReview(Review entity)
        {

            await _session.SaveOrUpdateAsync(entity);
        }
        public async Task SaveReviewUser(ReviewedBooks entity)
        {
            await _session.SaveOrUpdateAsync(entity);
        }
        public async Task Delete(User entity)
        {
            await _session.DeleteAsync(entity);
        }

        public User GetUserById(int id)
        {
            return _session.Query<User>().Where(x => x.user_id == id).FirstOrDefault();
        }
       public  bool LoginUser(string username, string password)
        {
            return _session.Query<User>().Where(x => x.username == username && x.password == password).Any();
        }
        public bool isAdmin(string username, string password)
        {
            return _session.Query<User>().Where(x => x.username == username && x.password == password && x.role=="admin").Any();
        }
        public int GetUserID(string username, string password)
        {
            User user = _session.Query<User>().Where(x => x.username == username && x.password == password).First();
            return user.user_id;
        }
        public async Task SavePurchase(BuyedBooks entity)
        {
            await _session.SaveOrUpdateAsync(entity);
        }

    }
}
