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

        // GET api/tienda/productos
        [HttpGet("productos")]
        public IActionResult GetProductos()
            => Ok(_repo.ObtenerProductos());

        // GET api/tienda/categorias
        [HttpGet("categorias")]
        public IActionResult GetCategorias()
            => Ok(_repo.ObtenerCategorias());

        // GET api/tienda/clientes
        [HttpGet("clientes")]
        public IActionResult GetClientes()
            => Ok(_repo.ObtenerClientes());

        // POST api/tienda/productos
        [HttpPost("productos")]
        public IActionResult CrearProducto([FromBody] Producto p)
        {
            if (p == null || string.IsNullOrEmpty(p.Nombre))
                return BadRequest("Datos inválidos");

            _repo.InsertarProducto(p);
            return Ok(new { mensaje = "Producto creado correctamente" });
        }
        // PUT api/tienda/productos/5
        [HttpPut("productos/{id}")]
        public IActionResult ActualizarProducto(int id, [FromBody] Producto p)
        {
            if (p == null || string.IsNullOrEmpty(p.Nombre))
                return BadRequest("Datos inválidos");

            p.Id = id;
            _repo.ActualizarProducto(p);

            return Ok(new { mensaje = $"Producto {id} actualizado correctamente" });
        }

        // DELETE api/tienda/productos/5
        [HttpDelete("productos/{id}")]
        public IActionResult EliminarProducto(int id)
        {
            _repo.EliminarProducto(id);
            return Ok(new { mensaje = $"Producto {id} eliminado" });
        }
    }
}