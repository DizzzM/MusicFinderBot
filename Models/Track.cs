namespace MusicFinderBot.Models
{
    public class Track
    {
        public string Id { get; set; }
        public string Artist { get; set; }
        public string ArtistId { get; set; }
        public string Name { get; set; }
        public string SpotifyURL { get; set; }
        public string ImageURL { get; set; }
        public string Lyrics { get; set; }
    }
}
