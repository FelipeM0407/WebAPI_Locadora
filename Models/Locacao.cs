using System;
using System.ComponentModel.DataAnnotations;

namespace Locadora_CineClub.Models
{
    public class Locacao
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int FilmeId { get; set; }
        public DateTime Dt_Alocacao { get; set; }
        public DateTime Dt_Entrega { get; set; }
        public DateTime Dt_Devolvido { get; set; }

    }
}
