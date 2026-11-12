using libSC.entidades;

namespace libSC.interfaces
{
    public interface IPiscinasAplicacion
    {
        Piscinas Insertar(Piscinas entidad);
        Piscinas Actualizar(Piscinas entidad);
        List<Piscinas> Consultar();
        List<Piscinas> Buscar(Piscinas entidad);
        Piscinas Borrar(Piscinas entidad);
    }
}
