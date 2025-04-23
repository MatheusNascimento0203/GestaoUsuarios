using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace GerenciadorUsuario.Models
{
    public class Usuario
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Selecione o Papel do Usuário.")]
        public int IdPapelUsuario { get; set; }
        [Required(ErrorMessage = "Selecione o Status do Usuário.")]
        public int IdStatusUsuario { get; set; }
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo foto é obrigatório.")]
        public string Foto { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime? DataUltimaAtualizacao { get; set; }
        [ForeignKey("IdPapelUsuario")]
        [ValidateNever]
        public PapelUsuario PapelUsuario { get; set; }
        [ForeignKey("IdStatusUsuario")]
        [ValidateNever]
        public StatusUsuario StatusUsuario { get; set; }
        
    }
}