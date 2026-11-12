using System.ComponentModel.DataAnnotations.Schema;

namespace libSC.entidades
{
    public class Reservas
    {
        public int Id { get; set; }
        public string? Codigo { get; set; }
        public string? Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Personas { get; set; }
        public DateTime Fecha { get; set; }
        public bool Estado { get; set; }
        public decimal Monto { get; set; }

        [NotMapped]
        public decimal PorcentajeDescuento { get; set; }

        public int Piscina { get; set; }
        [ForeignKey("Piscina")] public Piscinas? _Piscina { get; set; }
    }
}
