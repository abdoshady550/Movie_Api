import { useState } from 'react';
import { Film, Plus, Settings, Search } from 'lucide-react';
import { setApiVersion } from '../../lib/api';

interface HeaderProps {
  onAddMovie: () => void;
  onSearch: (query: string) => void;
}

export function Header({ onAddMovie, onSearch }: HeaderProps) {
  const [searchQuery, setSearchQuery] = useState('');
  const [apiVersion, setCurrentApiVersion] = useState<'1.0' | '2.0'>('2.0');

  const handleVersionChange = (version: '1.0' | '2.0') => {
    setCurrentApiVersion(version);
    setApiVersion(version);
  };

  const handleSearchSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSearch(searchQuery);
  };

  return (
    <header className="glass-effect sticky top-0 z-40 border-b border-dark-700/50">
      <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div className="flex items-center justify-between h-16">
          {/* Logo */}
          <div className="flex items-center space-x-3">
            <div className="bg-gradient-to-r from-primary-500 to-purple-600 p-2 rounded-xl">
              <Film className="w-6 h-6 text-white" />
            </div>
            <div>
              <h1 className="text-xl font-bold bg-gradient-to-r from-white to-dark-300 bg-clip-text text-transparent">
                MovieHub
              </h1>
              <p className="text-xs text-dark-400">Professional Movie Management</p>
            </div>
          </div>

          {/* Search Bar */}
          <form onSubmit={handleSearchSubmit} className="flex-1 max-w-md mx-8">
            <div className="relative">
              <Search className="absolute left-3 top-1/2 transform -translate-y-1/2 text-dark-400 w-4 h-4" />
              <input
                type="text"
                placeholder="Search movies..."
                value={searchQuery}
                onChange={(e) => setSearchQuery(e.target.value)}
                className="w-full pl-10 pr-4 py-2 bg-dark-700/50 border border-dark-600 rounded-lg text-white placeholder-dark-400 focus:outline-none focus:ring-2 focus:ring-primary-500 focus:border-transparent transition-all duration-200"
              />
            </div>
          </form>

          {/* Actions */}
          <div className="flex items-center space-x-4">
            {/* API Version Selector */}
            <div className="flex items-center space-x-2">
              <Settings className="w-4 h-4 text-dark-400" />
              <select
                value={apiVersion}
                onChange={(e) => handleVersionChange(e.target.value as '1.0' | '2.0')}
                className="bg-dark-700 border border-dark-600 rounded-lg px-3 py-1 text-sm text-white focus:outline-none focus:ring-2 focus:ring-primary-500"
              >
                <option value="1.0">API v1.0</option>
                <option value="2.0">API v2.0</option>
              </select>
            </div>

            {/* Add Movie Button */}
            <button
              onClick={onAddMovie}
              className="btn-primary flex items-center space-x-2"
            >
              <Plus className="w-4 h-4" />
              <span className="hidden sm:inline">Add Movie</span>
            </button>
          </div>
        </div>
      </div>
    </header>
  );
}