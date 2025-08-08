namespace PruebaTecnica.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public required string Sexo { get; set; }
        public decimal Ingresos { get; set; }

        // Relación: Un cliente puede tener varias cuentas
        public ICollection<CuentaBancaria> Cuentas { get; set; } = new List<CuentaBancaria>();
    }
}
