using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorUsuario.Models;


namespace GerenciadorUsuario.ViewModels
{
    public class UsuarioViewModel
    {
        
        public int Id { get; set; }
        public int IdPapelUsuario { get; set; }
        public int IdStatusUsuario { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Foto { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public DateTime? DataUltimaAtualizacao { get; set; }
        public PapelUsuario PapelUsuario { get; set; }
        public StatusUsuario StatusUsuario { get; set; }


        public string ValidarFormulario()
        {
            if (string.IsNullOrEmpty(Nome) && string.IsNullOrEmpty(Email) && string.IsNullOrEmpty(Foto))
            {
                return "Preencha os campos Nome, Email e Foto.";
            }

            if (string.IsNullOrEmpty(Nome))
            {
                return "O preenchimento do campo Nome é obrigatório.";
            }

            if (string.IsNullOrEmpty(Email))
            {
                return "O preenchimento do campo Email é obrigatório.";
            }

            if (string.IsNullOrEmpty(Foto))
            {
                return "O preenchimento do campo foto é obrigatório.";
            }

            return string.Empty;
        }

    }
}