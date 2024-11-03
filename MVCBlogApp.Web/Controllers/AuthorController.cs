using Microsoft.AspNetCore.Mvc;
using MVCBlogApp.Web.Helpers;
using MVCBlogApp.Web.Models;
using MVCBlogApp.Web.Repositories;

namespace MVCBlogApp.Web.Controllers
{
    public class AuthorController : Controller
    {
        private readonly AuthorRepository _authorRepository;

        public AuthorController(AuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public IActionResult Index()
        {
            try
            {
                var author = _authorRepository.GetAllAuthorsCount();
                return View(author);
            }
            catch (Exception ex)
            {
                LogHelper.LogToSqlServer(ex.Message);
                return RedirectToAction("Error404", "Home");
            }
        }

        public IActionResult Update(int id)
        {
            try
            {
                var author = _authorRepository.GetAuthorById(id);
                return View(author);
            }
            catch (Exception ex)
            {
                LogHelper.LogToSqlServer(ex.Message);
                return RedirectToAction("Error404", "Home");
            }
        }

        [HttpPost]
        public IActionResult Update(Author author)
        {
            try
            {
                _authorRepository.Update(author);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogHelper.LogToSqlServer(ex.Message);
                return RedirectToAction("Error404", "Home");
            }
        }

        public IActionResult Delete(int id)
        {
            try
            {
                var author = _authorRepository.GetAuthorById(id);
                _authorRepository.Delete(author);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                LogHelper.LogToSqlServer(ex.Message);
                return RedirectToAction("Error404", "Home");
            }
        }

    }
}
