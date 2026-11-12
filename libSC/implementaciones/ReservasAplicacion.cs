using libSC.entidades;
using libSC.interfaces;
using libSC.nucleo;
using Microsoft.EntityFrameworkCore;

namespace libSC.implementaciones
{
    public class ReservasAplicacion : IReservasAplicacion
    {
        private IConexion conexion;
        private IAuditoriasAplicacion auditoriasAplicacion;

        public ReservasAplicacion(IConexion conexion, IAuditoriasAplicacion auditoriasAplicacion)
        {
            this.conexion = conexion;
            this.auditoriasAplicacion = auditoriasAplicacion;
        }

        public Reservas Insertar(Reservas entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");

            if (Validar(entidad))
                throw new Exception("lbFaltaInformacion");

            if (this.conexion.Reservas!.Any(reserva => reserva.Codigo == entidad.Codigo))
                throw new Exception("lbYaSeGuardo");

            var piscina = this.conexion.Piscinas!.Find(entidad.Piscina);
            if (piscina == null)
                throw new Exception("lbPiscinaNoExiste");

            var ocupados = this.conexion.Reservas!
                .Where(reserva => reserva.Piscina == entidad.Piscina && reserva.Estado)
                .Sum(reserva => reserva.Personas);

            if (ocupados + entidad.Personas > piscina.Capacidad)
                throw new Exception("lbCupoCompleto");

            var porcentaje = this.ConsultarDescuento(entidad);
            entidad.Monto = entidad.Personas * CalculadoraDescuentos.CalcularValorFinal(piscina.TarifaBase, porcentaje);

            this.conexion.Reservas!.Add(entidad!);
            this.conexion.SaveChanges();

            this.auditoriasAplicacion.Registrar(new Auditorias
            {
                Tabla = "Reservas",
                Operacion = "Insertar",
                Descripcion = $"Alta de la reserva {entidad.Codigo} - {entidad.Nombre} para {entidad.Personas} personas",
                Reserva = entidad.Id,
                Piscina = entidad.Piscina,
                Monto = entidad.Monto,
                Exitosa = true
            });

            return entidad;
        }

        public List<Reservas> Consultar()
        {
            return this.conexion.Reservas!
                .Where(reserva => reserva.Estado)
                .Include(reserva => reserva._Piscina)
                .ToList();
        }

        public Reservas Actualizar(Reservas entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            if (Validar(entidad))
                throw new Exception("lbFaltaInformacion");

            if (this.conexion.Reservas!.Any(reserva => reserva.Codigo == entidad.Codigo && reserva.Id != entidad.Id))
                throw new Exception("lbYaSeGuardo");

            var piscina = this.conexion.Piscinas!.Find(entidad.Piscina);
            if (piscina == null)
                throw new Exception("lbPiscinaNoExiste");

            var ocupados = this.conexion.Reservas!
                .Where(reserva => reserva.Piscina == entidad.Piscina && reserva.Estado && reserva.Id != entidad.Id)
                .Sum(reserva => reserva.Personas);

            if (ocupados + entidad.Personas > piscina.Capacidad)
                throw new Exception("lbCupoCompleto");

            var porcentaje = this.ConsultarDescuento(entidad);
            entidad.Monto = entidad.Personas * CalculadoraDescuentos.CalcularValorFinal(piscina.TarifaBase, porcentaje);

            var entry = this.conexion.Entry<Reservas>(entidad);
            entry.State = EntityState.Modified;
            this.conexion.SaveChanges();

            this.auditoriasAplicacion.Registrar(new Auditorias
            {
                Tabla = "Reservas",
                Operacion = "Actualizar",
                Descripcion = $"Modificacion de la reserva {entidad.Codigo} - {entidad.Nombre}",
                Reserva = entidad.Id,
                Piscina = entidad.Piscina,
                Monto = entidad.Monto,
                Exitosa = true
            });

            return entidad;
        }

        public List<Reservas> Buscar(Reservas entidad)
        {
            return this.conexion.Reservas!
                .Where(reserva => reserva.Estado && reserva.Nombre!.Contains(entidad.Nombre!))
                .Include(reserva => reserva._Piscina)
                .ToList();
        }

        public Reservas Borrar(Reservas entidad)
        {
            if (entidad == null)
                throw new Exception("lbFaltaInformacion");
            if (entidad.Id == 0)
                throw new Exception("lbNoSeGuardo");

            var existente = this.conexion.Reservas!
                .AsNoTracking()
                .FirstOrDefault(reserva => reserva.Id == entidad.Id);

            if (existente == null)
                throw new Exception("lbNoEncontrado");

            this.conexion.Reservas!.Remove(existente!);
            this.conexion.SaveChanges();

            this.auditoriasAplicacion.Registrar(new Auditorias
            {
                Tabla = "Reservas",
                Operacion = "Borrar",
                Descripcion = $"Baja de la reserva {existente.Codigo} - {existente.Nombre}",
                Reserva = null,
                Piscina = existente.Piscina,
                Monto = existente.Monto,
                Exitosa = true
            });

            return existente;
        }

        public decimal ConsultarDescuento(Reservas entidad)
        {
            var edad = CalculadoraDescuentos.CalcularEdad(entidad.FechaNacimiento);
            var esFechaEspecial = CalculadoraDescuentos.EsFechaEspecial(entidad.Fecha);

            return CalculadoraDescuentos.CalcularPorcentajeTotal(edad, esFechaEspecial);
        }

        private bool Validar(Reservas entidad)
        {
            return string.IsNullOrEmpty(entidad.Codigo) ||
                string.IsNullOrEmpty(entidad.Nombre) ||
                entidad.Personas <= 0 ||
                entidad.Piscina == 0 ||
                entidad.FechaNacimiento == default ||
                entidad.Fecha == default;
        }
    }
}
