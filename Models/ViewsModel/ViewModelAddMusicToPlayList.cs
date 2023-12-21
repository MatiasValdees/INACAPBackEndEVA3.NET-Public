namespace EVA3.Models.ViewsModel
{
    public class ViewModelAddMusicToPlayList
    {
        public PlayList PlayList { get; set; }
        public IEnumerable<Music> Musics { get; set; }
    }
}
