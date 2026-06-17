using Microsoft.AspNetCore.Mvc;
using TiendaMVC.Models;

namespace TiendaMVC.Controllers
{
    public class ProductosController : Controller
    {
        private readonly TiendaRepository _repo;
        public ProductosController(IConfiguration config)
            => _repo = new TiendaRepository(config.GetConnectionString("TiendaDB")!);

        public IActionResult Index()
            => View(_repo.ObtenerProductos());

        public IActionResult Crear()
        {
            // Pasamos la lista de categorías para el <select>
            ViewBag.Categorias = _repo.ObtenerCategorias();
            return View();
        }

        [HttpPost]
        public IActionResult Crear(Producto p)
        {
            if (ModelState.IsValid) { _repo.InsertarProducto(p); return RedirectToAction("Index"); }
            ViewBag.Categorias = _repo.ObtenerCategorias();
            return View(p);
        }

        public IActionResult Eliminar(int id)
        {
            _repo.EliminarProducto(id);
            return RedirectToAction("Index");
        }
    }
}