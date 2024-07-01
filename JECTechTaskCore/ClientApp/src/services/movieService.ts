import axios from 'axios';
import { Movie, MovieDetails } from '../Interfaces';

const API_URL = 'https://localhost:44343/movies';

export const searchMovies = (title: string) => {
    return axios.get<Movie[]>(`${API_URL}/search`, { params: { title } });
};

export const getMovieDetails = (id: string) => {
    return axios.get<MovieDetails>(`${API_URL}/${id}`);
};