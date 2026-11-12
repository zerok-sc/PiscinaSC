using libSC.entidades;
using libSC.interfaces;
using Microsoft.EntityFrameworkCore;

namespace libSC.implementaciones
{
    public class AuditoriasAplicacion : IAuditoriasAplicacion
    {
        private IConexion conexion;

        public AuditoriasAplicacion(IConexion conexion)
        {
            this.conexion = conexion;
        }

        public Auditorias Insertar(Auditorias entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            entidad.Fecha = DateTime.Now;
            this.conexion.Auditorias!.Add(entidad!);
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Auditorias> Consultar()
        {
            return this.conexion.Auditorias!
                .Include(registro => registro._Reserva)
                .Include(registro => registro._Piscina)
                .OrderByDescending(registro => registro.Fecha)
                .ToList();
        }

        public Auditorias Actualizar(Auditorias entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var entry = this.conexion.Entry<Auditorias>(entidad);
            entry.State = EntityState.Modified;
            this.conexion.SaveChanges();
            return entidad;
        }

        public List<Auditorias> Buscar(Auditorias entidad)
        {
            return this.conexion.Auditorias!
                .Where(registro => registro.Descripcion!.Contains(entidad.Descripcion!))
                .Include(registro => registro._Reserva)
                .Include(registro => registro._Piscina)
                .OrderByDescending(registro => registro.Fecha)
                .ToList();
        }

        public Auditorias Borrar(Auditorias entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            this.conexion.Auditorias!.Remove(entidad!);
            this.conexion.SaveChanges();
            return entidad;
        }

        public Auditorias Registrar(Auditorias entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            entidad.Fecha = DateTime.Now;
            this.conexion.Auditorias!.Add(entidad);
            this.conexion.SaveChanges();
            return entidad;
        }
    }
}
