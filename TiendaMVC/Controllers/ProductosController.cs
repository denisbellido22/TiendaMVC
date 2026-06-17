using Microsoft.AspNetCore.Mvc;
using TiendaMVC.Models;

namespace TiendaMVC.Controllers
{
    public class ProductosController : Controller
    {
        private readonly TiendaRepository _repo;
        public ProductosController(IConfiguration config)
            => _repo = new TiendaRepository(config.GetConnectionString("TiendaDB")!);

        // LISTAR
        public IActionResult Index()
            => View(_repo.ObtenerProductos());

        // CREAR (GET)
        public IActionResult Crear()
        {
            ViewBag.Categorias = _repo.ObtenerCategorias();
            return View();
        }

        // CREAR (POST)
        [HttpPost]
        public IActionResult Crear(Producto p)
        {
            if (ModelState.IsValid)
            {
                _repo.InsertarProducto(p);
                return RedirectToAction("Index");
            }
            ViewBag.Categorias = _repo.ObtenerCategorias();
            return View(p);
        }

        // EDITAR (GET)
        public IActionResult Editar(int id)
        {
            var producto = _repo.ObtenerProductoPorId(id);
            if (producto == null) return NotFound();

            ViewBag.Categorias = _repo.ObtenerCategorias();
            return View(producto);
        }

        // EDITAR (POST)
        [HttpPost]
        public IActionResult Editar(Producto p)
        {
            if (ModelState.IsValid)
            {
                _repo.ActualizarProducto(p); // Asegúrate de recibir el objeto o el id + objeto según tu repo
                return RedirectToAction("Index");
            }
            ViewBag.Categorias = _repo.ObtenerCategorias();
            return View(p);
        }

        // ELIMINAR
        public IActionResult Eliminar(int id)
        {
            _repo.EliminarProducto(id);
            return RedirectToAction("Index");
        }
    }
}