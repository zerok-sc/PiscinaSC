using libSC.entidades;

namespace libSC.interfaces
{
    public interface IReservasAplicacion
    {
        Reservas Insertar(Reservas entidad);
        Reservas Actualizar(Reservas entidad);
        List<Reservas> Consultar();
        List<Reservas> Buscar(Reservas entidad);
        Reservas Borrar(Reservas entidad);
        decimal ConsultarDescuento(Reservas entidad);
    }
}
