import { Movie } from '../../types/movie';
import { MovieCard } from './MovieCard';
import { LoadingSpinner } from '../ui/LoadingSpinner';

interface MovieGridProps {
  movies: Movie[];
  isLoading: boolean;
  onEdit: (movie: Movie) => void;
  onDelete: (id: number) => void;
}

export function MovieGrid({ movies, isLoading, onEdit, onDelete }: MovieGridProps) {
  if (isLoading) {
    return (
      <div className="flex items-center justify-center py-20">
        <LoadingSpinner size="lg" />
      </div>
    );
  }

  if (movies.length === 0) {
    return (
      <div className="text-center py-20">
        <div className="bg-dark-800 rounded-full w-20 h-20 flex items-center justify-center mx-auto mb-4">
          <svg className="w-10 h-10 text-dark-500" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M7 4V2a1 1 0 011-1h8a1 1 0 011 1v2h4a1 1 0 011 1v1a1 1 0 01-1 1H3a1 1 0 01-1-1V5a1 1 0 011-1h4zM3 8v11a2 2 0 002 2h14a2 2 0 002-2V8H3z" />
          </svg>
        </div>
        <h3 className="text-xl font-semibold text-white mb-2">No movies found</h3>
        <p className="text-dark-400">Start by adding your first movie to the collection.</p>
      </div>
    );
  }

  return (
    <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4 gap-6">
      {movies.map((movie) => (
        <MovieCard
          key={movie.id}
          movie={movie}
          onEdit={onEdit}
          onDelete={onDelete}
        />
      ))}
    </div>
  );
}