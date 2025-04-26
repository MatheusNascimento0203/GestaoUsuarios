using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GerenciadorUsuario.Models;
using GerenciadorUsuario.Repository;
using GerenciadorUsuario.ViewModels;

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

    public IActionResult  Index()
    {
        return View();
    }


    [HttpGet ("buscar-usuarios")]
    public async Task<IActionResult> BuscarUsuario()
    {
        var usuarios = await _usuarioRepository.BuscarAsync();

        return View("_Grid", usuarios.OrderBy(x => x.Nome));       

    }

    [HttpGet ("criar-usuario")]
    public async Task<IActionResult> CriarUsuario()
    {
        var statusUsuarios = await _statusUsuarioRepository.ObterStatusUsuarios();
        var papelUsuarios = await _papelUsuarioRepository.ObterPapelUsuarios();

        if (statusUsuarios == null || papelUsuarios == null)
            return StatusCode(500, "Erro ao carregar dados para o formulário.");

        ViewBag.StatusUsuarios = statusUsuarios;
        ViewBag.PapelUsuarios = papelUsuarios;
        return View("_Form");
    }

    [HttpPost ("criar-usuario")]
    public async Task<IActionResult> CriarUsuario(UsuarioViewModel model)
    {
        if (model.ValidarFormulario() != string.Empty)
            return BadRequest( model.ValidarFormulario() );

        var usuario = await _usuarioRepository.BuscarPorEmailAsync(model.Email);

        if (usuario != null)
            return BadRequest(new { message = "Já existe um usuário cadastrado com esse email." });

        await _usuarioRepository.CriarAsync( new Usuario{
            Nome = model.Nome,
            Email = model.Email,
            Foto = model.Foto,
            IdPapelUsuario = model.IdPapelUsuario,
            IdStatusUsuario = model.IdStatusUsuario
        });
        return Ok();        
    }

    [HttpPost ("{usuarioId}/excluir-usuario")]
    public async Task<IActionResult> ExcluirUsuario(int usuarioId)
    {
        try
        {
            var usuario = await _usuarioRepository.ExcluirUsuario(usuarioId);
            return Ok(usuario);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            var mensagemErroCompleta = ex.InnerException?.Message ?? ex.Message;
            return BadRequest(new
            {
                message = "Erro ao excluir o usuario",
                detalhes = mensagemErroCompleta
            });
        }
    }

    [HttpGet ("{id}/atualizar-usuario")]
    public async Task<IActionResult> AtualizarUsuario(int id)
    {

        var usuario = await _usuarioRepository.BuscarPorIdAsync(id);

        if (usuario == null)
            return BadRequest(new { message = "Usuario não encontrado." });

        var statusUsuarios = await _statusUsuarioRepository.ObterStatusUsuarios();
        var papelUsuarios = await _papelUsuarioRepository.ObterPapelUsuarios();

        if (statusUsuarios == null || papelUsuarios == null)
            return StatusCode(500, "Erro ao carregar dados para o formulário.");

        var viewModel = new UsuarioViewModel
        {
            Id = usuario.Id,
            Nome = usuario.Nome,
            Email = usuario.Email,
            Foto = usuario.Foto,
            IdPapelUsuario = usuario.IdPapelUsuario,
            IdStatusUsuario = usuario.IdStatusUsuario
        };

        ViewBag.StatusUsuarios = statusUsuarios;
        ViewBag.PapelUsuarios = papelUsuarios;
        return View("_Form", viewModel);        
    }

    [HttpPost ("{id}/atualizar-usuario")]
    public async Task<IActionResult> AtualizarUsuario(UsuarioViewModel model)
    {
        if (model.ValidarFormulario() != string.Empty)
            return BadRequest(new { message = model.ValidarFormulario() });

        var usuario = await _usuarioRepository.BuscarPorIdAsync(model.Id);


        if (usuario == null)
            return BadRequest(new { message = "Usuario não encontrado." });
        
        usuario.Nome = model.Nome;
        usuario.Email = model.Email;
        usuario.Foto = model.Foto;
        usuario.IdPapelUsuario = model.IdPapelUsuario;
        usuario.IdStatusUsuario = model.IdStatusUsuario;

        await _usuarioRepository.AtualizarAsync(usuario);
        return Ok();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
