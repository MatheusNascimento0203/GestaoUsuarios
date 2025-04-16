using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorUsuario.Models
{
    public class StatusUsuario
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public int Id { get; set; }
        [Required (ErrorMessage = "O campo Nome do Status é obrigatório.")]
        public string NomeStatusUsuario { get; set; }
    }
}