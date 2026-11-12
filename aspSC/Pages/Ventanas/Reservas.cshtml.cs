using libSC.entidades;
using libSC.interfaces;
using libSC.nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace aspSC.Pages.Ventanas
{
    public class ReservasModel : PageModel
    {
        private IReservasAplicacion IReservasAplicacion;

        public ReservasModel(IReservasAplicacion IReservasAplicacion)
        {
            this.IReservasAplicacion = IReservasAplicacion;
            this.Filtro = new Reservas();
        }

        [BindProperty] public List<Reservas> Lista { get; set; }
        [BindProperty] public Reservas? Filtro { get; set; }
        [BindProperty] public Reservas? Actual { get; set; }
        [BindProperty] public bool Formulario { get; set; } = false;
        [BindProperty] public bool Borrar { get; set; } = false;

        public void OnGet() { OnPostBtConsultar(); }

        public void OnPostBtConsultar()
        {
            try
            {
                Filtro!.Nombre = Filtro!.Nombre ?? "";
                Lista = this.IReservasAplicacion.Buscar(Filtro!);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtNuevo()
        {
            try
            {
                Formulario = true;
                this.Actual = new Reservas();
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtGuardar()
        {
            try
            {
                if (this.Actual!.Id == 0)
                    this.Actual = this.IReservasAplicacion.Insertar(this.Actual!);
                else
                    this.Actual = this.IReservasAplicacion.Actualizar(this.Actual!);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
            finally
            {
                OnPostBtConsultar();
            }
        }

        public void OnPostBtEditar(string data)
        {
            try
            {
                Formulario = true;
                OnPostBtConsultar();
                this.Actual = this.Lista.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtPregBorrar(string data)
        {
            try
            {
                Borrar = true;
                OnPostBtConsultar();
                this.Actual = this.Lista.FirstOrDefault(x => x.Id == Convert.ToInt32(data));
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }

        public void OnPostBtBorrar()
        {
            try
            {
                if (this.Actual!.Id == 0)
                    return;
                this.Actual = this.IReservasAplicacion.Borrar(this.Actual!);
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
            finally
            {
                OnPostBtConsultar();
            }
        }

        public void OnPostBtDescuento(string data)
        {
            try
            {
                OnPostBtConsultar();
                this.Actual = this.Lista.FirstOrDefault(x => x.Id == Convert.ToInt32(data));

                if (this.Actual == null)
                    return;

                var porcentaje = this.IReservasAplicacion.ConsultarDescuento(this.Actual!);
                var tarifa = this.Actual!._Piscina != null ? this.Actual!._Piscina.TarifaBase : 0m;
                var valorFinal = this.Actual!.Personas * CalculadoraDescuentos.CalcularValorFinal(tarifa, porcentaje);

                ViewData["Mensaje"] =
                    $"Descuento {porcentaje}% - Valor final {valorFinal}";
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }
    }
}
