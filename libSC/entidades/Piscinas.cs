namespace libSC.entidades
{
    public class Piscinas
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public int Capacidad { get; set; }
        public decimal TarifaBase { get; set; }
        public bool Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
