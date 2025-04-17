using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorUsuario.Data;
using GerenciadorUsuario.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorUsuario.Repository
{
    public class UsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CriarUsuario(Usuario usuario)
        {
            var usuarioExistente = await _context.Usuario.AnyAsync(u => u.Email == usuario.Email);

            if (usuarioExistente)
            {
                throw new InvalidOperationException("Usuário já cadastrado");
            }

            await _context.Usuario.AddAsync(usuario);
            await _context.SaveChangesAsync();
        }

        
    }
}