using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorUsuario.Repository;
using Microsoft.AspNetCore.Mvc;
using GerenciadorUsuario.Models;


namespace GerenciadorUsuario.Controllers
{
    public class PapelUsuarioController : Controller
    {
        private readonly PapelUsuarioRepository _papelUsuarioRepository;

        public PapelUsuarioController(PapelUsuarioRepository papelUsuarioRepository)
        {
            _papelUsuarioRepository = papelUsuarioRepository;
        }

        public async Task<IActionResult> Index()
        {
            var papelUsuarios = await _papelUsuarioRepository.ObterPapelUsuarios();
            return View(papelUsuarios);
        }

        [HttpPost]
        public async Task<IActionResult> CriarPapelUsuario(PapelUsuario papelUsuario)
        {
            
                try
                {
                    papelUsuario.NomePapelUsuario = papelUsuario.NomePapelUsuario.ToUpper();
                    await _papelUsuarioRepository.CriarPapelUsuario(papelUsuario);
                    return Ok();
                }
                catch (InvalidOperationException ex)
                {
                    return BadRequest(new {message = ex.Message });
                }
                catch (Exception)
                {
                    return BadRequest(new {message = "Erro ao cadastrar o papel do usuario"});
                }            
        }

        [HttpPost("{idPapel}/remover-papel-usuario")]
        public async Task<ActionResult> RemoverPapelUsuario(int idPapel)
        {
            try
            {
                var papelUsuario = await _papelUsuarioRepository.ExcluirPapelUsuario(idPapel);
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Erro ao remover o papel do usuario" });
            }
        }
    }
}