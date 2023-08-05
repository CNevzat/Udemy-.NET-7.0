using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Data;
using UdemyWebApplication.Models;
using UdemyWebApplication.Utility;

namespace UdemyWebApplication.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class RentController : Controller
    {
        private readonly IRentRepository _rentRepository;
        private readonly IBookRepository _bookRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;

        public RentController(IRentRepository context, IBookRepository bookRepository, IWebHostEnvironment webHostEnvironment)
        {
            _rentRepository = context;
            _bookRepository = bookRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Rent> objRentList = _rentRepository.GetAll(includeProps: "Book").ToList();
            return View(objRentList);
        }

        public IActionResult AddUpdateRent(int? id)
        {
            IEnumerable<SelectListItem> BookList = _bookRepository.GetAll()
              .Select(k => new SelectListItem
              {
                  Text = k.BookName,
                  Value = k.ID.ToString()
              });
            ViewBag.BookList = BookList; //Controller'dan View'a veri aktarmak için ViewBag kullanılır.

            if (id == null || id == 0)
            {
                // Add
                return View();
            }
            else
            {
                // Update
                Rent? rentVT = _rentRepository.Get(u => u.ID == id);
                if (rentVT == null)
                {
                    return NotFound();
                }
                return View(rentVT);
            }
        }

        [HttpPost]
        public IActionResult AddUpdateRent(Rent rent)
        {
            if (ModelState.IsValid)
            {

                if (rent.ID == 0)
                {
                    _rentRepository.Add(rent);
                    TempData["Successful"] = "Rent is successful";
                }
                else
                {
                    _rentRepository.Update(rent);
                    TempData["Successful"] = "Rent is updated";
                }

                _rentRepository.Save();
                return RedirectToAction("Index", "Rent");
            }
            return View();
        }

        public IActionResult DeleteRent(int? id)
        {
            IEnumerable<SelectListItem> BookList = _bookRepository.GetAll()
             .Select(k => new SelectListItem
             {
                 Text = k.BookName,
                 Value = k.ID.ToString()
             });
            ViewBag.BookList = BookList; //Controller'dan View'a veri aktarmak için ViewBag kullanılır.

            if (id == null || id == 0)
            {
                return NotFound();
            }
            Rent? rentVT = _rentRepository.Get(u => u.ID == id);
            if (rentVT == null)
            {
                return NotFound();
            }
            return View(rentVT);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            Rent? rent = _rentRepository.Get(u => u.ID == id);
            if (rent == null)
            {
                return NotFound();
            }
            _rentRepository.Remove(rent);
            _rentRepository.Save();
            TempData["Successful"] = "Rent has been removed";
            return RedirectToAction("Index", "Rent");
        }
    }
}
