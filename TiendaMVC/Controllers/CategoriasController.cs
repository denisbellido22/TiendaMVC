using Microsoft.AspNetCore.Mvc;
using TiendaMVC.Models;

namespace TiendaMVC.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly TiendaRepository _repo;
        public CategoriasController(IConfiguration config)
            => _repo = new TiendaRepository(config.GetConnectionString("TiendaDB")!);

        // LISTAR
        public IActionResult Index()
            => View(_repo.ObtenerCategorias());

        // CREAR (GET)
        public IActionResult Crear() => View();

        // CREAR (POST)
        [HttpPost]
        public IActionResult Crear(Categoria c)
        {
            if (ModelState.IsValid)
            {
                _repo.InsertarCategoria(c);
                return RedirectToAction("Index");
            }
            return View(c);
        }

        // EDITAR (GET)
        public IActionResult Editar(int id)
        {
            var categoria = _repo.ObtenerCategoriaPorId(id);
            if (categoria == null) return NotFound();
            return View(categoria);
        }

        // EDITAR (POST)
        [HttpPost]
        public IActionResult Editar(Categoria c)
        {
            if (ModelState.IsValid)
            {
                _repo.ActualizarCategoria(c);
                return RedirectToAction("Index");
            }
            return View(c);
        }

        // ELIMINAR
        public IActionResult Eliminar(int id)
        {
            // Nota: Podría fallar si hay productos asociados debido a la clave foránea.
            _repo.EliminarCategoria(id);
            return RedirectToAction("Index");
        }
    }
}