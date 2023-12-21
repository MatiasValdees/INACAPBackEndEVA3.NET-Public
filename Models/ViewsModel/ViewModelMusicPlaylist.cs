namespace EVA3.Models.ViewsModel
{
    public class ViewModelMusicPlaylist
    {
        public Music music {get; set;}
        public IEnumerable<PlayList> playlists { get; set;}
    }
}
