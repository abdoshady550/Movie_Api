import { useState, useEffect } from 'react';
import { useForm } from 'react-hook-form';
import { zodResolver } from '@hookform/resolvers/zod';
import { z } from 'zod';
import { Upload, X } from 'lucide-react';
import { Movie, CreateMovieDto, UpdateMovieDto } from '../../types/movie';
import { useGenres } from '../../hooks/useGenres';
import { LoadingSpinner } from '../ui/LoadingSpinner';

const movieSchema = z.object({
  title: z.string().min(1, 'Title is required').max(50, 'Title must be less than 50 characters'),
  year: z.number().min(1900, 'Year must be after 1900').max(new Date().getFullYear() + 5, 'Year cannot be too far in the future'),
  rate: z.number().min(0, 'Rating must be at least 0').max(10, 'Rating cannot exceed 10'),
  storyline: z.string().min(1, 'Storyline is required').max(2500, 'Storyline must be less than 2500 characters'),
  genraId: z.number().min(1, 'Genre is required'),
});

type MovieFormData = z.infer<typeof movieSchema>;

interface MovieFormProps {
  movie?: Movie;
  onSubmit: (data: CreateMovieDto | UpdateMovieDto) => void;
  onCancel: () => void;
  isLoading: boolean;
}

export function MovieForm({ movie, onSubmit, onCancel, isLoading }: MovieFormProps) {
  const [selectedFile, setSelectedFile] = useState<File | null>(null);
  const [previewUrl, setPreviewUrl] = useState<string | null>(null);
  const { data: genres, isLoading: genresLoading } = useGenres();

  const {
    register,
    handleSubmit,
    formState: { errors },
    setValue,
    watch,
  } = useForm<MovieFormData>({
    resolver: zodResolver(movieSchema),
    defaultValues: movie ? {
      title: movie.title,
      year: movie.year,
      rate: movie.rate,
      storyline: movie.storyline,
      genraId: movie.genraId,
    } : {
      year: new Date().getFullYear(),
      rate: 5,
    },
  });

  useEffect(() => {
    if (movie && movie.posterUrl) {
      setPreviewUrl(`http://localhost:5037${movie.posterUrl}`);
    }
  }, [movie]);

  const handleFileChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const file = e.target.files?.[0];
    if (file) {
      setSelectedFile(file);
      const url = URL.createObjectURL(file);
      setPreviewUrl(url);
    }
  };

  const removeFile = () => {
    setSelectedFile(null);
    setPreviewUrl(null);
  };

  const onFormSubmit = (data: MovieFormData) => {
    if (!movie && !selectedFile) {
      alert('Please select a poster image');
      return;
    }

    const formData = {
      ...data,
      ...(selectedFile && { poster: selectedFile }),
    };

    onSubmit(formData);
  };

  if (genresLoading) {
    return (
      <div className="flex items-center justify-center py-8">
        <LoadingSpinner />
      </div>
    );
  }

  return (
    <form onSubmit={handleSubmit(onFormSubmit)} className="space-y-6">
      {/* Title */}
      <div>
        <label className="block text-sm font-medium text-white mb-2">
          Title *
        </label>
        <input
          {...register('title')}
          type="text"
          className="input-field"
          placeholder="Enter movie title"
        />
        {errors.title && (
          <p className="mt-1 text-sm text-red-400">{errors.title.message}</p>
        )}
      </div>

      {/* Year and Rating */}
      <div className="grid grid-cols-2 gap-4">
        <div>
          <label className="block text-sm font-medium text-white mb-2">
            Year *
          </label>
          <input
            {...register('year', { valueAsNumber: true })}
            type="number"
            className="input-field"
            placeholder="2024"
          />
          {errors.year && (
            <p className="mt-1 text-sm text-red-400">{errors.year.message}</p>
          )}
        </div>

        <div>
          <label className="block text-sm font-medium text-white mb-2">
            Rating * (0-10)
          </label>
          <input
            {...register('rate', { valueAsNumber: true })}
            type="number"
            step="0.1"
            min="0"
            max="10"
            className="input-field"
            placeholder="8.5"
          />
          {errors.rate && (
            <p className="mt-1 text-sm text-red-400">{errors.rate.message}</p>
          )}
        </div>
      </div>

      {/* Genre */}
      <div>
        <label className="block text-sm font-medium text-white mb-2">
          Genre *
        </label>
        <select
          {...register('genraId', { valueAsNumber: true })}
          className="input-field"
        >
          <option value="">Select a genre</option>
          {genres?.map((genre) => (
            <option key={genre.id} value={genre.id}>
              {genre.name}
            </option>
          ))}
        </select>
        {errors.genraId && (
          <p className="mt-1 text-sm text-red-400">{errors.genraId.message}</p>
        )}
      </div>

      {/* Storyline */}
      <div>
        <label className="block text-sm font-medium text-white mb-2">
          Storyline *
        </label>
        <textarea
          {...register('storyline')}
          rows={4}
          className="input-field resize-none"
          placeholder="Enter movie storyline..."
        />
        {errors.storyline && (
          <p className="mt-1 text-sm text-red-400">{errors.storyline.message}</p>
        )}
      </div>

      {/* Poster Upload */}
      <div>
        <label className="block text-sm font-medium text-white mb-2">
          Poster {!movie && '*'}
        </label>
        
        {previewUrl ? (
          <div className="relative">
            <img
              src={previewUrl}
              alt="Preview"
              className="w-full h-48 object-cover rounded-lg border border-dark-600"
            />
            <button
              type="button"
              onClick={removeFile}
              className="absolute top-2 right-2 p-1 bg-red-600 hover:bg-red-700 rounded-full transition-colors"
            >
              <X className="w-4 h-4 text-white" />
            </button>
          </div>
        ) : (
          <div className="border-2 border-dashed border-dark-600 rounded-lg p-6 text-center hover:border-dark-500 transition-colors">
            <Upload className="w-8 h-8 text-dark-400 mx-auto mb-2" />
            <p className="text-dark-400 mb-2">Click to upload poster</p>
            <input
              type="file"
              accept="image/jpeg,image/png"
              onChange={handleFileChange}
              className="hidden"
              id="poster-upload"
            />
            <label
              htmlFor="poster-upload"
              className="btn-secondary cursor-pointer inline-block"
            >
              Choose File
            </label>
            <p className="text-xs text-dark-500 mt-2">JPG or PNG, max 10MB</p>
          </div>
        )}
      </div>

      {/* Form Actions */}
      <div className="flex justify-end space-x-4 pt-4 border-t border-dark-700">
        <button
          type="button"
          onClick={onCancel}
          className="btn-secondary"
          disabled={isLoading}
        >
          Cancel
        </button>
        <button
          type="submit"
          className="btn-primary flex items-center space-x-2"
          disabled={isLoading}
        >
          {isLoading && <LoadingSpinner size="sm" />}
          <span>{movie ? 'Update Movie' : 'Create Movie'}</span>
        </button>
      </div>
    </form>
  );
}