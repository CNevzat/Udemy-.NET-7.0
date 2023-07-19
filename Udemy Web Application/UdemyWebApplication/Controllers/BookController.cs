using Microsoft.AspNetCore.Mvc;
using UdemyWebApplication.Models;
using UdemyWebApplication.Utility;

namespace UdemyWebApplication.Controllers
{
	public class BookController : Controller
	{
		private readonly IBookRepository _bookRepository;
		public BookController(IBookRepository context)
		{
            _bookRepository = context;
		}
		public IActionResult Index()
		{
			List<Book> objBookList = _bookRepository.GetAll().ToList();
			return View(objBookList);
		}
		public IActionResult AddNewBook()
		{
			return View();
		}
		[HttpPost]
        public IActionResult AddNewBook(Book book)
        {
			if (ModelState.IsValid)
			{
                _bookRepository.Add(book); 
                TempData["Successful"] = "New book has been added";
                _bookRepository.Save();       
				return RedirectToAction("Index", "Book");
			}
			return View();
        }
        public IActionResult UpdateBook(int? id)
        {
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Book? bookVT = _bookRepository.Get(u=> u.ID == id);
			if (bookVT == null)
			{
				return NotFound();
			}
            return View(bookVT);
        }
        [HttpPost]
        public IActionResult UpdateBook(Book book) 
        {
            if (ModelState.IsValid)
            {
                _bookRepository.Update(book);
                _bookRepository.Save();
                TempData["Successful"] = "Book has been updated";
                return RedirectToAction("Index", "Book");
            }
            return View();
        }
        public IActionResult DeleteBook(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Book? bookVT = _bookRepository.Get(u=> u.ID == id);
            if (bookVT == null)
            {
                return NotFound();
            }
            return View(bookVT);
        }
        [HttpPost , ActionName("DeleteBook")]
        public IActionResult Delete(int? id) 
        {
            Book? book = _bookRepository.Get(u => u.ID == id);
            if (book == null) 
            {
                return NotFound();
            }
            _bookRepository.Remove(book);
            _bookRepository.Save();
            TempData["Successful"] = "Book has been removed";
            return RedirectToAction("Index","Book");
        }
    }
}
