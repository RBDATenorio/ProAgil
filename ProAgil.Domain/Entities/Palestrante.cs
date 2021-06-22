using System.Collections.Generic;

namespace ProAgil.Domain.Entities
{
    public class Palestrante
    {
        public int PalestranteId { get; set; }
        public string Nome { get; set; }
        public string Resumo { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public List<RedeSocial> RedesSociais { get; set; }
        public List<PalestranteEvento> PalestrantesEventos { get; set; }

    }
}