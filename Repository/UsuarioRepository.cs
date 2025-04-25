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

        public async Task<List<Usuario>> BuscarAsync(string nome = null, string email = null, string papelUsuario = null, string statusUsuario = null )        
        {
            var query = _context.Usuario.AsQueryable();

            if (!string.IsNullOrEmpty(nome) || !string.IsNullOrEmpty(email) || !string.IsNullOrEmpty(papelUsuario) || !string.IsNullOrEmpty(statusUsuario))           
            {
                query = query.Where(u => EF.Functions.Like(u.Nome, $"%{nome}%") || 
                                         EF.Functions.Like(u.Email, $"%{email}%") || 
                                         EF.Functions.Like(u.PapelUsuario.NomePapelUsuario, $"%{papelUsuario}%") ||
                                         EF.Functions.Like(u.StatusUsuario.NomeStatusUsuario, $"%{statusUsuario}%"));
            }
 
            return await query.Include(u => u.PapelUsuario)
                .Include(u => u.StatusUsuario)
                .ToListAsync();
        }

        public async Task<Usuario> BuscarPorIdAsync(int id)
        {
            return await _context.Usuario.Include(u => u.PapelUsuario)
                .Include(u => u.StatusUsuario)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public Task CriarAsync(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            return _context.SaveChangesAsync();
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

        public async Task AtualizarAsync( Usuario usuario)
        {
            _context.Usuario.Update(usuario);
            await _context.SaveChangesAsync();
        }
        
    }
}