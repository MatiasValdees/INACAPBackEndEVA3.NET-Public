using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EVA3.Models
{
    public class Music
    {
        [Key]
        public int IdCancion { get; set; }
        [Required]
        public string Titulo{ get; set; }
        [Required]
        public string Artista { get; set; }
        [Required]
        public string Album { get; set; }
        [Required]
        public string Duracion { get; set; }
        public List<PlayListMusic> playListMusics { get; set; }


    }
}
