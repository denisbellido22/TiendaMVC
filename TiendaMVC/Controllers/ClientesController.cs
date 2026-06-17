using Microsoft.AspNetCore.Mvc;
using TiendaMVC.Models;

namespace TiendaMVC.Controllers
{
    public class ClientesController : Controller
    {
        private readonly TiendaRepository _repo;
        public ClientesController(IConfiguration config)
            => _repo = new TiendaRepository(config.GetConnectionString("TiendaDB")!);

        // GET: Clientes
        public IActionResult Index()
            => View(_repo.ObtenerClientes());

        // GET: Clientes/Crear
        public IActionResult Crear() => View();

        // POST: Clientes/Crear
        [HttpPost]
        public IActionResult Crear(Cliente c)
        {
            if (ModelState.IsValid)
            {
                _repo.InsertarCliente(c);
                return RedirectToAction("Index");
            }
            return View(c);
        }

        // GET: Clientes/Editar/5
        public IActionResult Editar(int id)
        {
            var cliente = _repo.ObtenerClientePorId(id);
            if (cliente == null) return NotFound();

            return View(cliente);
        }

        // POST: Clientes/Editar/5
        [HttpPost]
        public IActionResult Editar(Cliente c)
        {
            if (ModelState.IsValid)
            {
                _repo.ActualizarCliente(c);
                return RedirectToAction("Index");
            }
            return View(c);
        }

        // GET/POST: Clientes/Eliminar/5
        public IActionResult Eliminar(int id)
        {
            _repo.EliminarCliente(id);
            return RedirectToAction("Index");
        }
    }
}