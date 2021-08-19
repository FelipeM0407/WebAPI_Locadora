using System;
using System.ComponentModel.DataAnnotations;

namespace Locadora_CineClub.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(60, ErrorMessage = "Nome do Cliente deve ter entre 3 e 60 caracteres !")]
        [MinLength(3, ErrorMessage = "Nome do Cliente deve ter entre 3 e 60 caracteres !")]
        public string Nome { get; set; }
        public bool Ativo { get; set; }
        public DateTime Dt_Cadastro { get; set; }

    }
}
