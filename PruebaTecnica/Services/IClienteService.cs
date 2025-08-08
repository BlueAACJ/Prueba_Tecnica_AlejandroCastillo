using PruebaTecnica.Models;

namespace PruebaTecnica.Services
{

    public interface IClienteService
    {
        Task<Cliente> CrearClienteAsync(Cliente cliente);
        Task<List<Cliente>> ObtenerTodosAsync();

        Task<Cliente?> ObtenerPorIdAsync(int id);
        Task<Cliente?> ActualizarClienteAsync(int id, Cliente clienteActualizado);

    }
}
