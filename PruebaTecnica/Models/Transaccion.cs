namespace PruebaTecnica.Models
{
    public class Transaccion
    {
        public int Id { get; set; }

        // Relación con CuentaBancaria
        public int CuentaBancariaId { get; set; }
        public required CuentaBancaria CuentaBancaria { get; set; }

        public char Tipo { get; set; } // D (Deposito) o R (Retiro)
        public decimal Monto { get; set; }
        public decimal SaldoDespues { get; set; }
        public DateTime FechaHora { get; set; }
    }
}
