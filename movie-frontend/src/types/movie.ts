export interface Genre {
  id: number;
  name: string;
}

export interface Movie {
  id: number;
  title: string;
  year: number;
  rate: number;
  storyline: string;
  poster?: number[] | null;
  posterUrl?: string | null;
  genraId: number;
  genraName?: string;
}

export interface CreateMovieDto {
  title: string;
  year: number;
  rate: number;
  storyline: string;
  poster: File;
  genraId: number;
}

export interface UpdateMovieDto {
  title?: string;
  year?: number;
  rate?: number;
  storyline?: string;
  poster?: File;
  genraId?: number;
}

export interface ApiResponse<T> {
  success: boolean;
  massage: string;
  version?: string;
  data: T;
  error?: any;
}