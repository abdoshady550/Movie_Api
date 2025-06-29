namespace Movie_Api.Model.Eintites
{
    public class Movie
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        public string? Storyline { get; set; }
        public byte[]? Poster { get; set; }

        public int GenraId { get; set; }

        public Genra? Genra { get; set; }


    }
}
