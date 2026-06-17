using Microsoft.AspNetCore.Mvc;
using TiendaMVC.Models;

namespace TiendaMVC.Controllers
{
    public class ClientesController : Controller
    {
        private readonly TiendaRepository _repo;
        public ClientesController(IConfiguration config)
            => _repo = new TiendaRepository(config.GetConnectionString("TiendaDB")!);

        public IActionResult Index()
            => View(_repo.ObtenerClientes());

        public IActionResult Crear() => View();

        [HttpPost]
        public IActionResult Crear(Cliente c)
        {
            if (ModelState.IsValid) { _repo.InsertarCliente(c); return RedirectToAction("Index"); }
            return View(c);
        }

        public IActionResult Eliminar(int id)
        {
            _repo.EliminarCliente(id);
            return RedirectToAction("Index");
        }
    }
}