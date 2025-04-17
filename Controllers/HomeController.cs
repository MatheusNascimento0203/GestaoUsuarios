using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GerenciadorUsuario.Models;
using GerenciadorUsuario.Repository;

namespace GerenciadorUsuario.Controllers;

public class HomeController : Controller
{   
    private readonly StatusUsuarioRepository _statusUsuarioRepository; 
    private readonly PapelUsuarioRepository _papelUsuarioRepository;
    private readonly UsuarioRepository _usuarioRepository;
    
    public HomeController(StatusUsuarioRepository statusUsuarioRepository, PapelUsuarioRepository papelUsuarioRepository, UsuarioRepository usuarioRepository)
    {
        _papelUsuarioRepository = papelUsuarioRepository;    
        _statusUsuarioRepository = statusUsuarioRepository;
        _usuarioRepository = usuarioRepository;
    }

    public async Task<IActionResult>  Index()
    {
        ViewBag.StatusUsuarios = await _statusUsuarioRepository.ObterStatusUsuarios();
        ViewBag.PapelUsuarios = await _papelUsuarioRepository.ObterPapelUsuarios();
        return View();
    }

    [HttpPost ("criar-usuario")]
    public async Task<IActionResult> CriarUsuario(Usuario usuario)
    {

        if (usuario.Nome == null || usuario.Nome == string.Empty)
        {
            return BadRequest(new { message = "O campo Nome é obrigatório." });
        }

        if (usuario.Email == null || usuario.Email == string.Empty)
        {
            return BadRequest(new { message = "O campo Email é obrigatório." });
        }

        if (usuario.Foto == null || usuario.Foto == string.Empty)
        {
            return BadRequest(new { message = "O campo foto é obrigatório." });
        }
     
        try
        {
            await _usuarioRepository.CriarUsuario(usuario);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception)
        {
            return BadRequest(new { message = "Erro ao cadastrar o usuario" });
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
