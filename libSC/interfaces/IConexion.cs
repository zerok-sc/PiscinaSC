using libSC.entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace libSC.interfaces
{
    public interface IConexion
    {
        DbSet<Piscinas>? Piscinas { get; set; }
        DbSet<Reservas>? Reservas { get; set; }
        DbSet<Auditorias>? Auditorias { get; set; }

        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
    }
}
