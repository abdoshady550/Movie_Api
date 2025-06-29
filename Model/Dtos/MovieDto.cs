using Movie_Api.Model.Eintites;

namespace Movie_Api.Model.Dtos
{
    public class MovieDto
    {
        public string? Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        public string? Storyline { get; set; }
        public IFormFile? Poster { get; set; }

        public int GenraId { get; set; }

        public Genra? Genra { get; set; }

    }
}
