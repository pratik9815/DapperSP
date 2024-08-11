using DapperWithSQL.ViewModel;

namespace DapperWithSQL.IRepository
{
    public interface IMovieRepository
    {
        public List<MovieViewModel> Get();
        public MovieViewModel GetById(int id);
        public bool AddMovieViewModel(MovieViewModel model);
        public bool UpdateMovieViewModel(MovieViewModel model);
        public bool DeleteMovieViewModel(int id);
    }
}
