using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorUsuario.Data;
using GerenciadorUsuario.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorUsuario.Repository
{
    public class StatusUsuarioRepository
    {
        private readonly AppDbContext _context;

        public StatusUsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async  Task CriarStatusUsuario( StatusUsuario statusUsuario)
        {
            var statusExistente = await _context.StatusUsuario.AnyAsync(s => s.NomeStatusUsuario == statusUsuario.NomeStatusUsuario);

            if (statusExistente)
            {
                throw new InvalidOperationException("Status já cadastrado");
            }

            _context.StatusUsuario.Add(statusUsuario);
            await _context.SaveChangesAsync();
        }

        public async Task<StatusUsuario> ExcluirStatusUsuario(int idStatus)
        {
            var statusUsuario = await _context.StatusUsuario.FindAsync(idStatus);

            if (statusUsuario == null)
            {
                throw new InvalidOperationException("Status não encontrado");
            }

            _context.StatusUsuario.Remove(statusUsuario);
            await _context.SaveChangesAsync();
            return statusUsuario;
        }

        public async Task <IEnumerable<StatusUsuario>> ObterStatusUsuarios()
        {
            return await _context.StatusUsuario.OrderBy(s => s.NomeStatusUsuario).ToListAsync();
        }
    }
}