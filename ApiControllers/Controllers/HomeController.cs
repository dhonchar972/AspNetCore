using ApiControllers.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiControllers.Controllers;

// Standart HTML controller,in REST not needed
public class HomeController : Controller
{
    // DI!!!
    private IRepository Repository { get; set; }
    // public HomeController(IRepository repo) => repository = repo;
    public HomeController(IRepository repo)
    {
        Repository = repo;
    }
    // DI!!!

    public ViewResult Index()
    {
        return View(Repository.Reservations);
    }

    [HttpPost]
    public IActionResult AddReservation(Reservation reservation)
    {
        Repository.AddReservation(reservation);
        return RedirectToAction("Index");
    }
}