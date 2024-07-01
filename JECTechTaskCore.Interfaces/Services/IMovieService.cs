using JECTechTaskCore.Models.Movies;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JECTechTaskCore.Interfaces.Services
{
    public interface IMovieService
    {
        /// <summary>
        /// Search movie by title
        /// </summary>
        /// <param name="title">Movie name</param>
        /// <returns>List of movies</returns>
        Task<IEnumerable<Movie>> SearchMoviesAsync(string title);

        /// <summary>
        /// Get movie datails
        /// </summary>
        /// <param name="imdbId">imdb movie Id</param>
        /// <returns>Movie details</returns>
        Task<MovieDetails> GetMovieDetailsAsync(string imdbId);
    }
}
