using PruebaTecnica.Models;

namespace PruebaTecnica.Services
{
    public interface ICuentaService
    {
        Task<CuentaBancaria> CrearCuentaAsync(int clienteId, decimal saldoInicial);
        Task<decimal> ConsultarSaldoAsync(string numeroCuenta);
        Task<IEnumerable<Transaccion>> ObtenerHistorialAsync(string numeroCuenta);
        Task<CuentaBancaria?> ObtenerCuentaPorNumeroAsync(string numeroCuenta);
        Task<List<CuentaBancaria>> ObtenerPorClienteAsync(int clienteId);

    }
}
