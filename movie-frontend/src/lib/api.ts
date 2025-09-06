import axios from 'axios';
import { ApiResponse, Movie, Genre, CreateMovieDto, UpdateMovieDto } from '../types/movie';

const API_BASE_URL = 'http://localhost:5037/api';

const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

// API Version management
export const setApiVersion = (version: '1.0' | '2.0') => {
  api.defaults.headers['Accept'] = `application/json;v=${version}`;
};

// Set default version
setApiVersion('2.0');

// Movies API
export const moviesApi = {
  getAll: async (): Promise<ApiResponse<Movie[]>> => {
    const response = await api.get('/movie');
    return response.data;
  },

  getById: async (id: number): Promise<ApiResponse<Movie>> => {
    const response = await api.get(`/movie/${id}`);
    return response.data;
  },

  getByGenre: async (genreId: number): Promise<ApiResponse<Movie[]>> => {
    const response = await api.get(`/movie/GetByGenraId?genraId=${genreId}`);
    return response.data;
  },

  create: async (movieData: CreateMovieDto): Promise<ApiResponse<Movie>> => {
    const formData = new FormData();
    formData.append('title', movieData.title);
    formData.append('year', movieData.year.toString());
    formData.append('rate', movieData.rate.toString());
    formData.append('storyline', movieData.storyline);
    formData.append('poster', movieData.poster);
    formData.append('genraId', movieData.genraId.toString());

    const response = await api.post('/movie', formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
    return response.data;
  },

  update: async (id: number, movieData: UpdateMovieDto): Promise<ApiResponse<Movie>> => {
    const formData = new FormData();
    if (movieData.title) formData.append('title', movieData.title);
    if (movieData.year) formData.append('year', movieData.year.toString());
    if (movieData.rate) formData.append('rate', movieData.rate.toString());
    if (movieData.storyline) formData.append('storyline', movieData.storyline);
    if (movieData.poster) formData.append('poster', movieData.poster);
    if (movieData.genraId) formData.append('genraId', movieData.genraId.toString());

    const response = await api.put(`/movie/${id}`, formData, {
      headers: {
        'Content-Type': 'multipart/form-data',
      },
    });
    return response.data;
  },

  delete: async (id: number): Promise<ApiResponse<Movie>> => {
    const response = await api.delete(`/movie/${id}`);
    return response.data;
  },
};

// Genres API
export const genresApi = {
  getAll: async (): Promise<ApiResponse<Genre[]>> => {
    const response = await api.get('/genra');
    return response.data;
  },

  getById: async (id: number): Promise<ApiResponse<Genre>> => {
    const response = await api.get(`/genra/${id}`);
    return response.data;
  },

  create: async (name: string): Promise<ApiResponse<Genre>> => {
    const response = await api.post('/genra', { name });
    return response.data;
  },

  update: async (id: number, name: string): Promise<ApiResponse<Genre>> => {
    const response = await api.put(`/genra/${id}`, { name });
    return response.data;
  },

  delete: async (id: number): Promise<ApiResponse<Genre>> => {
    const response = await api.delete(`/genra/${id}`);
    return response.data;
  },
};

export { api };