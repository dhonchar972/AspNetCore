using ApiControllers.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiControllers.Controllers
{
    // Standart HTML controller,in REST not needed
    public class HomeController : Controller
    {
        // DI!!!
        private IRepository repository { get; set; }
        // public HomeController(IRepository repo) => repository = repo;
        public HomeController(IRepository repo)
        {
            repository = repo;
        }
        // DI!!!

        public ViewResult Index()
        {
            return View(repository.Reservations);
        }

        [HttpPost]
        public IActionResult AddReservation(Reservation reservation)
        {
            repository.AddReservation(reservation);
            return RedirectToAction("Index");
        }
    }
}