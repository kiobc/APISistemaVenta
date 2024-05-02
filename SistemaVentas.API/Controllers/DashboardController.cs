using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.DTO;
using SistemaVentas.API.Utilidad;

namespace SistemaVentas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _dashboardServicio;

        public DashboardController(IDashboardService dashboardServicio)
        {
            _dashboardServicio = dashboardServicio;
        }

        [HttpGet]
        [Route("Resumen")]
        public async Task<IActionResult> Resumen()
        {
            var rsp = new Response<DashboardDTO>();
            try
            {
                rsp.status = true;
                rsp.value = await _dashboardServicio.Resumen();
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }
    }
}
