using System.ComponentModel.DataAnnotations.Schema;

namespace libSC.entidades
{
    public class Auditorias
    {
        public int Id { get; set; }
        public DateTime Fecha { get; set; }
        public string? Tabla { get; set; }
        public string? Operacion { get; set; }
        public string? Descripcion { get; set; }
        public decimal Monto { get; set; }
        public bool Exitosa { get; set; }

        public int? Reserva { get; set; }
        [ForeignKey("Reserva")] public Reservas? _Reserva { get; set; }

        public int? Piscina { get; set; }
        [ForeignKey("Piscina")] public Piscinas? _Piscina { get; set; }
    }
}
