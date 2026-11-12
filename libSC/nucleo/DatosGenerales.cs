namespace libSC.nucleo
{
    public class DatosGenerales
    {
        public static List<DateTime> FechasEspeciales = new List<DateTime>
        {
            new DateTime(2026, 11, 16),
            new DateTime(2026, 12, 24),
            new DateTime(2026, 12, 25),
            new DateTime(2027, 1, 1)
        };

        public static string StringConexion()
        {
            return "server=localhost;database=db_trabajo;User ID=sa;Password=TraBajo$2026;TrustServerCertificate=true;";
        }
    }
}
