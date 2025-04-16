using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorUsuario.Data;
using GerenciadorUsuario.Models;
using Microsoft.EntityFrameworkCore;

namespace GerenciadorUsuario.Repository
{
    public class PapelUsuarioRepository
    {
        private readonly AppDbContext _context;

        public PapelUsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PapelUsuario>> ObterPapelUsuarios()
        {
            return await _context.PapelUsuario.OrderBy(p => p.NomePapelUsuario).ToListAsync();
        }

        public async Task CriarPapelUsuario(PapelUsuario papelUsuario)
        {
            var papelExistente = await _context.PapelUsuario.AnyAsync(p => p.NomePapelUsuario == papelUsuario.NomePapelUsuario);

            if (papelExistente)
            {
                throw new InvalidOperationException("Papel já cadastrado");
            }

            _context.PapelUsuario.Add(papelUsuario);
            await _context.SaveChangesAsync();
        }

        public async Task<PapelUsuario> ExcluirPapelUsuario(int idPapel)
        {
            var papelUsuario = await _context.PapelUsuario.FindAsync(idPapel);

            if (papelUsuario == null)
            {
                throw new InvalidOperationException("Papel não encontrado");
            }

            _context.PapelUsuario.Remove(papelUsuario);
            await _context.SaveChangesAsync();

            return papelUsuario;
        }
    }
}