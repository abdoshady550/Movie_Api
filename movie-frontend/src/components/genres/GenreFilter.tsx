import { useGenres } from '../../hooks/useGenres';
import { LoadingSpinner } from '../ui/LoadingSpinner';

interface GenreFilterProps {
  selectedGenre: number | null;
  onGenreChange: (genreId: number | null) => void;
}

export function GenreFilter({ selectedGenre, onGenreChange }: GenreFilterProps) {
  const { data: genres, isLoading } = useGenres();

  if (isLoading) {
    return <LoadingSpinner size="sm" />;
  }

  return (
    <div className="flex flex-wrap gap-2">
      <button
        onClick={() => onGenreChange(null)}
        className={`px-4 py-2 rounded-full text-sm font-medium transition-all duration-200 ${
          selectedGenre === null
            ? 'bg-primary-600 text-white shadow-lg'
            : 'bg-dark-700 text-dark-300 hover:bg-dark-600 hover:text-white'
        }`}
      >
        All Genres
      </button>
      
      {genres?.map((genre) => (
        <button
          key={genre.id}
          onClick={() => onGenreChange(genre.id)}
          className={`px-4 py-2 rounded-full text-sm font-medium transition-all duration-200 ${
            selectedGenre === genre.id
              ? 'bg-primary-600 text-white shadow-lg'
              : 'bg-dark-700 text-dark-300 hover:bg-dark-600 hover:text-white'
          }`}
        >
          {genre.name}
        </button>
      ))}
    </div>
  );
}