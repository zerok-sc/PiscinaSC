using libSC.entidades;
using libSC.interfaces;
using Microsoft.EntityFrameworkCore;

namespace libSC.implementaciones
{
    public class PiscinasAplicacion : IPiscinasAplicacion
    {
        private IConexion conexion;
        private IAuditoriasAplicacion auditoriasAplicacion;

        public PiscinasAplicacion(IConexion conexion, IAuditoriasAplicacion auditoriasAplicacion)
        {
            this.conexion = conexion;
            this.auditoriasAplicacion = auditoriasAplicacion;
        }

        public Piscinas Insertar(Piscinas entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (Validar(entidad))
                throw new Exception("lbFaltaInformacion");

            if (this.conexion.Piscinas!.Any(piscina => piscina.Codigo == entidad.Codigo))
                throw new Exception("lbYaSeGuardo");

            this.conexion.Piscinas!.Add(entidad!);
            this.conexion.SaveChanges();

            this.auditoriasAplicacion.Registrar(new Auditorias
            {
                Tabla = "Piscinas",
                Operacion = "Insertar",
                Descripcion = $"Alta de la piscina {entidad.Codigo} - {entidad.Nombre} con capacidad {entidad.Capacidad}",
                Piscina = entidad.Id,
                Monto = 0m,
                Exitosa = true
            });

            return entidad;
        }

        public List<Piscinas> Consultar()
        {
            return this.conexion.Piscinas!
                .Where(piscina => piscina.Estado)
                .ToList();
        }

        public Piscinas Actualizar(Piscinas entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            if (Validar(entidad))
                throw new Exception("lbFaltaInformacion");

            if (this.conexion.Piscinas!.Any(piscina => piscina.Codigo == entidad.Codigo && piscina.Id != entidad.Id))
                throw new Exception("lbYaSeGuardo");

            var entry = this.conexion.Entry<Piscinas>(entidad);
            entry.State = EntityState.Modified;
            this.conexion.SaveChanges();

            this.auditoriasAplicacion.Registrar(new Auditorias
            {
                Tabla = "Piscinas",
                Operacion = "Actualizar",
                Descripcion = $"Modificacion de la piscina {entidad.Codigo} - {entidad.Nombre} con capacidad {entidad.Capacidad}",
                Piscina = entidad.Id,
                Monto = 0m,
                Exitosa = true
            });

            return entidad;
        }

        public List<Piscinas> Buscar(Piscinas entidad)
        {
            return this.conexion.Piscinas!
                .Where(piscina => piscina.Estado && piscina.Nombre!.Contains(entidad.Nombre!))
                .ToList();
        }

        public Piscinas Borrar(Piscinas entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var existente = this.conexion.Piscinas!
                .AsNoTracking()
                .FirstOrDefault(piscina => piscina.Id == entidad.Id);

            if (existente == null)
                throw new Exception("lbNoEncontrado");

            this.conexion.Piscinas!.Remove(existente!);
            this.conexion.SaveChanges();

            this.auditoriasAplicacion.Registrar(new Auditorias
            {
                Tabla = "Piscinas",
                Operacion = "Borrar",
                Descripcion = $"Baja de la piscina {existente.Codigo} - {existente.Nombre}",
                Piscina = null,
                Monto = 0m,
                Exitosa = true
            });

            return existente;
        }

        private bool Validar(Piscinas entidad)
        {
            return string.IsNullOrEmpty(entidad.Codigo) ||
                string.IsNullOrEmpty(entidad.Nombre) ||
                entidad.Capacidad <= 0 ||
                entidad.TarifaBase <= 0 ||
                entidad.FechaCreacion == default;
        }
    }
}
