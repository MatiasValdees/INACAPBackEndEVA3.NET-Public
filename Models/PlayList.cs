using System.ComponentModel.DataAnnotations;

namespace EVA3.Models
{
    public class PlayList
    {
        [Key]
        public int PlaylistId { get; set; }
        [Required]
        public string Name { get; set; }

        public List<PlayListMusic> playListMusics { get; set; }
    }
}
