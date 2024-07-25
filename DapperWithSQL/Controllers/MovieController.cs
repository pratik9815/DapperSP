using Dapper;
using DapperWithSQL.DataContext;
using DapperWithSQL.Models;
using Microsoft.AspNetCore.Mvc;

namespace DapperWithSQL.Controllers
{
    public class MovieController : Controller
    {
        private readonly DapperContext _context;

        public MovieController(DapperContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Movies> result;
            using (var conn = _context.DbConnection())
            {
                result = conn.Query<Movies>("select *from movies");
            }
            return View(result);  
        }
        public IActionResult Welcome()
        {
            return View();  
        }
        public IActionResult AddMovie()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddMovie(Movies movies)
        {
            if(ModelState.IsValid)
            {
                using (var conn = _context.DbConnection())
                {
                    var sql = "insert into movies(movie_name,movie_price) select " + "'" + movies.Movie_Name + "'" + " as movie_name,"
                        + "'" + movies.Movie_Price + "'" + " as movies_price";
                    conn.Execute(sql);
                }
                return RedirectToAction("MovieList");
            }
            return View();
        }
        public IActionResult MovieList() 
        {
            IEnumerable<Movies> result;
            using (var conn = _context.DbConnection())
            {
                result =  conn.Query<Movies>("select *from movies");
            }
            return View(result);
        }
    }
}
