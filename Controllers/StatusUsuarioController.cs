using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorUsuario.Models;
using GerenciadorUsuario.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorUsuario.Controllers
{
    public class StatusUsuarioController : Controller
    {
        private readonly StatusUsuarioRepository _statusUsuarioRepository;

        public StatusUsuarioController(StatusUsuarioRepository statusUsuarioRepository)
        {
            _statusUsuarioRepository = statusUsuarioRepository;
        }

        public async Task<IActionResult> Index()
        {
            var statusUsuarios = await _statusUsuarioRepository.ObterStatusUsuarios();
            return View(statusUsuarios);
        }

        [HttpPost("criar-status-usuario")]
        public async Task<IActionResult> CriarStatusUsuario(StatusUsuario statusUsuario)
        {

            try
            {  
                statusUsuario.NomeStatusUsuario = statusUsuario.NomeStatusUsuario.ToUpper();              
                await _statusUsuarioRepository.CriarStatusUsuario(statusUsuario);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new {message = ex.Message });
            }
            catch (Exception)
            {
                
                return BadRequest(new {message = "Erro ao cadastrar o status do usuario"});
            }
            
        }

        [HttpPost("{idStatus}/remover-status-usuario")] 
        public async Task<ActionResult> RemoverStatusUsuario(int idStatus)
        {
            try
            {
                var statusUsuario = await _statusUsuarioRepository.ExcluirStatusUsuario(idStatus);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Erro ao remover o status do usuario" });
            }
        }

    }
}