using DapperWithSQL.DataContext;
using DapperWithSQL.IRepository;
using DapperWithSQL.ViewModel;

namespace DapperWithSQL.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private readonly DapperContext _context;
        public MovieRepository(DapperContext context) 
        {
            _context = context;
        }
        public List<MovieViewModel> Get()
        {
            List<MovieViewModel> viewModel = new List<MovieViewModel>
            {
                new MovieViewModel
                {
                    Id = 1,
                    Movie_Name = "Intersteller",
                    Movie_Price = "40.40",
                    CategoryName = "Category"
                },
                new MovieViewModel
                {
                    Id = 1,
                    Movie_Name = "Intersteller",
                    Movie_Price = "40.40",
                    CategoryName = "Category"
                }
            };
            MovieViewModel model1 = new MovieViewModel
            {
                Id = 1,
                Movie_Name = "Intersteller",
                Movie_Price = "40.40",
                CategoryName = "Category"
            };



            viewModel.Add(model1);
            return viewModel;

            
        }
        public MovieViewModel GetById(int id)
        {
            throw new NotImplementedException();
        }
        public bool AddMovieViewModel(MovieViewModel model)
        {
            throw new NotImplementedException();
        }
        public bool UpdateMovieViewModel(MovieViewModel model)
        {
            throw new NotImplementedException();
        }
        public bool DeleteMovieViewModel(int id)
        {
            throw new NotImplementedException();
        }
    }
}
