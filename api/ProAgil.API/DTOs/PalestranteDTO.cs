using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.API.DTOs
{
    public class PalestranteDTO
    {
        public int PalestranteId { get; set; }

        [Required]
        public string Nome { get; set; }
        public string Resumo { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public List<RedeSocialDTO> RedesSociais { get; set; }
        public List<EventoDTO> Eventos { get; set; } 
    }
}