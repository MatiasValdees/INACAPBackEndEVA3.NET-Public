namespace EVA3.Models.ViewsModel
{
    public class ViewModelAddMusic
    {
        public Music music {  get; set; }
        public IEnumerable<PlayList> playlists { get; set; }
    }
}
