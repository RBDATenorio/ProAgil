using System.ComponentModel.DataAnnotations;

namespace ProAgil.API.DTOs
{
    public class RedeSocialDTO
    {
        public int RedeSocialId { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string URL { get; set; }
    }
}