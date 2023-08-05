using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using UdemyWebApplication.Models;
using UdemyWebApplication.Utility;

namespace UdemyWebApplication.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class BookTypeController : Controller
    {   
        private readonly IBookTypeRepository _bookTypeRepository;
        public BookTypeController(IBookTypeRepository context)
        {
            _bookTypeRepository = context;
        }
        public IActionResult Index()
        {
            List<BookType> objBookTypeList = _bookTypeRepository.GetAll().ToList();
            return View(objBookTypeList);
        }
        public IActionResult AddNewBookType()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddNewBookType(BookType bookType) //parametre gönderilecek olan nesne yani kitap türü
        {
            if (ModelState.IsValid)
            {
                _bookTypeRepository.Add(bookType); //dependency injection ile oluşturduğumuz global nesne(_applicationDBContext)
                                                   //ile kitap türlerine erişip entity framework komutlarından Add ile oluşturduğumuz
                                                   //BookType türündeki bookType nesnemizi veritabanına ekliyoruz.
                TempData["Successful"] = "New book type has been added";
                _bookTypeRepository.Save();           //SaveChanges methodu kullanılmazsa bigiler veritabanına eklenmez
                return RedirectToAction("Index", "BookType");
            }
            return View();
        }
        public IActionResult UpdateBookType(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            BookType? bookTypeVT = _bookTypeRepository.Get(u => u.ID == id);
            if (bookTypeVT == null)
            {
                return NotFound();
            }
            return View(bookTypeVT);
        }
        [HttpPost]
        public IActionResult UpdateBookType(BookType bookType)
        {
            if (ModelState.IsValid)
            {
                _bookTypeRepository.Update(bookType);
                _bookTypeRepository.Save();
                TempData["Successful"] = "Book type has been updated";
                return RedirectToAction("Index", "BookType");
            }
            return View();
        }
        public IActionResult DeleteBookType(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            BookType? bookTypeVT = _bookTypeRepository.Get(u => u.ID == id);
            if (bookTypeVT == null)
            {
                return NotFound();
            }
            return View(bookTypeVT);
        }
        [HttpPost, ActionName("DeleteBookType")]
        public IActionResult Delete(int? id)
        {
            BookType? bookType = _bookTypeRepository.Get(u => u.ID == id);
            if (bookType == null)
            {
                return NotFound();
            }
            _bookTypeRepository.Remove(bookType);
            _bookTypeRepository.Save();
            TempData["Successful"] = "Book type has been removed";
            return RedirectToAction("Index", "BookType");
        }
    }
}
