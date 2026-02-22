using Library.Domain.Entity;
using Library.Domain.Enum;
using Library.Domain.Exceptions;
using Library.Infrastructure.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Library.AppWeb.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;

        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IActionResult> Index(string? searchTerm, Genre? genre)
        {
            var books = await _bookRepository.SearchAsync(searchTerm, genre);
            return View(books);
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book == null)
            {
                throw new BookNotFoundException(id);
            }

            return View(book);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if(!ModelState.IsValid)
            {
                return View(book);
            }

            try
            {
                await _bookRepository.AddAsync(book);
                TempData["Success"] = "Book added successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (DuplicateIsbnException ex)
            {
                ModelState.AddModelError("ISBN", ex.Message);
                return View(book);
            }
            catch (InvalidPublicationYearException ex)
            {
                ModelState.AddModelError("PublicationYear", ex.Message);
                return View(book);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Book book)
        {
            if (!ModelState.IsValid)
            {
                return View(book);
            }

            try
            {
                await _bookRepository.UpdateAsync(book);
                TempData["Success"] = "Book updated successfully.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(book);
            }
        }

        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _bookRepository.DeleteAsync(id);
                TempData["Success"] = "Book deleted successfully.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}