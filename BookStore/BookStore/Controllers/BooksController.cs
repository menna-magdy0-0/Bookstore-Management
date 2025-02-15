using BookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace BookStore.Controllers
{
    public class BooksController : Controller
    {
        StoreContext context = new StoreContext();
        //remote attribute using Ajax call
        public IActionResult IsTitleAvailable(string Title)
        {
            var book = context.Books.FirstOrDefault(x => x.Title == Title);
            if (book == null)
            {
                return Json(true);
            }
            return Json($"Book Title {Title} is already taken");
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<Book> books = context.Books.ToList();
            return View("Index", books);
        }
        public IActionResult Details(int id)
        {
            Book BookModel = context.Books.FirstOrDefault(x => x.Id == id);
            if (BookModel == null)
            {
                return NotFound();
            }
            return View("Details",BookModel);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View("Create");
        }
        [HttpPost]
        public IActionResult SaveCreate(Book bookFromRequest)
        {
            if (ModelState.IsValid)
            {
                context.Books.Add(bookFromRequest);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View("Create",bookFromRequest);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Book BookModel = context.Books.FirstOrDefault(e => e.Id == id);
            if (BookModel == null)
            {
                return NotFound();
            }
            return View("Edit",BookModel);
        }
        [HttpPost]
        public IActionResult SaveEdit(int id,Book bookFromRequest)
        {
            if (ModelState.IsValid)
            {
                Book BookModel = context.Books.FirstOrDefault(e => e.Id == id);
                BookModel.Title = bookFromRequest.Title;
                BookModel.Author = bookFromRequest.Author;
                BookModel.Price = bookFromRequest.Price;
                BookModel.Genre = bookFromRequest.Genre;
                BookModel.PublishedDate = bookFromRequest.PublishedDate;
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            

            return View("Edit",bookFromRequest);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            Book BookModel = context.Books.FirstOrDefault(e => e.Id == id);
            if (BookModel==null)
            {
                return NotFound();
            }
            return View("Delete", BookModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Book BookModel = context.Books.FirstOrDefault(e => e.Id == id);
            if (BookModel != null)
            {
                context.Books.Remove(BookModel);
                context.SaveChanges();
            }
            
            return RedirectToAction("Index");
        }

    }
}
