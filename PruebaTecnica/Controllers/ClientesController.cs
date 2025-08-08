using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.Models;
using PruebaTecnica.Services;

namespace PruebaTecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        // Endpoint para crear un nuevo cliente
        [HttpPost]
        public async Task<ActionResult<Cliente>> CrearCliente([FromBody] Cliente cliente)
        {
            var clienteCreado = await _clienteService.CrearClienteAsync(cliente);
            return CreatedAtAction(nameof(CrearCliente), new { id = clienteCreado.Id }, clienteCreado);
        }

        // Endpoint para obtener todos los clientes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> ObtenerTodos()
        {
            var clientes = await _clienteService.ObtenerTodosAsync();
            return Ok(clientes);
        }

        // Endpoint para obtener un cliente por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> ObtenerPorId(int id)
        {
            var cliente = await _clienteService.ObtenerPorIdAsync(id);
            if (cliente == null)
                return NotFound($"Cliente con ID {id} no encontrado.");

            return Ok(cliente);
        }

        // endpoint para actualizar un cliente, Debe de ser toda la informacion actualizada
        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCliente(int id, [FromBody] Cliente cliente)
        {
            if (cliente.Id != 0 && cliente.Id != id)
                return BadRequest("El ID del cliente en la ruta no coincide con el del cuerpo.");

            var actualizado = await _clienteService.ActualizarClienteAsync(id, cliente);
            if (actualizado == null)
                return NotFound($"No se encontró el cliente con ID {id}.");

            return Ok(actualizado);
        }
    }
}
