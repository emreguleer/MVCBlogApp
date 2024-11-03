using Microsoft.AspNetCore.Mvc;
using MVCBlogApp.Web.Helpers;
using MVCBlogApp.Web.Models;
using MVCBlogApp.Web.Repositories;

namespace MVCBlogApp.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly CategoryRepository _categoryRepository;

        public CategoryController(CategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IActionResult Index()
        {
            try
            {
                var categories = _categoryRepository.GetAllCategoriesCount();
                return View(categories);
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
                var category = _categoryRepository.GetCategoryById(id);
                return View(category);
            }
            catch (Exception ex)
            {
                LogHelper.LogToSqlServer(ex.Message);
                return RedirectToAction("Error404", "Home");
            }
        }

        [HttpPost]
        public IActionResult Update(Category category)
        {
            try
            {
                _categoryRepository.Update(category);
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
                var category = _categoryRepository.GetCategoryById(id);
                _categoryRepository.Delete(category);
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
