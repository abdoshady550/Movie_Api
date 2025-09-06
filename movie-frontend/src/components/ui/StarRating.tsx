import { Star } from 'lucide-react';
import { cn } from '../../lib/utils';

interface StarRatingProps {
  rating: number;
  maxRating?: number;
  size?: 'sm' | 'md' | 'lg';
  showValue?: boolean;
  className?: string;
}

export function StarRating({ 
  rating, 
  maxRating = 10, 
  size = 'md', 
  showValue = true,
  className 
}: StarRatingProps) {
  const normalizedRating = (rating / maxRating) * 5;
  const fullStars = Math.floor(normalizedRating);
  const hasHalfStar = normalizedRating % 1 >= 0.5;
  
  const sizeClasses = {
    sm: 'w-3 h-3',
    md: 'w-4 h-4',
    lg: 'w-5 h-5',
  };

  return (
    <div className={cn('flex items-center gap-2', className)}>
      <div className="flex items-center">
        {[...Array(5)].map((_, index) => {
          const isFilled = index < fullStars;
          const isHalf = index === fullStars && hasHalfStar;
          
          return (
            <Star
              key={index}
              className={cn(
                sizeClasses[size],
                isFilled || isHalf
                  ? 'text-yellow-400 fill-yellow-400'
                  : 'text-dark-500'
              )}
            />
          );
        })}
      </div>
      {showValue && (
        <span className="text-sm font-medium text-dark-300">
          {rating.toFixed(1)}
        </span>
      )}
    </div>
  );
}