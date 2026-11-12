using libSC.entidades;
using libSC.interfaces;
using libSC.nucleo;
using Microsoft.EntityFrameworkCore;

namespace libSC.implementaciones
{
    public class Conexion : DbContext, IConexion
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(DatosGenerales.StringConexion()!, p => { });
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder modelo)
        {
            modelo.Entity<Auditorias>()
                .HasOne(auditoria => auditoria._Reserva)
                .WithMany()
                .HasForeignKey(auditoria => auditoria.Reserva)
                .OnDelete(DeleteBehavior.SetNull);

            modelo.Entity<Auditorias>()
                .HasOne(auditoria => auditoria._Piscina)
                .WithMany()
                .HasForeignKey(auditoria => auditoria.Piscina)
                .OnDelete(DeleteBehavior.SetNull);
        }

        public DbSet<Piscinas>? Piscinas { get; set; }
        public DbSet<Reservas>? Reservas { get; set; }
        public DbSet<Auditorias>? Auditorias { get; set; }
    }
}
