using libSC.entidades;
using libSC.interfaces;
using libSC.nucleo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace aspSC.Pages.Ventanas
{
    public class AuditoriasModel : PageModel
    {
        private IAuditoriasAplicacion IAuditoriasAplicacion;

        public AuditoriasModel(IAuditoriasAplicacion IAuditoriasAplicacion)
        {
            this.IAuditoriasAplicacion = IAuditoriasAplicacion;
            this.Filtro = new Auditorias();
        }

        [BindProperty] public List<Auditorias> Lista { get; set; }
        [BindProperty] public Auditorias? Filtro { get; set; }

        public void OnGet() { OnPostBtConsultar(); }

        public void OnPostBtConsultar()
        {
            try
            {
                if (string.IsNullOrEmpty(Filtro!.Descripcion))
                {
                    Lista = this.IAuditoriasAplicacion.Consultar();
                }
                else
                {
                    Lista = this.IAuditoriasAplicacion.Buscar(Filtro!);
                }
            }
            catch (Exception ex)
            {
                LogConversor.Log(ex, ViewData!);
            }
        }
    }
}
