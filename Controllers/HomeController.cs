using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GerenciadorUsuario.Models;
using GerenciadorUsuario.Repository;

namespace GerenciadorUsuario.Controllers;

public class HomeController : Controller
{   
    private readonly StatusUsuarioRepository _statusUsuarioRepository;
    
    public HomeController(StatusUsuarioRepository statusUsuarioRepository)
    {
        _statusUsuarioRepository = statusUsuarioRepository;
    }

    public async Task<IActionResult>  Index()
    {
        ViewBag.StatusUsuarios = await _statusUsuarioRepository.ObterStatusUsuarios();
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
