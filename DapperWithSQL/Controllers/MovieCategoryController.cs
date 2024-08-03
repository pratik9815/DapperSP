using Dapper;
using DapperWithSQL.DataContext;
using DapperWithSQL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DapperWithSQL.Controllers
{
    public class MovieCategoryController : Controller
    {
        private readonly DapperContext _context;
        private readonly MovieStateService _stateService;


        public MovieCategoryController(DapperContext context, MovieStateService stateService)
        {
            _context = context;
            _stateService = stateService;
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

        public IActionResult AddMovieCategory(int? id)
        {
            if (id == null)
            {
                return View();
            }
            string sql = $"SELECT *FROM MOVIECATEGORIES WITH(NOLOCK) WHERE ID = {id}";
            MovieCategory category = new MovieCategory(); 
            using (var conn = _context.DbConnection())
            {
                category = conn.QueryFirstOrDefault<MovieCategory>(sql);
            }
            _stateService.IsMovieUpdated = true;
            return View(category);
        }
        [HttpPost]
        public IActionResult AddMovieCategory(MovieCategory category)
        {
            if (ModelState.IsValid)
            {
                if (_stateService.IsMovieUpdated) 
                {
                    using (IDbConnection conn = _context.DbConnection())
                    {

                        string sql = "UPDATE MOVIECATEGORIES SET GENRE =  @Genre, DESCRIPTION = @Description WHERE ID = @Id";
                        DynamicParameters parameters = new DynamicParameters();
                        parameters.Add("@GENRE", category.Genre);
                        parameters.Add("@Description", category.Description);
                        parameters.Add("@Id", category.Id);
                        conn.Execute(sql,parameters);
                    }
                    _stateService.IsMovieUpdated = false;
                    return RedirectToAction("Index");
                }
                using (var conn = _context.DbConnection())
                {
                    string sql = "INSERT INTO MovieCategories(Genre,[Description]) values('" + category.Genre + "','" + category.Description + "')";
                    conn.Execute(sql);
                    return RedirectToAction("Index");
                }
            }
            return View();
        }
            
        public IActionResult UpdateMovieCategory(int id)
        {
            return View();
        }
    }
}
