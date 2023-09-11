using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using UdemyWebApplication.Models;
using UdemyWebApplication.Utility;

namespace UdemyWebApplication.Controllers
{
    
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IBookTypeRepository _bookTypeRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(IBookRepository context, IBookTypeRepository bookTypeRepository, IWebHostEnvironment webHostEnvironment)
        {
            _bookRepository = context;
            _bookTypeRepository = bookTypeRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        [Authorize(Roles = "Admin,Student")]
        public IActionResult Index()
        {
            List<Book> objBookList = _bookRepository.GetAll(includeProps: "BookType").ToList();
            return View(objBookList);
        }
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult AddUpdateNewBook(int? id)
        {
            IEnumerable<SelectListItem> BookTypeList = _bookTypeRepository.GetAll()
              .Select(k => new SelectListItem
              {
                  Text = k.Name,
                  Value = k.ID.ToString()
              });
            ViewBag.BookTypeList = BookTypeList; //Controller'dan View'a veri aktarmak için ViewBag kullanılır.

            if (id == null || id == 0)
            {
                // Add
                return View();
            }
            else
            {
                // Update
                Book? bookVT = _bookRepository.Get(u => u.ID == id);
                if (bookVT == null)
                {
                    return NotFound();
                }
                return View(bookVT);
            }
        }
        [Authorize(Roles = UserRoles.Role_Admin)]
        [HttpPost]
        public IActionResult AddUpdateNewBook(Book book, IFormFile? file)
        {
            var errors = ModelState.Values.SelectMany(x => x.Errors); // ModelState hatalarının ne oldugunu görüntülemek için (breakpoint koy hataya bak)

            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath; //wwwroot'un pathini verir 
                string bookPath = Path.Combine(wwwRootPath, @"img");

                if (file != null)
                {
                    using (var fileStream = new FileStream(Path.Combine(bookPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    book.ImageURL = @"\img\" + file.FileName;
                }

                if (book.ID == 0)
                {
                    _bookRepository.Add(book);
                    TempData["Successful"] = "New book has been added";
                }
                else
                {
                    _bookRepository.Update(book);
                    TempData["Successful"] = "The book has been updated";
                }

                _bookRepository.Save();
                return RedirectToAction("Index", "Book");
            }
            return View();
        }
     

        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult DeleteBook(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Book? bookVT = _bookRepository.Get(u => u.ID == id);
            if (bookVT == null)
            {
                return NotFound();
            }
            return View(bookVT);
        }


        [Authorize(Roles = UserRoles.Role_Admin)]
        [HttpPost, ActionName("DeleteBook")]
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
            return RedirectToAction("Index", "Book");
        }
    }
}
