using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie_Api.Model.Eintites;

namespace Movie_Api.Data.Configrations
{
    public class MovieConfigration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Title)
                .HasColumnType("VARCHAR")
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(e => e.Storyline)
                .HasColumnType("VARCHAR")
                .HasMaxLength(2500)
                .IsRequired();

            builder.Property(e => e.Year)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(e => e.Rate)
                .HasColumnType("float")
                .IsRequired();
            builder.HasData(SeedMovies());
        }
        public static List<Movie> SeedMovies()
        {

            return new List<Movie>
        {
            new Movie
            {
                Id = 1,
                Title = "Inception",
                Year = 2010,
                Rate = 8.8,
                Storyline = "A skilled thief is given a chance at redemption if he can successfully perform an inception.",
                Poster = null, // سيبها null أو ضيف بيانات Base64 لو حابب
                GenraId = 5 // Sci-Fi
            },
            new Movie
            {
                Id = 2,
                Title = "The Conjuring",
                Year = 2013,
                Rate = 7.5,
                Storyline = "Paranormal investigators help a family terrorized by a dark presence.",
                Poster = null,
                GenraId = 4 // Horror
            },
            new Movie
            {
                Id = 3,
                Title = "Titanic",
                Year = 1997,
                Rate = 7.8,
                Storyline = "A young couple falls in love on the ill-fated RMS Titanic.",
                Poster = null,
                GenraId = 6 // Romance
            },
            new Movie
            {
                Id = 4,
                Title = "The Matrix",
                Year = 1999,
                Rate = 8.7,
                Storyline = "A computer hacker learns about the true nature of his reality.",
                Poster = null,
                GenraId = 5 // Sci-Fi
            },
            new Movie
            {
                Id = 5,
                Title = "The Dark Knight",
                Year = 2008,
                Rate = 9.0,
                Storyline = "Batman faces the Joker, a criminal mastermind spreading chaos in Gotham.",
                Poster = null,
                GenraId = 1 // Action
            }
        };
        }
    }
}
