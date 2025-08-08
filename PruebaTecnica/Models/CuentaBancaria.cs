using System.Transactions;

namespace PruebaTecnica.Models
{
    public class CuentaBancaria
    {
        public int Id { get; set; }
        public required string NumeroCuenta { get; set; }
        public decimal Saldo { get; set; }

        // Relación con Cliente
        public int ClienteId { get; set; }
        public required Cliente Cliente { get; set; }

        // Relación: Una cuenta tiene muchas transacciones
        public ICollection<Transaccion> Transacciones { get; set; } = new List<Transaccion>();
    }
}
