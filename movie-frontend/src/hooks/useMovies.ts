import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { moviesApi } from '../lib/api';
import { CreateMovieDto, UpdateMovieDto } from '../types/movie';
import toast from 'react-hot-toast';

export const useMovies = () => {
  return useQuery({
    queryKey: ['movies'],
    queryFn: async () => {
      const response = await moviesApi.getAll();
      return response.data;
    },
  });
};

export const useMovie = (id: number) => {
  return useQuery({
    queryKey: ['movie', id],
    queryFn: async () => {
      const response = await moviesApi.getById(id);
      return response.data;
    },
    enabled: !!id,
  });
};

export const useMoviesByGenre = (genreId: number) => {
  return useQuery({
    queryKey: ['movies', 'genre', genreId],
    queryFn: async () => {
      const response = await moviesApi.getByGenre(genreId);
      return response.data;
    },
    enabled: !!genreId,
  });
};

export const useCreateMovie = () => {
  const queryClient = useQueryClient();
  
  return useMutation({
    mutationFn: (movieData: CreateMovieDto) => moviesApi.create(movieData),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['movies'] });
      toast.success('Movie created successfully!');
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.massage || 'Failed to create movie');
    },
  });
};

export const useUpdateMovie = () => {
  const queryClient = useQueryClient();
  
  return useMutation({
    mutationFn: ({ id, data }: { id: number; data: UpdateMovieDto }) => 
      moviesApi.update(id, data),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['movies'] });
      toast.success('Movie updated successfully!');
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.massage || 'Failed to update movie');
    },
  });
};

export const useDeleteMovie = () => {
  const queryClient = useQueryClient();
  
  return useMutation({
    mutationFn: (id: number) => moviesApi.delete(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['movies'] });
      toast.success('Movie deleted successfully!');
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.massage || 'Failed to delete movie');
    },
  });
};