using Dapper;
using DapperWithSQL.DataContext;
using DapperWithSQL.Models;
using DapperWithSQL.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DapperWithSQL.Controllers
{
    public class MovieCategoryController : Controller
    {
        private readonly DapperContext _context;


        public MovieCategoryController(DapperContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<MovieCategoryViewModel> categories = new List<MovieCategoryViewModel>();
            using (var conn = _context.DbConnection())
            {
                string sql = "SELECT *FROM MovieCategories WITH(NOLOCK)";
                categories = conn.Query<MovieCategoryViewModel>(sql);
            }
            return View(categories);
        }

        public IActionResult AddMovieCategory(int? id)
        {
            if (id == null)
            {
                return View(new MovieCategoryViewModel());
            }
            string sql = $"SELECT *FROM MOVIECATEGORIES WITH(NOLOCK) WHERE ID = {id}";
            MovieCategoryViewModel category = new MovieCategoryViewModel();
            using (var conn = _context.DbConnection())
            {
                category = conn.QueryFirstOrDefault<MovieCategoryViewModel>(sql);
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult AddMovieCategory(MovieCategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                if (category.Id == null)
                {
                    using (var conn = _context.DbConnection())
                    {
                        string sql = "INSERT INTO MovieCategories(Genre,[Description]) values('" + category.Genre + "','" + category.Description + "')";
                        conn.Execute(sql);

                    }
                    return RedirectToAction("Index");
                }

                using (IDbConnection conn = _context.DbConnection())
                {

                    string sql = "UPDATE MOVIECATEGORIES SET GENRE =  @Genre, DESCRIPTION = @Description WHERE ID = @Id";
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@GENRE", category.Genre);
                    parameters.Add("@Description", category.Description);
                    parameters.Add("@Id", category.Id);
                    conn.Execute(sql, parameters);
                }
                
                return RedirectToAction("Index");


            }
            return View();
        }

        public IActionResult RemoveMovieCategory(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            string sql = "DELETE FROM MOVIECATEGORIES WHERE ID = @ID";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("@ID", id);
            using (var conn = _context.DbConnection())
            {
                conn.Execute(sql, parameters);
            }
            return RedirectToAction("Index");
        }
    }
}
