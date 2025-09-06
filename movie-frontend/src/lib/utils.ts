import { type ClassValue, clsx } from 'clsx';
import { twMerge } from 'tailwind-merge';

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs));
}

export function formatRating(rating: number): string {
  return rating.toFixed(1);
}

export function getImageUrl(poster: number[] | null | undefined, posterUrl?: string | null): string {
  if (posterUrl) {
    return `http://localhost:5037${posterUrl}`;
  }
  
  if (poster && poster.length > 0) {
    const blob = new Blob([new Uint8Array(poster)], { type: 'image/jpeg' });
    return URL.createObjectURL(blob);
  }
  
  return `https://images.unsplash.com/photo-1489599511986-c2d4d2d0e8b8?w=400&h=600&fit=crop&crop=center`;
}

export function truncateText(text: string, maxLength: number): string {
  if (text.length <= maxLength) return text;
  return text.substring(0, maxLength) + '...';
}