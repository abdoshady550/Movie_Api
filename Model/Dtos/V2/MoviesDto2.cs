namespace Movie_Api.Model.Dtos.V2
{
    public class MovieDto2
    {

        public string? Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        public string? Storyline { get; set; }
        public IFormFile Poster { get; set; }
        public string? GenraName { get; set; }
        public int GenraId { get; set; }

    }
    public class ReadMovieDto2
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Rate { get; set; }
        public string Storyline { get; set; }
        public int Year { get; set; }
        public string? PosterUrl { get; set; }
        public int GenraId { get; set; }
        public string? GenraName { get; set; }
    }
    public class UpdateMovieDto2
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
