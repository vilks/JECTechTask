using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using JECTechTaskCore.Interfaces.Services;
using JECTechTaskCore.Models.Movies;
using JECTechTaskCore.Models.Settings;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json.Linq;
namespace JECTechTaskCore.Services.Movies
{
    public class MovieService : IMovieService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl = "http://www.omdbapi.com/";
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(20);

        public MovieService(MovieSettings movieSettings, IMemoryCache cache)
        {
            _httpClient = new HttpClient();
            _apiKey = movieSettings.ApiKey;
            _cache = cache;
        }

        public async Task<IEnumerable<Movie>> SearchMoviesAsync(string title)
        {
            var cacheKey = $"Search_{title}";
            if (!_cache.TryGetValue(cacheKey, out IEnumerable<Movie> movies))
            {
                var response = await _httpClient.GetStringAsync($"{_baseUrl}?apikey={_apiKey}&s={title}");
                var json = JObject.Parse(response);

                if (json["Response"].ToString() == "False")
                {
                    return null;
                }

                movies = json["Search"].ToObject<IEnumerable<Movie>>();

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = _cacheDuration
                };

                _cache.Set(cacheKey, movies, cacheEntryOptions);
            }

            return movies;
        }

        public async Task<MovieDetails> GetMovieDetailsAsync(string imdbId)
        {
            var cacheKey = $"MovieDetails_{imdbId}";
            if (!_cache.TryGetValue(cacheKey, out MovieDetails movieDetails))
            {
                var response = await _httpClient.GetStringAsync($"{_baseUrl}?apikey={_apiKey}&i={imdbId}");
                movieDetails = JObject.Parse(response).ToObject<MovieDetails>();

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = _cacheDuration
                };

                _cache.Set(cacheKey, movieDetails, cacheEntryOptions);
            }

            return movieDetails;
        }
    }
}
