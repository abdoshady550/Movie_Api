import { useState, useMemo } from 'react';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import { Toaster } from 'react-hot-toast';
import { Header } from './components/layout/Header';
import { MovieGrid } from './components/movies/MovieGrid';
import { MovieForm } from './components/movies/MovieForm';
import { GenreFilter } from './components/genres/GenreFilter';
import { Modal } from './components/ui/Modal';
import { useMovies, useCreateMovie, useUpdateMovie, useDeleteMovie } from './hooks/useMovies';
import { Movie, CreateMovieDto, UpdateMovieDto } from './types/movie';

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      retry: 1,
      refetchOnWindowFocus: false,
    },
  },
});

function MovieApp() {
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editingMovie, setEditingMovie] = useState<Movie | null>(null);
  const [selectedGenre, setSelectedGenre] = useState<number | null>(null);
  const [searchQuery, setSearchQuery] = useState('');

  const { data: movies = [], isLoading } = useMovies();
  const createMovieMutation = useCreateMovie();
  const updateMovieMutation = useUpdateMovie();
  const deleteMovieMutation = useDeleteMovie();

  // Filter movies based on genre and search query
  const filteredMovies = useMemo(() => {
    let filtered = movies;

    // Filter by genre
    if (selectedGenre) {
      filtered = filtered.filter(movie => movie.genraId === selectedGenre);
    }

    // Filter by search query
    if (searchQuery.trim()) {
      const query = searchQuery.toLowerCase();
      filtered = filtered.filter(movie =>
        movie.title.toLowerCase().includes(query) ||
        movie.storyline.toLowerCase().includes(query) ||
        movie.genraName?.toLowerCase().includes(query)
      );
    }

    return filtered;
  }, [movies, selectedGenre, searchQuery]);

  const handleAddMovie = () => {
    setEditingMovie(null);
    setIsModalOpen(true);
  };

  const handleEditMovie = (movie: Movie) => {
    setEditingMovie(movie);
    setIsModalOpen(true);
  };

  const handleDeleteMovie = async (id: number) => {
    if (window.confirm('Are you sure you want to delete this movie?')) {
      deleteMovieMutation.mutate(id);
    }
  };

  const handleSubmitMovie = async (data: CreateMovieDto | UpdateMovieDto) => {
    try {
      if (editingMovie) {
        await updateMovieMutation.mutateAsync({
          id: editingMovie.id,
          data: data as UpdateMovieDto,
        });
      } else {
        await createMovieMutation.mutateAsync(data as CreateMovieDto);
      }
      setIsModalOpen(false);
      setEditingMovie(null);
    } catch (error) {
      // Error handling is done in the mutation hooks
    }
  };

  const handleCloseModal = () => {
    setIsModalOpen(false);
    setEditingMovie(null);
  };

  const isFormLoading = createMovieMutation.isPending || updateMovieMutation.isPending;

  return (
    <div className="min-h-screen bg-dark-900">
      <Header onAddMovie={handleAddMovie} onSearch={setSearchQuery} />
      
      <main className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8 py-8">
        {/* Filters */}
        <div className="mb-8">
          <div className="flex items-center justify-between mb-4">
            <h2 className="text-2xl font-bold text-white">
              {searchQuery ? `Search Results for "${searchQuery}"` : 'Movie Collection'}
            </h2>
            <div className="text-sm text-dark-400">
              {filteredMovies.length} {filteredMovies.length === 1 ? 'movie' : 'movies'}
            </div>
          </div>
          
          <GenreFilter
            selectedGenre={selectedGenre}
            onGenreChange={setSelectedGenre}
          />
        </div>

        {/* Movies Grid */}
        <MovieGrid
          movies={filteredMovies}
          isLoading={isLoading}
          onEdit={handleEditMovie}
          onDelete={handleDeleteMovie}
        />
      </main>

      {/* Movie Form Modal */}
      <Modal
        isOpen={isModalOpen}
        onClose={handleCloseModal}
        title={editingMovie ? 'Edit Movie' : 'Add New Movie'}
        maxWidth="2xl"
      >
        <MovieForm
          movie={editingMovie || undefined}
          onSubmit={handleSubmitMovie}
          onCancel={handleCloseModal}
          isLoading={isFormLoading}
        />
      </Modal>

      <Toaster
        position="top-right"
        toastOptions={{
          duration: 4000,
          style: {
            background: '#1e293b',
            color: '#fff',
            border: '1px solid #334155',
          },
        }}
      />
    </div>
  );
}

function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <MovieApp />
    </QueryClientProvider>
  );
}

export default App;