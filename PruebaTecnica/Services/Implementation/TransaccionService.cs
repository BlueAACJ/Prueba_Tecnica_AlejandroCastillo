using PruebaTecnica.Models;
using PruebaTecnica.Database;
using Microsoft.EntityFrameworkCore;

namespace PruebaTecnica.Services.Implementation
{
    public class TransaccionService : ITransaccionService
    {
        private readonly BaseDatos _context;

        public TransaccionService(BaseDatos contexto)
        {
            _context = contexto;
        }

        // En el inicializador de objeto Transaccion, agrega la propiedad requerida CuentaBancaria
        public async Task<Transaccion> DepositarAsync(string numeroCuenta, decimal monto)
        {
            var cuenta = await _context.Set<CuentaBancaria>()
                .FirstAsync(c => c.NumeroCuenta == numeroCuenta);

            cuenta.Saldo += monto;

            var transaccion = new Transaccion
            {
                CuentaBancariaId = cuenta.Id,
                CuentaBancaria = cuenta,
                Tipo = 'D', // Depósito
                Monto = monto,
                SaldoDespues = cuenta.Saldo,
                FechaHora = DateTime.UtcNow
            };

            _context.Set<Transaccion>().Add(transaccion);
            await _context.SaveChangesAsync();

            return transaccion;
        }

        // Método para retirar dinero de una cuenta bancaria
        public async Task<Transaccion> RetirarAsync(string numeroCuenta, decimal monto)
        {
            var cuenta = await _context.Set<CuentaBancaria>()
                .FirstAsync(c => c.NumeroCuenta == numeroCuenta);

            // Verificar si hay fondos suficientes
            if (cuenta.Saldo < monto)
                throw new Exception("Fondos insuficientes");

            cuenta.Saldo -= monto;

            var transaccion = new Transaccion
            {
                CuentaBancariaId = cuenta.Id,
                CuentaBancaria = cuenta, 
                Tipo = 'R', // Retiro
                Monto = monto,
                SaldoDespues = cuenta.Saldo,
                FechaHora = DateTime.UtcNow
            };

            _context.Set<Transaccion>().Add(transaccion);
            await _context.SaveChangesAsync();

            return transaccion;
        }

        // Método para obtener una transacción por su ID
        public async Task<Transaccion?> ObtenerPorIdAsync(int id)
        {
            return await _context.Transacciones
                .Include(t => t.CuentaBancaria)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

    }
}
