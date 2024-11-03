using Microsoft.AspNetCore.Mvc;
using MVCBlogApp.Web.Helpers;
using MVCBlogApp.Web.Repositories;
using MVCBlogApp.Web.ViewModels;

namespace MVCBlogApp.Web.Controllers
{
    public class ArticleController : Controller
    {
        private readonly ArticleRepository _articleRepository;

        public ArticleController(ArticleRepository articleRepository)
        {
            _articleRepository = articleRepository;
        }

        public IActionResult Index()
        {
            try
            {
                var articleVMs = _articleRepository.GetAllArticles();
                return View(articleVMs);
            }
            catch (Exception ex)
            {
                LogHelper.LogToSqlServer(ex.Message);
                return RedirectToAction("Error404", "Home");
            }
        }

        public IActionResult ArticleInfo(int id)
        {
            try
            {
                var articleVM = _articleRepository.GetArticleById(id);
                return View(articleVM);
            }
            catch (Exception ex)
            {
                LogHelper.LogToSqlServer(ex.Message);
                return RedirectToAction("Error404", "Home");
            }
        }

        public IActionResult AuthorArticles(int id)
        {
            try
            {
                var articleVMs = _articleRepository.GetArticlesByAuthorId(id);
                return View(articleVMs);
            }
            catch (Exception ex)
            {
                LogHelper.LogToSqlServer(ex.Message);
                return RedirectToAction("Error404", "Home");
            }
        }

        public IActionResult CategoryArticles(int id)
        {
            try
            {
                var articleVMs = _articleRepository.GetArticlesByCategoryId(id);
                return View(articleVMs);
            }
            catch (Exception ex)
            {
                LogHelper.LogToSqlServer(ex.Message);
                return RedirectToAction("Error404", "Home");
            }
        }


    }
}
