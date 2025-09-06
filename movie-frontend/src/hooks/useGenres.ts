import { useQuery, useMutation, useQueryClient } from '@tanstack/react-query';
import { genresApi } from '../lib/api';
import toast from 'react-hot-toast';

export const useGenres = () => {
  return useQuery({
    queryKey: ['genres'],
    queryFn: async () => {
      const response = await genresApi.getAll();
      return response.data;
    },
  });
};

export const useGenre = (id: number) => {
  return useQuery({
    queryKey: ['genre', id],
    queryFn: async () => {
      const response = await genresApi.getById(id);
      return response.data;
    },
    enabled: !!id,
  });
};

export const useCreateGenre = () => {
  const queryClient = useQueryClient();
  
  return useMutation({
    mutationFn: (name: string) => genresApi.create(name),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['genres'] });
      toast.success('Genre created successfully!');
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.massage || 'Failed to create genre');
    },
  });
};

export const useUpdateGenre = () => {
  const queryClient = useQueryClient();
  
  return useMutation({
    mutationFn: ({ id, name }: { id: number; name: string }) => 
      genresApi.update(id, name),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['genres'] });
      toast.success('Genre updated successfully!');
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.massage || 'Failed to update genre');
    },
  });
};

export const useDeleteGenre = () => {
  const queryClient = useQueryClient();
  
  return useMutation({
    mutationFn: (id: number) => genresApi.delete(id),
    onSuccess: () => {
      queryClient.invalidateQueries({ queryKey: ['genres'] });
      toast.success('Genre deleted successfully!');
    },
    onError: (error: any) => {
      toast.error(error.response?.data?.massage || 'Failed to delete genre');
    },
  });
};