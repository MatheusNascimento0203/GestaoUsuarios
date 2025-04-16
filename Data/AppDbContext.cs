using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GerenciadorUsuario.Models;
using Microsoft.EntityFrameworkCore;


namespace GerenciadorUsuario.Data
{
    public class AppDbContext : DbContext
    {
        
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<PapelUsuario> PapelUsuario { get; set; }        
        public DbSet<StatusUsuario> StatusUsuario { get; set; }

    }
}