using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.API.DTOs
{
    public class EventoDTO
    {
        public int EventoId { get; set; }
        public string ImagemURL  { get; set; }
        public string Local { get; set; }
        public string DataEvento { get; set;}

        [Required (ErrorMessage = "Tema obrigatório")]
        public string Tema { get; set; }

        [Range(2, 150, ErrorMessage = "Limitado entre 2 e 150 pessoas")]
        public int QtdPessoas { get; set; }
        public string Telefone { get; set; }
        
        [EmailAddress]
        public string Email { get; set; }
        public List<LoteDTO> Lotes { get; set; }
        public List<RedeSocialDTO> RedesSociais { get; set; }
        public List<PalestranteDTO> Palestrantes { get; set; }

    }
}