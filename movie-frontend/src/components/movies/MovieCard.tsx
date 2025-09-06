import { useState } from 'react';
import { Edit, Trash2, Calendar, Star } from 'lucide-react';
import { Movie } from '../../types/movie';
import { getImageUrl, truncateText } from '../../lib/utils';
import { StarRating } from '../ui/StarRating';

interface MovieCardProps {
  movie: Movie;
  onEdit: (movie: Movie) => void;
  onDelete: (id: number) => void;
}

export function MovieCard({ movie, onEdit, onDelete }: MovieCardProps) {
  const [imageError, setImageError] = useState(false);
  const [isHovered, setIsHovered] = useState(false);

  const imageUrl = getImageUrl(movie.poster, movie.posterUrl);

  return (
    <div 
      className="card overflow-hidden group animate-fade-in"
      onMouseEnter={() => setIsHovered(true)}
      onMouseLeave={() => setIsHovered(false)}
    >
      {/* Movie Poster */}
      <div className="relative aspect-[2/3] overflow-hidden">
        <img
          src={imageError ? `https://images.unsplash.com/photo-1489599511986-c2d4d2d0e8b8?w=400&h=600&fit=crop&crop=center` : imageUrl}
          alt={movie.title}
          className="w-full h-full object-cover transition-transform duration-500 group-hover:scale-110"
          onError={() => setImageError(true)}
        />
        
        {/* Overlay */}
        <div className={`absolute inset-0 bg-gradient-to-t from-black/80 via-transparent to-transparent transition-opacity duration-300 ${isHovered ? 'opacity-100' : 'opacity-0'}`} />
        
        {/* Action Buttons */}
        <div className={`absolute top-4 right-4 flex space-x-2 transition-all duration-300 ${isHovered ? 'opacity-100 translate-y-0' : 'opacity-0 -translate-y-2'}`}>
          <button
            onClick={() => onEdit(movie)}
            className="p-2 bg-primary-600 hover:bg-primary-700 rounded-lg transition-colors shadow-lg"
          >
            <Edit className="w-4 h-4 text-white" />
          </button>
          <button
            onClick={() => onDelete(movie.id)}
            className="p-2 bg-red-600 hover:bg-red-700 rounded-lg transition-colors shadow-lg"
          >
            <Trash2 className="w-4 h-4 text-white" />
          </button>
        </div>

        {/* Rating Badge */}
        <div className="absolute top-4 left-4">
          <div className="bg-black/70 backdrop-blur-sm rounded-lg px-2 py-1 flex items-center space-x-1">
            <Star className="w-3 h-3 text-yellow-400 fill-yellow-400" />
            <span className="text-white text-sm font-medium">{movie.rate.toFixed(1)}</span>
          </div>
        </div>
      </div>

      {/* Movie Info */}
      <div className="p-4">
        <div className="flex items-start justify-between mb-2">
          <h3 className="font-semibold text-lg text-white group-hover:text-primary-400 transition-colors line-clamp-1">
            {movie.title}
          </h3>
          <div className="flex items-center text-dark-400 text-sm ml-2">
            <Calendar className="w-3 h-3 mr-1" />
            {movie.year}
          </div>
        </div>

        {/* Genre */}
        {movie.genraName && (
          <div className="mb-3">
            <span className="inline-block bg-primary-600/20 text-primary-400 px-2 py-1 rounded-full text-xs font-medium">
              {movie.genraName}
            </span>
          </div>
        )}

        {/* Storyline */}
        <p className="text-dark-300 text-sm leading-relaxed mb-4">
          {truncateText(movie.storyline, 120)}
        </p>

        {/* Rating */}
        <div className="flex items-center justify-between">
          <StarRating rating={movie.rate} size="sm" />
          <div className="text-xs text-dark-400">
            ID: {movie.id}
          </div>
        </div>
      </div>
    </div>
  );
}