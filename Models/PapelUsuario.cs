using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GerenciadorUsuario.Models
{
    public class PapelUsuario
    {   
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo Nome do Papel do usuário é obrigatório.")]
        public string NomePapelUsuario { get; set; }
    }
}