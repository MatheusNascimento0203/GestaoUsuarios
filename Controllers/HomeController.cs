using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GerenciadorUsuario.Models;
using GerenciadorUsuario.Repository;

namespace GerenciadorUsuario.Controllers;

public class HomeController : Controller
{   
    private readonly StatusUsuarioRepository _statusUsuarioRepository; 
    private readonly PapelUsuarioRepository _papelUsuarioRepository;
    
    public HomeController(StatusUsuarioRepository statusUsuarioRepository, PapelUsuarioRepository papelUsuarioRepository)
    {
        _papelUsuarioRepository = papelUsuarioRepository;    
        _statusUsuarioRepository = statusUsuarioRepository;
    }

    public async Task<IActionResult>  Index()
    {
        ViewBag.StatusUsuarios = await _statusUsuarioRepository.ObterStatusUsuarios();
        ViewBag.PapelUsuarios = await _papelUsuarioRepository.ObterPapelUsuarios();
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
