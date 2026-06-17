using Microsoft.AspNetCore.Mvc;
using TiendaMVC.Models;

namespace TiendaMVC.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly TiendaRepository _repo;
        public CategoriasController(IConfiguration config)
            => _repo = new TiendaRepository(config.GetConnectionString("TiendaDB")!);

        public IActionResult Index()
            => View(_repo.ObtenerCategorias());

        public IActionResult Crear() => View();

        [HttpPost]
        public IActionResult Crear(Categoria c)
        {
            if (ModelState.IsValid) { _repo.InsertarCategoria(c); return RedirectToAction("Index"); }
            return View(c);
        }

        public IActionResult Eliminar(int id)
        {
            _repo.EliminarCategoria(id);
            return RedirectToAction("Index");
        }
    }
}