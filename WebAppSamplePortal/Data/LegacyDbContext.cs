using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using WebAppSamplePortal.Data.Abstractions;
using WebAppSamplePortal.Data.ModelMap;
using WebAppSamplePortal.Data.Models;

namespace WebAppSamplePortal.Data
{
    public class LegacyDbContext : DbContext
    {
        //private readonly DbConnection _connection;
        //private readonly DbTransaction _transaction;

        public LegacyDbContext(DbContextOptions options):base(options)
        {

        }

        //public LegacyDbContext(DbConnection connection)
        //{
        //    //_connection = uow.Property<DbConnection>();
        //    //_transaction = uow.Property<DbTransaction>();

        //    ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        //}

        //public DbSet<FornecedorDbModel> Fornecedores { get; set; }
        //public DbSet<LicitacaoDbModel> Licitacoes { get; set; }
        //public DbSet<FornecedorRecursoDbModel> FornecedorRecursos { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder builder)
        //{
        //    //builder.UseMySQL(_connection);
        //    //Database.UseTransaction(_transaction);
        //}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new FornecedorDbMap());
            builder.ApplyConfiguration(new FornecedorRecursoDbMap());
        }
    }
}
