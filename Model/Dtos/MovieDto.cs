using Movie_Api.Model.Eintites;

namespace Movie_Api.Model.Dtos
{
    public class MovieDto
    {

        public string? Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        public string? Storyline { get; set; }
        public IFormFile Poster { get; set; }
        public string? GenraName { get; set; }
        public int GenraId { get; set; }

    }
    public class ReadMovieDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Rate { get; set; }
        public string Storyline { get; set; }
        public int Year { get; set; }
        public byte[] Poster { get; set; }
        public int GenraId { get; set; }
        public string? GenraName { get; set; }
    }
    public class UpdateMovieDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int? Year { get; set; }
        public double? Rate { get; set; }
        public string? Storyline { get; set; }
        public IFormFile? Poster { get; set; }

        public int? GenraId { get; set; }
        public string? GenraName { get; set; }

    }
}
