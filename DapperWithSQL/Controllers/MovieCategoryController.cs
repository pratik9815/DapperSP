using Dapper;
using DapperWithSQL.DataContext;
using DapperWithSQL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DapperWithSQL.Controllers
{
    public class MovieCategoryController : Controller
    {
        private DapperContext _context;

        public MovieCategoryController(DapperContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<MovieCategory> categories;
            using (var conn = _context.DbConnection())
            {
                string sql = "SELECT *FROM MovieCategories WITH(NOLOCK)";
                categories = conn.Query<MovieCategory>(sql);
            }
            return View(categories);
        }

        public IActionResult AddMovieCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddMovieCategory(MovieCategory category)
        {
            if (ModelState.IsValid)
            {
                using (var conn = _context.DbConnection())
                {
                    string sql = "INSERT INTO MovieCategories(Genre,[Description]) values('" + category.Genre + "','" + category.Description + "')";
                    conn.Execute(sql);
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
    }
}
