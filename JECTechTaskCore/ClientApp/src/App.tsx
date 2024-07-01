import React, { useState } from 'react';
import { searchMovies, getMovieDetails } from './services/movieService';
import './custom.css';
import { Movie, MovieDetails } from './Interfaces';

const App: React.FC = () => {
    const [query, setQuery] = useState<string>('');
    const [movies, setMovies] = useState<Movie[]>([]);
    const [selectedMovie, setSelectedMovie] = useState<MovieDetails | null>(null);
    const [history, setHistory] = useState<string[]>([]);
    const [noResults, setNoResults] = useState<boolean>(false);

    const handleSearch = async (searchQuery?: string) => {
        const searchValue = searchQuery || query;
        const response = await searchMovies(searchValue);
        if (response.data.length > 0) {
            setMovies(response.data);
            setNoResults(false);
        } else {
            setMovies([]);
            setNoResults(true);
        }
        setSelectedMovie(null);
        setHistory((prevHistory) => {
            const newHistory = [...prevHistory, searchValue].slice(-5);
            return newHistory;
        });
    };

    const handleMovieClick = async (id: string) => {
        const response = await getMovieDetails(id);
        setSelectedMovie(response.data);
    };

    return (
        <div className="App">
            <header className="App-header">
                <h1>Movie Search</h1>
                <div className="search-bar">
                    <input
                        type="text"
                        value={query}
                        onChange={(e) => setQuery(e.target.value)}
                        placeholder="Search for a movie"
                    />
                    <button onClick={() => handleSearch()}>Search</button>
                </div>
                <div className="history">
                    <h3>Search History:</h3>
                    <ul>
                        {history.map((item, index) => (
                            <li key={index} onClick={() => handleSearch(item)} className="history-item">
                                {item}
                            </li>
                        ))}
                    </ul>
                </div>
                {noResults && (
                    <div className="no-results">
                        <h2>No Results Found</h2>
                    </div>
                )}
                {selectedMovie && (
                    <div className="movie-details">
                        <div className="movie-details-header">
                            <img src={selectedMovie.poster} alt={selectedMovie.title} className="movie-poster" />
                            <div className="movie-info">
                                <h2>{selectedMovie.title}</h2>
                                <p><strong>Year:</strong> {selectedMovie.year}</p>
                                <p><strong>Rated:</strong> {selectedMovie.rated}</p>
                                <p><strong>Released:</strong> {selectedMovie.released}</p>
                                <p><strong>Runtime:</strong> {selectedMovie.runtime}</p>
                                <p><strong>Genre:</strong> {selectedMovie.genre}</p>
                                <p><strong>Director:</strong> {selectedMovie.director}</p>
                                <p><strong>Writer:</strong> {selectedMovie.writer}</p>
                                <p><strong>Actors:</strong> {selectedMovie.actors}</p>
                                <p><strong>Awards:</strong> {selectedMovie.awards}</p>
                                <p><strong>IMDB Rating:</strong> {selectedMovie.imdbRating}</p>
                            </div>
                        </div>
                        <div className="movie-plot">
                            <p>{selectedMovie.plot}</p>
                        </div>
                    </div>
                )}
                <div className="movie-list">
                    {movies.map((movie) => (
                        <div key={movie.imdbID} className="movie-item" onClick={() => handleMovieClick(movie.imdbID)}>
                            <h3>{movie.title}</h3>
                            <img src={movie.poster} alt={movie.title} />
                        </div>
                    ))}
                </div>
            </header>
        </div>
    );
};

export default App;