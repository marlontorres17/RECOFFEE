using Dapper;
using Entity.Model.Operational;
using Entity.Model.Parameter;
using Entity.Model.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Model.Context
{
    public class ApplicationDbContext : DbContext
    {
        protected readonly IConfiguration _configuration;
        public object Module;
        public object Person;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración para evitar ciclos o múltiples cascadas en la relación Farm-Person
            modelBuilder.Entity<Farm>()
                .HasOne(f => f.Person)
                .WithMany()  // Si hay una relación inversa, especifícala aquí
                .HasForeignKey(f => f.PersonId)
                .OnDelete(DeleteBehavior.NoAction); // Evitar cascada al eliminar

            // Configuración para la relación Farm-City
            modelBuilder.Entity<Farm>()
                .HasOne(f => f.City)
                .WithMany()
                .HasForeignKey(f => f.CityId)
                .OnDelete(DeleteBehavior.NoAction); // Evitar cascada al eliminar

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
        }

        public readonly struct DapperEFCoreCommand : IDisposable
        {
            public DapperEFCoreCommand(DbContext context, string text, object parameters, int? timeout, CommandType? type, CancellationToken ct)
            {
                var transaction = context.Database.CurrentTransaction?.GetDbTransaction();
                var commandType = type ?? CommandType.Text;
                var commandTimeout = timeout ?? context.Database.GetCommandTimeout() ?? 30;

                Definition = new CommandDefinition(
                    text,
                    parameters,
                    transaction,
                    commandTimeout,
                    commandType,
                    cancellationToken: ct);
            }

            public CommandDefinition Definition { get; }

            public void Dispose()
            {
            }
        }

        public override int SaveChanges()
        {
            EnsureAudit();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            EnsureAudit();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        public async Task<IEnumerable<T>> QueryAsync<T>(string text, object parameters = null, int? timeout = null, CommandType? type = null)
        {
            using var command = new DapperEFCoreCommand(this, text, parameters, timeout, type, CancellationToken.None);
            var connection = this.Database.GetDbConnection();
            return await connection.QueryAsync<T>(command.Definition);
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string text, object parameters = null, int? timeout = null, CommandType? type = null)
        {
            using var command = new DapperEFCoreCommand(this, text, parameters, timeout, type, CancellationToken.None);
            var connection = this.Database.GetDbConnection();
            return await connection.QueryFirstOrDefaultAsync<T>(command.Definition);
        }

        private void EnsureAudit()
        {
            ChangeTracker.DetectChanges();
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.OnCreate();  // Llamamos a OnCreate al agregar un nuevo registro
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.OnUpdate();  // Llamamos a OnUpdate cuando el registro es modificado
                }
                else if (entry.State == EntityState.Deleted || !entry.Entity.State)
                {
                    entry.Entity.State = false; // Marcamos el estado como false (lógico) al eliminar
                    entry.Entity.OnDelete();    // Llamamos a OnDelete cuando el registro es eliminado
                    entry.State = EntityState.Modified; // Se asegura que no elimine físicamente, sino lógico
                }
            }
        }

        // security
        public DbSet<Role> roles => Set<Role>();
        public DbSet<View> views => Set<View>();
        public DbSet<Security.Module> modules => Set<Security.Module>();
        public DbSet<RoleView> roles_views => Set<RoleView>();
        public DbSet<Person> persons => Set<Person>();
        public DbSet<User> users => Set<User>();
        public DbSet<UserRole> users_roles => Set<UserRole>();

        // parameter
        public DbSet<Country> countrys => Set<Country>();
        public DbSet<Department> departments => Set<Department>();
        public DbSet<City> citys => Set<City>();

        // operational
        public DbSet<Farm> farms => Set<Farm>();
        public DbSet<Benefit> benefits => Set<Benefit>();
        public DbSet<Lot> lots => Set<Lot>();
        public DbSet<Harvest> harvests => Set<Harvest>();
        public DbSet<PersonBenefit> personBenefits => Set<PersonBenefit>();
        public DbSet<CollectionDetail> collectionDetails => Set<CollectionDetail>();
        public DbSet<Liquidation> liquidations => Set<Liquidation>();
    }
}
