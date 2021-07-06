using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NHibernate.Linq;
using NHibernateTestProject.Models;
using NHibernateTestProject.NHibernate;

namespace NHibernateTestProject.Controllers
{
    public class CategoryController : Controller
    {
   
        private readonly IMapper _mapper;
        private readonly IMapperSession _session;

        public CategoryController(IMapperSession session)
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
                    var categories = await _session.Categories

                                              .ToListAsync();
                    return categories;//_mapper.Map<List<BookMap>>(books);
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
            return View();
        }


        [HttpPost]
        public ActionResult Create(Category category)
        {
            try
            {

                _session.BeginTransaction();
        
                _session.SaveCategory(category);
                _session.Commit();



                  return RedirectToAction("Index");
            }
            catch (Exception exception)
            {
                return View();
            }
        }
        public ActionResult Edit(int id)
        {

            var category = _session.GetCategoryById(id);
            return View(category);
        }




        [HttpPost]
        public ActionResult Edit(int id, Category category)
        {
            try
            {

                _session.BeginTransaction();
                var categorytoUpdate = _session.GetCategoryById(id); //Important


                categorytoUpdate.Name = category.Name;


                _session.SaveCategory(categorytoUpdate);
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

                var category = _session.GetCategoryById(id);
                _session.Delete(category);
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

    