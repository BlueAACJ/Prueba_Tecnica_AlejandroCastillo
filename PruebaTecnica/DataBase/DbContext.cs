using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Models;

namespace PruebaTecnica.Database
{
    public class BaseDatos : DbContext
    {
        // Constructor que recibe opciones desde la configuración del proyecto
        public BaseDatos(DbContextOptions<BaseDatos> opciones) : base(opciones) { }

        // DbSet 
        public DbSet<Cliente> Clientes => Set<Cliente>();
        public DbSet<CuentaBancaria> CuentasBancarias => Set<CuentaBancaria>();
        public DbSet<Transaccion> Transacciones => Set<Transaccion>();
    }
}
