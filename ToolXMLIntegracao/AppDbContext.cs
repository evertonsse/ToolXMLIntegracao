using Microsoft.EntityFrameworkCore;
using ToolXMLIntegracao.Models;

namespace ToolXMLIntegracao {
    public class AppDbContext : DbContext {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
        }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Contador> Contadores { get; set; }
        public DbSet<EmpresaContador> EmpresaContadores { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Empresa>(entity => {
                entity.HasKey(e => e.CNPJ);
                entity.HasIndex(e => e.CNPJ).IsUnique();
                entity.HasQueryFilter(e => e.DeletedAt == null);
            });

            modelBuilder.Entity<Documento>(entity => {
                entity.HasKey(d => d.ChaveAcesso);
                entity.HasIndex(d => d.ChaveAcesso).IsUnique();

                entity.HasOne(d => d.Empresa)
                      .WithMany(e => e.Documentos)
                      .HasForeignKey(d => d.CNPJ)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasQueryFilter(d => d.DeletedAt == null);
            });

            modelBuilder.Entity<Contador>(entity => {
                entity.HasKey(c => c.CpfCnpj);
                entity.HasIndex(c => c.CpfCnpj).IsUnique();
                entity.HasQueryFilter(c => c.DeletedAt == null);
            });


            modelBuilder.Entity<EmpresaContador>(entity => {
                entity.HasKey(ec => new { ec.CnpjEmpresa, ec.CnpjContador });
                entity.HasOne(ec => ec.Empresa)
                      .WithMany(e => e.EmpresaContadores)
                      .HasForeignKey(ec => ec.CnpjEmpresa)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(ec => ec.Contador)
                      .WithMany(c => c.EmpresaContadores)
                      .HasForeignKey(ec => ec.CnpjContador)
                      .HasPrincipalKey(c => c.CpfCnpj)
                      .OnDelete(DeleteBehavior.Cascade);
            });


        }

        public override int SaveChanges() {
            UpdateTimestamps();
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default) {
            UpdateTimestamps();
            return await base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateTimestamps() {
            var entries = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries) {
                if (entry.Entity is Empresa empresa) {
                    if (entry.State == EntityState.Added)
                        empresa.CreatedAt = DateTime.UtcNow;
                    empresa.UpdatedAt = DateTime.UtcNow;
                } else if (entry.Entity is Documento documento) {
                    if (entry.State == EntityState.Added)
                        documento.CreatedAt = DateTime.UtcNow;
                    documento.UpdatedAt = DateTime.UtcNow;
                } else if (entry.Entity is Contador contador) {
                    if (entry.State == EntityState.Added)
                        contador.CreatedAt = DateTime.UtcNow;
                    contador.UpdatedAt = DateTime.UtcNow;
                }
            }
        }

    }

}
