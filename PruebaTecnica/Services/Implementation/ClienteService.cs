using Microsoft.EntityFrameworkCore;
using PruebaTecnica.Database;
using PruebaTecnica.Models;

namespace PruebaTecnica.Services.Implementation
{
    public class ClienteService : IClienteService
    {
        private readonly BaseDatos _context;
        public ClienteService(BaseDatos contexto)
        {
            _context = contexto;
        }

        // Crear un nuevo cliente
        public async Task<Cliente> CrearClienteAsync(Cliente cliente)
        {
            _context.Set<Cliente>().Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        // Obtener todos los clientes
        public async Task<List<Cliente>> ObtenerTodosAsync()
        {
            return await _context.Clientes.ToListAsync();
        }

        // Obtener un cliente por ID
        public async Task<Cliente?> ObtenerPorIdAsync(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        // Actualizar un cliente existente
        public async Task<Cliente?> ActualizarClienteAsync(int id, Cliente datos)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return null;

            // Actualizar los campos del cliente
            cliente.Nombre = datos.Nombre;
            cliente.FechaNacimiento = datos.FechaNacimiento;
            cliente.Sexo = datos.Sexo;
            cliente.Ingresos = datos.Ingresos;

            await _context.SaveChangesAsync();
            return cliente;
        }
    }
}
