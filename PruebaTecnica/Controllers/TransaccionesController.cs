using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Models;
using PruebaTecnica.Services;

namespace PruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaccionesController : ControllerBase
    {
        private readonly ITransaccionService _transaccionService;
        public TransaccionesController(ITransaccionService transaccionService)
        {
            _transaccionService = transaccionService;
        }

        // Endpoint para realizar un depósito en una cuenta bancaria
        [HttpPost("{numeroCuenta}/depositar")]
        public async Task<ActionResult<Transaccion>> Depositar(string numeroCuenta, [FromQuery] decimal monto)
        {
            var transaccion = await _transaccionService.DepositarAsync(numeroCuenta, monto);
            return Ok(transaccion);
        }

        // Endpoint para realizar un retiro de una cuenta bancaria
        [HttpPost("{numeroCuenta}/retirar")]
        public async Task<ActionResult<Transaccion>> Retirar(string numeroCuenta, [FromQuery] decimal monto)
        {
            var transaccion = await _transaccionService.RetirarAsync(numeroCuenta, monto);
            return Ok(transaccion);
        }

        // Endpoint para obtener una transacción por su ID
        [HttpGet("transaccion/{id}")]
        public async Task<ActionResult<Transaccion>> ObtenerTransaccion(int id)
        {
            var transaccion = await _transaccionService.ObtenerPorIdAsync(id);
            if (transaccion == null)
                return NotFound($"Transacción con ID {id} no encontrada.");

            return Ok(transaccion);
        }

    }
}
