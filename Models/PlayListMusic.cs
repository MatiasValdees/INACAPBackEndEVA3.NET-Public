namespace EVA3.Models
{
    public class PlayListMusic
    {
        public PlayList playList { get; set; }
        public int PlayListId { get; set; }

        public Music music { get; set; }
        public int MusicId { get; set; } 

    }
}
