using Dapper;
using DapperWithSQL.DataContext;
using DapperWithSQL.Models;
using DapperWithSQL.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

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
            IEnumerable<MovieViewModel> result;
            using (var conn = _context.DbConnection())
            {
                string sql = @"
                SELECT m.Id, m.Movie_Name, m.Movie_Price, c.Genre AS CategoryName
                FROM Movies m
                JOIN MovieCategories c ON m.MovieCategory_Id = c.Id";
                result = conn.Query<MovieViewModel>(sql);
            }
            return View(result);
        }
        [HttpGet]
        public IActionResult AddMovie()
        {
            Movies model = new Movies();
            using (var conn = _context.DbConnection())
            {
                IEnumerable<MovieCategory> result = conn.Query<MovieCategory>("Select *from moviecategories with(nolock)");
                model = new Movies
                {
                    Categories = result.Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Genre
                    })
                };
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult AddMovie(Movies movies)
        {
            if (ModelState.IsValid)
            {
                using (var conn = _context.DbConnection())
                {
                    var sql = "insert into movies(movie_name,movie_price,MovieCategory_Id) select " + "'" + movies.Movie_Name + "'" + " as movie_name,"
                        + "'" + movies.Movie_Price + "'" + " as movies_price, " + "'" + movies.MovieCategory_Id + "'" + "as MovieCategory_Id";
                    conn.Execute(sql);
                }
                return RedirectToAction("Index");
            }
            return View();
        }
        public IActionResult MovieList()
        {
            IEnumerable<Movies> result;
            using (var conn = _context.DbConnection())
            {
                result = conn.Query<Movies>("select *from movies");
            }
            return View(result);
        }

        public ActionResult UpdateMovie(int id)
        {
            Movies movies = new Movies();
            IEnumerable<MovieCategory> result;
            using (var conn = _context.DbConnection())
            {
                string sql = "select c.Genre, m.* from Movies m join MovieCategories c on c.Id = m.MovieCategory_Id where m.Id = '" + id + "'";
                result = conn.Query<MovieCategory>("Select *from moviecategories with(nolock)");

                movies = conn.QueryFirstOrDefault<Movies>(sql);
            }


            movies.Categories = result.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Genre
            });
            return View(movies);
        }
        [HttpPost]
        public ActionResult UpdateMovie(Movies movies) 
        {
            if (movies == null)
            {
                return View();
            }
            if(ModelState.IsValid)
            {
                string sql = $"update movies set movie_name = '{movies.Movie_Name}' ,movie_price = '{movies.Movie_Price}', MovieCategory_Id = '{movies.MovieCategory_Id}' where " +
                    $"id = '{movies.Id}'"  ;
                using(var conn = _context.DbConnection())
                {
                    conn.Execute(sql);
                }
                return RedirectToAction("Index");
            }
            return View();
        }

        public ActionResult DeleteMovie(int id)
        {
            string sql = $"delete from movies where id = '{id}'";
            using(var conn = _context.DbConnection())
            {
                conn.Execute(sql);
            }
            return RedirectToAction("Index");
        }
    }
}
