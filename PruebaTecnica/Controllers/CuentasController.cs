using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Models;
using PruebaTecnica.Services;

namespace PruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentasController : ControllerBase
    {
        private readonly ICuentaService _cuentaService;
        public CuentasController(ICuentaService cuentaService)
        {
            _cuentaService = cuentaService;
        }

        // Endpoint para crear una nueva cuenta bancaria para un cliente existente
        [HttpPost]
        public async Task<ActionResult<CuentaBancaria>> CrearCuenta(int clienteId, [FromQuery] decimal saldoInicial)
        {
            var cuenta = await _cuentaService.CrearCuentaAsync(clienteId, saldoInicial);
            return Ok(cuenta);
        }

        // Endpoint para Consultar el saldo de una cuenta bancaria
        [HttpGet("{numeroCuenta}/saldo")]
        public async Task<ActionResult<decimal>> ObtenerSaldo(string numeroCuenta)
        {
            var saldo = await _cuentaService.ConsultarSaldoAsync(numeroCuenta);
            return Ok(saldo);
        }

        // Endpoint para Solicitar el historial de transacciones de una cuenta bancaria
        [HttpGet("{numeroCuenta}/transacciones")]
        public async Task<ActionResult<IEnumerable<Transaccion>>> ObtenerHistorial(string numeroCuenta)
        {
            var historial = await _cuentaService.ObtenerHistorialAsync(numeroCuenta);
            return Ok(historial);
        }

        // Endpoint para obtener una cuenta bancaria por su número
        [HttpGet("cuenta/{numeroCuenta}")]
        public async Task<ActionResult<CuentaBancaria>> ObtenerCuenta(string numeroCuenta)
        {
            var cuenta = await _cuentaService.ObtenerCuentaPorNumeroAsync(numeroCuenta);
            if (cuenta == null)
                return NotFound("Cuenta no encontrada.");
            return Ok(cuenta);
        }

        // Endpoint para obtener todas las cuentas bancarias de un cliente específico
        [HttpGet("cliente/{clienteId}")]
        public async Task<ActionResult<IEnumerable<CuentaBancaria>>> ObtenerCuentasDeCliente(int clienteId)
        {
            var cuentas = await _cuentaService.ObtenerPorClienteAsync(clienteId);
            return Ok(cuentas);
        }

    }
}
