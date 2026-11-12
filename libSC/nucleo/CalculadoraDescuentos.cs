namespace libSC.nucleo
{
    public class CalculadoraDescuentos
    {
        public static decimal PorcentajeMenor = 20m;
        public static decimal PorcentajeMayor = 5.2m;
        public static decimal PorcentajeTerceraEdad = 15m;
        public static decimal PorcentajeFechaEspecial = 7.5m;

        public static int CalcularEdad(DateTime fechaNacimiento)
        {
            var edad = DateTime.Now.Year - fechaNacimiento.Year;

            if (fechaNacimiento.Date > DateTime.Now.Date.AddYears(-edad))
                edad--;

            return edad;
        }

        public static bool EsFechaEspecial(DateTime fecha)
        {
            return DatosGenerales.FechasEspeciales
                .Any(fechaEspecial => fechaEspecial.Date == fecha.Date);
        }

        public static decimal CalcularPorcentajeTotal(int edad, bool esFechaEspecial)
        {
            if (esFechaEspecial)
                return PorcentajeFechaEspecial;

            if (edad < 18)
                return PorcentajeMenor;

            if (edad >= 65)
                return PorcentajeTerceraEdad;

            return PorcentajeMayor;
        }

        public static decimal CalcularValorFinal(decimal valorBase, decimal porcentajeDescuento)
        {
            return valorBase - (valorBase * porcentajeDescuento / 100m);
        }
    }
}
