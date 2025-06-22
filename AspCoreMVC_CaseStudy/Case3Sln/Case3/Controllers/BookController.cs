using Case3.Models;
using Microsoft.AspNetCore.Mvc;

namespace Case3.Controllers
{
    [Route("Book")]
    public class BookController : Controller
    {
        private static List<Book> books = new List<Book>();
        [Route("/")]
        [Route("Index")]
        public IActionResult Index()
        {
            return View(books);
        }
        [Route("Add",Name ="Add")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]

        public IActionResult Add(Book book)
        {
            if (books.Any(b => b.Isbn == book.Isbn))
            {
                ModelState.AddModelError("Isbn", "ISBN must be unique.");
            }

            if (ModelState.IsValid)
            {
                books.Add(book);
                return RedirectToAction("Index");
            }

            return View(book);
        }
    }
}
