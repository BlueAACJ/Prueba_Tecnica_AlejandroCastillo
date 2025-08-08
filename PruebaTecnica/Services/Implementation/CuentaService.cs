using PruebaTecnica.Models;
using PruebaTecnica.Database;
using Microsoft.EntityFrameworkCore;

namespace PruebaTecnica.Services.Implementation
{
    public class CuentaService : ICuentaService
    {
        // Inyección de dependencias del contexto de base de datos
        private readonly BaseDatos _dbContext;

        // Constructor que recibe el contexto de base de datos
        public CuentaService(BaseDatos dbContext)
        {
            _dbContext = dbContext;
        }

        // Crear una nueva cuenta bancaria para un cliente
        public async Task<CuentaBancaria> CrearCuentaAsync(int clienteId, decimal saldoInicial)
        {
            var cliente = await _dbContext.Clientes.FindAsync(clienteId);
            // Validar que el cliente existe
            if (cliente == null)
                throw new Exception("Cliente no encontrado");

            // Validar que el cliente existe
            var clienteExiste = await _dbContext.Clientes.AnyAsync(c => c.Id == clienteId);
            if (!clienteExiste)
                throw new ArgumentException("El cliente no existe.");

            // Validar que el saldo inicial sea mayor a cero
            if (saldoInicial <= 0)
                throw new ArgumentException("El saldo inicial debe ser mayor a cero.");

            var cuentaNueva = new CuentaBancaria
            {
                ClienteId = clienteId,
                Cliente = cliente,
                // Genera un numero de cuenta único tomando los primeros 10 caracteres de un GUID
                NumeroCuenta = Guid.NewGuid().ToString().Substring(0, 10),
                Saldo = saldoInicial
            };

            _dbContext.CuentasBancarias.Add(cuentaNueva);
            await _dbContext.SaveChangesAsync();

            return cuentaNueva;
        }

        // Consultar el saldo de una cuenta bancaria
        public async Task<decimal> ConsultarSaldoAsync(string numeroCuenta)
        {
            var cuenta = await _dbContext.CuentasBancarias
                .FirstOrDefaultAsync(c => c.NumeroCuenta == numeroCuenta);

            return cuenta?.Saldo ?? throw new Exception("Cuenta no encontrada");
        }

        // Obtener el historial de transacciones de una cuenta bancaria
        public async Task<IEnumerable<Transaccion>> ObtenerHistorialAsync(string numeroCuenta)
        {
            var cuenta = await _dbContext.CuentasBancarias
                .Include(c => c.Transacciones)
                .FirstOrDefaultAsync(c => c.NumeroCuenta == numeroCuenta);

            return cuenta?.Transacciones ?? throw new Exception("Cuenta no encontrada");
        }

        // Obtener informacion de una cuenta bancaria por su número
        public async Task<CuentaBancaria?> ObtenerCuentaPorNumeroAsync(string numeroCuenta)
        {
            return await _dbContext.CuentasBancarias
                .Include(c => c.Cliente)
                .Include(c => c.Transacciones)
                .FirstOrDefaultAsync(c => c.NumeroCuenta == numeroCuenta);
        }

        // Obtener todas las cuentas bancarias de un cliente específico
        public async Task<List<CuentaBancaria>> ObtenerPorClienteAsync(int clienteId)
        {
            return await _dbContext.CuentasBancarias
                .Where(c => c.ClienteId == clienteId)
                .ToListAsync();
        }
    }
}
