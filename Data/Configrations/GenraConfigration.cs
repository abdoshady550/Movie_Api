using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Movie_Api.Model.Eintites;

namespace Movie_Api.Data.Configrations
{
    public class GenraConfigration : IEntityTypeConfiguration<Genra>
    {
        public void Configure(EntityTypeBuilder<Genra> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id);

            builder.Property(e => e.Name)
                .HasColumnType("VARCHAR")
                .HasMaxLength(50)
                .IsRequired();

            builder.HasData(SeedGenres());

        }
        public static List<Genra> SeedGenres()
        {
            return new List<Genra>
        {
            new Genra { Id = 1, Name = "Action" },
            new Genra { Id = 2, Name = "Comedy" },
            new Genra { Id = 3, Name = "Drama" },
            new Genra { Id = 4, Name = "Horror" },
            new Genra { Id = 5, Name = "Sci-Fi" },
            new Genra { Id = 6, Name = "Romance" }
        };
        }
    }
}
