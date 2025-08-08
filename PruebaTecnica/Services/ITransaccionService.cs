using PruebaTecnica.Models;

namespace PruebaTecnica.Services
{
    public interface ITransaccionService
    {
        Task<Transaccion> DepositarAsync(string numeroCuenta, decimal monto);
        Task<Transaccion> RetirarAsync(string numeroCuenta, decimal monto);

        Task<Transaccion?> ObtenerPorIdAsync(int id);
    }
}
