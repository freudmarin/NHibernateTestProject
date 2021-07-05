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

        public   Book GetBookById(int id)
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
    }
}
