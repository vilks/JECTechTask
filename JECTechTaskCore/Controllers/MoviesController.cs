using JECTechTaskCore.Interfaces.Services;
using JECTechTaskCore.Models.Movies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JECTechTaskCore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet("search")]
        public async Task<IEnumerable<Movie>> Search([FromQuery] string title)
        {
            return await _movieService.SearchMoviesAsync(title);
        }

        [HttpGet("{id}")]
        public async Task<MovieDetails> Get(string id)
        {
            return await _movieService.GetMovieDetailsAsync(id);
        }
    }
}
