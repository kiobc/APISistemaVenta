using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using SistemaVentas.BLL.Servicios.Contrato;
using SistemaVentas.DTO;
using SistemaVentas.API.Utilidad;
using System.Globalization;
using SistemaVentas.BLL.Servicios;

namespace SistemaVentas.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly IVentaService _ventaServicio;

        public VentaController(IVentaService ventaServicio)
        {
            _ventaServicio = ventaServicio;
        }

        [HttpPost]
        [Route("Registrar")]
        public async Task<IActionResult> Registrar([FromBody] VentaDTO venta)
        {
            var rsp = new Response<VentaDTO>();
            try
            {
                rsp.status = true;
                rsp.value = await _ventaServicio.Registrar(venta);
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }


        [HttpGet]
        [Route("Historial")]
        public async Task<IActionResult> Historial(string buscarPor, string? numeroVenta, string? fechaInicio, string? fechaFin)
        {
            var rsp = new Response<List<VentaDTO>>();
            numeroVenta = numeroVenta ?? string.Empty;

            try
            {
                DateTime? parsedFechaInicio = null;
                DateTime? parsedFechaFin = null;

                if (!string.IsNullOrEmpty(fechaInicio))
                {
                    parsedFechaInicio = DateTime.ParseExact(fechaInicio, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                if (!string.IsNullOrEmpty(fechaFin))
                {
                    parsedFechaFin = DateTime.ParseExact(fechaFin, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                }

                var ventas = await _ventaServicio.Historial(buscarPor, numeroVenta, parsedFechaInicio?.ToString("dd/MM/yyyy"), parsedFechaFin?.ToString("dd/MM/yyyy"));

                rsp.status = true;
                rsp.value = ventas;

                foreach (var venta in rsp.value)
                {
                    if (DateTime.TryParseExact(venta.FechaRegistro, "yyyy-MM-ddTHH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime parsedDate))
                    {
                        venta.FechaRegistro = parsedDate.ToString("dd/MM/yyyy");
                    }
                }
            }
            catch (Exception ex)
            {
                rsp.status = false;
                rsp.msg = ex.Message;
            }
            return Ok(rsp);
        }


        [HttpGet]
        [Route("Reporte")]
        public async Task<IActionResult> Reporte(string? fechaInicio, string? fechaFin)
        {
            var rsp = new Response<List<ReporteDTO>>();
          
           
            try
            {
                rsp.status = true;
                rsp.value = await _ventaServicio.Reporte(fechaInicio, fechaFin);
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
