using Microsoft.AspNetCore.Mvc;
using TiendaMVC.Models;

namespace TiendaMVC.Controllers
{
    [Route("api/tienda")]
    [ApiController]
    public class TiendaApiController : ControllerBase
    {
        private readonly TiendaRepository _repo;

        public TiendaApiController(IConfiguration config)
            => _repo = new TiendaRepository(config.GetConnectionString("TiendaDB")!);

        // ==========================================
        //  CRUD: PRODUCTOS
        // ==========================================

        [HttpGet("productos")]
        [Tags("Productos")] // ◄ Esto lo agrupa en la sección "Productos"
        public IActionResult GetProductos() => Ok(_repo.ObtenerProductos());

        [HttpGet("productos/{id}")]
        [Tags("Productos")]
        public IActionResult GetProductoPorId(int id)
        {
            var producto = _repo.ObtenerProductoPorId(id);
            if (producto == null) return NotFound($"Producto con ID {id} no encontrado");
            return Ok(producto);
        }

        [HttpPost("productos")]
        [Tags("Productos")]
        public IActionResult CrearProducto([FromBody] Producto p)
        {
            if (p == null || string.IsNullOrEmpty(p.Nombre) || p.Precio <= 0)
                return BadRequest("Datos del producto inválidos");

            _repo.InsertarProducto(p);
            return Ok(new { mensaje = "Producto creado correctamente" });
        }

        [HttpPut("productos/{id}")]
        [Tags("Productos")]
        public IActionResult ActualizarProducto(int id, [FromBody] Producto p)
        {
            if (p == null || string.IsNullOrEmpty(p.Nombre))
                return BadRequest("Datos del producto inválidos");

            p.Id = id;
            _repo.ActualizarProducto(p);
            return Ok(new { mensaje = $"Producto {id} actualizado correctamente" });
        }

        [HttpDelete("productos/{id}")]
        [Tags("Productos")]
        public IActionResult EliminarProducto(int id)
        {
            _repo.EliminarProducto(id);
            return Ok(new { mensaje = $"Producto {id} eliminado correctamente" });
        }

        // ==========================================
        //  CRUD: CATEGORIAS
        // ==========================================

        [HttpGet("categorias")]
        [Tags("Categorias")] // ◄ Esto lo agrupa en la sección "Categorias"
        public IActionResult GetCategorias() => Ok(_repo.ObtenerCategorias());

        [HttpGet("categorias/{id}")]
        [Tags("Categorias")]
        public IActionResult GetCategoriaPorId(int id)
        {
            var categoria = _repo.ObtenerCategoriaPorId(id);
            if (categoria == null) return NotFound($"Categoría con ID {id} no encontrada");
            return Ok(categoria);
        }

        [HttpPost("categorias")]
        [Tags("Categorias")]
        public IActionResult CrearCategoria([FromBody] Categoria c)
        {
            if (c == null || string.IsNullOrEmpty(c.Nombre))
                return BadRequest("Datos de la categoría inválidos");

            _repo.InsertarCategoria(c);
            return Ok(new { mensaje = "Categoría creada correctamente" });
        }

        [HttpPut("categorias/{id}")]
        [Tags("Categorias")]
        public IActionResult ActualizarCategoria(int id, [FromBody] Categoria c)
        {
            if (c == null || string.IsNullOrEmpty(c.Nombre))
                return BadRequest("Datos de la categoría inválidos");

            c.Id = id;
            _repo.ActualizarCategoria(c);
            return Ok(new { mensaje = $"Categoría {id} actualizada correctamente" });
        }

        [HttpDelete("categorias/{id}")]
        [Tags("Categorias")]
        public IActionResult EliminarCategoria(int id)
        {
            _repo.EliminarCategoria(id);
            return Ok(new { mensaje = $"Categoría {id} eliminada correctamente" });
        }

        // ==========================================
        //  CRUD: CLIENTES
        // ==========================================

        [HttpGet("clientes")]
        [Tags("Clientes")] // ◄ Esto lo agrupa en la sección "Clientes"
        public IActionResult GetClientes() => Ok(_repo.ObtenerClientes());

        [HttpGet("clientes/{id}")]
        [Tags("Clientes")]
        public IActionResult GetClientePorId(int id)
        {
            var cliente = _repo.ObtenerClientePorId(id);
            if (cliente == null) return NotFound($"Cliente con ID {id} no encontrado");
            return Ok(cliente);
        }

        [HttpPost("clientes")]
        [Tags("Clientes")]
        public IActionResult CrearCliente([FromBody] Cliente c)
        {
            if (c == null || string.IsNullOrEmpty(c.Nombre))
                return BadRequest("Datos del cliente inválidos");

            _repo.InsertarCliente(c);
            return Ok(new { mensaje = "Cliente creado correctamente" });
        }

        [HttpPut("clientes/{id}")]
        [Tags("Clientes")]
        public IActionResult ActualizarCliente(int id, [FromBody] Cliente c)
        {
            if (c == null || string.IsNullOrEmpty(c.Nombre))
                return BadRequest("Datos del cliente inválidos");

            c.Id = id;
            _repo.ActualizarCliente(c);
            return Ok(new { mensaje = $"Cliente {id} actualizado correctamente" });
        }

        [HttpDelete("clientes/{id}")]
        [Tags("Clientes")]
        public IActionResult EliminarCliente(int id)
        {
            _repo.EliminarCliente(id);
            return Ok(new { mensaje = $"Cliente {id} eliminado correctamente" });
        }
    }
}