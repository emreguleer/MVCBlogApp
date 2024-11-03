using Microsoft.AspNetCore.Mvc;
using MVCBlogApp.Web.Repositories;
using MVCBlogApp.Web.Models;
using MVCBlogApp.Web.ViewModels;
using MVCBlogApp.Web.Helpers;

namespace MVCBlogApp.Web.Controllers
{
    public class EditorController : Controller
    {
        private readonly ArticleRepository _articleRepository;
        private readonly AuthorRepository _authorRepository;
        private readonly CategoryRepository _categoryRepository;

        public EditorController(ArticleRepository articleRepository, AuthorRepository authorRepository, CategoryRepository categoryRepository)
        {
            _articleRepository = articleRepository;
            _authorRepository = authorRepository;
            _categoryRepository = categoryRepository;
        }

        public IActionResult Add()
        {
            try
            {
                var auditors = _authorRepository.GetAllAuthors();
                var categories = _categoryRepository.GetAllCategories();
                ViewData["auditors"] = auditors;
                ViewData["categories"] = categories;
                return View();
            }
            catch (Exception ex)
            {
                LogHelper.LogToSqlServer(ex.Message);
                return RedirectToAction("Error404", "Home");
            }
        }

        [HttpPost]
        public IActionResult Add(Article article)
        {
            try
            {
                _articleRepository.Add(article);
                return RedirectToAction("Add");
            }
            catch (Exception ex)
            {
                LogHelper.LogToSqlServer(ex.Message);
                return RedirectToAction("Error404", "Home");
            }
        }

        public IActionResult AddAuthor()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                LogHelper.LogToSqlServer(ex.Message);
                return RedirectToAction("Error404", "Home");
            }
        }

        [HttpPost]
        public IActionResult AddAuthor(Author author)
        {
            try
            {
                _authorRepository.Add(author);
                return RedirectToAction("Add");
            }
            catch (Exception ex)
            {
                LogHelper.LogToSqlServer(ex.Message);
                return RedirectToAction("Error404", "Home");
            }
        }

        public IActionResult AddCategory()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                LogHelper.LogToSqlServer(ex.Message);
                return RedirectToAction("Error404", "Home");
            }
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            try
            {
                _categoryRepository.Add(category);
                return RedirectToAction("Add");
            }
            catch (Exception ex)
            {
                LogHelper.LogToSqlServer(ex.Message);
                return RedirectToAction("Error404", "Home");
            }
        }

        public IActionResult UpdateArticle(int id)
        {
            try
            {
                var articleVM = _articleRepository.GetArticleById(id);
                var auditors = _authorRepository.GetAllAuthors();
                var categories = _categoryRepository.GetAllCategories();
                ViewData["auditors"] = auditors;
                ViewData["categories"] = categories;
                return View(articleVM);
            }
            catch (Exception ex)
            {
                LogHelper.LogToSqlServer(ex.Message);
                return RedirectToAction("Error404", "Home");
            }
        }

        [HttpPost]
        public IActionResult UpdateArticle(ArticleVM articleVM)
        {
            try
            {
                _articleRepository.Update(articleVM);
                return RedirectToAction("Index", "Article");
            }
            catch (Exception ex)
            {
                LogHelper.LogToSqlServer(ex.Message);
                return RedirectToAction("Error404", "Home");
            }
        }

        public IActionResult DeleteArticle(int id)
        {
            try
            {
                var article = _articleRepository.GetArticleById(id);
                _articleRepository.Delete(article);
                return RedirectToAction("Index", "Article");
            }
            catch (Exception ex)
            {
                LogHelper.LogToSqlServer(ex.Message);
                return RedirectToAction("Error404", "Home");
            }
        }

    }
}
