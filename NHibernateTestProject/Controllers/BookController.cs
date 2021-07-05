using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using NHibernate.Linq;
using NHibernateTestProject.Models;
using NHibernateTestProject.NHibernate;

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

        public async Task<IActionResult> Index()
        {
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
         
            _session.BeginTransaction();
            Category c = _session.Categories.Where(c => c.category_id == book.category_id).FirstOrDefault();
            book.Category = c;
                  _session.SaveBook(book);
                _session.Commit();
       


               return RedirectToAction("Index");
             
         
        }
        public ActionResult Edit(int id)
        {
            var book = _session.GetBookById(id);
            ViewData["CategoriesId"] = new SelectList(_session.Categories, "category_id", "Name", book.category_id);

         
            return View(book);
        }




        [HttpPost]
        public ActionResult Edit(int id, Book book)
        {
            ViewData["CategoriesId"] = new SelectList(_session.Categories, "category_id", "Name", book.category_id);
            try
            {

                _session.BeginTransaction();
                var booktoUpdate = _session.GetBookById(id); //Important


               booktoUpdate.Title = book.Title;
                booktoUpdate.Author = book.Author;
                booktoUpdate.category_id= book.category_id;

                _session.SaveBook(booktoUpdate);
                _session.Commit();


                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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


                return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                return View();
            }

        }
    }
}
