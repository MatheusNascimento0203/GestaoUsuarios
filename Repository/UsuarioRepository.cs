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

        public async Task<List<Usuario>> ObterUsuarios()
        {
            return await _context.Usuario
                .Include(u => u.PapelUsuario)
                .Include(u => u.StatusUsuario)
                .ToListAsync();
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

        public async Task<Usuario> ExcluirUsuario(int id)
        {
            var usuario = await _context.Usuario.FindAsync(id);

            if (usuario == null)
            {
                throw new InvalidOperationException("Usuário não encontrado");
            }

            _context.Usuario.Remove(usuario);
            await _context.SaveChangesAsync();

            return usuario;
        }

        public async Task<Usuario> AtualizarUsuario(int usuarioId, Usuario usuario)
        {
            var usuarioExistente = await _context.Usuario.FindAsync(usuarioId);
            var usuarioEmailExistente = await _context.Usuario.AnyAsync(u => u.Email == usuario.Email && u.Id != usuarioId);

            if (usuarioExistente == null)
            {
                throw new InvalidOperationException("Usuário não encontrado");
            }

            if (usuarioEmailExistente)
            {
                throw new InvalidOperationException("Email já cadastrado");
            }

            usuarioExistente.Nome = usuario.Nome;
            usuarioExistente.Email = usuario.Email;
            usuarioExistente.Foto = usuario.Foto;
            usuarioExistente.IdPapelUsuario = usuario.IdPapelUsuario;
            usuarioExistente.IdStatusUsuario = usuario.IdStatusUsuario;
            usuarioExistente.DataUltimaAtualizacao = DateTime.Now;

            _context.Usuario.Update(usuario);
            await _context.SaveChangesAsync();
            return usuarioExistente;
        }
        
    }
}