using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using NHibernate;
using NHibernate.Linq;
using NHibernateTestProject.Models;
using NHibernateTestProject.NHibernate;
using System.Security.Claims;
using NHibernateTestProject.Models.ViewModels;

namespace NHibernateTestProject.Controllers
{
    public class BookController : Controller
    {

        private readonly IMapper _mapper;
        private readonly IMapperSession _session;

        public BookController(IMapperSession session)
        {
            _session = session;
        }
        //Basic
        //public IActionResult Index()
        //{
        //    var books = _session.Books.ToList();

        //    return View(books);
        //}

        //2nd way
        //public async Task<IActionResult> Index()
        //{
        //    try
        //    {
        //        _session.BeginTransaction();

        //        var books = await _session.Books

        //                                    .ToListAsync();
        //        await _session.Commit();

        //        var bookModels = _mapper.Map<BookMap>(books);

        //        return View(bookModels);
        //    }
        //    catch
        //    {
        //        // log error here
        //        await _session.Rollback();

        //        return View("Error");
        //    }
        //    finally
        //    {
        //        _session.CloseTransaction();
        //    }
        //}
        //  3rd way
        public async Task<IActionResult> DisplayCategories()
        {
            return RedirectToAction("Index", "Category");
        }
        [HttpPost]
        public async Task<IActionResult> Buy(int bookID)
        {
            TempData["book_id"] = bookID;

            BuyedBooks buyedbooks = new BuyedBooks();

            _session.BeginTransaction();

            buyedbooks.book_id = bookID;
            buyedbooks.quantity = 1;
            buyedbooks.user_id = (int)TempData["user_id"];
            User u = _session.Users.Where(u => u.user_id ==  buyedbooks.user_id).FirstOrDefault();

            buyedbooks.User = u;
         
            Book book = _session.Books.Where(c => c.book_id == buyedbooks.book_id).FirstOrDefault();

            buyedbooks.Book = book;
            //  buyedbooks.user_id = _session.GetUserID(model.username, model.password);
            _session.SavePurchase(buyedbooks);
            _session.Commit();
            return RedirectToAction("Index", "Book");
    
        }
        public async Task<IActionResult> Index()
        {
            string role = (string) TempData["role"];
            @ViewBag.role = role;
         
            _session.BeginTransaction();
           
          //  IQuery sqlQuery = _session.CreateSQLQuery("Select c.Name from dbo.category c inner join dbo.Book b on c.category_id = b.category_id group by c.Name having count(c.category_id) = (Select Max(Total) from CountByCategory)");
            try
            {
                var models = await _session.RunInTransaction(async () =>
                {
                    var books = await _session.Books

                                              .ToListAsync();



                    return books;//_mapper.Map<List<BookMap>>(books);
                });
    

                return View(models);
            }
            catch
            {
                // log error here

                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult AddReview()
        {



            return View(new UserReviewModel());
        }
        [HttpPost]
        public async Task<IActionResult> AddReview(UserReviewModel model)
        {
            ReviewedBooks reviewedBooks = new ReviewedBooks();
            Review review = new Review();
            int userID = (int)TempData["user_id"];
            int bookID = (int)TempData["book_id"];
            _session.BeginTransaction();
     
            
          
      
            User u = _session.Users.Where(u => u.user_id ==  userID).FirstOrDefault();
            Book book = _session.Books.Where(c => c.book_id == bookID).FirstOrDefault();
            review.User = u;
            review.review_text = model.review_text;
         
            
            reviewedBooks.Review = review;
           reviewedBooks.Book = book;
            reviewedBooks.User = u;
            reviewedBooks.score = model.score;
            reviewedBooks.user_id = userID;
            reviewedBooks.review_id = model.review_id;
          reviewedBooks.book_id = bookID;
            _session.SaveReview(review);
            _session.Commit();
            _session.SaveReviewUser(reviewedBooks);
            _session.Commit();
            return RedirectToAction("Index", "Book");
           
        }
        public ActionResult Create()
        {
                
          
            Book book = new Book();
            ViewData["CategoriesId"] = new SelectList(_session.Categories, "category_id", "Name", book.category_id);
            return View();
        }


        [HttpPost]
        public ActionResult Create(Book book)
        {

            

                ViewData["CategoriesId"] = new SelectList(_session.Categories, "category_id", "Name", book.category_id);
                try
                {
                    _session.BeginTransaction();
                    Category c = _session.Categories.Where(c => c.category_id == book.category_id).FirstOrDefault();

                    book.Category = c;


                    _session.SaveBook(book);
                    _session.Commit();


                    //       _session.CloseTransaction();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
           
        
            //else
            //    return View();
            //else
            //{
            //    string messages = string.Join("; ", ModelState.Values
            //                            .SelectMany(x => x.Errors)
            //                            .Select(x => x.ErrorMessage));
         
            //    return View(messages);
            //}

        
        public ActionResult Edit(int id)
        {
            var book = _session.GetBookById(id);
           
            ViewData["CategoriesId"] = new SelectList(_session.Categories, "category_id", "Name", book.category_id);

         
            return View(book);
        }




        [HttpPost]
        public ActionResult Edit(int id, Book book)
        {
            if (ModelState.IsValid)
            {
                ViewData["CategoriesId"] = new SelectList(_session.Categories, "category_id", "Name", book.category_id);
                try
                {

                    _session.BeginTransaction();
                    var booktoUpdate = _session.GetBookById(id); //Important
                    Category c = _session.Categories.Where(c => c.category_id == book.category_id).FirstOrDefault();
                    booktoUpdate.Category = c;

                    booktoUpdate.Title = book.Title;
                    booktoUpdate.Author = book.Author;


                    _session.SaveBook(booktoUpdate);
                    _session.Commit();

                    //    _session.CloseTransaction();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            else
            return View(book);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {
                _session.BeginTransaction();

                var book = _session.GetBookById(id);
                _session.Delete(book);
                _session.Commit();
                _session.CloseTransaction();

                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                return View();
            }

        }
    }
}
